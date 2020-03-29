using Matrimony.Helper;
using System;

namespace GenerateSecurityKey
{
    class Program
    {
        static void Main(string[] args)
        {
            string json = "{profile: 'self', email: 'a @a.com', phone: '9748749692', password: 'a@123'}";
            string encryptedStringFromAngular = "fXkLM7rOug4I0msVm6NVQ4Cz7RNdvwNApyXCE8JBvr5jWaEufrduA2d8MqzrJTWF4bUM9IMi53fBiQriOrHdYkioTY4NBjKF7SKO0rs19ZfH4cHi1jnD5ir3FrwVxPW4VYLTvzQYtSvGG4Vz5oNXbQ==";
            string decryptAES = AESEncryDecry.DecryptStringAES(encryptedStringFromAngular);
                //Crypto.Encrypt(json);//"U2FsdGVkX1 + HIU3e5wpRfW6H6B9AVueM0PZcqAeWzDpSzwDQsPQi5R7QIfu + owZI1ZJULi8MAIpS1IqOvwdAeg + Ytoq3ient6cysIEuhWISUU0lIPzRAv6QjcjBhAHuk";
            //string decryptAES = DecryptTest(encryptedAES);
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
