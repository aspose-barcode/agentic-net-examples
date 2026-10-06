// Title: Generate Planet barcodes from CSV values and save as PNG
// Description: Demonstrates reading numeric strings from a CSV file, creating a Planet barcode for each value, and storing the images as PNG files.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator with EncodeTypes.Planet. Typical use cases include batch creation of barcodes for inventory, tracking, or labeling systems where data originates from CSV or other flat files. Developers often need to loop through data sources, configure barcode parameters, and export images in common formats such as PNG.
// Prompt: Generate a batch of Planet barcodes from a CSV list of numeric values, saving each as PNG.
// Tags: barcode, planet, csv, batch, png, generation, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Program that reads numeric values from a CSV file and generates Planet barcodes saved as PNG images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a temporary output folder, writes a sample CSV, reads each line,
    /// generates a Planet barcode, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for output
        string outputFolder = Path.Combine(Path.GetTempPath(), "PlanetBarcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Prepare a sample CSV file with numeric values
        string csvPath = Path.Combine(outputFolder, "values.csv");
        string[] sampleValues = new string[] { "123456", "987654321", "55555555", "000001", "24680" };
        File.WriteAllLines(csvPath, sampleValues);

        // Verify that the CSV file exists before attempting to read it
        if (!File.Exists(csvPath))
        {
            Console.WriteLine("CSV file not found: " + csvPath);
            return;
        }

        // Read all lines (numeric values) from the CSV file
        string[] lines = File.ReadAllLines(csvPath);
        foreach (string line in lines)
        {
            // Trim whitespace and skip empty lines
            string codeText = line.Trim();
            if (string.IsNullOrEmpty(codeText))
                continue;

            // Generate Planet barcode for the numeric value
            using (var generator = new BarcodeGenerator(EncodeTypes.Planet, codeText))
            {
                // Optional: set module size for better visibility
                generator.Parameters.Barcode.XDimension.Pixels = 4f;

                // Build the output file path and save the barcode as PNG
                string outputPath = Path.Combine(outputFolder, $"Planet_{codeText}.png");
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Saved barcode for '{codeText}' to '{outputPath}'");
            }
        }

        Console.WriteLine("Barcode generation completed. Output folder: " + outputFolder);
    }
}