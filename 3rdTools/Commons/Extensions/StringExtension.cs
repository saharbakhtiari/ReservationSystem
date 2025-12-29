using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Extensions;

public static partial class StringExtension
{
    public static string PadLeftExactly(this string str, int totalLenght, char paddingChar)
    {
        return str.PadLeft(totalLenght, paddingChar).Substring(0, totalLenght);
    }
    public static string PadRightExactly(this string str, int totalLenght, char paddingChar)
    {
        return str.PadRight(totalLenght, paddingChar).Substring(0, totalLenght);
    }
    public static string SubStringFinalItem(this string str, int index, int finalFieldLength)
    {
        return (str.Length > (index + finalFieldLength) ? str.Substring(index, finalFieldLength) : str.Substring(index));
    }
    public static int ToIntWithDafault(this string s)
    {
        int value;
        return int.TryParse(s, out value) ? value : default;
    }
    public static long TolongWithDefault(this string s)
    {
        long value;
        return long.TryParse(s, out value) ? value : default;
    }
    public static decimal ToDecimal(this string stringNo)
    {
        return decimal.TryParse(stringNo, out decimal value) ? value : default;
    }
    public static int ToInt(this string stringNo)
    {
        return int.TryParse(stringNo, out int value) ? value : default;
    }
    public static int ToIntTime(this string stringNo)
    {
        return int.TryParse(stringNo.Replace(":", "").PadRight(8, '0'), out int value) ? value : default;
    }
    public static List<String> ToSeperateInput(this string input, string seprator = ",")
    {
        var listString = input.Split(seprator).ToList();
        return listString;
    }
    public static bool IsNullOrEmpty(this string source)
    {
        return String.IsNullOrEmpty(source);
    }
    public static bool IsNullOrWhiteSpace(this string source)
    {
        return String.IsNullOrWhiteSpace(source);
    }
    public static string Encrypt(this string source, string key)
    {
        byte[] iv = new byte[16];
        byte[] array;

        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = iv;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                    {
                        streamWriter.Write(source);
                    }

                    array = memoryStream.ToArray();
                }
            }
        }
        return Convert.ToBase64String(array);
    }
    public static string Decrypt(this string source, string key)
    {
        byte[] iv = new byte[16];
        byte[] buffer = Convert.FromBase64String(source);

        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = iv;
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new MemoryStream(buffer))
            {
                using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }

    }
    public static bool Contains(this string source, List<string> contents)
    {
        if(source.IsNullOrEmpty())
            return false;
        foreach (var item in contents)
        {
            var pattern = @"\b" + item + @"\b";
            Regex rgx = new Regex(pattern);
            Match match = rgx.Match(source);
            if(match.Success) return true;
            // if (item.IsNullOrWhiteSpace().Not() && source.Contains(pattern)) return true;
        }
        return false;
    }

}
