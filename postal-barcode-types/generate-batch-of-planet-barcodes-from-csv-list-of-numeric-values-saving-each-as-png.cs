// Title: Generate Planet Barcodes from CSV and Save as PNG
// Description: Demonstrates reading numeric values from a CSV file and creating Planet symbology barcodes, saving each barcode as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode batch generation category. It showcases the use of BarcodeGenerator with EncodeTypes.Planet, configuring barcode parameters, and saving images via BarCodeImageFormat. Typical scenarios include bulk barcode creation from data sources such as CSV, databases, or APIs, where developers need to automate image output for inventory, tracking, or labeling purposes.
// Prompt: Generate a batch of Planet barcodes from a CSV list of numeric values, saving each as PNG.
// Tags: planet, barcode, generation, csv, png, aspose.barcode, batch

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that reads numeric values from a temporary CSV file,
/// generates a Planet barcode for each value, and saves the barcodes as PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Performs the CSV read, barcode generation,
    /// and cleanup of temporary resources.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for output barcodes
        string outputFolder = Path.Combine(Path.GetTempPath(), "PlanetBarcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Create a temporary CSV file with sample numeric values
        string csvPath = Path.Combine(Path.GetTempPath(), "PlanetValues_" + Guid.NewGuid().ToString("N") + ".csv");
        string[] sampleValues = { "123456", "987654321", "5555555", "000001", "123" };
        File.WriteAllLines(csvPath, sampleValues);

        // Read numeric values from CSV
        List<string> values = new List<string>();
        if (File.Exists(csvPath))
        {
            foreach (string line in File.ReadAllLines(csvPath))
            {
                string trimmed = line.Trim();
                if (!string.IsNullOrEmpty(trimmed))
                {
                    values.Add(trimmed);
                }
            }
        }
        else
        {
            Console.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        // Generate Planet barcode for each value
        foreach (string codeText in values)
        {
            try
            {
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Planet, codeText))
                {
                    // Optional: set module size (pixel width of a single barcode element)
                    generator.Parameters.Barcode.XDimension.Pixels = 4;

                    // Build output file path and save as PNG
                    string fileName = $"Planet_{codeText}.png";
                    string filePath = Path.Combine(outputFolder, fileName);
                    generator.Save(filePath, BarCodeImageFormat.Png);

                    Console.WriteLine($"Saved barcode for '{codeText}' to '{filePath}'");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to generate barcode for '{codeText}': {ex.Message}");
            }
        }

        // Cleanup temporary CSV (optional)
        try
        {
            File.Delete(csvPath);
        }
        catch
        {
            // Ignore cleanup errors
        }

        Console.WriteLine("Barcode generation completed.");
    }
}