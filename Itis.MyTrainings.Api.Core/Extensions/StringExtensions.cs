using System.Security.Cryptography;
using System.Text;

namespace Itis.MyTrainings.Api.Core.Extensions;

public static class StringExtensions
{
    /// <summary>
    /// Зашифровать по Sha256
    /// </summary>
    /// <param name="inputString"></param>
    /// <returns></returns>
    public static string HashSha256(this string inputString)
    {
        var inputBytes = Encoding.UTF32.GetBytes(inputString);
        var hashedBytes = SHA256.HashData(inputBytes);

        return Convert.ToBase64String(hashedBytes);
    }
}