// Title: Center-aligned text for multiple EAN‑13 barcodes
// Description: Demonstrates how to generate a set of EAN‑13 barcodes with the human‑readable text centered beneath each barcode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and CodetextParameters to control barcode appearance. Developers often need to customize text alignment, size, and image format when creating barcodes for packaging, inventory, or retail applications. The snippet shows typical steps: setting dimensions, aligning code text, and saving images.
// Prompt: Center-align barcode text for a collection of EAN‑13 barcodes by setting CodetextParameters.Alignment to TextAlignment.Center.
// Tags: ean-13, barcode, text alignment, generation, png, aspose.barcode, codetextparameters

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates EAN‑13 barcodes with centered code text and saves them as PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates output directory, iterates over sample EAN‑13 codes, configures barcode parameters, and saves each image.
    /// </summary>
    static void Main()
    {
        // Determine the output directory and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        Directory.CreateDirectory(outputDir);

        // List of sample EAN‑13 codes to be encoded
        List<string> eanCodes = new List<string>
        {
            "1234567890128",
            "4006381333931",
            "5901234123457",
            "9780306406157",
            "7351353712345"
        };

        // Generate a barcode for each code in the collection
        foreach (string code in eanCodes)
        {
            // Initialize the generator with EAN‑13 symbology and the current code
            using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, code))
            {
                // Set the X‑dimension (module width) in pixels
                generator.Parameters.Barcode.XDimension.Pixels = 2;

                // Center-align the human‑readable text beneath the barcode
                generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

                // Build the full file path for the PNG image
                string filePath = Path.Combine(outputDir, $"EAN13_{code}.png");

                // Save the generated barcode image to disk
                generator.Save(filePath, BarCodeImageFormat.Png);

                // Output a confirmation message to the console
                Console.WriteLine($"Saved barcode for {code} to {filePath}");
            }
        }

        // Indicate that all barcodes have been generated
        Console.WriteLine("Barcode generation completed.");
    }
}