using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace WebApiBasicAuth.Scheme
{
    public class BasicAuth : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly UserManager<IdentityUser> _userManager;

        public BasicAuth(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock,UserManager<IdentityUser> userManager) : base(options, logger, encoder, clock)
        {
            _userManager = userManager;
        }

        protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var auth = Request.Headers.ContainsKey("Authorization");
            if (auth)
            {
                var headerValue = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);

                var bytes = Convert.FromBase64String(headerValue.Parameter);

                var credentials = Encoding.UTF8.GetString(bytes);

                var array=credentials.Split(':');
                var userName = array[0];
                var password = array[1];


                var user=await _userManager.FindByNameAsync(userName);
                if (user != null)
                {
                    var validatePassword = await _userManager.CheckPasswordAsync(user, password);

                    if (!validatePassword)
                    {
                        return AuthenticateResult.Fail("Şifreniz hatalı!");
                    }
                    else
                    {
                        //Claims
                        var claims = new[] {new Claim(ClaimTypes.Name,userName)};

                        //Identity
                        var identity = new ClaimsIdentity(claims,Scheme.Name);

                        //Principal
                        var principal = new ClaimsPrincipal(identity);

                        //Ticket
                        var ticket=new AuthenticationTicket(principal,Scheme.Name);

                        return AuthenticateResult.Success(ticket);
                    }

                }
                else
                {
                    return AuthenticateResult.Fail("Yetkiniz bulunmuyor!");
                }
            }
            else
            {
                return AuthenticateResult.Fail("Yetkiniz bulunmuyor!");
            }
        }
    }
}
