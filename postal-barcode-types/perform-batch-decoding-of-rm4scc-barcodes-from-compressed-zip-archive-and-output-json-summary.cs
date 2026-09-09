// Title: Batch decode RM4SCC barcodes from a ZIP archive and generate JSON report
// Description: Demonstrates generating RM4SCC barcode images, compressing them into a ZIP file, decoding each image, and outputting a JSON summary of detection results.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases how to use BarcodeGenerator to create barcodes, BarCodeReader to decode them, and System.IO.Compression to handle ZIP archives. Typical use cases include batch processing of barcode images stored in archives, automated quality checks, and reporting results in JSON for downstream systems.
// Prompt: Perform batch decoding of RM4SCC barcodes from a compressed ZIP archive and output JSON summary.
// Tags: rm4scc, barcode, batch-decoding, zip, json, aspose.barcode, generation, recognition

using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch generation, archiving, decoding of RM4SCC barcodes and JSON reporting.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample RM4SCC barcodes, archives them,
    /// decodes each image from the ZIP, and prints a JSON summary to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for sample barcodes
        string tempFolder = Path.Combine(Path.GetTempPath(), "RM4SCCBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Sample RM4SCC code texts
        string[] sampleTexts = new string[]
        {
            "123456ASPOSE",
            "ABCDEF",
            "9876543210"
        };

        // Generate barcode images and save them to the temporary folder
        for (int i = 0; i < sampleTexts.Length; i++)
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.RM4SCC, sampleTexts[i]))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 4;
                generator.Parameters.Barcode.BarHeight.Pixels = 50;
                string imagePath = Path.Combine(tempFolder, $"barcode_{i + 1}.png");
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }
        }

        // Create a ZIP archive containing the generated images
        string zipPath = Path.Combine(Path.GetTempPath(), "RM4SCCBatch_" + Guid.NewGuid().ToString("N") + ".zip");
        ZipFile.CreateFromDirectory(tempFolder, zipPath);

        // Prepare a list to hold the JSON summary objects
        var summary = new List<object>();

        // Process each entry in the ZIP archive
        using (var zip = ZipFile.OpenRead(zipPath))
        {
            foreach (var entry in zip.Entries)
            {
                // Skip non‑image files
                if (!entry.FullName.EndsWith(".png", StringComparison.OrdinalIgnoreCase) &&
                    !entry.FullName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) &&
                    !entry.FullName.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) &&
                    !entry.FullName.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                try
                {
                    // Load the entry into a memory stream for decoding
                    using (var entryStream = entry.Open())
                    using (var ms = new MemoryStream())
                    {
                        entryStream.CopyTo(ms);
                        ms.Position = 0;

                        // Initialize the barcode reader for all supported types
                        using (var reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
                        {
                            // Required by the API: reset stream position and set the image again
                            ms.Position = 0;
                            reader.SetBarCodeImage(ms);

                            BarCodeResult[] results = reader.ReadBarCodes();

                            // No barcode detected in this image
                            if (results.Length == 0)
                            {
                                summary.Add(new
                                {
                                    ImageFile = entry.FullName,
                                    Detected = false,
                                    Message = "No barcode detected"
                                });
                                continue;
                            }

                            // Add detection details for each barcode found
                            foreach (var result in results)
                            {
                                var bounds = result.Region.Rectangle;
                                summary.Add(new
                                {
                                    ImageFile = entry.FullName,
                                    Detected = true,
                                    CodeText = result.CodeText,
                                    CodeTypeName = result.CodeTypeName,
                                    ReadingQuality = result.ReadingQuality,
                                    Region = new
                                    {
                                        X = bounds.X,
                                        Y = bounds.Y,
                                        Width = bounds.Width,
                                        Height = bounds.Height,
                                        Angle = result.Region.Angle
                                    }
                                });
                            }
                        }
                    }
                }
                catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
                {
                    // Handle files that cannot be interpreted as images
                    summary.Add(new
                    {
                        ImageFile = entry.FullName,
                        Detected = false,
                        Message = "Invalid image format"
                    });
                }
                catch (Exception ex)
                {
                    // General error handling for unexpected issues
                    summary.Add(new
                    {
                        ImageFile = entry.FullName,
                        Detected = false,
                        Message = $"Error: {ex.Message}"
                    });
                }
            }
        }

        // Serialize the summary to formatted JSON and write to console
        string json = JsonSerializer.Serialize(summary, new JsonSerializerOptions { WriteIndented = true });
        Console.WriteLine(json);

        // Cleanup temporary files (ignore any errors)
        try
        {
            Directory.Delete(tempFolder, true);
            File.Delete(zipPath);
        }
        catch
        {
            // Cleanup failures are non‑critical for this example
        }
    }
}