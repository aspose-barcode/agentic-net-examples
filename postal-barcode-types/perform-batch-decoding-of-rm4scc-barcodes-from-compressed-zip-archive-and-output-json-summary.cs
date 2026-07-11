// Title: Batch decode RM4SCC barcodes from ZIP and output JSON summary
// Description: Demonstrates how to extract images from a ZIP archive, decode RM4SCC barcodes using Aspose.BarCode, and produce a JSON report of the results.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating batch processing of image files within compressed archives. It showcases the BarCodeReader, DecodeType, and QualitySettings classes for high‑performance decoding, a common requirement for developers handling large volumes of barcodes in automated workflows.
// Prompt: Perform batch decoding of RM4SCC barcodes from a compressed ZIP archive and output JSON summary.
// Tags: rm4scc, batch decoding, zip, json, aspose.barcode, barcodereader, qualitysettings

using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch decoding of RM4SCC barcodes from a ZIP archive and outputs a JSON summary.
/// </summary>
class Program
{
    // Simple DTO for JSON output
    class BarcodeInfo
    {
        public string FileName { get; set; }
        public string CodeTypeName { get; set; }
        public string CodeText { get; set; }
        public RectangleInfo Region { get; set; }
    }

    class RectangleInfo
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }

    /// <summary>
    /// Entry point. Reads a ZIP file (default "barcodes.zip" or first argument), decodes RM4SCC barcodes in image entries, and prints a JSON report.
    /// </summary>
    /// <param name="args">Command‑line arguments; optionally the path to the ZIP archive.</param>
    static void Main(string[] args)
    {
        // Determine ZIP archive path; allow override via first command‑line argument
        string zipPath = args.Length > 0 ? args[0] : "barcodes.zip";

        // Validate that the ZIP file exists
        if (!File.Exists(zipPath))
        {
            Console.WriteLine($"Error: ZIP file not found at '{zipPath}'.");
            return;
        }

        // Collection to hold decoding results for JSON serialization
        var summary = new List<BarcodeInfo>();

        // Open the ZIP archive for reading
        using (FileStream zipStream = new FileStream(zipPath, FileMode.Open, FileAccess.Read))
        using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Read))
        {
            // Iterate over each entry in the archive
            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                // Process only common image file extensions
                string ext = Path.GetExtension(entry.Name).ToLowerInvariant();
                if (ext != ".png" && ext != ".jpg" && ext != ".jpeg" && ext != ".bmp")
                    continue;

                // Open the image entry stream and create a barcode reader for RM4SCC
                using (Stream entryStream = entry.Open())
                using (BarCodeReader reader = new BarCodeReader(entryStream, DecodeType.RM4SCC))
                {
                    // Use a high‑performance preset for faster decoding; adjust if needed
                    reader.QualitySettings = QualitySettings.HighPerformance;

                    // Read all barcodes found in the image
                    BarCodeResult[] results = reader.ReadBarCodes();
                    foreach (BarCodeResult result in results)
                    {
                        // Skip results without readable text
                        if (string.IsNullOrEmpty(result.CodeText))
                            continue;

                        // Map the result to the DTO
                        var rect = result.Region.Rectangle;
                        var info = new BarcodeInfo
                        {
                            FileName = entry.Name,
                            CodeTypeName = result.CodeTypeName,
                            CodeText = result.CodeText,
                            Region = new RectangleInfo
                            {
                                X = rect.X,
                                Y = rect.Y,
                                Width = rect.Width,
                                Height = rect.Height
                            }
                        };
                        summary.Add(info);
                    }
                }
            }
        }

        // Serialize the collected information to formatted JSON and output to console
        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(summary, jsonOptions);
        Console.WriteLine(json);
    }
}