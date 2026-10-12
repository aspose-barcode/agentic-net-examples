// Title: Generate and Validate a Code 128 Barcode with Checksum
// Description: Demonstrates how to create a Code 128 barcode with checksum enabled using Aspose.BarCode, save it as PNG, and then decode it to confirm the checksum passes.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, setting checksum options via Parameters.Barcode.IsChecksumEnabled, and BarCodeReader for decoding and validating checksum integrity. Developers working with barcode symbologies often need to ensure data integrity, making checksum validation a common requirement in inventory, shipping, and point‑of‑sale systems.
// Prompt: Generate a Code 128 barcode with checksum enabled, then decode it to validate checksum correctness.
// Tags: code128, checksum, barcode generation, barcode recognition, aspose.barcode, png, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Code 128 barcode with checksum enabled and decoding it to verify checksum correctness.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary folder, generates the barcode image, decodes it, and cleans up.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the barcode image file
        string barcodePath = Path.Combine(tempFolder, "code128.png");

        // Generate a Code128 barcode with checksum enabled and save it as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Aspose123"))
        {
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image file was successfully created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Decode the barcode and validate checksum (presence of a result implies checksum passed)
        BaseDecodeType decodeType = DecodeType.Code128;
        using (BarCodeReader reader = new BarCodeReader(barcodePath, decodeType))
        {
            // Ensure checksum validation is enabled (default behavior, set explicitly for clarity)
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            BarCodeResult[] results = reader.ReadBarCodes();
            if (results.Length > 0)
            {
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"Decoded Type: {result.CodeTypeName}");
                    Console.WriteLine($"Decoded Text: {result.CodeText}");
                }
                Console.WriteLine("Checksum validation succeeded.");
            }
            else
            {
                Console.WriteLine("No barcode detected or checksum validation failed.");
            }
        }

        // Clean up temporary files and folder
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup not critical for demo
        }
    }
}