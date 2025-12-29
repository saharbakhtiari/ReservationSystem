using log4stash.ErrorHandling;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading.Tasks;

namespace log4stash.Authentication
{
    public class AuthenticationMethodChooser : IAuthenticationMethodChooser
    {
        private readonly IExternalEventWriter _eventWriter;
        private IAuthenticator _innerMethod;

        public AuthenticationMethodChooser()
        {
        }

        public AuthenticationMethodChooser(IExternalEventWriter eventWriter)
        {
            _eventWriter = eventWriter;
        }

        public void UpdateEventWriter(IExternalEventWriter eventWriter)
        {
            if (_innerMethod is AwsAuthenticationMethod)
            {
                (_innerMethod as AwsAuthenticationMethod).EventWriter = eventWriter;
            }
        }

        public void AddFilter(IAuthenticator method)
        {
            _innerMethod = method;
        }

        #region Helpers for common authentication methods

        public void AddBasic(BasicAuthenticationMethod method)
        {
            AddFilter(method);
        }

        public void AddAws(AwsAuthenticationMethod method)
        {
            method.EventWriter = _eventWriter;
            AddFilter(method);
        }

        public void AddApiKey(ApiKeyAuthenticationMethod method)
        {
            AddFilter(method);
        }



        #endregion


        public ValueTask Authenticate(IRestClient client, RestRequest request)
        {
            if (_innerMethod == null)
            {
                return ValueTask.CompletedTask;
            }
            return _innerMethod.Authenticate(client, request);
        }
    }
}
