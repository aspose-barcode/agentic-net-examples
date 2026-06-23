using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating Code39 barcodes from a list of strings (read from a CSV file or sample data)
/// and saving them as SVG files using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Reads input data, generates barcodes, and writes SVG files to the output directory.
    /// </summary>
    static void Main()
    {
        // Path to the optional CSV file containing barcode texts (one per line).
        string csvPath = "input.csv";

        // Array to hold the barcode texts.
        string[] codeTexts;

        // Load barcode texts from CSV if it exists; otherwise use predefined sample data.
        if (File.Exists(csvPath))
        {
            // Read all lines from the CSV file; each line is expected to contain a single value.
            codeTexts = File.ReadAllLines(csvPath);
        }
        else
        {
            // Sample data used when the CSV file is missing.
            codeTexts = new string[]
            {
                "ABC123",
                "987654321",
                "CODE-39",
                "Aspose.BarCode",
                "Sample001"
            };
            Console.WriteLine("CSV file not found. Using sample data.");
        }

        // Ensure the output directory for barcode images exists.
        string outputDir = "Barcodes";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Process each barcode text.
        foreach (string rawLine in codeTexts)
        {
            // Trim whitespace and guard against null values.
            string codeText = rawLine?.Trim();

            // Skip empty or null lines.
            if (string.IsNullOrEmpty(codeText))
                continue;

            // Create a safe file name by removing invalid path characters
            string safeFileName = string.Concat(codeText.Split(Path.GetInvalidFileNameChars()));

            // Full path for the SVG output file.
            string outputPath = Path.Combine(outputDir, safeFileName + ".svg");

            // Generate a Code39 barcode with full ASCII support.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, codeText))
            {
                // No additional settings are required for basic Code39 generation.
                try
                {
                    // Save the barcode as an SVG file.
                    generator.Save(outputPath, BarCodeImageFormat.Svg);
                    Console.WriteLine($"Saved barcode for \"{codeText}\" to \"{outputPath}\"");
                }
                catch (Exception ex)
                {
                    // Handle potential errors (e.g., licensing restrictions) and continue processing.
                    Console.WriteLine($"Failed to save barcode for \"{codeText}\": {ex.Message}");
                }
            }
        }

        Console.WriteLine("Processing completed.");
    }
}
