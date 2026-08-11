// Title: Batch decode Swiss Post Parcel barcodes with checksum validation
// Description: Demonstrates how to read multiple Swiss Post Parcel barcodes from a folder (simulating a network share), validate checksums, and report mismatches.
// Category-Description: This example belongs to the barcode recognition and generation category of Aspose.BarCode for .NET. It showcases using BarCodeReader for batch decoding, configuring checksum validation, and using BarcodeGenerator to create sample barcodes. Developers working with bulk barcode processing, validation, and integration with file systems commonly use these APIs.
// Prompt: Perform batch decoding of Swiss Post Parcel international barcodes from a network share and handle checksum mismatches.
// Tags: swisspostparcel, batch decoding, checksum validation, barcodereader, barcodegenerator, console output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that batch‑processes Swiss Post Parcel barcodes from a folder,
/// validates their checksums, and outputs the results to the console.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Scans a folder for PNG images, reads Swiss Post Parcel barcodes,
    /// validates checksums, and reports successful reads or mismatches.
    /// </summary>
    static void Main()
    {
        // Define the folder that simulates a network share.
        // For the purpose of this self‑contained example we use a local folder.
        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");

        // Ensure the folder exists.
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // Seed a few sample Swiss Post Parcel barcodes if the folder is empty.
        SeedSampleBarcodes(folderPath);

        // Retrieve all PNG image files in the folder.
        string[] imageFiles = Directory.GetFiles(folderPath, "*.png");
        foreach (string imageFile in imageFiles)
        {
            // Verify the file exists before processing.
            if (!File.Exists(imageFile))
            {
                Console.WriteLine($"File not found: {imageFile}");
                continue;
            }

            // Create a reader configured for Swiss Post Parcel barcodes.
            using (BarCodeReader reader = new BarCodeReader(imageFile, DecodeType.SwissPostParcel))
            {
                // Enable checksum validation.
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                // Disallow recognition of barcodes with incorrect checksums.
                reader.QualitySettings.AllowIncorrectBarcodes = false;

                // Read all barcodes from the image.
                BarCodeResult[] results = reader.ReadBarCodes();

                if (results.Length == 0)
                {
                    Console.WriteLine($"No barcode detected in file: {Path.GetFileName(imageFile)}");
                    continue;
                }

                // Process each detected barcode.
                foreach (BarCodeResult result in results)
                {
                    // If CodeText is null or empty, the checksum likely failed.
                    if (string.IsNullOrEmpty(result.CodeText))
                    {
                        Console.WriteLine($"Checksum mismatch in file: {Path.GetFileName(imageFile)}");
                    }
                    else
                    {
                        Console.WriteLine($"File: {Path.GetFileName(imageFile)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                    }
                }
            }
        }
    }

    // Generates a few sample Swiss Post Parcel barcodes if none exist.
    private static void SeedSampleBarcodes(string folderPath)
    {
        // Check if there are already PNG files.
        if (Directory.GetFiles(folderPath, "*.png").Length > 0)
            return;

        // Sample data for Swiss Post Parcel barcodes.
        string[] sampleTexts = new[]
        {
            "1234567890123456", // Example valid code (replace with real format as needed)
            "9876543210987654",
            "5555555555555555"
        };

        // Create a barcode image for each sample text.
        for (int i = 0; i < sampleTexts.Length; i++)
        {
            string filePath = Path.Combine(folderPath, $"Sample{i + 1}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, sampleTexts[i]))
            {
                // Optional: adjust barcode appearance.
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }
    }
}