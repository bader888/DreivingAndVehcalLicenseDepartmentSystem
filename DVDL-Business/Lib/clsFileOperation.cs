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

        public static bool RememberUsernameAndPassword(string Username, string Password)
        {

            //--> In General 
            //create file and save the username and password in it if the usermae is empty delete the file 
            try
            {
                //1-this will get the current project directory folder.
                string currentDirectory = System.IO.Directory.GetCurrentDirectory();


                //2-Define the path to the text file where you want to save the data
                string filePath = currentDirectory + "\\data.txt";

                //3-incase the username is empty, delete the file
                if (Username == "" && File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;

                }

                // concatonate username and passwrod withe seperator.
                string dataToSave = Username + "#//#" + Password;

                // Create a StreamWriter to write to the file
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // Write the data to the file
                    writer.WriteLine(dataToSave);

                    return true;
                }
            }
            catch (Exception ex)
            {
                //  MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }

        }

        public static bool GetStoredCredential(ref string Username, ref string Password)
        {
            //this will get the stored username and password and will return true if found and false if not found.
            try
            {
                //gets the current project's directory
                string currentDirectory = System.IO.Directory.GetCurrentDirectory();

                // Path for the file that contains the credential.
                string filePath = currentDirectory + "\\data.txt";

                // Check if the file exists before attempting to read it
                if (File.Exists(filePath))
                {
                    // Create a StreamReader to read from the file
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        // Read data line by line until the end of the file
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {

                            string[] result = line.Split(new string[] { "#//#" }, StringSplitOptions.None);

                            //the first index in array is the user name and second index is the password
                            Username = result[0];
                            Password = result[1];
                        }
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }

        }
    }
}