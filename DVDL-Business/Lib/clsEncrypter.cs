using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DVDL_Business.Global
{
    public class clsEncrypter
    {
        public static class EncryptionHelper
        {
            private static readonly string EncryptionKey = "2"; // Replace with your secret key

            private static readonly string Salt = "!@#$%%^^"; // Replace with your salt value

            public static string EncryptText(string plainText)
            {
                using (Aes aesAlg = Aes.Create())
                {
                    Rfc2898DeriveBytes keyDerivation = new Rfc2898DeriveBytes(EncryptionKey, Encoding.UTF8.GetBytes(Salt));
                    aesAlg.Key = keyDerivation.GetBytes(32); // AES-256
                    aesAlg.IV = keyDerivation.GetBytes(16); // AES IV is 16 bytes

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aesAlg.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            using (StreamWriter writer = new StreamWriter(cryptoStream))
                            {
                                writer.Write(plainText);
                            }
                        }
                        return Convert.ToBase64String(memoryStream.ToArray());
                    }
                }
            }

            public static string DecryptText(string encryptedText)
            {
                using (Aes aesAlg = Aes.Create())
                {
                    Rfc2898DeriveBytes keyDerivation = new Rfc2898DeriveBytes(EncryptionKey, Encoding.UTF8.GetBytes(Salt));
                    aesAlg.Key = keyDerivation.GetBytes(32); // AES-256
                    aesAlg.IV = keyDerivation.GetBytes(16); // AES IV is 16 bytes

                    using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(encryptedText)))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aesAlg.CreateDecryptor(), CryptoStreamMode.Read))
                        {
                            using (StreamReader reader = new StreamReader(cryptoStream))
                            {
                                return reader.ReadToEnd();
                            }
                        }
                    }
                }
            }
        }


    }
}
