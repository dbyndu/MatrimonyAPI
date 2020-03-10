using Matrimony.Helper;
using System;

namespace GenerateSecurityKey
{
    class Program
    {
        static void Main(string[] args)
        {
            string json = "{profile: 'self', email: 'a @a.com', phone: '9748749692', password: 'a@123'}";
            string encryptedAES = Crypto.Encrypt(json);//"U2FsdGVkX1 + HIU3e5wpRfW6H6B9AVueM0PZcqAeWzDpSzwDQsPQi5R7QIfu + owZI1ZJULi8MAIpS1IqOvwdAeg + Ytoq3ient6cysIEuhWISUU0lIPzRAv6QjcjBhAHuk";
            DecryptTest(encryptedAES);
            //GetBytes();
        }

        public static void GetBytes()
        {
            var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
            var bytes = new byte[512 / 8];
            rng.GetBytes(bytes);
            Console.WriteLine(Convert.ToBase64String(bytes));
            Console.ReadLine();
        }

        public static string DecryptTest(string cipherText)
        {
           string decyp = Crypto.Decrypt(cipherText);
            return decyp;
        }
    }
}
