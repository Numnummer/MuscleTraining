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
    
    /// <summary>
    /// Возвращает строку переведеннуб на латиницу
    /// </summary>
    /// <param name="source">Исходная строка</param>
    /// <returns>Строка переведенная на латиницу</returns>
    public static string ToTransliatiateLatin(this string source)
    {
        var transliterationDict = new Dictionary<char, string>
        {
            {'А', "A"}, {'Б', "B"}, {'В', "V"}, {'Г', "G"}, {'Д', "D"}, {'Е', "E"}, {'Ё', "Yo"}, {'Ж', "Zh"}, {'З', "Z"},
            {'И', "I"}, {'Й', "J"}, {'К', "K"}, {'Л', "L"}, {'М', "M"}, {'Н', "N"}, {'О', "O"}, {'П', "P"}, {'Р', "R"}, 
            {'С', "S"}, {'Т', "T"}, {'У', "U"}, {'Ф', "F"}, {'Х', "H"}, {'Ц', "Ts"}, {'Ч', "Ch"}, {'Ш', "Sh"}, 
            {'Щ', "Shch"}, {'Ъ', ""}, {'Ы', "Y"}, {'Ь', ""}, {'Э', "E"}, {'Ю', "Yu"}, {'Я', "Ya"}, {'а', "a"}, 
            {'б', "b"}, {'в', "v"}, {'г', "g"}, {'д', "d"}, {'е', "e"}, {'ё', "yo"}, {'ж', "zh"}, {'з', "z"}, {'и', "i"},
            {'й', "j"}, {'к', "k"}, {'л', "l"}, {'м', "m"}, {'н', "n"}, {'о', "o"}, {'п', "p"}, {'р', "r"}, {'с', "s"},
            {'т', "t"}, {'у', "u"}, {'ф', "f"}, {'х', "h"}, {'ц', "ts"}, {'ч', "ch"}, {'ш', "sh"}, {'щ', "shch"}, 
            {'ъ', ""}, {'ы', "y"}, {'ь', ""}, {'э', "e"}, {'ю', "yu"}, {'я', "ya"}
        };
        
        return string.Join("", 
                source.Select(c => transliterationDict.TryGetValue(c, out var value) 
                    ? value 
                    : c.ToString()));
    }
}