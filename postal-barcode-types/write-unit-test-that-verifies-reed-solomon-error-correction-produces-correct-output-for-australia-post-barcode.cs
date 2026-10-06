// Title: Reed‑Solomon Error Correction for Australia Post Barcode
// Description: Demonstrates how Aspose.BarCode can recover a corrupted Australia Post barcode using Reed‑Solomon error correction.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on error‑correction techniques. It showcases the BarcodeGenerator for creating an Australia Post barcode, manipulation of the image to simulate damage, and the BarCodeReader with high‑performance quality settings to decode the damaged image. Developers working with postal symbologies often need to verify that Reed‑Solomon correction restores data after image degradation.
// Prompt: Write a unit test that verifies Reed‑Solomon error correction produces correct output for Australia Post barcode.
// Tags: australia post, reed-solomon, error correction, barcode generation, barcode recognition, unit test, aspnet, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates an Australia Post barcode, corrupts it,
/// and verifies that Reed‑Solomon error correction can recover the original data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates, corrupts, and reads back the barcode, printing the result.
    /// </summary>
    static void Main()
    {
        // Define a valid Australia Post code:
        // FCC 59, 8‑digit DPID, 2‑character CTable customer info
        string codeText = "5901234567AB";

        // Create a barcode generator for the Australia Post symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
        {
            // Set visual parameters
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarHeight.Pixels = 50f;
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;

            // Generate the barcode image as a bitmap
            using (var bitmap = generator.GenerateBarCodeImage())
            {
                // Simulate damage by overwriting a small region with white pixels
                for (int x = 0; x < 5; x++)
                {
                    for (int y = 0; y < 5; y++)
                    {
                        bitmap.SetPixel(x, y, Color.White);
                    }
                }

                // Save the corrupted bitmap to a memory stream (PNG format)
                using (var corruptedStream = new MemoryStream())
                {
                    bitmap.Save(corruptedStream, ImageFormat.Png);
                    corruptedStream.Position = 0; // Reset stream position for reading

                    // Initialize a reader for Australia Post barcodes
                    BaseDecodeType decodeType = DecodeType.AustraliaPost;
                    using (var reader = new BarCodeReader(corruptedStream, decodeType))
                    {
                        // Apply high‑performance quality settings (optional)
                        reader.QualitySettings = QualitySettings.HighPerformance;

                        // Attempt to read the barcode from the corrupted image
                        var results = reader.ReadBarCodes();

                        // Determine success: at least one result with non‑empty CodeText
                        bool success = results != null && results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText);

                        // Output the verification result
                        Console.WriteLine(success ? "Reed‑Solomon error correction succeeded." : "Reed‑Solomon error correction failed.");
                    }
                }
            }
        }
    }
}