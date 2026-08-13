// Title: Validate Code 39 barcodes with optional checksum using BarCodeReader
// Description: Demonstrates generating Code 39 barcode images, some with checksum enabled, and then reading them back while validating optional checksums.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating Code 39 images and BarCodeReader with ChecksumValidation.On to verify optional checksums. Developers often need to batch‑process barcode images, ensure data integrity, and handle checksum validation in scanning workflows.
// Prompt: Process a folder of Code 39 images using BarCodeReader with ChecksumValidation.On to validate optional checksums.
// Tags: code39, checksum validation, barcode generation, barcode recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates Code 39 barcode images (including one with checksum enabled)
/// and then reads all images in a folder while validating optional checksums.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, saves them to a folder, and processes the folder
    /// using BarCodeReader with checksum validation turned on.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a folder for sample barcode images
        // --------------------------------------------------------------------
        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        Directory.CreateDirectory(folderPath);

        // Sample Code39 texts (one with checksum enabled)
        string[] sampleTexts = { "CODE39", "CODE39CHK", "123ABC" };

        // --------------------------------------------------------------------
        // Generate sample barcode images
        // --------------------------------------------------------------------
        foreach (string text in sampleTexts)
        {
            string imagePath = Path.Combine(folderPath, $"{text}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39, text))
            {
                // Enable checksum for the second sample (CODE39CHK)
                if (text == "CODE39CHK")
                {
                    generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
                }

                // Save the generated barcode image to disk
                generator.Save(imagePath);
                Console.WriteLine($"Generated barcode: {imagePath}");
            }
        }

        // --------------------------------------------------------------------
        // Process all PNG images in the folder using BarCodeReader with checksum validation
        // --------------------------------------------------------------------
        string[] imageFiles = Directory.GetFiles(folderPath, "*.png");
        foreach (string file in imageFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            using (var reader = new BarCodeReader(file, DecodeType.Code39))
            {
                // Turn on validation of optional checksums
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                // Read and output each detected barcode
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(file)} - Detected CodeText: {result.CodeText}");
                }
            }
        }
    }
}