using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Matrimony.Helper
{
    public static class Helper
    {

    }

    public static class ConfigurationHelper
    {
        public static readonly string JWTAUTHENTICATIONKEY = "Jwt";
    }

    /// <summary>
    /// Encryption and Decryption using Advance Encryption Standard
    /// </summary>
    public static class Crypto
    {
        private readonly static string EncryptionKey = "MAKV2SPBNI99212";

        /// <summary>
        /// Encryption using Advance Encryption Standard
        /// </summary>
        public static string Encrypt(string plainText)
        {
            string encryptedText = string.Empty;
            byte[] clearBytes = Encoding.Unicode.GetBytes(plainText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pbkd = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pbkd.GetBytes(32);
                encryptor.IV = pbkd.GetBytes(16);
                encryptor.Padding = PaddingMode.PKCS7;
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                    }
                    encryptedText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return encryptedText;
        }
        /// <summary>
        /// Decryption using Advance Encryption Standard
        /// </summary>
        public static string Decrypt(string cipherText)
        {
            string decryptedText = string.Empty;
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pbkd = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pbkd.GetBytes(32);
                encryptor.IV = pbkd.GetBytes(16);
                encryptor.Padding = PaddingMode.PKCS7;
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                    }
                    decryptedText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
                return decryptedText;
        }
    }
}
