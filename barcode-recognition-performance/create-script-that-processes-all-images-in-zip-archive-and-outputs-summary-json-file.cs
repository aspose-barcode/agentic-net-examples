// Title: Process Barcode Images from ZIP and Generate Summary JSON
// Description: The example creates sample barcode images, packages them into a ZIP archive, reads each image, extracts barcodes, and writes a JSON summary.
// Category-Description: This sample belongs to the Aspose.BarCode image processing and recognition category. It demonstrates using BarcodeGenerator to create barcodes, ZipArchive for archive handling, BarCodeReader for decoding multiple symbologies, and System.Text.Json for output. Developers often need to batch‑process images stored in archives to extract barcode data and produce reports.
// Prompt: Create a script that processes all images in a zip archive and outputs a summary JSON file.
// Tags: barcode generation, barcode recognition, zip archive, json output, aspose.barcode, c#

using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating barcodes, packaging them into a ZIP, decoding them, and producing a JSON summary.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// Generates sample barcodes, creates a ZIP archive, reads barcodes from images inside the archive,
    /// and writes a summary JSON file.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for sample barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample barcodes to generate (type, text, file name)
        var samples = new List<(BaseEncodeType encodeType, string text, string fileName)>
        {
            (EncodeTypes.Code128, "CODE128_SAMPLE", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png"),
            (EncodeTypes.DataMatrix, "DMATRIX", "datamatrix.png")
        };

        // Generate each barcode image and save it to the temporary folder
        foreach (var (encodeType, text, fileName) in samples)
        {
            var generator = new BarcodeGenerator(encodeType, text);
            string filePath = Path.Combine(tempFolder, fileName);
            generator.Save(filePath, BarCodeImageFormat.Png);
        }

        // Create a zip archive containing the generated images
        string zipPath = Path.Combine(Path.GetTempPath(), "BarcodesZip_" + Guid.NewGuid().ToString("N") + ".zip");
        using (FileStream zipToCreate = new FileStream(zipPath, FileMode.Create))
        using (var archive = new ZipArchive(zipToCreate, ZipArchiveMode.Update))
        {
            foreach (string file in Directory.GetFiles(tempFolder))
            {
                string entryName = Path.GetFileName(file);
                archive.CreateEntryFromFile(file, entryName);
            }
        }

        // Process all images in the zip archive and collect barcode information
        var summary = new List<FileSummary>();
        using (FileStream zipToOpen = new FileStream(zipPath, FileMode.Open))
        using (var archive = new ZipArchive(zipToOpen, ZipArchiveMode.Read))
        {
            foreach (var entry in archive.Entries)
            {
                if (IsImageFile(entry.Name))
                {
                    using (var entryStream = entry.Open())
                    using (var ms = new MemoryStream())
                    {
                        // Copy entry data to a memory stream for barcode reading
                        entryStream.CopyTo(ms);
                        ms.Position = 0;

                        // Initialize the barcode reader with the desired symbologies
                        using (var reader = new BarCodeReader(ms,
                            DecodeType.QR,
                            DecodeType.Code128,
                            DecodeType.DataMatrix,
                            DecodeType.Aztec,
                            DecodeType.Pdf417))
                        {
                            var barcodes = new List<BarcodeInfo>();
                            foreach (var result in reader.ReadBarCodes())
                            {
                                barcodes.Add(new BarcodeInfo
                                {
                                    Type = result.CodeTypeName,
                                    Text = result.CodeText
                                });
                            }

                            // Add the file's barcode results to the summary list
                            summary.Add(new FileSummary
                            {
                                FileName = entry.Name,
                                Barcodes = barcodes
                            });
                        }
                    }
                }
            }
        }

        // Serialize the summary list to a formatted JSON string
        string json = JsonSerializer.Serialize(summary, new JsonSerializerOptions { WriteIndented = true });
        string jsonPath = Path.Combine(tempFolder, "summary.json");
        File.WriteAllText(jsonPath, json);

        Console.WriteLine("Processing complete.");
        Console.WriteLine("Summary JSON written to: " + jsonPath);
    }

    /// <summary>
    /// Determines whether a file name has a supported image extension.
    /// </summary>
    /// <param name="fileName">The file name to evaluate.</param>
    /// <returns>True if the file is an image; otherwise, false.</returns>
    static bool IsImageFile(string fileName)
    {
        string ext = Path.GetExtension(fileName).ToLowerInvariant();
        return ext == ".png" || ext == ".jpg" || ext == ".jpeg" || ext == ".bmp" || ext == ".gif" || ext == ".tiff" || ext == ".tif";
    }
}

/// <summary>
/// Represents the barcode detection results for a single file.
/// </summary>
class FileSummary
{
    public string FileName { get; set; }
    public List<BarcodeInfo> Barcodes { get; set; }
}

/// <summary>
/// Holds information about a detected barcode.
/// </summary>
class BarcodeInfo
{
    public string Type { get; set; }
    public string Text { get; set; }
}