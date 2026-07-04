// Title: EAN13 Barcode Generation and Recognition with Checksum Validation Disabled
// Description: This example generates a valid EAN13 barcode image, then reads it with checksum validation turned off to demonstrate false‑positive detection.
// Prompt: Generate a barcode with BarcodeGenerator, then read it using ChecksumValidation.Off to observe false positive detections.
// Tags: ean13, barcode generation, barcode recognition, checksumvalidation, off, aspnet, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how to generate an EAN13 barcode and read it with checksum validation disabled,
/// which can lead to false‑positive detections.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode image, verifies its creation,
    /// and then reads it with <see cref="ChecksumValidation.Off"/> to show the effect on detection.
    /// </summary>
    static void Main()
    {
        // Path where the generated barcode image will be saved
        string barcodePath = "barcode.png";

        // ------------------------------------------------------------
        // Generate an EAN13 barcode with a valid code text (includes correct checksum)
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890128"))
        {
            // Save the generated barcode image to the specified file
            generator.Save(barcodePath);
        }

        // ------------------------------------------------------------
        // Verify that the barcode image was successfully created
        // ------------------------------------------------------------
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine($"Failed to create barcode image at '{barcodePath}'.");
            return;
        }

        // ------------------------------------------------------------
        // Read the barcode with checksum validation turned OFF (false positive detection)
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.EAN13))
        {
            // Disable checksum validation to allow detection of potentially invalid barcodes
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;

            // Perform recognition and iterate over all detected barcodes
            foreach (var result in reader.ReadBarCodes())
            {
                // Output the raw code text detected in the barcode
                Console.WriteLine($"Detected CodeText: {result.CodeText}");

                // Extended data for 1D barcodes: separate value and checksum components
                Console.WriteLine($"Value: {result.Extended.OneD.Value}");
                Console.WriteLine($"Checksum: {result.Extended.OneD.CheckSum}");

                // Additional quality metrics provided by the recognizer
                Console.WriteLine($"Confidence: {result.Confidence}");
                Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");
            }
        }
    }
}