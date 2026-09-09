// Title: Generate Australia Post 4‑state postal barcode with Reed‑Solomon error correction
// Description: Demonstrates creating an Australia Post 4‑state postal barcode using Aspose.BarCode, applying Reed‑Solomon error correction.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on postal symbologies. It showcases the use of BarcodeGenerator, EncodeTypes, and barcode parameters such as XDimension, BarHeight, and AustralianPost encoding settings. Developers often need to generate compliant postal barcodes for mailing automation, requiring correct encoding tables and error‑correction techniques.
// Prompt: Generate an Australia Post 4‑state postal barcode applying Reed‑Solomon error correction technique.
// Tags: barcode, australia post, postal, reed-solomon, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates an Australia Post 4‑state postal barcode with Reed‑Solomon error correction using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates output folder, builds barcode text, configures generator, and saves PNG image.
    /// </summary>
    static void Main()
    {
        // Define and create the output directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated barcode image
        string outputPath = Path.Combine(outputDir, "AustraliaPost.png");

        // Sample Australia Post code: FCC 62, DPID 01234567, customer info "AB"
        // Format: <FCC><DPID><CustomerInfo>
        string codeText = "6201234567AB";

        // Initialize the barcode generator for Australia Post symbology
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
        {
            // Set the module (X) dimension in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Set the height of the barcode bars in pixels
            generator.Parameters.Barcode.BarHeight.Pixels = 50f;

            // Choose the encoding table for customer information (CTable) which includes Reed‑Solomon error correction
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Australia Post barcode saved to: {outputPath}");
    }
}