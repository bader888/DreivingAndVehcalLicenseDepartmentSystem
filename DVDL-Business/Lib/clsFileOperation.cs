using System;
using System.IO;

namespace DVDL_Business.Lib
{
    public class clsFileOperations
    {
        static public bool IsFileEmpty(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    FileInfo fileInfo = new FileInfo(filePath);
                    if (fileInfo.Length == 0)
                    {
                        return true; // The file is empty
                    }
                    else
                    {
                        return false; // The file is not empty
                    }
                }
                else
                {
                    throw new FileNotFoundException("The specified file does not exist.");
                }
            }
            catch (Exception ex)
            {
                //  Console.WriteLine($"Error: {ex.Message}");
                return false; // An error occurred
            }
        }

        static public void ClearFile(string filePath)
        {
            try
            {
                // Overwrite the file with an empty string to clear its content
                File.WriteAllText(filePath, string.Empty);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }


        static public void AddUserCredentialsAndClearFile(string filePath, string username, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                {
                    throw new ArgumentException("Username and password cannot be empty or whitespace.");
                }

                // Check if the file exists; if not, create a new one
                if (!File.Exists(filePath))
                {
                    using (FileStream fs = File.Create(filePath))
                    {
                        // Create an empty file
                    }
                }

                // Format the credentials as a string (e.g., "username:password")
                string credentials = $"{username}:{password}";

                // Clear the file by overwriting it with the new credentials
                File.WriteAllText(filePath, credentials + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

    }






}