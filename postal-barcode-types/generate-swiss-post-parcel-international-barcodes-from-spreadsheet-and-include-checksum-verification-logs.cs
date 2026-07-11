// Title: Swiss Post Parcel Barcode Generation and Checksum Validation
// Description: Generates Swiss Post Parcel barcodes from sample data and demonstrates checksum validation during recognition.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to use EncodeTypes.SwissPostParcel for barcode creation and DecodeType.SwissPostParcel for reading. It highlights typical use cases such as parcel tracking where checksum verification ensures data integrity. Developers often need to generate barcodes, save them as images, and validate them during scanning, making this a common pattern in logistics applications.
// Prompt: Generate Swiss Post Parcel international barcodes from a spreadsheet and include checksum verification logs.
// Tags: barcode symbology, generation, recognition, checksum, png, aspose.barcode, encode types, decode types

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode; // for EncodeTypes

/// <summary>
/// Demonstrates creating Swiss Post Parcel barcodes from a list of parcel codes,
/// saving them as PNG images, and performing recognition with checksum validation
/// both enabled and disabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes, saves them, and logs
    /// recognition results with checksum validation toggled.
    /// </summary>
    static void Main()
    {
        // Sample data representing rows from a spreadsheet (e.g., parcel IDs)
        var parcelCodes = new List<string>
        {
            "1234567890123",
            "9876543210987",
            "5555555555555",
            "1111111111111",
            "2222222222222"
        };

        // Directory to store generated barcode images
        string outputDir = "Barcodes";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Generate Swiss Post Parcel barcodes and save them as PNG files
        for (int i = 0; i < parcelCodes.Count; i++)
        {
            string code = parcelCodes[i];
            string filePath = Path.Combine(outputDir, $"SwissPost_{i + 1}.png");

            using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, code))
            {
                // Set image dimensions (optional)
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 150f;

                // Save the barcode image
                generator.Save(filePath, BarCodeImageFormat.Png);
                Console.WriteLine($"Generated barcode for '{code}' -> {filePath}");
            }
        }

        Console.WriteLine();
        Console.WriteLine("=== Barcode Recognition with Checksum Validation (On) ===");

        // Recognize each barcode with checksum validation enabled
        foreach (var file in Directory.GetFiles(outputDir, "*.png"))
        {
            using (var reader = new BarCodeReader(file, DecodeType.SwissPostParcel))
            {
                // Enable checksum validation
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(file)}");
                    Console.WriteLine($"  Detected CodeText: {result.CodeText}");
                    Console.WriteLine($"  Confidence: {result.Confidence}");
                    Console.WriteLine($"  ReadingQuality: {result.ReadingQuality}");
                }
            }
        }

        Console.WriteLine();
        Console.WriteLine("=== Barcode Recognition with Checksum Validation (Off) ===");

        // Recognize each barcode with checksum validation disabled
        foreach (var file in Directory.GetFiles(outputDir, "*.png"))
        {
            using (var reader = new BarCodeReader(file, DecodeType.SwissPostParcel))
            {
                // Disable checksum validation
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;

                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(file)}");
                    Console.WriteLine($"  Detected CodeText: {result.CodeText}");
                    Console.WriteLine($"  Confidence: {result.Confidence}");
                    Console.WriteLine($"  ReadingQuality: {result.ReadingQuality}");
                }
            }
        }
    }
}