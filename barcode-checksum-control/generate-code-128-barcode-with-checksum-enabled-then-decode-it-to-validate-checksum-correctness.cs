// Title: Generate and validate a Code 128 barcode with checksum
// Description: Demonstrates creating a Code 128 barcode with checksum enabled using Aspose.BarCode, saving it as PNG, and then decoding it to verify the checksum.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for encoding and BarCodeReader for decoding, focusing on checksum handling. Developers working with barcode symbologies often need to ensure data integrity by enabling and validating checksums during generation and scanning.
// Prompt: Generate a Code 128 barcode with checksum enabled, then decode it to validate checksum correctness.
// Tags: code128, checksum, barcode generation, barcode recognition, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Code 128 barcode with checksum enabled and decoding it to verify correctness.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates, saves, decodes, and cleans up a Code 128 barcode.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the output file path and the text to encode
        string barcodePath = Path.Combine(tempFolder, "code128.png");
        string codeText = "Aspose123";

        // Generate a Code128 barcode with checksum enabled
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to: {barcodePath}");

        // Decode the generated barcode using the appropriate decode type
        BaseDecodeType decodeType = DecodeType.Code128;
        using (BarCodeReader reader = new BarCodeReader(barcodePath, decodeType))
        {
            // The reader validates checksum by default; no extra configuration required
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText))
            {
                Console.WriteLine("Barcode decoded successfully.");
                Console.WriteLine($"Detected Type: {results[0].CodeTypeName}");
                Console.WriteLine($"Decoded Text: {results[0].CodeText}");
            }
            else
            {
                Console.WriteLine("Failed to decode the barcode.");
            }
        }

        // Optional cleanup of temporary files and folder
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Suppress any errors during cleanup
        }
    }
}