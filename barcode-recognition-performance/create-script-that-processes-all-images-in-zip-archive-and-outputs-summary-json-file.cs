// Title: Process barcode images in a zip archive and generate JSON summary
// Description: Generates sample barcode images, packages them into a zip file, extracts image metadata, and writes a formatted JSON summary.
// Category-Description: This example belongs to the Aspose.BarCode image generation and Aspose.Drawing image analysis category. It demonstrates how to use BarcodeGenerator to create barcodes, Aspose.Drawing to read image properties, and System.IO.Compression to work with zip archives. Typical use cases include batch processing of generated barcodes, creating archives for distribution, and summarizing image characteristics for reporting or further automation.
// Prompt: Create a script that processes all images in a zip archive and outputs a summary JSON file.
// Tags: barcode, symbology, generation, image, processing, zip, json, summary, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates creating barcode images, zipping them, extracting image metadata,
/// and outputting a JSON summary.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes, creates a zip, processes images, and writes JSON summary.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for sample barcode images
        string sampleFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(sampleFolder);

        // Generate sample barcode images and collect their file paths
        var barcodeFiles = new List<string>();
        using (var gen = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            string filePath = Path.Combine(sampleFolder, "code128.png");
            gen.Save(filePath, BarCodeImageFormat.Png);
            barcodeFiles.Add(filePath);
        }
        using (var gen = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            string filePath = Path.Combine(sampleFolder, "qr.png");
            gen.Save(filePath, BarCodeImageFormat.Png);
            barcodeFiles.Add(filePath);
        }
        using (var gen = new BarcodeGenerator(EncodeTypes.DataMatrix, "DM123"))
        {
            string filePath = Path.Combine(sampleFolder, "datamatrix.png");
            gen.Save(filePath, BarCodeImageFormat.Png);
            barcodeFiles.Add(filePath);
        }

        // Create a zip archive containing the generated images
        string zipPath = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N") + ".zip");
        using (var zipToCreate = new FileStream(zipPath, FileMode.Create))
        using (var archive = new ZipArchive(zipToCreate, ZipArchiveMode.Update))
        {
            foreach (var filePath in barcodeFiles)
            {
                string entryName = Path.GetFileName(filePath);
                var entry = archive.CreateEntry(entryName, CompressionLevel.Optimal);
                using (var entryStream = entry.Open())
                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    fileStream.CopyTo(entryStream);
                }
            }
        }

        // Process the zip archive and collect image information
        var summary = ProcessZipArchive(zipPath);

        // Serialize summary to formatted JSON
        string jsonOutput = JsonSerializer.Serialize(summary, new JsonSerializerOptions { WriteIndented = true });

        // Write JSON to a file in the temp folder
        string jsonPath = Path.Combine(Path.GetTempPath(), "ImageSummary_" + Guid.NewGuid().ToString("N") + ".json");
        File.WriteAllText(jsonPath, jsonOutput);

        // Output locations and JSON content for verification
        Console.WriteLine("Processed images from zip: " + zipPath);
        Console.WriteLine("Summary JSON written to: " + jsonPath);
        Console.WriteLine(jsonOutput);

        // Cleanup temporary files and folders
        try
        {
            foreach (var file in barcodeFiles)
            {
                if (File.Exists(file))
                    File.Delete(file);
            }
            if (Directory.Exists(sampleFolder))
                Directory.Delete(sampleFolder, true);
        }
        catch
        {
            // Ignored - cleanup failures should not crash the program
        }
    }

    // Represents information about an image inside the zip
    private class ImageInfo
    {
        public string FileName { get; set; }
        public long SizeBytes { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }

    // Extracts image entries from the zip and gathers their properties
    private static List<ImageInfo> ProcessZipArchive(string zipFilePath)
    {
        var result = new List<ImageInfo>();
        if (!File.Exists(zipFilePath))
        {
            Console.WriteLine("Zip file not found: " + zipFilePath);
            return result;
        }

        using (var zipToOpen = new FileStream(zipFilePath, FileMode.Open, FileAccess.Read))
        using (var archive = new ZipArchive(zipToOpen, ZipArchiveMode.Read))
        {
            foreach (var entry in archive.Entries)
            {
                // Simple filter for common image extensions
                string ext = Path.GetExtension(entry.Name).ToLowerInvariant();
                if (ext != ".png" && ext != ".jpg" && ext != ".jpeg" && ext != ".bmp" && ext != ".gif")
                    continue;

                using (var entryStream = entry.Open())
                // Load image using Aspose.Drawing
                using (var img = Image.FromStream(entryStream))
                {
                    var info = new ImageInfo
                    {
                        FileName = entry.Name,
                        SizeBytes = entry.Length,
                        Width = img.Width,
                        Height = img.Height
                    };
                    result.Add(info);
                }
            }
        }

        return result;
    }
}