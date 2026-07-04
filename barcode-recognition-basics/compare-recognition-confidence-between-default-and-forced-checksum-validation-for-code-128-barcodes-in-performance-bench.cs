// Title: Code128 checksum validation confidence comparison
// Description: Demonstrates how to generate a Code 128 barcode, then reads it twice—once with default checksum validation and once with forced checksum validation—to compare the confidence values returned by the recognizer.
// Prompt: Compare recognition confidence between default and forced checksum validation for Code 128 barcodes in a performance benchmark.
// Tags: code128, checksum validation, confidence, performance benchmark, aspose.barcode, barcode generation, barcode recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Code128 barcode, then reads it using default and forced checksum validation
/// to illustrate the difference in recognition confidence values.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode image, verifies its creation, and performs two recognition passes:
    /// one with default checksum handling and another with checksum validation forced on.
    /// </summary>
    static void Main()
    {
        // Define the file name for the generated barcode image
        const string imagePath = "code128.png";

        // ------------------------------------------------------------
        // Generate a Code128 barcode with sample data and save it to disk
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Save the barcode image to the specified path
            generator.Save(imagePath);
        }

        // ------------------------------------------------------------
        // Verify that the image file was successfully created
        // ------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Barcode image '{imagePath}' was not created.");
            return;
        }

        // ------------------------------------------------------------
        // Read the barcode using default checksum validation (no explicit setting)
        // ------------------------------------------------------------
        using (var readerDefault = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            foreach (var result in readerDefault.ReadBarCodes())
            {
                Console.WriteLine($"Default Validation Confidence: {result.Confidence}");
            }
        }

        // ------------------------------------------------------------
        // Read the same barcode with forced checksum validation (ChecksumValidation.On)
        // ------------------------------------------------------------
        using (var readerForced = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Enable forced checksum validation
            readerForced.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            foreach (var result in readerForced.ReadBarCodes())
            {
                Console.WriteLine($"Forced Validation Confidence: {result.Confidence}");
            }
        }
    }
}