using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demo program that reads barcode data from a CSV file and generates Han Xin barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Reads input CSV, validates files, creates output directory, and generates up to five barcode images.
    /// </summary>
    static void Main()
    {
        // Path to the input CSV file (used as a simple substitute for an Excel sheet)
        const string inputFile = "data.csv";

        // Verify that the input file exists before proceeding
        if (!File.Exists(inputFile))
        {
            Console.WriteLine($"Input file not found: {inputFile}");
            return;
        }

        // Directory where generated barcode images will be saved
        const string outputDir = "Barcodes";

        // Create the output directory if it does not already exist
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Read all lines from the CSV file; each line should contain the text to encode in a barcode
        string[] lines = File.ReadAllLines(inputFile);

        // Process a maximum of five rows to keep the example concise and fast
        int rowsToProcess = Math.Min(lines.Length, 5);

        // Iterate over each line to generate a barcode
        for (int i = 0; i < rowsToProcess; i++)
        {
            // Trim whitespace and skip empty lines
            string codeText = lines[i].Trim();
            if (string.IsNullOrEmpty(codeText))
            {
                Console.WriteLine($"Skipping empty line at index {i}.");
                continue;
            }

            // Create a barcode generator for the Han Xin symbology with the current text
            using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
            {
                // Optional: configure Han Xin specific parameters if needed
                // generator.Parameters.HanXin.HanXinEncodeMode = ...;
                // generator.Parameters.HanXin.HanXinErrorLevel = ...;
                // generator.Parameters.HanXin.HanXinVersion = ...;

                // Build the output file path (e.g., "Barcodes/hanxin_1.png")
                string outputPath = Path.Combine(outputDir, $"hanxin_{i + 1}.png");

                // Save the generated barcode image to the specified path
                generator.Save(outputPath);

                Console.WriteLine($"Generated barcode for row {i + 1}: {outputPath}");
            }
        }

        Console.WriteLine("Barcode generation completed.");
    }
}