namespace ScrumMovieTheater.Old.Helpers;

using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

public class DevelopmentAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public DevelopmentAuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder) : base(options, logger, encoder) { }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // Define hardcoded claims for development (e.g., User ID, Role, Permission)
        var claims = new[] {
            new Claim(ClaimTypes.NameIdentifier, "developer"),
            new Claim(ClaimTypes.Name, "Developer"),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }
}