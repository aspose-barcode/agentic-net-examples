// Title: Center-align EAN‑13 barcode text
// Description: Demonstrates generating EAN‑13 barcodes with the human‑readable text centered beneath the bars.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator, CodetextParameters, and TextAlignment to control the appearance of human‑readable text. Typical use cases include creating printable product labels where the text must be centered for aesthetic or regulatory reasons. Developers often need to adjust text alignment, font, and positioning when producing barcodes for packaging, inventory, or point‑of‑sale systems.
// Prompt: Center-align barcode text for a collection of EAN‑13 barcodes by setting CodetextParameters.Alignment to TextAlignment.Center.
// Tags: ean13, barcode, text alignment, center, aspose.barcode, png, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a set of EAN‑13 barcodes and saves them as PNG images with centered human‑readable text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Iterates over a list of sample codes, creates a barcode for each,
    /// centers the text, and writes the image to the output folder.
    /// </summary>
    static void Main()
    {
        // Sample EAN‑13 codes (12 digits; checksum will be calculated automatically)
        string[] ean13Codes = new string[]
        {
            "123456789012",
            "987654321098",
            "400638133393",
            "590123412345",
            "735135371234"
        };

        // Ensure the output directory exists
        string outputDir = "Barcodes";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        int index = 1;
        foreach (var code in ean13Codes)
        {
            // Validate that the code length is either 12 or 13 digits
            if (code.Length < 12 || code.Length > 13)
            {
                Console.WriteLine($"Skipping invalid code '{code}'. Must be 12 or 13 digits.");
                continue;
            }

            // Create a barcode generator for the current EAN‑13 code
            using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, code))
            {
                // Center‑align the human‑readable text beneath the barcode
                generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

                // Build the full file path for the PNG image
                string filePath = Path.Combine(outputDir, $"ean13_{index}.png");

                // Save the barcode image in PNG format
                generator.Save(filePath, BarCodeImageFormat.Png);
                Console.WriteLine($"Saved barcode to {filePath}");
            }

            index++;
        }
    }
}