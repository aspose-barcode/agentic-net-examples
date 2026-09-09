// Title: Generate Dutch KIX barcodes from JSON identifiers and save as BMP files
// Description: This example reads a JSON array of identifier strings, creates a Dutch KIX barcode for each identifier using Aspose.BarCode, and saves the barcodes as BMP images.
// Category-Description: Demonstrates Aspose.BarCode barcode generation for the Dutch KIX symbology. It shows how to deserialize JSON data, configure barcode parameters, and export images in BMP format. Useful for developers needing to batch‑create KIX barcodes for inventory, shipping, or retail applications.
// Prompt: Generate Dutch KIX barcodes using a JSON array of identifiers and export each as BMP images.
// Tags: barcode, dutchkix, json, bmp, aspose.barcode, generation, c#

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Program that generates Dutch KIX barcodes from a JSON array and saves them as BMP images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Parses JSON identifiers, creates barcodes, and writes BMP files to a temporary folder.
    /// </summary>
    static void Main()
    {
        // Sample JSON array of identifiers
        string json = "[\"123456ASPOSE\",\"ABC123\",\"9876543210\"]";

        // Deserialize JSON into a list of strings
        List<string> identifiers;
        try
        {
            identifiers = JsonSerializer.Deserialize<List<string>>(json);
            if (identifiers == null)
                throw new ArgumentException("JSON does not contain a valid array.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to parse JSON: {ex.Message}");
            return;
        }

        // Create a unique temporary output folder
        string outputFolder = Path.Combine(Path.GetTempPath(), "DutchKIX_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Iterate over each identifier and generate a barcode
        for (int i = 0; i < identifiers.Count; i++)
        {
            string codeText = identifiers[i];
            if (string.IsNullOrWhiteSpace(codeText))
            {
                Console.WriteLine($"Skipping empty identifier at index {i}.");
                continue;
            }

            // Build a safe file name for the BMP image
            string safeFileName = $"{i + 1}_{codeText}.bmp";
            foreach (char c in Path.GetInvalidFileNameChars())
                safeFileName = safeFileName.Replace(c, '_');

            string filePath = Path.Combine(outputFolder, safeFileName);

            try
            {
                // Initialize the barcode generator for Dutch KIX symbology
                using (var generator = new BarcodeGenerator(EncodeTypes.DutchKIX, codeText))
                {
                    // Optional: adjust appearance if needed
                    generator.Parameters.Barcode.XDimension.Pixels = 4;
                    generator.Parameters.Barcode.BarHeight.Pixels = 50;

                    // Save the generated barcode as a BMP image
                    generator.Save(filePath, BarCodeImageFormat.Bmp);
                }

                Console.WriteLine($"Generated barcode for '{codeText}' at: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating barcode for '{codeText}': {ex.Message}");
            }
        }

        Console.WriteLine($"All barcodes saved to: {outputFolder}");
    }
}