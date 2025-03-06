using System;
using Python.Runtime;
using Microsoft.Extensions.Configuration;
using Newtonsoft;
using Newtonsoft.Json;

namespace LexiGraph.src
{
    /// <summary>
    /// This class takes config information and faciliates the communication
    /// between python code and c# code.  
    /// </summary>
    public class CS_PythonBridge
	{
        private const string PythonTextCleanerModule = "PYTHON_Text_Prep";
        private const string PythonNgramGenerationModule = "PYTHON_Ngram_Builder";
        private const string PythonHistogramGenerator = "PYTHON_Generate_Histogram";

        private readonly string _pythonDll;
        private readonly string _scriptDirectory;

        public CS_PythonBridge(IConfiguration configuration)
		{
            // Read configuration values.
            _pythonDll = configuration["PythonSettings:PythonDll"];
            _scriptDirectory = configuration["PythonSettings:ScriptDirectory"];

            // Set the Python runtime shared library.
            Runtime.PythonDLL = _pythonDll;
        }

        /// <summary>
        /// Cleans the provided raw text by invoking a python function from
        /// an adjacent Python module.
        /// </summary>
        /// <param name="dirtyText">A string containing the unprocessed, raw
        /// text to be cleaned.</param>
        /// <returns>A string containing the processed text after removing
        /// unwanted characters and normalizing formatting.</returns>
        /// <exception cref="Exception">
        /// Thrown when an error occurs during the text cleaning.
        /// </exception>
        public string CleanText(string dirtyText)
        {
            // Determine the full path of the Python script.
            string fullPath = System.IO.Path.GetFullPath(_scriptDirectory);

            // Get the directory containing the script.
            string scriptDirectory = System.IO.Path.GetDirectoryName(fullPath);

            // Initialize the Python engine.
            PythonEngine.Initialize();
            try
            {
                using (Py.GIL())
                {
                    // Import the sys module and add the script's directory to sys.path.
                    dynamic sys = Py.Import("sys");
                    sys.path.append(scriptDirectory);

                    // Import the Python module 
                    dynamic textCleaner = Py.Import(PythonTextCleanerModule);

                    // Call the clean_text function from the Python module.
                    dynamic cleanText = textCleaner.clean_text(dirtyText);

                    return (string) cleanText;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Exception occurred cleaning text: " + ex.Message);
                throw;
            }
            finally
            {
                PythonEngine.Shutdown();
            }
        }


        /// <summary>
        /// Cleans the provided raw text by invoking a python function from
        /// an adjacent Python module.
        /// </summary>
        /// <param name="dirtyText">A string containing the unprocessed, raw
        /// text to be cleaned.</param>
        /// <returns>A string containing the processed text after removing
        /// unwanted characters and normalizing formatting.</returns>
        /// <exception cref="Exception">
        /// Thrown when an error occurs during the text cleaning.
        /// </exception>
        public string GetNgrams(string dirtyText, int n = 2)
        {
            // Determine the full path of the Python script.
            string fullPath = System.IO.Path.GetFullPath(_scriptDirectory);

            // Get the directory containing the script.
            string scriptDirectory = System.IO.Path.GetDirectoryName(fullPath);

            // Initialize the Python engine.
            PythonEngine.Initialize();
            try
            {
                using (Py.GIL())
                {
                    // Import the sys module and add the script's directory to sys.path.
                    dynamic sys = Py.Import("sys");
                    sys.path.append(scriptDirectory);

                    // Import the Python module 
                    dynamic ngram_generator = Py.Import(PythonNgramGenerationModule);

                    dynamic jsonResult = ngram_generator.create_bigram_dictionary_as_json(dirtyText);
                    string jsonStr = (string)jsonResult;
                    //var result = JsonConvert.DeserializeObject<Dictionary<string, int>>(jsonStr);
                    return jsonStr;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Exception occurred getting ngram dict : " + ex.Message);
                throw;
            }
            finally
            {
                PythonEngine.Shutdown();
            }
        }

        public void GenerateHistogram(string ngramJson)
        {
            // Determine the full path of the Python script.
            string fullPath = System.IO.Path.GetFullPath(_scriptDirectory);

            // Get the directory containing the script.
            string scriptDirectory = System.IO.Path.GetDirectoryName(fullPath);

            // Initialize the Python engine.
            PythonEngine.Initialize();
            try
            {
                using (Py.GIL())
                {
                    // Import the sys module and add the script's directory to sys.path.
                    dynamic sys = Py.Import("sys");
                    sys.path.append(scriptDirectory);

                    // Import the Python module 
                    dynamic graphGen = Py.Import(PythonHistogramGenerator);

                    // Call the python function to generate the histogram
                    graphGen.generate_histogram(ngramJson);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Exception occurred cleaning text: " + ex.Message);
                throw;
            }
            finally
            {
                PythonEngine.Shutdown();
            }
        }
    }
}

