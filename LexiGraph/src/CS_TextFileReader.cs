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
            // If filePath is not rooted, assume it's a file name in the current working directory.
            if (!Path.IsPathRooted(fileName))
            {
                fileName = Path.Combine(Environment.CurrentDirectory, fileName);
            }

            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException($"File not found: {fileName}");
            }

            // Read all text from the file.
            string fileContent = File.ReadAllText(fileName);

            return fileContent;
        }
    }
}

