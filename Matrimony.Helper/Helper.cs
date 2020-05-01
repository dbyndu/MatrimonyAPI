using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;

namespace Matrimony.Helper
{
    public static class GenericHelper
    {
        public static int CalculateAge(DateTime dateOfBirth)
        {
            int age = 0;
            if (dateOfBirth != DateTime.MinValue)
            {                
                age = DateTime.Now.Year - dateOfBirth.Year;
                if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                    age = age - 1;
            }
            return age;
        }

        public static Image ByteArrayToImage(byte[] buffer)
        {
            if (buffer == null) return null;
            //MemoryStream memStream = new MemoryStream();
            //memStream.Write(buffer, 0, buffer.Length);
            return Image.Load(buffer);
        }

        public static string ResizeImage(byte[] byteArray, int width, int height, string mode = "")
        {
            string resizedImageString = string.Empty;
            if (byteArray != null)
            {
                if (width > 0 && height > 0)
                {

                    Image img = ByteArrayToImage(byteArray);
                    //img.Mutate(x => x
                    //.Resize(new ResizeOptions
                    //{
                    //    Size = new Size(width, height),
                    //    Mode = ResizeMode.Max
                    //})
                    //.BoxBlur()); 
                    if (string.IsNullOrEmpty(mode))
                    {
                        img.Mutate(x => x
                        .Resize(new ResizeOptions
                        {
                            Size = new Size(width, height),
                            Mode = ResizeMode.Crop
                        })
                        );
                    }
                    else
                        {
                        img.Mutate(x => x
                        .Resize(new ResizeOptions
                        {
                            Size = new Size(width, height),
                            Mode = ResizeMode.Crop
                        })
                        );
                    }
                    using (MemoryStream ms = new MemoryStream())
                    {
                        img.Save(ms, new JpegEncoder());
                        resizedImageString = Convert.ToBase64String(ms.ToArray());
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(mode))
                        resizedImageString = Convert.ToBase64String(byteArray);
                    else
                    {
                        Image img = ByteArrayToImage(byteArray);
                        img.Mutate(x => x.BokehBlur());
                        using (MemoryStream ms = new MemoryStream())
                        {
                            img.Save(ms, new JpegEncoder());
                            resizedImageString = Convert.ToBase64String(ms.ToArray());
                        }
                    }
                }
            }
            return resizedImageString;
        }
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
    public static class AESEncryDecry
    {
        private static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
        {
            // Check arguments.  
            if (cipherText == null || cipherText.Length <= 0)
            {
                throw new ArgumentNullException("cipherText");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            // Declare the string used to hold  
            // the decrypted text.  
            string plaintext = null;

            // Create an RijndaelManaged object  
            // with the specified key and IV.  
            using (var rijAlg = new RijndaelManaged())
            {
                //Settings  
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = key;
                rijAlg.IV = iv;

                // Create a decrytor to perform the stream transform.  
                var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                try
                {
                    // Create the streams used for decryption.  
                    using (var msDecrypt = new MemoryStream(cipherText))
                    {
                        using(var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {

                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                // Read the decrypted bytes from the decrypting stream  
                                // and place them in a string.  
                                plaintext = srDecrypt.ReadToEnd();

                            }

                        }
                    }
                }
                catch
                {
                    plaintext = "keyError";
                }
            }

            return plaintext;
        }
        private static byte[] EncryptStringToBytes(string plainText, byte[] key, byte[] iv)
        {
            // Check arguments.  
            if (plainText == null || plainText.Length <= 0)
            {
                throw new ArgumentNullException("plainText");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            byte[] encrypted;
            // Create a RijndaelManaged object  
            // with the specified key and IV.  
            using (var rijAlg = new RijndaelManaged())
            {
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = key;
                rijAlg.IV = iv;

                // Create a decrytor to perform the stream transform.  
                var encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption.  
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.  
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.  
            return encrypted;
        }
        public static string DecryptStringAES(string cipherText)
        {
            var keybytes = Encoding.UTF8.GetBytes("8080808080808080");
            var iv = Encoding.UTF8.GetBytes("8080808080808080");

            var encrypted = Convert.FromBase64String(cipherText);
            var decriptedFromJavascript = DecryptStringFromBytes(encrypted, keybytes, iv);
            return decriptedFromJavascript;
        }
    }

    public static class UserCompletionPercentage
    {
        public static Dictionary<string, int> PercentageValues;

        public const string ShortRegistration = "ShortRegistration";
        public const string Registration = "Registration";
        public const string Image = "Image";
        public const string BasicDetailsMandatory = "BasicDetailsMandatory";
        public const string BasicDetailsOptional = "BasicDetailsOptional";
        public const string ReligionCasteMandatory = "ReligionCasteMandatory";
        public const string ReligionCasteOptional = "ReligionCasteOptional";
        public const string CareerEducationMandatory = "CareerEducationMandatory";
        public const string CareerEducationOptional = "CareerEducationOptional";
        public const string FamilyDetailsMandatory = "FamilyDetailsMandatory";
        public const string FamilyDetailsOptional = "FamilyDetailsOptional";
        public const string About = "About";
        public const string LifeStyleMandatory = "LifeStyleMandatory";
        public const string LifeStyleOptional = "LifeStyleOptional";
        public const string PreferenceMandatory = "PreferenceMandatory";
        public const string PreferenceOptional = "PreferenceOptional";
        public static int GetUserCompletionPercentage(string moduleName)
        {
            int returnValue = 0;
            if(PercentageValues == null)
            {
                PercentageValues = new Dictionary<string, int>();
                PercentageValues.Add(ShortRegistration, 10);
                PercentageValues.Add(Registration, 10);
                PercentageValues.Add(Image, 10);
                PercentageValues.Add(BasicDetailsMandatory, 5);
                PercentageValues.Add(BasicDetailsOptional, 5);
                PercentageValues.Add(ReligionCasteMandatory, 5);
                PercentageValues.Add(ReligionCasteOptional, 5);
                PercentageValues.Add(CareerEducationMandatory, 5);
                PercentageValues.Add(CareerEducationOptional, 5);
                PercentageValues.Add(FamilyDetailsMandatory, 5);
                PercentageValues.Add(FamilyDetailsOptional, 5);
                PercentageValues.Add(About, 5);
                PercentageValues.Add(LifeStyleMandatory, 5);
                PercentageValues.Add(LifeStyleOptional, 5);
                PercentageValues.Add(PreferenceMandatory, 10);
                PercentageValues.Add(PreferenceOptional, 5);
            }

            if (PercentageValues.ContainsKey(moduleName))
            {
                returnValue = PercentageValues.GetValueOrDefault(moduleName, 0);
            }
            return returnValue;
            
        }
    }
}
