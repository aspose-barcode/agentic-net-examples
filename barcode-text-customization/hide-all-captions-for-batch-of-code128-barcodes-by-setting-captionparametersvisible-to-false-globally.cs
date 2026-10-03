// Title: Hide Captions for a Batch of Code128 Barcodes
// Description: This example generates several Code128 barcodes and disables both the above and below caption text, producing clean barcode images.
// Category-Description: Demonstrates Aspose.BarCode barcode generation with global caption settings. It shows how to configure CaptionParameters to hide captions while creating multiple barcodes, a common requirement for clean visual output in inventory, shipping, or retail applications. The example uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes.
// Prompt: Hide all captions for a batch of Code128 barcodes by setting CaptionParameters.Visible to false globally.
// Tags: code128, barcode, caption, hide, generation, png, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a batch of Code128 barcodes and hides all caption text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates temporary folder, generates barcodes, hides captions, and lists output files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the generated barcode images.
        string tempDir = Path.Combine(Path.GetTempPath(), "Code128Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // List of text values to encode into Code128 barcodes.
        List<string> texts = new List<string> { "ABC123", "1234567890", "CODE128", "HELLO", "WORLD" };
        // Collection to store the full paths of generated image files.
        List<string> generatedFiles = new List<string>();

        // Iterate over each text value, generate a barcode, hide captions, and save as PNG.
        foreach (string txt in texts)
        {
            string filePath = Path.Combine(tempDir, txt + ".png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, txt))
            {
                // Hide both above and below caption text for a clean barcode image.
                generator.Parameters.CaptionAbove.Visible = false;
                generator.Parameters.CaptionBelow.Visible = false;

                // Save the barcode image in PNG format.
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            generatedFiles.Add(filePath);
        }

        // Output the list of generated barcode file paths.
        Console.WriteLine("Generated Code128 barcodes with captions hidden:");
        foreach (string f in generatedFiles)
        {
            Console.WriteLine(f);
        }
    }
}