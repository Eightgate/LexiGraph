using System;
using System.IO;

namespace LexiGraph.src
{
	public class CS_TextFileReader
	{
		public CS_TextFileReader()
		{
		}

        /// <summary>
        /// Reads all text from the specified file.
        /// </summary>
        /// <param name="fileName">
        /// Parameter representing file name presumed to be in the same directory.
        /// </param>
        /// <returns>A string containing all the text read from the file.</returns>
        public string ReadFile(string fileName)
        {
            try
            {
                // Read the entire file.
                string fileContent = File.ReadAllText(fileName);

                // Check if the file is empty or contains only whitespace.
                if (string.IsNullOrWhiteSpace(fileContent))
                {
                    throw new InvalidDataException("The file is empty.");
                }

                // Return the valid file content.
                return fileContent;
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.Error.WriteLine("Access denied reading file: " + ex.Message);
                throw;  // Optionally rethrow or handle further.
            }
            catch (IOException ex)
            {
                Console.Error.WriteLine("I/O error reading file: " + ex.Message);
                throw;
            }
            catch (InvalidDataException ex)
            {
                Console.Error.WriteLine("Empty file error: " + ex.Message);
                throw;
            }
        }
    }
}

