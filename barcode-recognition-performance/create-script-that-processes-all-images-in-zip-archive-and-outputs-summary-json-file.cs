using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

namespace BarcodeZipProcessor
{
    /// <summary>
    /// Entry point for the Barcode Zip Processor application.
    /// Scans image files inside a zip archive, detects barcodes, and writes a JSON summary.
    /// </summary>
    class Program
    {
        // Simple DTO for JSON output
        public class BarcodeInfo
        {
            public string FileName { get; set; }
            public List<DetectedBarcode> Barcodes { get; set; } = new List<DetectedBarcode>();
        }

        public class DetectedBarcode
        {
            public string TypeName { get; set; }
            public string CodeText { get; set; }
        }

        /// <summary>
        /// Main method that orchestrates the processing of the zip file.
        /// </summary>
        /// <param name="args">Command‑line arguments; first argument may be the zip file path.</param>
        static void Main(string[] args)
        {
            // Determine zip file path: use first argument if provided, otherwise default to "input.zip"
            string zipPath = args.Length > 0 ? args[0] : "input.zip";

            // Verify that the zip file exists before proceeding
            if (!File.Exists(zipPath))
            {
                Console.WriteLine($"Zip file not found: {zipPath}");
                return;
            }

            // Collection that will hold barcode information for each processed image
            var summary = new List<BarcodeInfo>();

            // Supported image extensions (case‑insensitive)
            string[] imageExtensions = new[] { ".png", ".jpg", ".jpeg", ".bmp", ".gif" };

            // Open the zip archive for reading
            using (var zip = ZipFile.OpenRead(zipPath))
            {
                // Iterate over each entry (file) in the archive
                foreach (var entry in zip.Entries)
                {
                    // Skip directory entries (they have an empty Name)
                    if (string.IsNullOrEmpty(entry.Name))
                        continue;

                    // Filter out non‑image files based on extension
                    string ext = Path.GetExtension(entry.Name).ToLowerInvariant();
                    if (Array.IndexOf(imageExtensions, ext) < 0)
                        continue;

                    // Prepare a DTO to hold barcode results for the current image
                    var info = new BarcodeInfo { FileName = entry.FullName };

                    // Open a stream to the image data inside the zip entry
                    using (var entryStream = entry.Open())
                    {
                        // Initialize the barcode reader to scan all supported barcode types
                        using (var reader = new BarCodeReader(entryStream, DecodeType.AllSupportedTypes))
                        {
                            // Read all barcodes found in the image
                            foreach (var result in reader.ReadBarCodes())
                            {
                                // Map the detection result to our DTO format
                                var detected = new DetectedBarcode
                                {
                                    TypeName = result.CodeTypeName,
                                    CodeText = result.CodeText
                                };
                                info.Barcodes.Add(detected);
                            }
                        }
                    }

                    // Add the populated info object to the summary list
                    summary.Add(info);
                }
            }

            // Serialize the summary list to a formatted JSON string
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(summary, jsonOptions);
            string outputPath = "summary.json";

            // Attempt to write the JSON output to disk and report success or failure
            try
            {
                File.WriteAllText(outputPath, json);
                Console.WriteLine($"Summary written to {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write summary: {ex.Message}");
            }
        }
    }
}