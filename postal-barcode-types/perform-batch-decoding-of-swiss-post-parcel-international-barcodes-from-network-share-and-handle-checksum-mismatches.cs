// Title: Batch decode Swiss Post Parcel barcodes with checksum handling
// Description: Demonstrates generating a set of Swiss Post Parcel barcode images, decoding them from a temporary folder, and handling checksum mismatches by toggling validation.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on batch processing of Swiss Post Parcel (RM) symbology. It shows how to use BarcodeGenerator, BarCodeReader, and BarcodeSettings.ChecksumValidation to read barcodes, manage checksum errors, and process multiple files—common tasks for logistics and shipping applications. Developers can adapt this pattern for bulk barcode scanning from network shares or other storage locations.
// Prompt: Perform batch decoding of Swiss Post Parcel international barcodes from a network share and handle checksum mismatches.
// Tags: swisspostparcel, barcode generation, barcode recognition, checksum validation, batch processing, aspnet, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch generation and decoding of Swiss Post Parcel barcodes,
/// including handling of checksum mismatches.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, decodes them with checksum validation,
    /// and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for the batch
        string batchFolder = Path.Combine(Path.GetTempPath(), "BatchSwissPost_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Prepare sample barcode data (correct, missing, and erroneous checksum)
        var samples = new List<(string CodeText, string Description)>
        {
            ("RM999605013CH", "Correct checksum"),
            ("RM99960501CH",  "Missing checksum (will be generated)"),
            ("RM999605017CH", "Erroneous checksum")
        };

        var barcodeFiles = new List<string>();

        // Generate barcode images for each sample
        foreach (var (codeText, description) in samples)
        {
            string filePath = Path.Combine(batchFolder, $"{description.Replace(' ', '_')}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, codeText))
            {
                // Set visual parameters
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Parameters.Barcode.BarHeight.Pixels = 40f;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        Console.WriteLine("Batch decoding results:");
        Console.WriteLine();

        // Process each generated barcode file
        foreach (string file in barcodeFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                using (var reader = new BarCodeReader(file, DecodeType.SwissPostParcel))
                {
                    // First attempt with checksum validation enabled
                    reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
                    BarCodeResult[] results = reader.ReadBarCodes();

                    if (results.Length > 0)
                    {
                        Console.WriteLine($"File: {Path.GetFileName(file)} - Checksum valid.");
                        foreach (var result in results)
                        {
                            Console.WriteLine($"  Type: {result.CodeTypeName}, Data: {result.CodeText}");
                        }
                    }
                    else
                    {
                        // Retry with checksum validation disabled to read mismatched barcodes
                        reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;
                        BarCodeResult[] offResults = reader.ReadBarCodes();

                        if (offResults.Length > 0)
                        {
                            Console.WriteLine($"File: {Path.GetFileName(file)} - Checksum mismatch detected, read with validation OFF.");
                            foreach (var result in offResults)
                            {
                                Console.WriteLine($"  Type: {result.CodeTypeName}, Data: {result.CodeText}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"File: {Path.GetFileName(file)} - Unable to read barcode.");
                        }
                    }
                }
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                Console.WriteLine($"File: {Path.GetFileName(file)} - Skipped (unloadable image).");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"File: {Path.GetFileName(file)} - Error: {ex.Message}");
            }

            Console.WriteLine();
        }

        // Cleanup temporary files and folder
        try
        {
            Directory.Delete(batchFolder, true);
        }
        catch
        {
            // If cleanup fails, ignore – temporary files will be removed by the OS eventually.
        }
    }
}