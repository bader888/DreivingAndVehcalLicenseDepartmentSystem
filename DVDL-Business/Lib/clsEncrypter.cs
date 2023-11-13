using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

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
    public class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convert the password string to bytes
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                // Compute the hash value of the password bytes
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);

                // Convert the hash bytes to a hexadecimal string
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    stringBuilder.Append(hashBytes[i].ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }

        public static bool VerifyPassword(string enteredPassword, string storedHashedPassword)
        {
            // Hash the entered password using the same method as during registration
            string enteredPasswordHash = HashPassword(enteredPassword);

            // Compare the hashed entered password with the stored hashed password
            return enteredPasswordHash.Equals(storedHashedPassword, StringComparison.OrdinalIgnoreCase);
        }
    }


}

