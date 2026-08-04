// Title: Display Human‑Readable Text for Code128 Barcodes
// Description: Generates several Code128 barcodes and makes the main barcode text visible below each barcode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, demonstrating how to configure CodeTextParameters (Location, Alignment, Visibility) for human‑readable output. Typical use cases include creating printable labels, inventory tags, or shipping documents where the encoded data must also be shown as text. Developers often need to adjust text placement and styling when working with BarcodeGenerator and related API classes such as Parameters, Barcode, and CodeTextParameters.
/// <summary>
/// Demonstrates how to generate Code128 barcodes with visible human‑readable text using Aspose.BarCode.
/// </summary>
using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Main program class.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates Code128 barcodes, shows the barcode text below each image, and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Define sample texts to encode.
        string[] texts = { "ABC123", "1234567890", "CODE128" };

        // Prepare output directory.
        string outputDir = "Barcodes";
        Directory.CreateDirectory(outputDir);

        // Iterate over each text, generate a barcode, and save it.
        for (int i = 0; i < texts.Length; i++)
        {
            string text = texts[i];
            string filePath = Path.Combine(outputDir, $"code128_{i + 1}.png");

            // Create a BarcodeGenerator for Code128 with the current text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                // Show human‑readable text below the barcode.
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
                // Center the text horizontally.
                generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

                // Save the barcode image as PNG.
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Inform the user about the saved file.
            Console.WriteLine($"Saved barcode for '{text}' to '{filePath}'.");
        }
    }
}