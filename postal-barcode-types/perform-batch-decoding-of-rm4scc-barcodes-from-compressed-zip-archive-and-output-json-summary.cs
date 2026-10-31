// Title: Batch decode RM4SCC barcodes from ZIP and output JSON summary
// Description: Demonstrates how to read multiple image files from a ZIP archive, decode RM4SCC barcodes using Aspose.BarCode, and produce a JSON report of the results.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating batch processing of images stored in compressed archives. It uses BarCodeReader, DecodeType, and related classes to decode RM4SCC symbology, collect metadata, and serialize the outcome to JSON. Developers needing high‑throughput barcode extraction from bulk files can adapt this pattern for various symbologies and output formats.
// Prompt: Perform batch decoding of RM4SCC barcodes from a compressed ZIP archive and output JSON summary.
// Tags: barcode, rm4scc, batch decoding, zip, json, aspose.barcode, barcoderecognition

using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Provides an entry point for batch decoding RM4SCC barcodes from a ZIP archive
/// and generating a JSON summary of the results.
/// </summary>
class Program
{
    /// <summary>
    /// Reads image files from a ZIP archive, decodes RM4SCC barcodes using Aspose.BarCode,
    /// and writes a formatted JSON report to the console.
    /// </summary>
    static void Main()
    {
        // Determine the full path to the ZIP archive containing barcode images
        string zipPath = Path.Combine(Directory.GetCurrentDirectory(), "rm4scc_barcodes.zip");
        if (!File.Exists(zipPath))
        {
            Console.WriteLine($"ZIP file not found: {zipPath}");
            return;
        }

        // Collection that will hold decoding results for each image
        var summary = new List<BarcodeInfo>();

        // Open the ZIP archive for reading
        using (var zip = ZipFile.OpenRead(zipPath))
        {
            // Iterate through each entry (file) in the archive
            foreach (var entry in zip.Entries)
            {
                // Process only supported image file extensions
                string ext = Path.GetExtension(entry.Name).ToLowerInvariant();
                if (ext != ".png" && ext != ".jpg" && ext != ".jpeg" && ext != ".bmp" && ext != ".gif" && ext != ".tif" && ext != ".tiff")
                    continue;

                // Open the entry stream and copy its contents into a memory stream
                using (var entryStream = entry.Open())
                using (var memory = new MemoryStream())
                {
                    entryStream.CopyTo(memory);
                    memory.Position = 0; // Reset position for reading

                    // Specify the decode type for RM4SCC barcodes
                    BaseDecodeType decodeType = DecodeType.RM4SCC;

                    // Create a BarCodeReader for the image data
                    using (var reader = new BarCodeReader(memory, decodeType))
                    {
                        // Optional: configure quality settings (commented out)
                        // reader.QualitySettings = QualitySettings.HighPerformance;

                        // Perform barcode detection
                        var results = reader.ReadBarCodes();

                        if (results.Length > 0)
                        {
                            // Use the first detected barcode in the image
                            var result = results[0];
                            summary.Add(new BarcodeInfo
                            {
                                FileName = entry.Name,
                                CodeText = result.CodeText,
                                CodeType = result.CodeTypeName,
                                ReadingQuality = result.ReadingQuality
                            });
                        }
                        else
                        {
                            // No barcode found – record null values
                            summary.Add(new BarcodeInfo
                            {
                                FileName = entry.Name,
                                CodeText = null,
                                CodeType = null,
                                ReadingQuality = 0
                            });
                        }
                    }
                }
            }
        }

        // Serialize the summary list to a formatted JSON string
        string json = JsonSerializer.Serialize(summary, new JsonSerializerOptions { WriteIndented = true });
        Console.WriteLine(json);
    }

    /// <summary>
    /// Simple DTO that holds barcode decoding information for a single file.
    /// </summary>
    class BarcodeInfo
    {
        public string FileName { get; set; }
        public string CodeText { get; set; }
        public string CodeType { get; set; }
        public double ReadingQuality { get; set; }
    }
}