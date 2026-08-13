// Title: Read barcodes from a zip archive and aggregate metadata
// Description: Demonstrates how to extract images from a zip file, recognize barcodes using Aspose.BarCode, and collect their metadata.
// Category-Description: This example belongs to the Aspose.BarCode image processing and barcode recognition category. It showcases the use of BarCodeReader, DecodeType, and related classes to batch‑process multiple images stored in a compressed archive, a common scenario for automated inventory or document scanning systems. Developers often need to read barcodes from bulk image collections, aggregate results, and integrate them into downstream workflows.
// Prompt: Read barcodes from a zip archive containing multiple image files and aggregate metadata.
// Tags: barcode, recognition, zip, batch processing, aspose.barcode, decode type, metadata

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates reading barcodes from images stored inside a zip archive and aggregating their metadata.
/// </summary>
class Program
{
    // Simple DTO to hold barcode metadata
    class BarcodeInfo
    {
        public string FileName { get; set; }
        public string CodeTypeName { get; set; }
        public string CodeText { get; set; }
        public Rectangle Region { get; set; }
    }

    /// <summary>
    /// Entry point. Processes the specified zip file (or default) and prints detected barcode information.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument may specify the zip file path.</param>
    static void Main(string[] args)
    {
        // Determine zip file path (argument or default)
        string zipPath = args.Length > 0 ? args[0] : "barcodes.zip";

        // Verify that the zip file exists
        if (!File.Exists(zipPath))
        {
            Console.WriteLine($"Zip file not found: {zipPath}");
            return;
        }

        var results = new List<BarcodeInfo>();

        // Open the zip archive for reading
        using (FileStream zipFileStream = new FileStream(zipPath, FileMode.Open, FileAccess.Read))
        using (ZipArchive archive = new ZipArchive(zipFileStream, ZipArchiveMode.Read))
        {
            // Iterate through each entry in the archive
            foreach (var entry in archive.Entries)
            {
                // Process only image files (png, jpg, jpeg, bmp)
                string ext = Path.GetExtension(entry.Name).ToLowerInvariant();
                if (ext != ".png" && ext != ".jpg" && ext != ".jpeg" && ext != ".bmp")
                    continue;

                // Load entry into a memory stream
                using (Stream entryStream = entry.Open())
                using (MemoryStream ms = new MemoryStream())
                {
                    entryStream.CopyTo(ms);
                    ms.Position = 0; // reset for reading

                    // Create bitmap from the memory stream
                    using (Bitmap bitmap = new Bitmap(ms))
                    {
                        // Initialize reader for all supported barcode types
                        using (BarCodeReader reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
                        {
                            // Read all barcodes found in the image
                            foreach (BarCodeResult result in reader.ReadBarCodes())
                            {
                                var info = new BarcodeInfo
                                {
                                    FileName = entry.Name,
                                    CodeTypeName = result.CodeTypeName,
                                    CodeText = result.CodeText,
                                    Region = result.Region.Rectangle
                                };
                                results.Add(info);
                            }
                        }
                    }
                }
            }
        }

        // Output aggregated metadata
        if (results.Count == 0)
        {
            Console.WriteLine("No barcodes were detected in the archive.");
        }
        else
        {
            Console.WriteLine("Detected barcodes:");
            foreach (var info in results)
            {
                Console.WriteLine($"File: {info.FileName}");
                Console.WriteLine($"  Type : {info.CodeTypeName}");
                Console.WriteLine($"  Text : {info.CodeText}");
                Console.WriteLine($"  Region: X={info.Region.X}, Y={info.Region.Y}, Width={info.Region.Width}, Height={info.Region.Height}");
                Console.WriteLine();
            }
        }
    }
}