// Title: Generate Australia Post 4‑state postal barcode with Reed‑Solomon error correction
// Description: Demonstrates how to create an Australia Post 4‑state postal barcode using Aspose.BarCode, applying Reed‑Solomon error correction.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation collection, showcasing the use of BarcodeGenerator with EncodeTypes.AustraliaPost. It covers configuring barcode dimensions, selecting the CTable encoding, and exporting the result as a PNG image. Developers dealing with postal symbologies, error‑correction techniques, and image output will find this pattern useful for quick integration.
// Prompt: Generate an Australia Post 4‑state postal barcode applying Reed‑Solomon error correction technique.
// Tags: australia post, barcode generation, png, aspose.barcode, aspose.barcode.generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates an Australia Post 4‑state postal barcode and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates the output folder, builds the barcode, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Prepare output directory in the system temporary folder
        string outputDir = Path.Combine(Path.GetTempPath(), "AustraliaPostBarcodeDemo");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Sample Australia Post code text:
        // FCC = 62 (allows up to 5 CTable characters), DPID = 80123456, Customer Info = "AB"
        string codeText = "6280123456AB";

        // Initialize the barcode generator for Australia Post symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
        {
            // Configure basic appearance: module size and bar height
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarHeight.Pixels = 50f;

            // Choose the CTable encoding which supports letters and digits
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;

            // Define the full path for the PNG output file
            string outputPath = Path.Combine(outputDir, "AustraliaPost.png");

            // Save the generated barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Inform the user where the file was saved
            Console.WriteLine($"Australia Post barcode saved to: {outputPath}");
        }
    }
}