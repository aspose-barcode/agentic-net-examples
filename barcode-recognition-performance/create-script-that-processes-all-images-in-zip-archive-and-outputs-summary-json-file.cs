// Title: Process barcodes in zipped images and generate JSON summary
// Description: Generates sample barcode images, archives them, scans each image for barcodes, and outputs a JSON file summarising the findings.
// Category-Description: This example demonstrates batch barcode generation and recognition using Aspose.BarCode. It covers creating barcodes with BarcodeGenerator, packaging files with System.IO.Compression, reading barcodes via BarCodeReader, and serialising results to JSON. Developers working on bulk scanning, archive processing, or reporting will find the use of EncodeTypes, DecodeType, BarCodeImageFormat and related classes especially useful.
// Prompt: Create a script that processes all images in a zip archive and outputs a summary JSON file.
// Tags: barcode symbology, generation, recognition, zip, json, aspose.barcode, csharp

using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates creating barcode images, zipping them, scanning each image for barcodes,
/// and writing a JSON summary of the detected codes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the full workflow from generation to JSON output.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Create a temporary folder to store generated barcode images.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // --------------------------------------------------------------------
        // 2. Define sample barcodes to generate (type, text, file name).
        // --------------------------------------------------------------------
        var samples = new List<(BaseEncodeType type, string text, string fileName)>
        {
            (EncodeTypes.Code128, "CODE128_SAMPLE", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png"),
            (EncodeTypes.DataMatrix, "DMATRIX", "datamatrix.png"),
            (EncodeTypes.Pdf417, "PDF417_SAMPLE", "pdf417.png")
        };

        // --------------------------------------------------------------------
        // 3. Generate each barcode image and save it as PNG.
        // --------------------------------------------------------------------
        foreach (var (type, text, fileName) in samples)
        {
            string filePath = Path.Combine(tempFolder, fileName);
            var generator = new BarcodeGenerator(type, text);
            generator.Save(filePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // 4. Archive the generated images into a ZIP file.
        // --------------------------------------------------------------------
        string zipPath = Path.Combine(Path.GetTempPath(), "BarcodesArchive_" + Guid.NewGuid().ToString("N") + ".zip");
        ZipFile.CreateFromDirectory(tempFolder, zipPath);

        // --------------------------------------------------------------------
        // 5. Prepare a collection to hold barcode detection results.
        // --------------------------------------------------------------------
        var summary = new List<BarcodeInfo>();

        // --------------------------------------------------------------------
        // 6. Validate that the ZIP archive exists before processing.
        // --------------------------------------------------------------------
        if (!File.Exists(zipPath))
        {
            Console.WriteLine($"Zip file not found: {zipPath}");
            return;
        }

        // --------------------------------------------------------------------
        // 7. Open the ZIP archive and iterate over each entry.
        // --------------------------------------------------------------------
        using (var zip = ZipFile.OpenRead(zipPath))
        {
            foreach (var entry in zip.Entries)
            {
                // Filter out non‑image files based on common extensions.
                string ext = Path.GetExtension(entry.FullName).ToLowerInvariant();
                if (ext != ".png" && ext != ".jpg" && ext != ".jpeg" && ext != ".bmp" && ext != ".gif" && ext != ".tiff")
                    continue;

                // ----------------------------------------------------------------
                // 8. Load the image entry into memory for barcode recognition.
                // ----------------------------------------------------------------
                using (var entryStream = entry.Open())
                using (var ms = new MemoryStream())
                {
                    entryStream.CopyTo(ms);
                    ms.Position = 0;

                    // ----------------------------------------------------------------
                    // 9. Recognise supported barcode types within the image.
                    // ----------------------------------------------------------------
                    using (var reader = new BarCodeReader(ms,
                        DecodeType.QR,
                        DecodeType.Code128,
                        DecodeType.DataMatrix,
                        DecodeType.Pdf417,
                        DecodeType.Aztec,
                        DecodeType.EAN13))
                    {
                        foreach (var result in reader.ReadBarCodes())
                        {
                            // Store each detected barcode's details.
                            summary.Add(new BarcodeInfo
                            {
                                FileName = entry.Name,
                                CodeType = result.CodeTypeName,
                                CodeText = result.CodeText
                            });
                        }
                    }
                }
            }
        }

        // --------------------------------------------------------------------
        // 10. Serialise the summary list to a formatted JSON file.
        // --------------------------------------------------------------------
        string jsonOutputPath = Path.Combine(tempFolder, "summary.json");
        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(summary, jsonOptions);
        File.WriteAllText(jsonOutputPath, json);

        // --------------------------------------------------------------------
        // 11. Report the processing outcome.
        // --------------------------------------------------------------------
        Console.WriteLine($"Processed {summary.Count} barcode(s). Summary written to: {jsonOutputPath}");
    }

    /// <summary>
    /// Simple DTO representing a detected barcode within an image file.
    /// </summary>
    class BarcodeInfo
    {
        public string FileName { get; set; }
        public string CodeType { get; set; }
        public string CodeText { get; set; }
    }
}