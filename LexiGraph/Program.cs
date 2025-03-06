using System;
using LexiGraph.src;
using Microsoft.Extensions.Configuration;

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

            // Build configuration from appsettings.json.
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            CS_PythonBridge pyBridge = new CS_PythonBridge(config);

            // Clean the data
            string cleanData = pyBridge.CleanText(rawData);

            // Get the bigrams as a json string
            string gramData = pyBridge.getNgrams(cleanData);

            // Generate graph 
            pyBridge.GenerateHistogram(gramData);

            Environment.Exit(0);
        }
    }
}