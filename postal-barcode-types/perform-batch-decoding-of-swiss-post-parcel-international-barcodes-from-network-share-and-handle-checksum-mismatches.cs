// Title: Batch decode Swiss Post Parcel barcodes with checksum validation
// Description: Demonstrates generating Swiss Post Parcel barcode images, storing them in a temporary network‑share‑like folder, and batch decoding them while detecting checksum mismatches.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating SwissPostParcel barcodes, BarCodeReader for batch decoding, and checksum validation settings. Developers working with postal barcode automation, bulk image processing, or quality‑control scenarios can use these APIs to validate barcode data and handle errors efficiently.
// Prompt: Perform batch decoding of Swiss Post Parcel international barcodes from a network share and handle checksum mismatches.
// Tags: swisspostparcel, barcode generation, barcode recognition, checksum validation, batch processing, aspnet.barcode, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Entry point for the batch Swiss Post Parcel barcode generation and decoding example.
/// </summary>
class Program
{
    /// <summary>
    /// Generates sample Swiss Post Parcel barcodes, saves them to a temporary folder,
    /// then reads them back in a batch, reporting success or checksum mismatches.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to simulate a network share
        string tempFolder = Path.Combine(Path.GetTempPath(), "BatchSwissPost_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Sample Swiss Post Parcel barcode texts (some with intentional checksum errors)
        var barcodeTexts = new List<string>
        {
            "1234567890123", // assume valid
            "9876543210987", // assume valid
            "1111111111111"  // assume invalid checksum
        };

        // Generate barcode images and collect file paths
        var generatedFiles = new List<string>();
        foreach (var text in barcodeTexts)
        {
            string filePath = Path.Combine(tempFolder, $"SwissPost_{text}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, text))
            {
                // Example of setting a barcode property (optional)
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            generatedFiles.Add(filePath);
        }

        Console.WriteLine($"Generated {generatedFiles.Count} barcode images in: {tempFolder}");
        Console.WriteLine();

        // Batch decode the generated images
        foreach (var file in generatedFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found, skipping: {file}");
                continue;
            }

            try
            {
                using (var reader = new BarCodeReader(file, DecodeType.SwissPostParcel))
                {
                    // Enable checksum validation
                    reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
                    // Allow reading even if checksum is wrong (so we can detect mismatch)
                    reader.QualitySettings.AllowIncorrectBarcodes = true;

                    var results = reader.ReadBarCodes();
                    if (results.Length == 0)
                    {
                        Console.WriteLine($"No barcode detected in file: {Path.GetFileName(file)}");
                        continue;
                    }

                    foreach (var result in results)
                    {
                        // If CodeText is null or empty, treat it as a checksum mismatch
                        if (string.IsNullOrEmpty(result.CodeText))
                        {
                            Console.WriteLine($"[Checksum Mismatch] File: {Path.GetFileName(file)}");
                        }
                        else
                        {
                            Console.WriteLine($"[Success] File: {Path.GetFileName(file)}");
                            Console.WriteLine($"  Type    : {result.CodeTypeName}");
                            Console.WriteLine($"  CodeText: {result.CodeText}");
                        }
                    }
                }
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                Console.WriteLine($"Unable to load image (skipped): {Path.GetFileName(file)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error processing {Path.GetFileName(file)}: {ex.Message}");
            }

            Console.WriteLine();
        }

        // Cleanup: delete temporary folder and its contents
        try
        {
            Directory.Delete(tempFolder, true);
            Console.WriteLine("Temporary files cleaned up.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to delete temporary folder: {ex.Message}");
        }
    }
}