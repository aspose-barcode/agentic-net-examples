// Title: Left-align text for multiple UPC‑A barcodes
// Description: Demonstrates generating a series of UPC‑A barcodes with the human‑readable text aligned to the left side of each barcode image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator, EncodeTypes, and TextAlignment to create barcodes. Typical use cases include batch creation of product barcodes where label layout requires left‑aligned text. Developers often need to control text positioning, image format, and output folders when automating barcode production.
// Prompt: Align barcode text left for a series of UPC‑A barcodes by setting TextAlignment.Left.
// Tags: upc-a, barcode, textalignment, left, generation, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Generates a set of UPC‑A barcodes with left‑aligned human‑readable text and saves them as PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary output folder, iterates over sample UPC‑A codes,
    /// generates each barcode with left‑aligned text, and writes the resulting PNG files to disk.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for output images
        string outputFolder = Path.Combine(Path.GetTempPath(), "UPCSeries_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Sample UPC‑A codes (12 digits each)
        string[] upcCodes = new string[]
        {
            "012345678905",
            "123456789012",
            "036000291452",
            "042100005264",
            "070123456789"
        };

        // Process each UPC‑A code
        foreach (string code in upcCodes)
        {
            // Generate UPC‑A barcode with left‑aligned text
            using (var generator = new BarcodeGenerator(EncodeTypes.UPCA, code))
            {
                // Set the text alignment to left
                generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Left;

                // Build the full file path for the PNG image
                string filePath = Path.Combine(outputFolder, $"UPC_{code}.png");

                // Save the barcode image in PNG format
                generator.Save(filePath, BarCodeImageFormat.Png);

                // Inform the user about the saved file
                Console.WriteLine($"Saved barcode for {code} to {filePath}");
            }
        }

        // Indicate that all barcodes have been generated
        Console.WriteLine("Barcode generation completed.");
    }
}