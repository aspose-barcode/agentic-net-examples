// Title: Center-align text for EAN‑13 barcodes using Aspose.BarCode
// Description: Demonstrates how to generate a set of EAN‑13 barcodes with the human‑readable text centered beneath each barcode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize barcode appearance using the BarcodeGenerator and its Parameters properties. It shows typical usage of EncodeTypes, CodeTextParameters, and image export for common scenarios such as product labeling and inventory systems. Developers often need to adjust text alignment, font, and output format when creating barcodes programmatically.
// Prompt: Center-align barcode text for a collection of EAN‑13 barcodes by setting CodetextParameters.Alignment to TextAlignment.Center.
// Tags: ean-13, barcode, text-alignment, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates multiple EAN‑13 barcodes with centered human‑readable text and saves them as PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates an output folder, iterates over sample codes,
    /// configures text alignment, and saves each barcode image.
    /// </summary>
    static void Main()
    {
        // Define the output folder for generated barcode images
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputFolder))
        {
            // Create the folder if it does not exist
            Directory.CreateDirectory(outputFolder);
        }

        // Sample EAN‑13 codes (12 digits; checksum will be calculated automatically)
        string[] ean13Codes = new[]
        {
            "123456789012",
            "987654321098",
            "555555555555",
            "111111111111",
            "222222222222"
        };

        // Generate a barcode for each sample code
        for (int i = 0; i < ean13Codes.Length; i++)
        {
            // Initialize the barcode generator with EAN‑13 symbology and the current code text
            using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, ean13Codes[i]))
            {
                // Center‑align the human‑readable text beneath the barcode
                generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

                // Build the file path for the PNG image
                string filePath = Path.Combine(outputFolder, $"EAN13_{i + 1}.png");

                // Save the barcode image to disk
                generator.Save(filePath);

                // Inform the user about the saved file
                Console.WriteLine($"Saved barcode to: {filePath}");
            }
        }
    }
}