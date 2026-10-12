// Title: Generate Code 16K Barcodes from CSV and Save as PNG
// Description: This example reads barcode identifiers and data from a CSV file, creates Code 16K barcodes, and saves each as a PNG image in a temporary folder.
// Category-Description: Demonstrates Aspose.BarCode generation workflow – reading external data, configuring barcode parameters, and exporting images. The example uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes, typical for batch barcode creation scenarios such as inventory labeling, shipping manifests, or product catalog generation. Developers often need to automate barcode image production from data sources like CSV, databases, or APIs; this snippet shows a concise pattern for that purpose.
// Prompt: Develop console application reading CSV barcode data, creating corresponding Code 16K PNG images.
// Tags: code16k, barcode, generation, png, csv, aspose.barcode, console

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates reading barcode data from a CSV file and generating Code 16K PNG images using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a temporary output folder, writes sample CSV data, reads it, generates barcodes, and saves PNG files.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a unique temporary folder for output
        string outputFolder = Path.Combine(Path.GetTempPath(), "Code16KBarcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Prepare a sample CSV file with barcode data
        string csvPath = Path.Combine(outputFolder, "data.csv");
        using (var writer = new StreamWriter(csvPath))
        {
            writer.WriteLine("barcode1,HELLO123");
            writer.WriteLine("barcode2,ABCDEF");
            writer.WriteLine("barcode3,1234567890");
        }

        // Read the CSV and generate Code 16K barcodes
        using (var reader = new StreamReader(csvPath))
        {
            while (!reader.EndOfStream)
            {
                // Read a line from the CSV file
                string line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    continue; // Skip empty lines

                // Split the line into filename and barcode text
                string[] parts = line.Split(',');
                if (parts.Length < 2)
                    continue; // Skip malformed lines

                string fileName = parts[0].Trim();
                string codeText = parts[1].Trim();

                // Generate barcode using Aspose.BarCode
                using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
                {
                    // Set visual parameters
                    generator.Parameters.Barcode.XDimension.Pixels = 2f;
                    generator.Parameters.Barcode.Code16K.AspectRatio = 10f;

                    // Define output file path and save as PNG
                    string outputPath = Path.Combine(outputFolder, fileName + ".png");
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Generated: {outputPath}");
                }
            }
        }

        Console.WriteLine("Barcode generation completed.");
    }
}