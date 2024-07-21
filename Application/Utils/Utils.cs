using System.Security.Cryptography;
using System.Text;
using Abstraction.Common;
using Microsoft.AspNetCore.Http;

namespace Application;

public static class Utils
{
    public static JwtSettings JwtSettings { get; set; }

    public static string MD5Hash(string value)
    {
        var md5 = MD5.Create();
        var byteValue = Encoding.ASCII.GetBytes(value);
        var byteHash = md5.ComputeHash(byteValue);
        var result = string.Join(separator: "", values: byteHash.Select(x => x.ToString("X2")));
        return result;
    }

    public static int GetCurrentUserId(HttpContext httpContext)
    {
        var claim = httpContext.User.Claims.FirstOrDefault(c => c.Type == "user-id");
        if (claim == null)
            throw new UnauthorizedAccessException();
        return Convert.ToInt32(claim.Value);

    }
}
