// Title: Generate Code 16K barcodes from CSV data
// Description: This example reads a CSV file containing filenames and barcode texts, then creates Code 16K PNG images using Aspose.BarCode.
// Category-Description: Demonstrates batch barcode generation with Aspose.BarCode in a console application. It showcases the BarcodeGenerator class, EncodeTypes.Code16K, and image saving via BarCodeImageFormat. Typical use cases include bulk creation of barcode assets for inventory, shipping, or labeling systems where developers need to automate image output from data sources.
// Prompt: Develop console application reading CSV barcode data, creating corresponding Code 16K PNG images.
// Tags: barcode, code16k, csv, png, batch-generation, aspose.barcode, console

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Console application that reads barcode data from a CSV file and generates Code 16K PNG images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Processes the CSV, generates barcodes, and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        const string csvPath = "input.csv";
        const string outputFolder = "Barcodes";

        // Verify that the CSV file exists
        if (!File.Exists(csvPath))
        {
            Console.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        // Ensure the output directory exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Read all lines from the CSV file
        string[] lines = File.ReadAllLines(csvPath);
        foreach (string rawLine in lines)
        {
            // Skip empty or whitespace-only lines
            if (string.IsNullOrWhiteSpace(rawLine))
                continue;

            // Expected CSV format: filename,codeText
            string[] parts = rawLine.Split(',');
            if (parts.Length < 2)
            {
                Console.WriteLine($"Invalid line (expected two columns): {rawLine}");
                continue;
            }

            string fileName = parts[0].Trim();
            string codeText = parts[1].Trim();

            // Validate that both filename and code text are provided
            if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(codeText))
            {
                Console.WriteLine($"Empty filename or codetext in line: {rawLine}");
                continue;
            }

            // Build the full output path and ensure a .png extension
            string outputPath = Path.Combine(outputFolder, fileName);
            if (!outputPath.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                outputPath += ".png";

            // Create and configure the barcode generator for Code16K
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code16K))
            {
                // Set the text to encode
                generator.CodeText = codeText;

                // Optional: configure Code16K specific parameters
                generator.Parameters.Barcode.Code16K.AspectRatio = 1.0f; // default aspect ratio
                generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = 1; // integer value
                generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = 1; // integer value

                // Save the generated barcode as a PNG image
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Generated barcode: {outputPath}");
        }

        Console.WriteLine("Processing completed.");
    }
}