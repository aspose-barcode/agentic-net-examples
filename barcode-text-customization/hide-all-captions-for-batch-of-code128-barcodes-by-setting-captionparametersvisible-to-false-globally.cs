// Title: Hide Captions for Batch Code128 Barcodes
// Description: Demonstrates generating multiple Code128 barcodes while disabling both above and below captions.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure global caption settings using the CaptionParameters class. It shows typical batch processing of barcodes, a common task when creating large sets of images for inventory, shipping, or labeling systems. Developers often need to hide captions to produce clean barcode graphics without human‑readable text.
// Prompt: Hide all captions for a batch of Code128 barcodes by setting CaptionParameters.Visible to false globally.
// Tags: code128, barcode, caption, hide, batch, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a batch of Code128 barcodes with captions disabled and saves them as PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary folder, generates barcodes, and writes status messages to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch
        string batchFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Define sample Code128 texts to encode
        string[] codeTexts = new string[] { "CODE128A", "CODE128B", "CODE128C", "CODE128D", "CODE128E" };

        // Iterate over each text value and generate a barcode image
        foreach (string text in codeTexts)
        {
            // Build the full file path for the output PNG
            string filePath = Path.Combine(batchFolder, $"{text}.png");

            // Initialize the barcode generator with Code128 symbology and the current text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                // Ensure both above and below captions are hidden
                generator.Parameters.CaptionAbove.Visible = false;
                generator.Parameters.CaptionBelow.Visible = false;

                // Save the generated barcode as a PNG file
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Output the location of the generated barcode
            Console.WriteLine($"Generated barcode: {filePath}");
        }

        // Indicate that the batch processing has finished
        Console.WriteLine("Batch generation completed.");
    }
}