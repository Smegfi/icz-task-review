using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace SlotMachineTest.Integration.Infrastructure;

public class TestAuthHandlerUserProvider
{
    private string[] _roles = [];
    private string _userName = string.Empty;

    public void SetUser(string userName, string[] roles)
    {
        _userName = userName;
        _roles = roles;
    }

    public string GetUserName()
    {
        return _userName;
    }

    public string[] GetRoles()
    {
        return _roles;
    }
}

public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public const string SchemeName = "Test";

    private readonly TestAuthHandlerUserProvider _userProvider;

    [Obsolete("Obsolete")]
    public TestAuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        TestAuthHandlerUserProvider userProvider)
        : base(options, logger, encoder, clock)
    {
        _userProvider = userProvider;
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var claims = new List<Claim> { new(ClaimTypes.Name, _userProvider.GetUserName()) };
        claims.AddRange(_userProvider.GetRoles().Select(r => new Claim(ClaimTypes.Role, r)));

        var identity = new ClaimsIdentity(claims, SchemeName);
        var principal = new ClaimsPrincipal(identity);

        return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(principal, SchemeName)));
    }
}