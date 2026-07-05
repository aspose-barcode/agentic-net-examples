// Title: Process images in a zip archive and generate barcode summary JSON
// Description: This example extracts each image from a zip file, scans for barcodes using Aspose.BarCode, and writes a JSON summary of detected barcodes per file.
// Prompt: Create a script that processes all images in a zip archive and outputs a summary JSON file.
// Tags: barcode, image-processing, zip, json, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates processing images from a zip archive, detecting barcodes, and outputting a JSON summary.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts optional zip file path argument, scans images for barcodes, and writes summary.json.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument may specify the zip file path.</param>
    static void Main(string[] args)
    {
        // Determine zip file path (use first argument or default to "images.zip")
        string zipPath = args.Length > 0 ? args[0] : "images.zip";

        // Define output JSON file name
        string outputPath = "summary.json";

        // Verify that the zip file exists before proceeding
        if (!File.Exists(zipPath))
        {
            Console.WriteLine($"Zip file not found: {zipPath}");
            return;
        }

        // Collection that will hold barcode information for each processed file
        var summary = new List<FileBarcodeInfo>();

        // Open the zip archive for reading
        using (FileStream zipFileStream = new FileStream(zipPath, FileMode.Open, FileAccess.Read))
        using (ZipArchive archive = new ZipArchive(zipFileStream, ZipArchiveMode.Read))
        {
            // Iterate through each entry in the archive
            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                // Process only supported image file types based on extension
                string ext = Path.GetExtension(entry.FullName).ToLowerInvariant();
                if (ext != ".png" && ext != ".jpg" && ext != ".jpeg" && ext != ".bmp" && ext != ".gif")
                    continue;

                // Prepare a container for barcode results of the current file
                var fileInfo = new FileBarcodeInfo
                {
                    FileName = entry.FullName,
                    Barcodes = new List<BarcodeResultInfo>()
                };

                // Read the zip entry into a memory stream (required for Aspose.Drawing.Bitmap)
                using (MemoryStream ms = new MemoryStream())
                {
                    using (Stream entryStream = entry.Open())
                    {
                        entryStream.CopyTo(ms);
                    }
                    ms.Position = 0; // Reset stream position before loading bitmap

                    // Load the image into a bitmap object
                    using (Bitmap bitmap = new Bitmap(ms))
                    {
                        // Initialize barcode reader to detect all supported barcode types
                        using (BarCodeReader reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
                        {
                            // Iterate over all detected barcodes in the image
                            foreach (BarCodeResult result in reader.ReadBarCodes())
                            {
                                fileInfo.Barcodes.Add(new BarcodeResultInfo
                                {
                                    CodeType = result.CodeTypeName,
                                    CodeText = result.CodeText
                                });
                            }
                        }
                    }
                }

                // Add the populated file information to the summary list
                summary.Add(fileInfo);
            }
        }

        // Serialize the summary collection to a formatted JSON string
        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(summary, jsonOptions);

        // Write the JSON output to the designated file
        File.WriteAllText(outputPath, json);
        Console.WriteLine($"Summary written to {outputPath}");
    }

    // Helper classes that define the JSON structure

    /// <summary>
    /// Represents barcode detection results for a single file.
    /// </summary>
    class FileBarcodeInfo
    {
        /// <summary>
        /// The relative path or name of the file within the zip archive.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// List of barcodes detected in the file.
        /// </summary>
        public List<BarcodeResultInfo> Barcodes { get; set; }
    }

    /// <summary>
    /// Represents a single barcode detection result.
    /// </summary>
    class BarcodeResultInfo
    {
        /// <summary>
        /// The type/name of the detected barcode (e.g., QR, Code128).
        /// </summary>
        public string CodeType { get; set; }

        /// <summary>
        /// The decoded text/value of the barcode.
        /// </summary>
        public string CodeText { get; set; }
    }
}