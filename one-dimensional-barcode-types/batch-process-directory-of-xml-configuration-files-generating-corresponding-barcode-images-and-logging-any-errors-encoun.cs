using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Generates barcode images from XML configuration files in batch mode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Accepts optional command‑line arguments:
    /// args[0] – input directory containing XML files (default: "BarcodesConfig").
    /// args[1] – output directory for generated images (default: inputDir\Output).
    /// </summary>
    /// <param name="args">Command‑line arguments.</param>
    static void Main(string[] args)
    {
        // Determine input directory: use first argument if provided, otherwise default.
        string inputDir = args.Length > 0 ? args[0] : "BarcodesConfig";

        // Determine output directory: use second argument if provided, otherwise create "Output" subfolder.
        string outputDir = args.Length > 1 ? args[1] : Path.Combine(inputDir, "Output");

        // Verify that the input directory exists; abort if it does not.
        if (!Directory.Exists(inputDir))
        {
            Console.WriteLine($"Input directory does not exist: {inputDir}");
            return;
        }

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDir);

        // Path to the error log file inside the output directory.
        string logPath = Path.Combine(outputDir, "error.log");

        // Retrieve all XML files from the input directory.
        string[] xmlFiles = Directory.GetFiles(inputDir, "*.xml");

        // If no XML files are found, inform the user and exit.
        if (xmlFiles.Length == 0)
        {
            Console.WriteLine($"No XML configuration files found in: {inputDir}");
            return;
        }

        // Open the log file for appending error messages.
        using (var logWriter = new StreamWriter(logPath, true))
        {
            // Process each XML configuration file.
            foreach (string xmlFile in xmlFiles)
            {
                try
                {
                    // Import barcode settings from the XML file.
                    using (var generator = BarcodeGenerator.ImportFromXml(xmlFile))
                    {
                        // Build the output file path with .png extension.
                        string outputFile = Path.Combine(outputDir, Path.GetFileNameWithoutExtension(xmlFile) + ".png");

                        // Save the generated barcode image.
                        generator.Save(outputFile);

                        // Inform the user about the successful generation.
                        Console.WriteLine($"Generated: {outputFile}");
                    }
                }
                catch (Exception ex)
                {
                    // Build a descriptive error message.
                    string message = $"Error processing '{xmlFile}': {ex.Message}";

                    // Write the error to console and log file with UTC timestamp.
                    Console.WriteLine(message);
                    logWriter.WriteLine($"{DateTime.UtcNow:u} {message}");
                }
            }
        }

        // Indicate that batch processing has finished.
        Console.WriteLine("Batch processing completed.");
    }
}