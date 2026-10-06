// Title: Generate Code11 barcode and read with checksum validation disabled
// Description: Demonstrates generating a Code11 barcode using BarcodeGenerator and then reading it with checksum validation turned off to illustrate false‑positive detection.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the core API classes BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Typical use cases include producing barcode images for inventory, shipping, or labeling and validating them during scanning. Developers often need to control checksum validation to handle legacy or non‑standard barcodes, making this pattern useful for troubleshooting and custom validation scenarios.
// Prompt: Generate a barcode with BarcodeGenerator, then read it using ChecksumValidation.Off to observe false positive detections.
// Tags: code11, barcode, generation, recognition, checksumvalidation, off, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation and reading with checksum validation disabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code11 barcode, reads it with checksum validation turned off,
    /// and outputs the decoded information to the console.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary folder for the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "ChecksumDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "code11.png");

        // Generate a Code11 barcode (intentionally using a code that may have an incorrect checksum)
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code11, "123456"))
        {
            // Set the X-dimension (module width) to 2 pixels for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            // Save the barcode image as PNG
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify the file was created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to generate barcode image.");
            return;
        }

        // Read the barcode with checksum validation turned OFF (allows false positives)
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.Code11))
        {
            // Disable checksum validation
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;

            Console.WriteLine("Reading barcode with ChecksumValidation.Off:");
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"1D Value: {result.Extended.OneD.Value}");
                Console.WriteLine($"1D CheckSum: {result.Extended.OneD.CheckSum}");
            }
        }

        // Clean up temporary files
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }
}