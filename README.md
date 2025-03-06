# LexiGraph

LexiGraph is a .NET application that demonstrates cross-language integration by processing text through a Python-based cleaning and n‑gram generation pipeline. The application accepts either a direct text input or a file containing text, cleans the text using a Python module, generates n‑gram data, and produces a histogram of n‑gram frequencies.

## Features

- **Cross-Language Integration:**  
  Leverages Python.NET to call Python code from C#.
- **Text Processing:**  
  Cleans raw (or "dirty") text using a Python module.
- **N-Gram Generation:**  
  Currently creates bigrams from cleaned text, with plans to expand to support n‑grams of other sizes in the future.
- **Visualization:**  
  Generates histograms of n‑gram frequencies using Python’s matplotlib, displaying the histogram and saving it to a file.
- **Configurable Settings:**  
  Uses an `appsettings.json` file to store configuration values such as the Python runtime DLL path and script directory.

## Requirements

- **.NET SDK:**  
  [.NET 7](https://dotnet.microsoft.com/)
- **Python:**  
  Python 3.10 (or the version configured in `appsettings.json`). Ensure your Python installation includes the shared library (e.g. `libpython3.10.dylib`).
- **NuGet Packages:**  
  - [Python.NET](https://www.nuget.org/packages/Python.Runtime)
  - [Shouldly](https://www.nuget.org/packages/Shouldly)
  - [xUnit](https://www.nuget.org/packages/xunit)
  - [xUnit.runner.visualstudio](https://www.nuget.org/packages/xunit.runner.visualstudio)
  - [Microsoft.NET.Test.Sdk](https://www.nuget.org/packages/Microsoft.NET.Test.Sdk)

## Setup

1. **Clone the Repository:**  
   Clone this repository to your local machine.

2. **Configure the Project:**  
   Ensure that your project file (csproj) includes the following in a `<PropertyGroup>`:
   ```xml
   <StartupObject>LexiGraph.Program</StartupObject>
   ```
   This tells the build system which class contains your `Main` method.

3. **appsettings.json:**  
   Place an `appsettings.json` file in the project root (next to your csproj file) with content similar to:
   ```json
   {
     "PythonSettings": {
       "PythonDll": "/Library/Frameworks/Python.framework/Versions/3.10/lib/libpython3.10.dylib",
       "ScriptDirectory": "/Users/UserName/ProjectDirectory/LexiGram/LexiGram/src/" 
     }
   }
   ```
   Adjust the paths as needed for your environment.

## Running the Application

You can run the application with one of the following command-line formats:

- **Process direct text:**  
  ```bash
  dotnet run text "This is sample text"
  ```
- **Process a text file:**  
  ```bash
  dotnet run filename sample.txt
  ```

The program will read the input, process it using the configured Python modules, and generate a histogram of the n‑grams.

## Testing

This project uses two testing frameworks:

- **C# Tests:**  
  xUnit (with Shouldly for assertions) is used for unit testing the C# code.
- **Python Tests:**  
  pytest is used for testing the Python modules.

### Running C# Tests

1. **Build the project:**
   ```bash
   dotnet build
   ```
2. **Run tests:**
   ```bash
   dotnet test
   ```

### Running Python Tests

1. Navigate to the directory containing your Python tests (e.g., a `tests` folder if you have one).
2. **Run pytest:**
   ```bash
   pytest
   ```

## Future Improvements

This project is under active development. Planned enhancements include:
- **File Structure Reorganization:**  
  Refactoring and reorganizing files for better maintainability.
- **Enhanced Naming Conventions:**  
  Providing clearer distinctions between bigram and n‑gram functionality.
- **Histogram Customization:**  
  Allowing users to choose the name and location of the histogram file that is created.
- **Additional Testing:**  
  Expanding both C# and Python tests as new functionality is added.

## Configuration

The project uses an `appsettings.json` file for configuration. Key settings include:

- **PythonDll:** The full path to the Python shared library (e.g., `libpython3.10.dylib`).
- **ScriptDirectory:** The directory where your Python modules reside.

This approach makes it easy to adjust paths and settings without modifying your source code.

