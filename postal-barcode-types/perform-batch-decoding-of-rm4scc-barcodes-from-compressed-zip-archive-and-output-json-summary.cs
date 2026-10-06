// Title: Batch decode RM4SCC barcodes from ZIP and output JSON
// Description: Demonstrates generating RM4SCC barcode images, packaging them into a ZIP archive, decoding them in bulk, and producing a JSON summary.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create barcodes, BarCodeReader to recognize them, and System.IO.Compression to handle ZIP archives. Developers often need to process multiple barcode images stored in archives for inventory, logistics, or batch verification scenarios.
// Prompt: Perform batch decoding of RM4SCC barcodes from a compressed ZIP archive and output JSON summary.
// Tags: rm4scc, barcode generation, barcode recognition, zip, json, aspose.barcode, batch processing

using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Simple DTO that holds information about a decoded barcode.
/// </summary>
class BarcodeInfo
{
    public string FileName { get; set; }
    public string CodeText { get; set; }
    public string CodeType { get; set; }
}

/// <summary>
/// Demonstrates batch generation, archiving, decoding of RM4SCC barcodes and JSON output.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample RM4SCC barcodes, zips them, decodes all images from the zip,
    /// and prints a JSON summary of the results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for all intermediate files.
        string tempDir = Path.Combine(Path.GetTempPath(), "RM4SCCBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Sample RM4SCC barcode texts to encode.
        var sampleTexts = new List<string> { "123456ASPOSE", "ABCDEF", "9876543210" };
        var imagePaths = new List<string>();

        // -----------------------------------------------------------------
        // Generate barcode images and store their file paths.
        // -----------------------------------------------------------------
        foreach (var text in sampleTexts)
        {
            string imagePath = Path.Combine(tempDir, $"{text}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.RM4SCC, text))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 4;
                generator.Parameters.Barcode.BarHeight.Pixels = 50;
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }
            imagePaths.Add(imagePath);
        }

        // -----------------------------------------------------------------
        // Create a ZIP archive that contains all generated barcode images.
        // -----------------------------------------------------------------
        string zipPath = Path.Combine(tempDir, "barcodes.zip");
        using (var zipToCreate = ZipFile.Open(zipPath, ZipArchiveMode.Create))
        {
            foreach (var imgPath in imagePaths)
            {
                zipToCreate.CreateEntryFromFile(imgPath, Path.GetFileName(imgPath));
            }
        }

        // -----------------------------------------------------------------
        // Batch decode barcodes from the ZIP archive.
        // -----------------------------------------------------------------
        var decodedResults = new List<BarcodeInfo>();
        using (var zip = ZipFile.OpenRead(zipPath))
        {
            foreach (var entry in zip.Entries)
            {
                // Skip directory entries.
                if (string.IsNullOrEmpty(entry.Name))
                    continue;

                using (var entryStream = entry.Open())
                using (var memory = new MemoryStream())
                {
                    // Copy entry data to a memory stream for the reader.
                    entryStream.CopyTo(memory);
                    memory.Position = 0;

                    using (var reader = new BarCodeReader(memory, DecodeType.AllSupportedTypes))
                    {
                        try
                        {
                            BarCodeResult[] barcodes = reader.ReadBarCodes();
                            foreach (var result in barcodes)
                            {
                                // Filter only RM4SCC results.
                                if (result.CodeTypeName == "RM4SCC")
                                {
                                    decodedResults.Add(new BarcodeInfo
                                    {
                                        FileName = entry.Name,
                                        CodeText = result.CodeText,
                                        CodeType = result.CodeTypeName
                                    });
                                }
                            }
                        }
                        catch (ArgumentException ex)
                        {
                            // Log and continue on unsupported file formats.
                            Console.WriteLine($"Skipping {entry.Name}: {ex.Message}");
                        }
                    }
                }
            }
        }

        // -----------------------------------------------------------------
        // Serialize the decoded results to a formatted JSON string and output.
        // -----------------------------------------------------------------
        string json = JsonSerializer.Serialize(decodedResults, new JsonSerializerOptions { WriteIndented = true });
        Console.WriteLine(json);

        // -----------------------------------------------------------------
        // Clean up temporary files and directories.
        // -----------------------------------------------------------------
        try
        {
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored – cleanup failure should not crash the program.
        }
    }
}