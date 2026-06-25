using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates reading RM4SCC barcodes from images stored inside a ZIP archive
/// and outputs the results as formatted JSON.
/// </summary>
class Program
{
    /// <summary>
    /// Represents a single decoded barcode entry.
    /// </summary>
    private class BarcodeInfo
    {
        public string FileName { get; set; }
        public string CodeTypeName { get; set; }
        public string CodeText { get; set; }
        public int Confidence { get; set; }
        public double ReadingQuality { get; set; }
        public RectangleF Region { get; set; }
    }

    /// <summary>
    /// Entry point of the application.
    /// Reads barcode images from a ZIP file, decodes them, and prints a JSON summary.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Path to the ZIP archive containing barcode images
        string zipPath = "barcodes.zip";

        // Verify that the ZIP file exists before proceeding
        if (!File.Exists(zipPath))
        {
            Console.WriteLine($"ZIP file not found: {zipPath}");
            return;
        }

        // Collection to hold decoding results
        var results = new List<BarcodeInfo>();

        // Open the ZIP archive for reading
        using (FileStream zipFileStream = new FileStream(zipPath, FileMode.Open, FileAccess.Read))
        using (ZipArchive archive = new ZipArchive(zipFileStream, ZipArchiveMode.Read))
        {
            // Iterate over each entry in the archive (expecting image files)
            foreach (var entry in archive.Entries)
            {
                // Skip directory entries (they have an empty Name)
                if (string.IsNullOrEmpty(entry.Name))
                    continue;

                // Load the entry's data into a memory stream for processing
                using (MemoryStream entryStream = new MemoryStream())
                {
                    // Copy the entry's raw stream into the memory stream
                    using (Stream entryOriginal = entry.Open())
                    {
                        entryOriginal.CopyTo(entryStream);
                    }

                    // Reset position to the beginning before reading
                    entryStream.Position = 0;

                    // Create a bitmap from the memory stream
                    using (Bitmap bitmap = new Bitmap(entryStream))
                    {
                        // Initialize a barcode reader configured for RM4SCC type
                        using (BarCodeReader reader = new BarCodeReader(bitmap, DecodeType.RM4SCC))
                        {
                            // Optional: enable checksum validation if desired
                            // reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                            // Read all barcodes found in the image
                            foreach (var result in reader.ReadBarCodes())
                            {
                                // Populate a BarcodeInfo instance with the decoded data
                                var info = new BarcodeInfo
                                {
                                    FileName = entry.Name,
                                    CodeTypeName = result.CodeTypeName,
                                    CodeText = result.CodeText,
                                    Confidence = (int)result.Confidence,
                                    ReadingQuality = result.ReadingQuality,
                                    Region = result.Region.Rectangle
                                };

                                // Add the result to the collection
                                results.Add(info);
                            }
                        }
                    }
                }
            }
        }

        // Serialize the results collection to formatted JSON
        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(results, jsonOptions);

        // Output the JSON to the console
        Console.WriteLine(json);
    }
}