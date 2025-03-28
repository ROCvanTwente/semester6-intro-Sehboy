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
        using (var db = new AppDbContext())
        {
            Console.Write("First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Street: ");
            string street = Console.ReadLine();

            Console.Write("Postal Address: ");
            string postalAddress = Console.ReadLine();

            Console.Write("City: ");
            string city = Console.ReadLine();

            Console.Write("House Number: ");
            int houseNumber = int.Parse(Console.ReadLine());

            Console.Write("Card Holder Name: ");
            string cardHolderName = Console.ReadLine();

            Console.Write("Enter Credit Card Number: ");
            string creditCardNumber = Console.ReadLine();

            string encryptedData = Encrypt(creditCardNumber);

            var card = new CreditCard
            {
                FirstName = firstName,
                LastName = lastName,
                Street = street,
                PostalAddress = postalAddress,
                City = city,
                HouseNumber = houseNumber,
                CardHolderName = cardHolderName,
                EncryptedCardNumber = encryptedData
            };

            db.CreditCards.Add(card);
            db.SaveChanges();

            Console.WriteLine("Credit card saved successfully!");
        }
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
