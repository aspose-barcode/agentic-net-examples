// Title: Generate and Validate Code39 Barcode with Checksum via REST-like Simulation
// Description: Demonstrates generating a Code39 barcode PNG with optional checksum control and reading it back with checksum validation, mimicking a REST API endpoint.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the BarcodeGenerator and BarCodeReader classes for creating barcodes, applying checksum settings, and validating them during decoding. Typical use cases include server‑side barcode image creation for web services and automated verification of scanned barcodes in enterprise applications.
// Prompt: Create a REST API endpoint that receives barcode data, applies checksum control, and returns a PNG image.
// Tags: code39, checksum, png, generation, recognition, aspose.barcode, rest, api

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Simulates a REST API that generates a barcode image with checksum control,
/// returns the PNG bytes, and validates the barcode by reading it back.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that mimics handling a REST request and response.
    /// </summary>
    static void Main()
    {
        // Simulated REST request payload: barcode data and checksum flag
        string barcodeData = "123456";
        bool enableChecksum = true;

        // Generate PNG image bytes based on request parameters
        byte[] pngBytes = GenerateBarcodePng(barcodeData, enableChecksum);

        // Save to a temporary file for demonstration purposes
        string outputPath = Path.Combine(Path.GetTempPath(), "generated_barcode.png");
        File.WriteAllBytes(outputPath, pngBytes);
        Console.WriteLine($"Barcode image saved to: {outputPath}");

        // Simulated REST response: decode the image and validate checksum
        ReadBarcodeFromBytes(pngBytes);
    }

    /// <summary>
    /// Generates a Code39 barcode PNG image with optional checksum control.
    /// </summary>
    /// <param name="data">The text to encode in the barcode.</param>
    /// <param name="enableChecksum">True to enable checksum; otherwise false.</param>
    /// <returns>Byte array containing the PNG image.</returns>
    static byte[] GenerateBarcodePng(string data, bool enableChecksum)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            // Initialize the barcode generator for Code39 symbology
            using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.Code39, data))
            {
                // Apply checksum control based on the request flag
                gen.Parameters.Barcode.IsChecksumEnabled = enableChecksum ? EnableChecksum.Yes : EnableChecksum.No;
                gen.Parameters.Barcode.ChecksumAlwaysShow = true;

                // Save the generated barcode as PNG into the memory stream
                gen.Save(ms, BarCodeImageFormat.Png);
            }

            // Return the PNG image bytes to the caller
            return ms.ToArray();
        }
    }

    /// <summary>
    /// Reads a barcode from a PNG byte array and validates its checksum.
    /// </summary>
    /// <param name="imageBytes">PNG image containing the barcode.</param>
    static void ReadBarcodeFromBytes(byte[] imageBytes)
    {
        using (MemoryStream ms = new MemoryStream(imageBytes))
        {
            // Initialize the barcode reader for Code39 symbology
            using (BarCodeReader reader = new BarCodeReader(ms, DecodeType.Code39))
            {
                // Enable checksum validation during decoding
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                // Iterate through all detected barcodes (typically one)
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Decoded Type: {result.CodeTypeName}");
                    Console.WriteLine($"Decoded Text: {result.CodeText}");
                    Console.WriteLine($"Checksum Value: {result.Extended.OneD.CheckSum}");
                }
            }
        }
    }
}