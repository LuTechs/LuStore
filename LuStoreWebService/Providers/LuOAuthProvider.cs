using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;

namespace LuStoreWebService.Providers
{
    public class LuOAuthProvider:OAuthAuthorizationServerProvider
    {
        public override  Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userName = context.UserName;
            var password = context.Password;
            if (userName == "admin" && password == "123")
            {
                context.Validated(new ClaimsIdentity(context.Options.AuthenticationType));
            }
            else
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
            }
           return Task.FromResult<object>(null);

        }
    }
}