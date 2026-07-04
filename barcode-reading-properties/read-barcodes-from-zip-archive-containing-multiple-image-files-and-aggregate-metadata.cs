// Title: Read barcodes from images inside a zip archive and output aggregated metadata as JSON
// Description: Demonstrates extracting image files from a zip, decoding any barcodes they contain, and collecting detailed information for each barcode.
// Prompt: Read barcodes from a zip archive containing multiple image files and aggregate metadata.
// Tags: barcode, zip, json, aspose.barcode, barcoderecognition, file-io

using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Represents detailed information about a detected barcode.
/// </summary>
class BarcodeInfo
{
    public string? FileName { get; set; }
    public string? CodeTypeName { get; set; }
    public string? CodeText { get; set; }
    public int Confidence { get; set; }
    public double ReadingQuality { get; set; }
    public float RegionX { get; set; }
    public float RegionY { get; set; }
    public float RegionWidth { get; set; }
    public float RegionHeight { get; set; }
    public double RegionAngle { get; set; }
}

class Program
{
    /// <summary>
    /// Entry point. Reads barcodes from image files inside a zip archive and prints aggregated metadata as JSON.
    /// </summary>
    static void Main()
    {
        // Path to the zip file containing barcode images
        const string zipPath = "barcodes.zip";

        // Verify that the zip file exists before proceeding
        if (!File.Exists(zipPath))
        {
            Console.WriteLine($"Zip file not found: {zipPath}");
            return;
        }

        // Collection to hold barcode information from all images
        var aggregatedData = new List<BarcodeInfo>();

        // Open the zip archive for reading
        using (var zip = ZipFile.OpenRead(zipPath))
        {
            // Iterate through each entry (file) in the archive
            foreach (var entry in zip.Entries)
            {
                // Determine the file extension and process only supported image types
                string ext = Path.GetExtension(entry.FullName).ToLowerInvariant();
                if (ext != ".png" && ext != ".jpg" && ext != ".jpeg" && ext != ".bmp" && ext != ".gif")
                    continue;

                // Open a stream to the image file inside the zip
                using (var entryStream = entry.Open())
                {
                    // Initialize the barcode reader
                    using (var reader = new BarCodeReader())
                    {
                        // Configure the reader to detect all supported barcode types
                        reader.BarCodeReadType = DecodeType.AllSupportedTypes;
                        // Load the image stream into the reader
                        reader.SetBarCodeImage(entryStream);

                        // Perform barcode detection
                        BarCodeResult[] results = reader.ReadBarCodes();

                        // Process each detected barcode
                        foreach (var result in results)
                        {
                            var rect = result.Region.Rectangle;

                            // Populate a BarcodeInfo instance with details from the detection result
                            var info = new BarcodeInfo
                            {
                                FileName = entry.FullName,
                                CodeTypeName = result.CodeTypeName,
                                CodeText = result.CodeText,
                                Confidence = (int)result.Confidence,
                                ReadingQuality = result.ReadingQuality,
                                RegionX = rect.X,
                                RegionY = rect.Y,
                                RegionWidth = rect.Width,
                                RegionHeight = rect.Height,
                                RegionAngle = result.Region.Angle
                            };

                            // Add the populated info to the aggregate list
                            aggregatedData.Add(info);
                        }
                    }
                }
            }
        }

        // Serialize the aggregated barcode data to formatted JSON
        string json = JsonSerializer.Serialize(aggregatedData, new JsonSerializerOptions { WriteIndented = true });
        // Output the JSON to the console
        Console.WriteLine(json);
    }
}