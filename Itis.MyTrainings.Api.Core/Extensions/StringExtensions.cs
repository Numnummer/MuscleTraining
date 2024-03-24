using System.Globalization;
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

    /// <summary>
    /// Возвращает строку с первым заглавным символом
    /// </summary>
    /// <param name="source">Исходная строка</param>
    public static string ToUpperFirstCharString(this string source)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        return source.Remove(1).ToUpper(CultureInfo.InvariantCulture) + source.Substring(1);
    }
}