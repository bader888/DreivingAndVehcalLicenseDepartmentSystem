using DVDL_Business;
using DVDL_Business.Lib;
using System;
using System.IO;
namespace DVLD.Users
{
    class UserAuthentication
    {

        public static void SetUserCredentials(string username, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                {
                    throw new ArgumentException("Username and password cannot be empty or whitespace.");
                }

                //// Format the credentials as a string (e.g., "username:password")
                //string credentials = $"{username}:{password}";

                //// Overwrite the file with the new credentials
                //File.WriteAllText(clsGlobal.filePath, credentials + Environment.NewLine);

                clsFileOperations.AddUserCredentialsAndClearFile(clsGlobal.filePath, username, password);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        public static (string username, string password) GetUserCredentials()
        {
            try
            {
                if (!File.Exists(clsGlobal.filePath))
                {
                    throw new FileNotFoundException("The file does not exist.");
                }

                string[] lines = File.ReadAllLines(clsGlobal.filePath);

                if (lines.Length == 0)
                {
                    throw new InvalidOperationException("The file is empty.");
                }

                string[] parts = lines[0].Split(':');

                if (parts.Length != 2)
                {
                    throw new FormatException("Invalid credential format in the file.");
                }

                string username = parts[0];
                string password = parts[1];

                return (username, password);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return (null, null);
            }
        }
    }


}

