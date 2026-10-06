// Title: Generate Dutch KIX Barcodes from JSON and Save as BMP
// Description: This example reads a JSON array of identifiers, creates a Dutch KIX barcode for each identifier using Aspose.BarCode, and saves the images as BMP files.
// Category-Description: Demonstrates Aspose.BarCode generation of Dutch KIX symbology. Shows how to deserialize JSON input, configure barcode parameters, and export images in BMP format. Useful for developers needing to batch‑create KIX barcodes for inventory, logistics, or retail applications.
// Prompt: Generate Dutch KIX barcodes using a JSON array of identifiers and export each as BMP images.
// Tags: barcode, dutchkix, json, bmp, aspose.barcode, generation, c#

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Sample program that generates Dutch KIX barcodes from a JSON array of identifiers
/// and saves each barcode as a BMP image using the Aspose.BarCode library.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Define a JSON array containing the identifiers to encode.
        // ------------------------------------------------------------
        string json = "[\"123456ASPOSE\",\"ABC123\",\"9876543210\"]";

        // ------------------------------------------------------------
        // Deserialize the JSON into a string array.
        // ------------------------------------------------------------
        string[] identifiers;
        try
        {
            identifiers = JsonSerializer.Deserialize<string[]>(json);
            if (identifiers == null)
            {
                Console.WriteLine("No identifiers found in JSON.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to parse JSON: {ex.Message}");
            return;
        }

        // ------------------------------------------------------------
        // Prepare the output directory for the generated BMP files.
        // ------------------------------------------------------------
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "DutchKIX_Barcodes");
        try
        {
            Directory.CreateDirectory(outputDir);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to create output directory: {ex.Message}");
            return;
        }

        // ------------------------------------------------------------
        // Iterate over each identifier, generate a barcode, and save it.
        // ------------------------------------------------------------
        foreach (string id in identifiers)
        {
            // Skip empty or whitespace identifiers.
            if (string.IsNullOrWhiteSpace(id))
                continue;

            // Build the full file path for the BMP image.
            string filePath = Path.Combine(outputDir, $"{id}_DutchKIX.bmp");

            try
            {
                // Create a barcode generator for Dutch KIX symbology.
                using (var generator = new BarcodeGenerator(EncodeTypes.DutchKIX, id))
                {
                    // Optional: adjust size parameters for better readability.
                    generator.Parameters.Barcode.XDimension.Pixels = 4f;
                    generator.Parameters.Barcode.BarHeight.Pixels = 50f;

                    // Save the generated barcode as a BMP image.
                    generator.Save(filePath, BarCodeImageFormat.Bmp);
                }

                Console.WriteLine($"Generated barcode for '{id}' at '{filePath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating barcode for '{id}': {ex.Message}");
            }
        }
    }
}