// Title: Process barcode images from a ZIP archive and generate JSON summary
// Description: Demonstrates extracting image files from a ZIP archive, reading barcodes with Aspose.BarCode, and outputting a structured JSON report.
// Category-Description: This example belongs to the Aspose.BarCode image processing collection, showcasing how to work with ZipArchive, BarcodeGenerator, BarCodeReader, and System.Text.Json. Typical use cases include batch barcode extraction from packaged assets, automated inventory scanning, and generating machine‑readable summaries. Developers often need to combine file I/O, barcode generation/recognition, and JSON serialization to integrate barcode data into downstream systems.
// Prompt: Create a script that processes all images in a zip archive and outputs a summary JSON file.
// Tags: barcode symbology, zip processing, json output, barcodereader, barcodegenerator, aspnet.barcode, image extraction

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text.Json;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates creating a ZIP archive with sample barcode images,
/// extracting those images, reading the barcodes, and writing a JSON summary.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Orchestrates ZIP creation, barcode processing,
    /// and JSON serialization while reporting progress to the console.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary working directory.
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeZipDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define paths for the ZIP archive and the resulting JSON file.
        string zipPath = Path.Combine(tempDir, "barcodes.zip");
        string jsonPath = Path.Combine(tempDir, "summary.json");

        // Create a sample ZIP file containing barcode images.
        CreateSampleZip(zipPath);

        // Extract images from the ZIP and read any barcodes they contain.
        List<SummaryItem> summary = ProcessZipAndReadBarcodes(zipPath);

        // Serialize the collected barcode information to a formatted JSON string.
        string json = JsonSerializer.Serialize(summary, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(jsonPath, json);

        // Output the locations of the generated files and display the JSON content.
        Console.WriteLine("Processed zip: " + zipPath);
        Console.WriteLine("Summary JSON: " + jsonPath);
        Console.WriteLine("JSON content:");
        Console.WriteLine(json);

        // Optional cleanup of the temporary directory.
        // Directory.Delete(tempDir, true);
    }

    /// <summary>
    /// Generates two sample barcode images (Code128 and QR) and stores them in a ZIP archive.
    /// </summary>
    /// <param name="zipFilePath">Full path where the ZIP archive will be created.</param>
    static void CreateSampleZip(string zipFilePath)
    {
        // Define the sample images with their file names and corresponding generators.
        var images = new List<(string FileName, BarcodeGenerator Generator)>
        {
            ("code128.png", new BarcodeGenerator(EncodeTypes.Code128, "ABC123")),
            ("qr.png", new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        };

        // Create the ZIP archive and add each generated image as an entry.
        using (FileStream zipFs = new FileStream(zipFilePath, FileMode.Create))
        using (ZipArchive zip = new ZipArchive(zipFs, ZipArchiveMode.Create, leaveOpen: false))
        {
            foreach (var (fileName, generator) in images)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    // Save the barcode image to a memory stream in PNG format.
                    generator.Save(ms, BarCodeImageFormat.Png);
                    ms.Position = 0;

                    // Create a new entry in the ZIP and copy the image data.
                    ZipArchiveEntry entry = zip.CreateEntry(fileName);
                    using (Stream entryStream = entry.Open())
                    {
                        ms.CopyTo(entryStream);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Reads all supported image files from the specified ZIP archive,
    /// extracts barcodes using Aspose.BarCode, and returns a collection of summary items.
    /// </summary>
    /// <param name="zipFilePath">Path to the ZIP archive containing images.</param>
    /// <returns>List of <see cref="SummaryItem"/> with detected barcode information.</returns>
    static List<SummaryItem> ProcessZipAndReadBarcodes(string zipFilePath)
    {
        var resultList = new List<SummaryItem>();

        if (!File.Exists(zipFilePath))
        {
            Console.WriteLine("Zip file not found: " + zipFilePath);
            return resultList;
        }

        // Open the ZIP archive for reading.
        using (FileStream zipFs = new FileStream(zipFilePath, FileMode.Open, FileAccess.Read))
        using (ZipArchive zip = new ZipArchive(zipFs, ZipArchiveMode.Read))
        {
            foreach (ZipArchiveEntry entry in zip.Entries)
            {
                // Filter entries to common image extensions.
                string ext = Path.GetExtension(entry.FullName).ToLowerInvariant();
                if (ext != ".png" && ext != ".jpg" && ext != ".jpeg" && ext != ".bmp" && ext != ".gif" && ext != ".tif" && ext != ".tiff")
                {
                    continue;
                }

                var item = new SummaryItem { FileName = entry.FullName, Barcodes = new List<BarcodeInfo>() };
                try
                {
                    // Load the image entry into a memory stream for barcode reading.
                    using (Stream entryStream = entry.Open())
                    using (MemoryStream ms = new MemoryStream())
                    {
                        entryStream.CopyTo(ms);
                        ms.Position = 0;

                        // Initialize the barcode reader with a set of common decode types.
                        using (BarCodeReader reader = new BarCodeReader(ms, DecodeType.Code128, DecodeType.QR, DecodeType.DataMatrix, DecodeType.Aztec, DecodeType.Pdf417))
                        {
                            // Iterate over all detected barcodes and collect their details.
                            foreach (BarCodeResult bcResult in reader.ReadBarCodes())
                            {
                                var info = new BarcodeInfo
                                {
                                    Type = bcResult.CodeTypeName,
                                    Text = bcResult.CodeText
                                };
                                item.Barcodes.Add(info);
                            }
                        }
                    }
                }
                catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
                {
                    Console.WriteLine($"Skipping unsupported image '{entry.FullName}': {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing '{entry.FullName}': {ex.Message}");
                }

                resultList.Add(item);
            }
        }

        return resultList;
    }
}

/// <summary>
/// Represents a single file entry in the ZIP archive and its associated barcode data.
/// </summary>
class SummaryItem
{
    /// <summary>
    /// Name of the file within the ZIP archive.
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// Collection of barcodes detected in the file.
    /// </summary>
    public List<BarcodeInfo> Barcodes { get; set; }
}

/// <summary>
/// Holds information about an individual barcode detected in an image.
/// </summary>
class BarcodeInfo
{
    /// <summary>
    /// Human‑readable name of the barcode symbology (e.g., "Code128", "QR").
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Decoded text or data stored in the barcode.
    /// </summary>
    public string Text { get; set; }
}