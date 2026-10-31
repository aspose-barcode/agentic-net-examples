// Title: Read Mixed‑Symbology Barcodes with Default Checksum Validation
// Description: Generates sample Code11, Code128, and QR barcodes, saves them to a temporary folder, and reads them back using Aspose.BarCode with ChecksumValidation.Default to enforce default checksum handling.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It demonstrates using BarcodeGenerator to create barcodes and BarCodeReader with BarcodeSettings.ChecksumValidation to control checksum processing. Developers working with mixed‑symbology images often need to validate checksums while reading 1D barcodes; this snippet shows the typical API usage of BarcodeGenerator, BarCodeReader, and related settings, making it useful for search queries about barcode checksum validation in C#.
/// Prompt: Apply ChecksumValidation.Default to enforce default checksum handling when reading mixed‑symbology images.
/// Tags: barcode, symbology, checksumvalidation, default, mixed, generation, recognition, aspose.barcode, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating and reading mixed‑symbology barcodes while applying
/// <see cref="ChecksumValidation.Default"/> to control checksum handling.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample barcodes, reads them with default
    /// checksum validation, and outputs the results to the console.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "MixedChecksum_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define a collection of sample barcodes (encode type, text, file name, decode type)
        var samples = new List<(BaseEncodeType encode, string text, string fileName, BaseDecodeType decode)>
        {
            (EncodeTypes.Code11, "123456", "code11.png", DecodeType.Code11),
            (EncodeTypes.Code128, "ABC123", "code128.png", DecodeType.Code128),
            (EncodeTypes.QR, "https://example.com", "qr.png", DecodeType.QR)
        };

        // --------------------------------------------------------------------
        // Generate barcode images and save them as PNG files in the temp folder
        // --------------------------------------------------------------------
        foreach (var sample in samples)
        {
            string filePath = Path.Combine(tempFolder, sample.fileName);
            using (BarcodeGenerator generator = new BarcodeGenerator(sample.encode, sample.text))
            {
                // Set X‑dimension to 2 pixels for better readability
                generator.Parameters.Barcode.XDimension.Pixels = 2;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // ---------------------------------------------------------------
        // Read each barcode image using default checksum validation settings
        // ---------------------------------------------------------------
        foreach (var sample in samples)
        {
            string filePath = Path.Combine(tempFolder, sample.fileName);
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            using (BarCodeReader reader = new BarCodeReader(filePath, sample.decode))
            {
                // Enforce the default checksum handling behavior
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Default;

                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {sample.fileName}");
                    Console.WriteLine($"  CodeType: {result.CodeTypeName}");
                    Console.WriteLine($"  CodeText: {result.CodeText}");

                    // For 1D barcodes, display the raw value and checksum if available
                    if (result.Extended?.OneD != null)
                    {
                        Console.WriteLine($"  1D Value: {result.Extended.OneD.Value}");
                        Console.WriteLine($"  1D CheckSum: {result.Extended.OneD.CheckSum}");
                    }
                }
            }
        }

        // -------------------------------------------------
        // Cleanup: delete the temporary folder and its files
        // -------------------------------------------------
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore any errors that occur during cleanup
        }
    }
}