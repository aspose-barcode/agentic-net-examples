// Title: Hide Captions for Batch of Code128 Barcodes
// Description: Demonstrates how to generate multiple Code128 barcodes while globally disabling their captions.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator and its Parameters.Caption* properties to control caption visibility. Developers often need to produce clean barcode images without human‑readable text for scanning applications, packaging, or UI display. The snippet shows typical batch processing, directory handling, and saving PNG files, useful for quick integration in .NET projects.
// Prompt: Hide all captions for a batch of Code128 barcodes by setting CaptionParameters.Visible to false globally.
// Tags: code128, barcode generation, hide captions, batch processing, aspnet, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch generation of Code128 barcodes with captions hidden.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a set of Code128 barcodes, disables both above and below captions, and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Define the output directory for generated barcode images
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputDir))
        {
            // Create the directory if it does not already exist
            Directory.CreateDirectory(outputDir);
        }

        // Sample data for a batch of Code128 barcodes
        string[] codes = new string[]
        {
            "ABC123",
            "DEF456",
            "GHI789",
            "JKL012",
            "MNO345"
        };

        // Iterate over each code, generate a barcode, hide captions, and save the image
        for (int i = 0; i < codes.Length; i++)
        {
            string codeText = codes[i];
            string filePath = Path.Combine(outputDir, $"Code128_{i + 1}.png");

            // Use a using block to ensure proper disposal of the generator
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Hide both above and below captions globally for this generator
                generator.Parameters.CaptionAbove.Visible = false;
                generator.Parameters.CaptionBelow.Visible = false;

                // Save the barcode image to the specified file path
                generator.Save(filePath);
            }

            // Inform the user that the barcode has been saved
            Console.WriteLine($"Saved barcode {i + 1} to: {filePath}");
        }

        // Final status message
        Console.WriteLine("All barcodes generated successfully.");
    }
}