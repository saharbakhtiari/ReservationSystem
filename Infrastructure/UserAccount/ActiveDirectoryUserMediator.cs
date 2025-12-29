using CustomLoggers;
using CustomLoggers.EmailSenders;
using Domain.Users;
using Novell.Directory.Ldap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.UserAccount
{
    public class ActiveDirectoryUserMediator : IUserMediator
    {
        private readonly IMailSenderConfiguration _mailSenderConfiguration;
        private readonly ICustomLogger<ActiveDirectoryUserMediator> _logger;

        public ActiveDirectoryUserMediator(IMailSenderConfiguration mailSenderConfiguration, ICustomLogger<ActiveDirectoryUserMediator> logger)
        {
            _mailSenderConfiguration = mailSenderConfiguration;
            _logger = logger;
        }
        public Task<bool> ValidateUser(string username, string password)
        {
            return Task.Run(() => _validateUser(_mailSenderConfiguration.DomainName, username, password));

        }
        public Task<List<UserModel>> SearchUsers(string searchFilter, bool exactly = false , int maxLoop = 10)
        {
            return Task.Run(() => _searchUsersASynchronous(searchFilter, exactly, maxLoop));
        }
        public async Task<UserModel> GetUser(string username)
        {
            var users = await SearchUsers(username, exactly: true);
            return users?.FirstOrDefault();
        }

        private bool _validateUser(string domainName, string username, string password)
        {
            string userDn = $"{username}@{domainName}";
            try
            {
                using (var connection = new LdapConnection { SecureSocketLayer = false })
                {
                    connection.Connect(domainName, LdapConnection.DefaultPort);
                    connection.Bind(userDn, password);

                    if (connection.Bound)
                        return true;
                }
            }
            catch (LdapException)
            {
                // Log exception
            }
            return false;
        }
        private List<UserModel> _searchUsersSynchronous(string searchFilter, bool exactly, int maxLoop)
        {

            List<UserModel> userModels = new();

            string userDn = $"{_mailSenderConfiguration.EmailUser}@{_mailSenderConfiguration.DomainName}";
            try
            {
                using (var connection = new LdapConnection { SecureSocketLayer = false })
                {
                    connection.Connect(_mailSenderConfiguration.DomainName, LdapConnection.DefaultPort);
                    connection.Bind(userDn, _mailSenderConfiguration.EmailPassword);

                    if (!connection.Bound)
                    {
                        return default;
                    }

                    var searchBase = String.Join(",", _mailSenderConfiguration.DomainName.Split(".").Select(x => $"DC={x}"));
                    var filter = exactly ? $"(cn={searchFilter})" : $"(cn={searchFilter}*)";
                    var lsc = connection.Search(searchBase, LdapConnection.ScopeSub, filter, null, false);

                    while (maxLoop-- > 0 && lsc.HasMore())
                    {
                        LdapEntry nextEntry = null;
                        try
                        {
                            nextEntry = lsc.Next();
                        }
                        catch (LdapReferralException eR)
                        {
                            // https://stackoverflow.com/questions/46052873/ldap-referal-error
                            // The response you received means that the directory you are requesting does not contain the data you look for, 
                            // but they are in another directory, and in the response there is the information about the "referral" directory 
                            // on which you need to rebind to "redo" the search.This principle in LDAP are the referral.
                            // https://www.novell.com/documentation/developer/ldapcsharp/?page=/documentation/developer/ldapcsharp/cnet/data/bp31k5d.html
                            // To enable referral following, use LDAPConstraints.setReferralFollowing passing TRUE to enable referrals, or FALSE (default) to disable referrals.

                            // are you sure your bind user meaning
                            // auth.impl.ldap.userid=CN=DotCMSUser,OU=Service Accounts,DC=mycompany,DC=intranet
                            // auth.impl.ldap.password = mypassword123
                            // has permissions to the user that is logging in and its groups?
                            _logger.LogWarning(eR, eR.LdapErrorMessage);
                            continue;
                        }
                        catch (LdapException e)
                        {
                            // WARNING: Here catches only LDAP-Exception, no other types...
                            _logger.LogError(e, e.LdapErrorMessage);
                            // Exception is thrown, go for next entry
                            continue;
                        }
                        catch (Exception e)
                        {
                            // WARNING: Here catches only LDAP-Exception, no other types...
                            _logger.LogError(e, e.Message);
                            // Exception is thrown, go for next entry
                            continue;
                        }

                        if (nextEntry is null)
                        {
                            // WARNING: Here catches only LDAP-Exception, no other types...
                            _logger.LogError("nextEntry is null");
                            // Exception is thrown, go for next entry
                            continue;
                        }

                        userModels.Add(MapLdapEntryToUserModel(nextEntry));
                    }
                }
            }
            catch (LdapException ex)
            {
                _logger.LogError(ex, ex.LdapErrorMessage);
                return default;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return default;
            }
            return userModels;
        }
        private List<UserModel> _searchUsersASynchronous(string searchFilter, bool exactly, int maxLoop)
        {
            List<UserModel> userModels = new();

            string userDn = $"{_mailSenderConfiguration.EmailUser}@{_mailSenderConfiguration.DomainName}";
            try
            {
                using (var connection = new LdapConnection { SecureSocketLayer = false })
                {
                    connection.Connect(_mailSenderConfiguration.DomainName, LdapConnection.DefaultPort);
                    connection.Bind(userDn, _mailSenderConfiguration.EmailPassword);

                    if (!connection.Bound)
                    {
                        return default;
                    }

                    var searchBase = String.Join(",", _mailSenderConfiguration.DomainName.Split(".").Select(x => $"DC={x}"));
                    var filter = exactly ? $"(&(objectClass=user)(objectClass=person)(cn={searchFilter}))" : $"(&(objectClass=user)(objectClass=person)(cn={searchFilter}*))";
                    var lsc = connection.Search(searchBase, LdapConnection.ScopeSub, filter, null, false,(LdapSearchQueue)null, (LdapSearchConstraints)null);


                    //Searching the Directory - Asynchronous
                    LdapMessage message;
                    while (maxLoop-- > 0 && (message = lsc.GetResponse()) != null)
                    {
                        if (message is LdapSearchResult)
                        {
                            LdapEntry nextEntry = (message as LdapSearchResult).Entry;
                            userModels.Add(MapLdapEntryToUserModel(nextEntry));
                        }
                    }
                }
            }
            catch (LdapException ex)
            {
                _logger.LogError(ex, ex.LdapErrorMessage);
                return default;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return default;
            }
            return userModels;
        }
        private UserModel MapLdapEntryToUserModel(LdapEntry ldapEntry)
        {
            if (ldapEntry is null)
                return null;

            UserModel userModel = new UserModel();
            userModel.UserName = ldapEntry.GetAttribute("cn").StringValue;

            LdapAttributeSet attributeSet = ldapEntry.GetAttributeSet();

            System.Collections.IEnumerator ienum = attributeSet.GetEnumerator();
            while (ienum.MoveNext())
            {
                LdapAttribute attribute = (LdapAttribute)ienum.Current;
                userModel.Attribute.Add(attribute.Name, attribute.StringValue);
            }

            return userModel;
        }

       
    }
}
