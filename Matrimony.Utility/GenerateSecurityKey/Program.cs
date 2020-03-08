using System;

namespace GenerateSecurityKey
{
    class Program
    {
        static void Main(string[] args)
        {
            var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
            var bytes = new byte[512 / 8];
            rng.GetBytes(bytes);
            Console.WriteLine(Convert.ToBase64String(bytes));
            Console.ReadLine();
        }
    }
}
