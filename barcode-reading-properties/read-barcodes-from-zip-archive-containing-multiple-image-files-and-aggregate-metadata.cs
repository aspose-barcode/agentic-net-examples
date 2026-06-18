using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Entry point for the barcode processing application.
/// Reads barcode images from a zip archive, extracts metadata, and outputs JSON.
/// </summary>
class Program
{
    /// <summary>
    /// Simple DTO to hold barcode metadata extracted from each image.
    /// </summary>
    class BarcodeInfo
    {
        public string ImageFile { get; set; }
        public string BarcodeType { get; set; }
        public string CodeText { get; set; }
        public string Confidence { get; set; }
        public double ReadingQuality { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public double Angle { get; set; }
    }

    /// <summary>
    /// Main method that orchestrates reading the zip file, processing each image,
    /// extracting barcode information, and printing the results as JSON.
    /// </summary>
    static void Main()
    {
        // Path to the zip archive containing barcode images
        string zipPath = "barcodes.zip";

        // Verify that the zip file exists before proceeding
        if (!File.Exists(zipPath))
        {
            Console.WriteLine($"Zip file not found: {zipPath}");
            return;
        }

        // Collection to hold all extracted barcode information
        var results = new List<BarcodeInfo>();

        // Open the zip archive for reading
        using (var zip = ZipFile.OpenRead(zipPath))
        {
            // Iterate over each entry (file) in the archive
            foreach (var entry in zip.Entries)
            {
                // Determine the file extension and process only supported image types
                string ext = Path.GetExtension(entry.Name).ToLowerInvariant();
                if (ext != ".png" && ext != ".jpg" && ext != ".jpeg" && ext != ".bmp" && ext != ".gif")
                    continue; // Skip non-image files

                // Open a stream to read the image data from the zip entry
                using (var stream = entry.Open())
                {
                    // Initialize the barcode reader with support for all barcode types
                    using (var reader = new BarCodeReader(stream, DecodeType.AllSupportedTypes))
                    {
                        // Read all barcodes found in the current image
                        foreach (var result in reader.ReadBarCodes())
                        {
                            // Extract the bounding rectangle of the detected barcode region
                            var region = result.Region.Rectangle;

                            // Populate the DTO with relevant metadata
                            var info = new BarcodeInfo
                            {
                                ImageFile = entry.Name,
                                BarcodeType = result.CodeTypeName,
                                CodeText = result.CodeText,
                                Confidence = result.Confidence.ToString(),
                                ReadingQuality = result.ReadingQuality,
                                X = (float)Math.Round((double)region.X),
                                Y = (float)Math.Round((double)region.Y),
                                Width = (float)Math.Round((double)region.Width),
                                Height = (float)Math.Round((double)region.Height),
                                Angle = result.Region.Angle
                            };

                            // Add the populated DTO to the results list
                            results.Add(info);
                        }
                    }
                }
            }
        }

        // Serialize the aggregated metadata to formatted JSON
        string json = JsonSerializer.Serialize(results, new JsonSerializerOptions { WriteIndented = true });

        // Output the JSON to the console
        Console.WriteLine(json);
    }
}