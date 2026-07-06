// Title: Generate batch of Aztec barcodes with custom text spacing
// Description: Demonstrates setting the human‑readable text spacing to 2.5 pixels for each Aztec barcode in a batch, then saving them as PNG images.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode appearance using BarcodeGenerator, EncodeTypes, and CodeTextParameters. Typical use cases include creating multiple barcodes with consistent visual settings for inventory, ticketing, or packaging applications. Developers often need to adjust text spacing, size, or font to meet branding or readability requirements.
// Prompt: Set barcode text spacing to 2.5 pixels for a batch of Aztec barcodes to improve readability.
// Tags: aztec, barcode, text spacing, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a batch of Aztec barcodes with custom text spacing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates Aztec barcodes from a list of code texts, sets text spacing to 2.5 pixels, and saves each as a PNG file.
    /// </summary>
    static void Main()
    {
        // Sample codetexts for the batch
        string[] codetexts = { "ABC123", "XYZ789", "HELLO", "WORLD", "AZTEC" };

        // Ensure the output directory exists
        string outputDir = "AztecBarcodes";
        Directory.CreateDirectory(outputDir);

        // Iterate over each codetext and generate a barcode
        for (int i = 0; i < codetexts.Length; i++)
        {
            // Create a generator for the current codetext using Aztec symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.Aztec, codetexts[i]))
            {
                // Set human‑readable text spacing to 2.5 pixels
                generator.Parameters.Barcode.CodeTextParameters.Space.Point = 2.5f;

                // Build the file path for the PNG output
                string filePath = Path.Combine(outputDir, $"Aztec_{i + 1}.png");

                // Save the barcode image in PNG format
                generator.Save(filePath, BarCodeImageFormat.Png);

                // Log the saved file location
                Console.WriteLine($"Saved: {filePath}");
            }
        }
    }
}