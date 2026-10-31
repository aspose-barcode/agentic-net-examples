// Title: Generate Code 16K barcodes from CSV data
// Description: This example reads a CSV file containing barcode text and output file names, then creates Code 16K PNG images using Aspose.BarCode.
// Category-Description: Demonstrates basic Aspose.BarCode generation for the Code 16K symbology. It covers reading input data, configuring barcode parameters (module size, aspect ratio, quiet zones), and saving images. Developers working with barcode creation, batch processing, or file‑based workflows can use this pattern as a starting point for automating barcode image production.
// Prompt: Develop console application reading CSV barcode data, creating corresponding Code 16K PNG images.
// Tags: barcode, code16k, csv, png, aspose.barcode, generation, console

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Console application that reads barcode data from a CSV file and generates Code 16K PNG images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Reads CSV, creates output directory, generates barcodes, and writes status messages.
    /// </summary>
    static void Main()
    {
        // Define input CSV path and output directory
        string csvPath = "barcodes.csv";
        string outputDir = "output";

        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // If the CSV file does not exist, create a sample file with example data
        if (!File.Exists(csvPath))
        {
            using (var writer = new StreamWriter(csvPath))
            {
                writer.WriteLine("Aspose.Barcode,code1.png");
                writer.WriteLine("12345678901231,code2.png");
                writer.WriteLine("HELLO123,code3.png");
            }
        }

        // Read all lines from the CSV file
        string[] lines = File.ReadAllLines(csvPath);
        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i].Trim();

            // Skip empty lines
            if (string.IsNullOrEmpty(line))
                continue;

            // Split each line into barcode text and output file name
            string[] parts = line.Split(',');
            if (parts.Length < 2)
            {
                Console.WriteLine($"Skipping invalid line {i + 1}: '{line}'");
                continue;
            }

            string codeText = parts[0];
            string fileName = parts[1];
            string outputPath = Path.Combine(outputDir, fileName);

            // Generate the barcode using Aspose.BarCode
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
            {
                // Set module size (pixel width of the smallest bar)
                generator.Parameters.Barcode.XDimension.Pixels = 2f;

                // Set aspect ratio (recommended value > 8 for Code 16K)
                generator.Parameters.Barcode.Code16K.AspectRatio = 10;

                // Optional quiet zone settings (defaults are acceptable)
                generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = 10;
                generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = 1;

                // Save the generated barcode as a PNG image
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Generated '{outputPath}' for code text '{codeText}'.");
        }

        Console.WriteLine("Barcode generation completed.");
    }
}