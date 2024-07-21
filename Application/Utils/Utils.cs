using System.Security.Cryptography;
using System.Text;

namespace Application;

public static class Utils
{
    public static string MD5Hash(string value)
    {
        var md5 = MD5.Create();
        var byteValue = Encoding.ASCII.GetBytes(value);
        var byteHash = md5.ComputeHash(byteValue);
        var result = string.Join(separator: "", values: byteHash.Select(x => x.ToString("X2")));
        return result;
    }

    public static int GetCurrentUserId()
    {
        return 1;
    }
}
