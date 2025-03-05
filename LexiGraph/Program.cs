using System;
using LexiGraph.src;

/// <summary>
/// Main entry point for C# execution of LexiGraph
/// </summary>
/// <remarks>
/// Can take a string input for processing or will use a default string if none
/// is provided. 
/// </remarks>
namespace LexiGraph
{
    class Program
    {
        public static void Main(string[] args)
        {
            // Verify that an argument has been passed
            if (args.Length == 0)
            {
                Console.WriteLine("Please call this program with a text file or txt to process");
                Console.WriteLine("Example:  dotnet run filename sample.txt");
                Console.WriteLine(@"Example: dotnet run text ""This is sample text""");
                Environment.Exit(2);
            }

            // TODO: Validate input

            // Get the raw data to be passed later
            String rawData;
            if (args[0].ToLower() == "text")
                rawData = args[1];
            else
            {
                CS_TextFileReader textFileReader = new CS_TextFileReader();
                rawData = textFileReader.ReadFile(args[1]);
            }

            // 

            Console.WriteLine(rawData);

        }
    }
}