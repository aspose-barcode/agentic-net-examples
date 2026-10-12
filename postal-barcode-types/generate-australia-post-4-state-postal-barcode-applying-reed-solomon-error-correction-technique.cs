// Title: Generate Australia Post 4‑state postal barcode with Reed‑Solomon error correction
// Description: Demonstrates creating an Australia Post 4‑state barcode using Aspose.BarCode, applying Reed‑Solomon error correction.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on postal symbologies. It showcases the use of BarcodeGenerator, EncodeTypes, and barcode parameters such as XDimension, BarHeight, and AustralianPost encoding settings. Developers often need to generate printable postal barcodes with error correction for mailing automation and integration with logistics systems.
// Prompt: Generate an Australia Post 4‑state postal barcode applying Reed‑Solomon error correction technique.
// Tags: barcode, australia post, 4-state, reed-solomon, error correction, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of an Australia Post 4‑state barcode with Reed‑Solomon error correction using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode image and saves it to a temporary folder.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for output
        string outputDir = Path.Combine(Path.GetTempPath(), "AustraliaPostDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define full path for the resulting PNG file
        string outputPath = Path.Combine(outputDir, "AustraliaPost.png");

        // Sample data to encode in the barcode
        string codeText = "6280123456ABCD";

        // Initialize the generator for Australia Post 4‑state symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
        {
            // Set barcode visual parameters
            generator.Parameters.Barcode.XDimension.Pixels = 4f;      // Width of the smallest bar element
            generator.Parameters.Barcode.BarHeight.Pixels = 50f;    // Height of the barcode

            // Configure encoding table for Australian Post (Reed‑Solomon error correction is applied internally)
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine("Barcode saved to: " + outputPath);
    }
}