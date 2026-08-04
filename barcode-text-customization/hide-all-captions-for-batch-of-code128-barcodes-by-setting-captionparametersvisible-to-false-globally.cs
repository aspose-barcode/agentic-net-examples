// Title: Hide Captions for Multiple Code128 Barcodes
// Description: Demonstrates generating a batch of Code128 barcodes and disabling both above and below captions.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure global caption visibility using the BarcodeGenerator.Parameters.CaptionAbove and CaptionBelow properties. Typical use cases include creating clean barcode images for packaging, inventory, or point‑of‑sale systems where textual captions are not required. Developers often need to adjust these settings across many barcodes in a single run.
// Prompt: Hide all captions for a batch of Code128 barcodes by setting CaptionParameters.Visible to false globally.
// Tags: code128, barcode, hide-caption, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a series of Code128 barcodes with captions hidden and saves them as PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates an output folder, generates barcodes, and writes a completion message.
    /// </summary>
    static void Main()
    {
        // Determine and ensure the output directory exists
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Define the set of texts to encode as Code128 barcodes
        string[] codeTexts = new string[]
        {
            "ABC123",
            "9876543210",
            "CODE128TEST",
            "1234567890",
            "HELLOWORLD"
        };

        // Iterate over each text, generate a barcode, hide its captions, and save the image
        for (int i = 0; i < codeTexts.Length; i++)
        {
            string text = codeTexts[i];
            string filePath = Path.Combine(outputFolder, $"barcode_{i + 1}.png");

            // Create a barcode generator for Code128 with the current text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                // Hide both above and below captions globally
                generator.Parameters.CaptionAbove.Visible = false;
                generator.Parameters.CaptionBelow.Visible = false;

                // Save the generated barcode as a PNG file
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // Inform the user about the number of barcodes generated and their location
        Console.WriteLine($"Generated {codeTexts.Length} Code128 barcodes in '{outputFolder}'.");
    }
}