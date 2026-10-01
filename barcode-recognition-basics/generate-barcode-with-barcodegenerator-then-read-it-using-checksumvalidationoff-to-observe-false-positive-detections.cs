// Title: Generate and Read a Code128 Barcode with Checksum Validation Disabled
// Description: This example creates a Code128 barcode image using Aspose.BarCode's BarcodeGenerator, saves it as PNG, and then reads it back with checksum validation turned off to demonstrate handling of false positive detections.
// Category-Description: Demonstrates Aspose.BarCode barcode generation and recognition workflow, focusing on checksum validation settings. Shows how to use BarcodeGenerator, BarCodeReader, and BarcodeSettings.ChecksumValidation to control validation behavior during scanning. Useful for developers implementing custom barcode scanning where checksum errors need to be ignored.
// Prompt: Generate a barcode with BarcodeGenerator, then read it using ChecksumValidation.Off to observe false positive detections.
// Tags: code128, barcode generation, barcode recognition, checksumvalidation, off, aspnet, aspose.barcode, png, c#
using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation and reading with checksum validation disabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates a Code128 barcode, saves it as PNG,
    /// then reads it back with checksum validation turned off.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "code128.png");

        // Generate a Code128 barcode with the specified text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            // Save the generated barcode as a PNG image
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was successfully created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to generate the barcode image.");
            return;
        }

        // Set up the reader to decode Code128 barcodes from the saved image
        BaseDecodeType decodeType = DecodeType.Code128;
        using (var reader = new BarCodeReader(barcodePath, decodeType))
        {
            // Disable checksum validation to allow false positive detections
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;

            // Perform the barcode reading operation
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output the decoding results
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
            }
            else
            {
                foreach (var result in results)
                {
                    Console.WriteLine($"Detected CodeText: {result.CodeText}");
                    Console.WriteLine($"Detected CodeType: {result.CodeTypeName}");
                }
            }
        }

        // Clean up temporary files (optional)
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failure should not affect demo execution
        }
    }
}