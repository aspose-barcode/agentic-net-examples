// Title: Generate Code128 Barcode PNG with Checksum
// Description: Demonstrates creating a Code128 barcode image with checksum enabled and returning it as a PNG byte array.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to configure checksum control, select image formats, and produce barcode images programmatically. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes, common in scenarios such as REST APIs, document processing, and inventory systems where developers need to generate barcodes on the fly.
// Prompt: Create a REST API endpoint that receives barcode data, applies checksum control, and returns a PNG image.
// Tags: code128, checksum, png, aspose.barcode, barcode-generation, rest-api

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode PNG with checksum enabled.
/// </summary>
class Program
{
    // Simulates a REST endpoint that receives barcode data,
    // enables checksum calculation, and returns a PNG image as a byte array.
    static byte[] GenerateBarcodePng(string codeText)
    {
        // Use Code128 symbology for this example.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Enable checksum calculation and make it visible in the human‑readable text.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;

            // Save the barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                return ms.ToArray(); // Return the PNG bytes.
            }
        }
    }

    /// <summary>
    /// Entry point that simulates a REST call, generates the barcode PNG, and saves it to a temporary file.
    /// </summary>
    static void Main()
    {
        // Sample request payload.
        string sampleData = "1234567890";

        // Call the simulated endpoint.
        byte[] pngBytes = GenerateBarcodePng(sampleData);

        // Write the PNG to a file so we can verify the output.
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode.png");
        File.WriteAllBytes(outputPath, pngBytes);

        Console.WriteLine($"Barcode PNG generated and saved to: {outputPath}");
        Console.WriteLine($"Byte size: {pngBytes.Length}");
    }
}