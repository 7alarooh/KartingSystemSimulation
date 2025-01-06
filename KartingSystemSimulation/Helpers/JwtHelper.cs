using System.IdentityModel.Tokens.Jwt;

public static class JwtHelper
{
    public static string ExtractToken(HttpRequest request)
    {
        const string authorizationHeader = "Authorization";
        const string bearerPrefix = "Bearer ";
        if (request.Headers.ContainsKey(authorizationHeader))
        {
            var token = request.Headers[authorizationHeader].ToString();
            if (token.StartsWith(bearerPrefix, StringComparison.OrdinalIgnoreCase))
            {
                return token.Substring(bearerPrefix.Length).Trim();
            }
        }
        return null; // Token not found
    }

    public static string GetClaimValue(string jwtToken, string claimType)
    {
        var handler = new JwtSecurityTokenHandler();
        if (handler.CanReadToken(jwtToken))
        {
            var jwtTokenObj = handler.ReadJwtToken(jwtToken);
            var claim = jwtTokenObj.Claims.FirstOrDefault(c => c.Type == claimType);
            return claim?.Value;
        }
        throw new ArgumentException("Invalid JWT Token.");
    }

    public static bool HasPermission(string jwtToken, string requiredPermission)
    {
        var permissions = GetClaimValue(jwtToken, "Permissions");
        if (!string.IsNullOrEmpty(permissions))
        {
            return permissions.Split(',').Contains(requiredPermission);
        }
        return false;
    }
}
