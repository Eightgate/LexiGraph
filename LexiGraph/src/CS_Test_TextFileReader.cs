using System;
using System.IO;
using Xunit;
using Shouldly;
using LexiGraph.src;

namespace LexiGraph.Tests
{
    public class CS_TextFileReaderTests : IDisposable
    {
        // Create a temporary file path in the current working directory.
        private readonly string tempFilePath;

        public CS_TextFileReaderTests()
        {
            tempFilePath = Path.Combine(Environment.CurrentDirectory, Path.GetRandomFileName());
        }

        public void Dispose()
        {
            // Clean up: Delete the temporary file if it exists.
            if (File.Exists(tempFilePath))
            {
                File.Delete(tempFilePath);
            }
        }

        [Fact]
        public void ReadFile_WithAbsolutePath_ReturnsExpectedContent()
        {
            // Setup
            string expectedContent = "Howdy, Cowboy!  Want some Chilli?";
            File.WriteAllText(tempFilePath, expectedContent);
            var reader = new CS_TextFileReader();

            // Action
            string actualContent = reader.ReadFile(tempFilePath);

            // Assertion
            actualContent.ShouldBe(expectedContent);
        }

        [Fact]
        public void ReadFile_WithRelativePath_ReturnsExpectedContent()
        {
            // Setup
            string expectedContent = "Relative path test using Shouldly";
            // Use only the file name so that the method combines it with Environment.CurrentDirectory.
            string fileName = Path.GetFileName(tempFilePath);
            File.WriteAllText(tempFilePath, expectedContent);
            var reader = new CS_TextFileReader();

            // Action
            string actualContent = reader.ReadFile(fileName);

            // Assertion
            actualContent.ShouldBe(expectedContent);
        }

        [Fact]
        public void ReadFile_NonExistentFile_ThrowsFileNotFoundException()
        {
            // Setup
            CS_TextFileReader reader = new CS_TextFileReader();
            string nonExistentFile = "this_file_does_not_exist.txt";

            // Action & Assertion
            Should.Throw<FileNotFoundException>(() => reader.ReadFile(nonExistentFile));
        }
    }
}
