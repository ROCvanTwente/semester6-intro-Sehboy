using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

class Program
{
    private static readonly byte[] Key = Encoding.UTF8.GetBytes("0123456789ABCDEF0123456789ABCDEF");
    private static readonly byte[] IV = Encoding.UTF8.GetBytes("0123456789ABCDEF");

    static void Main()
    {
        Console.Write("Enter credit card number: ");
        string creditCardNumber = Console.ReadLine();

        string encryptedData = Encrypt(creditCardNumber);
        Console.WriteLine($"Encrypted: {encryptedData}");

        string decryptedData = Decrypt(encryptedData);
        Console.WriteLine($"Decrypted: {decryptedData}");
    }

    static string Encrypt(string plainText)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Key;
            aes.IV = IV;

            using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
            using (var ms = new MemoryStream())
            using (var cryptoStream = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            {
                byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                cryptoStream.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }

    static string Decrypt(string cipherText)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Key;
            aes.IV = IV;

            using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
            using (var ms = new MemoryStream(Convert.FromBase64String(cipherText)))
            using (var cryptoStream = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            using (var reader = new StreamReader(cryptoStream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
