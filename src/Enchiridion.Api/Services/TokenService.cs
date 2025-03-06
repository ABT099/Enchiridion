using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Enchiridion.Api.Services;

public static class TokenService
{
    public static string GenerateToken(int id, string authId, string role)
    {
        var identity = new ClaimsIdentity();
        identity.AddClaim(new Claim(EnchiridionConstants.Claims.Id, id.ToString()));
        identity.AddClaim(new Claim(EnchiridionConstants.Claims.AuthId, authId));
        identity.AddClaim(new Claim(EnchiridionConstants.Claims.Role, role));

        var key = new RsaSecurityKey(EnchiridionConstants.Keys.RsaKey);

        var handler = new JsonWebTokenHandler();
        var token = handler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = "https://localhost:5001",
            Subject = identity,
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha256)
        });

        return token;
    }

    public static int GetUserId(HttpContext httpContext) =>
        int.TryParse(GetClaim(EnchiridionConstants.Claims.Id, httpContext), out var id) 
            ? id
            : throw new Exception("Invalid id");
    
    public static string GetAuthId(HttpContext httpContext) =>
        GetClaim(EnchiridionConstants.Claims.AuthId, httpContext);
    
    private static string GetClaim(string claimType, HttpContext context) => context.User.Claims
        .FirstOrDefault(x => x.Type.Equals(claimType))?.Value!;

    public static string GetTokenFromContext(HttpContext context)
    {
        var authorizationHeader = context.Request.Headers.Authorization.ToString();
        if (string.IsNullOrWhiteSpace(authorizationHeader) ||
            !authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidTokenException("Invalid token, the token must be a Bearer token.");
        }

        var token = authorizationHeader["Bearer ".Length..].Trim();

        if (string.IsNullOrWhiteSpace(token))
        {
            throw new InvalidTokenException("Token not found.");
        }
        
        return token;
    }

    public class InvalidTokenException(string message) : Exception(message);
}