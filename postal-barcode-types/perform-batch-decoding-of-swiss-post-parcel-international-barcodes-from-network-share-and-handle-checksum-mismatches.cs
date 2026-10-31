// Title: Batch decode Swiss Post Parcel barcodes with checksum handling
// Description: Demonstrates generating sample Swiss Post Parcel International barcodes, then decoding them in a batch while handling checksum mismatches.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category, showcasing how to use BarcodeGenerator, BarCodeReader, and checksum validation settings. Typical use cases include bulk scanning of postal parcels, validating barcode integrity, and fallback decoding when checksums are incorrect. Developers often need to generate test images, read multiple files, and toggle checksum validation to ensure robust barcode processing.
// Prompt: Perform batch decoding of Swiss Post Parcel international barcodes from a network share and handle checksum mismatches.
// Tags: swisspostparcel, barcode, batch-decoding, checksum-validation, generation, reading, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Program that generates sample Swiss Post Parcel barcodes, then batch decodes them,
/// demonstrating checksum validation handling.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcode images, attempts to read them with checksum validation on,
    /// and retries with validation off if needed. Cleans up temporary files afterwards.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for the batch operation
        string batchFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Prepare sample barcode data (Swiss Post Parcel International)
        var samples = new List<(string CodeText, string FileName)>
        {
            ("RM999605013CH", "CorrectChecksum.png"),   // valid checksum
            ("RM999605017CH", "WrongChecksum.png"),    // invalid checksum
            ("RM99960501CH",  "MissingChecksum.png")   // no checksum, will be generated automatically
        };

        var generatedFiles = new List<string>();

        // Generate barcode images for each sample
        foreach (var (codeText, fileName) in samples)
        {
            string filePath = Path.Combine(batchFolder, fileName);
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, codeText))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Parameters.Barcode.BarHeight.Pixels = 40f;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            generatedFiles.Add(filePath);
        }

        Console.WriteLine("Batch decoding results:");
        Console.WriteLine();

        // Batch decode each generated file
        foreach (string filePath in generatedFiles)
        {
            Console.WriteLine($"File: {Path.GetFileName(filePath)}");

            if (!File.Exists(filePath))
            {
                Console.WriteLine("  File not found, skipping.");
                continue;
            }

            bool decoded = false;

            // First attempt with default checksum validation (On)
            try
            {
                using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.SwissPostParcel))
                {
                    reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
                    BarCodeResult[] results = reader.ReadBarCodes();
                    if (results.Length > 0)
                    {
                        foreach (BarCodeResult result in results)
                        {
                            Console.WriteLine($"  Detected (checksum OK): Type={result.CodeTypeName}, Text={result.CodeText}");
                        }
                        decoded = true;
                    }
                }
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                Console.WriteLine($"  Warning: Unable to load image - {ex.Message}");
                continue;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  Error during reading (checksum ON): {ex.Message}");
            }

            // If not decoded, try with checksum validation turned off to handle mismatches
            if (!decoded)
            {
                try
                {
                    using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.SwissPostParcel))
                    {
                        reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;
                        BarCodeResult[] results = reader.ReadBarCodes();
                        if (results.Length > 0)
                        {
                            foreach (BarCodeResult result in results)
                            {
                                Console.WriteLine($"  Detected (checksum ignored): Type={result.CodeTypeName}, Text={result.CodeText}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("  No barcode detected even with checksum disabled.");
                        }
                    }
                }
                catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
                {
                    Console.WriteLine($"  Warning: Unable to load image - {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"  Error during reading (checksum OFF): {ex.Message}");
                }
            }

            Console.WriteLine();
        }

        // Clean up temporary files and folder
        try
        {
            Directory.Delete(batchFolder, true);
        }
        catch
        {
            // If cleanup fails, ignore – files will be removed by the OS eventually.
        }
    }
}