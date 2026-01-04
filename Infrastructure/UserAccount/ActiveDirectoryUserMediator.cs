using CustomLoggers;
using CustomLoggers.EmailSenders;
using Domain.Users;
using Infrastructure.UserAccount;
using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

public class ActiveDirectoryUserMediator : IUserMediator
{
    private readonly IMailSenderConfiguration _mailSenderConfiguration;
    private readonly ICustomLogger<ActiveDirectoryUserMediator> _logger;

    public ActiveDirectoryUserMediator(IMailSenderConfiguration mailSenderConfiguration, ICustomLogger<ActiveDirectoryUserMediator> logger)
    {
        _mailSenderConfiguration = mailSenderConfiguration;
        _logger = logger;
    }

    public async Task<bool> ValidateUser(string username, string password)
    {
        return await Task.Run(() => _validateUser(_mailSenderConfiguration.DomainName, username, password));
    }

    public async Task<List<UserModel>> SearchUsers(string searchFilter, bool exactly = false, int maxLoop = 10)
    {
        return await Task.Run(() => _searchUsers(searchFilter, exactly, maxLoop));
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
            var identifier = new LdapDirectoryIdentifier(domainName);
            using (var connection = new LdapConnection(identifier))
            {
                connection.Credential = new NetworkCredential(userDn, password);
                connection.AuthType = AuthType.Negotiate;
                connection.Bind();
                return true;
            }
        }
        catch (LdapException ex)
        {
            _logger.LogError(ex, ex.Message);
            return false;
        }
    }

    private List<UserModel> _searchUsers(string searchFilter, bool exactly, int maxLoop)
    {
        List<UserModel> userModels = new();
        string userDn = $"{_mailSenderConfiguration.EmailUser}@{_mailSenderConfiguration.DomainName}";
        try
        {
            var identifier = new LdapDirectoryIdentifier(_mailSenderConfiguration.DomainName);
            using (var connection = new LdapConnection(identifier))
            {
                connection.Credential = new NetworkCredential(userDn, _mailSenderConfiguration.EmailPassword);
                connection.AuthType = AuthType.Negotiate;
                connection.Bind();

                    var searchBase = string.Join(",", _mailSenderConfiguration.DomainName.Split(".").Select(x => $"DC={x}"));
                    var filter = exactly ? $"(&(objectClass=user)(cn={searchFilter}))" : $"(&(objectClass=user)(cn={searchFilter}*))";

                    var request = new SearchRequest(searchBase, filter, SearchScope.Subtree);
                    var response = (SearchResponse)connection.SendRequest(request);

                    foreach (SearchResultEntry entry in response.Entries)
                    {
                        userModels.Add(MapLdapEntryToUserModel(entry));
                    }
            }
        }
        catch (LdapException ex)
        {
            _logger.LogError(ex, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }
        return userModels;
    }

    private UserModel MapLdapEntryToUserModel(SearchResultEntry ldapEntry)
    {
        if (ldapEntry is null)
            return null;

        UserModel userModel = new UserModel
        {
            UserName = ldapEntry.Attributes["cn"]?[0]?.ToString(),
            Attribute = new Dictionary<string, string>()
        };

        foreach (string attrName in ldapEntry.Attributes.AttributeNames)
        {
            userModel.Attribute[attrName] = ldapEntry.Attributes[attrName][0]?.ToString();
        }

        return userModel;
    }
}
