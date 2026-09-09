// Title: Generate Planet Barcodes from CSV Values and Save as PNG
// Description: Demonstrates how to read a list of numeric values from a CSV string, generate Planet symbology barcodes for each value using Aspose.BarCode, and save the images as PNG files.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of the BarcodeGenerator class with EncodeTypes.Planet. Typical use cases include batch creation of barcodes from data sources such as CSV files, databases, or spreadsheets, where each record is encoded as a separate image. Developers often need to configure barcode parameters like X‑Dimension and output format, then save each barcode to a file system location.
// Prompt: Generate a batch of Planet barcodes from a CSV list of numeric values, saving each as PNG.
// Tags: planet barcode,csv processing,barcode generation,png output,aspose.barcode,encode types,barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Program that generates Planet barcodes from a CSV list of numeric values and saves each as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that performs the barcode generation process.
    /// </summary>
    static void Main()
    {
        // Sample CSV data containing numeric values to encode.
        string csvData = "123456,789012,345678,901234,567890";

        // Split the CSV string into individual values, removing any empty entries.
        string[] values = csvData.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        // Create a unique temporary folder for the generated barcode images.
        string outputFolder = Path.Combine(Path.GetTempPath(), "PlanetBarcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        Console.WriteLine("Generating Planet barcodes in: " + outputFolder);

        // Iterate over each numeric value and generate a corresponding barcode.
        for (int i = 0; i < values.Length; i++)
        {
            // Trim whitespace from the current value.
            string codeText = values[i].Trim();

            // Validate that the value is a non‑empty numeric string.
            if (string.IsNullOrEmpty(codeText) || !long.TryParse(codeText, out _))
            {
                Console.WriteLine($"Skipping invalid numeric value: '{codeText}'");
                continue;
            }

            // Build the full file path for the PNG image.
            string filePath = Path.Combine(outputFolder, $"Planet_{codeText}.png");

            // Initialize the barcode generator with Planet symbology and the current value.
            using (var generator = new BarcodeGenerator(EncodeTypes.Planet, codeText))
            {
                // Optional: set the module (X‑Dimension) size in pixels for better readability.
                generator.Parameters.Barcode.XDimension.Pixels = 4f;

                // Save the generated barcode as a PNG file.
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Saved barcode for '{codeText}' to '{filePath}'");
        }

        Console.WriteLine("Barcode generation completed.");
    }
}