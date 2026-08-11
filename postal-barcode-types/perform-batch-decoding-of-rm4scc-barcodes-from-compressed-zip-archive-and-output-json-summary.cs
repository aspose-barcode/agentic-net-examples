// Title: Batch decode RM4SCC barcodes from a ZIP archive and output JSON summary
// Description: Demonstrates how to generate sample RM4SCC barcode images, package them into a ZIP file, decode each image, and produce a JSON report of the decoded values.
// Category-Description: This example belongs to the Aspose.BarCode barcode processing category, focusing on batch decoding of images stored in compressed archives. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for recognition, and .NET ZipArchive for handling ZIP files. Developers often need to process large sets of barcode images efficiently, and this pattern provides a reusable approach for such scenarios.
// Prompt: Perform batch decoding of RM4SCC barcodes from a compressed ZIP archive and output JSON summary.
// Tags: rm4scc, batch decoding, zip, json, aspose.barcode, barcodegeneration, barcoderecognition

using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch decoding of RM4SCC barcodes from a ZIP archive and outputs a JSON summary.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a sample ZIP if missing, decodes barcodes, and prints JSON.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Path for the sample ZIP archive
        string zipPath = Path.Combine(Directory.GetCurrentDirectory(), "rm4scc_barcodes.zip");

        // Ensure a sample ZIP exists (generate if missing)
        if (!File.Exists(zipPath))
        {
            CreateSampleZip(zipPath);
        }

        // Decode all RM4SCC barcodes inside the ZIP and collect results
        var summary = DecodeBarcodesFromZip(zipPath);

        // Serialize summary to JSON and output to console
        string json = JsonSerializer.Serialize(summary, new JsonSerializerOptions { WriteIndented = true });
        Console.WriteLine(json);
    }

    // Generates a ZIP file containing a few RM4SCC barcode images
    private static void CreateSampleZip(string zipPath)
    {
        // Temporary folder to hold generated images before zipping
        string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);

        try
        {
            // Create 3 sample barcodes
            for (int i = 1; i <= 3; i++)
            {
                string codeText = $"AB{i:D2}CD"; // Simple 6‑character code suitable for RM4SCC
                string fileName = $"barcode_{i}.png";
                string filePath = Path.Combine(tempDir, fileName);

                using (var generator = new BarcodeGenerator(EncodeTypes.RM4SCC, codeText))
                {
                    // Save directly to file as PNG
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }
            }

            // Create ZIP archive from the generated files
            using (var zipToCreate = new FileStream(zipPath, FileMode.Create))
            using (var archive = new ZipArchive(zipToCreate, ZipArchiveMode.Create))
            {
                foreach (string file in Directory.GetFiles(tempDir, "*.png"))
                {
                    string entryName = Path.GetFileName(file);
                    archive.CreateEntryFromFile(file, entryName);
                }
            }
        }
        finally
        {
            // Clean up temporary files
            if (Directory.Exists(tempDir))
            {
                Directory.Delete(tempDir, true);
            }
        }
    }

    // Reads the ZIP archive, decodes RM4SCC barcodes, and returns a summary object
    private static List<DecodedFile> DecodeBarcodesFromZip(string zipPath)
    {
        var results = new List<DecodedFile>();

        using (var zipStream = new FileStream(zipPath, FileMode.Open, FileAccess.Read))
        using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Read))
        {
            foreach (var entry in archive.Entries)
            {
                // Process only image files (PNG/JPG/BMP)
                if (!entry.FullName.EndsWith(".png", StringComparison.OrdinalIgnoreCase) &&
                    !entry.FullName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) &&
                    !entry.FullName.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) &&
                    !entry.FullName.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                using (var entryStream = entry.Open())
                using (var memory = new MemoryStream())
                {
                    // Copy entry data to memory for decoding
                    entryStream.CopyTo(memory);
                    memory.Position = 0;

                    var decodedTexts = new List<string>();

                    // Use BarCodeReader with RM4SCC decode type
                    using (var reader = new BarCodeReader(memory, DecodeType.RM4SCC))
                    {
                        foreach (var result in reader.ReadBarCodes())
                        {
                            if (!string.IsNullOrEmpty(result.CodeText))
                            {
                                decodedTexts.Add(result.CodeText);
                            }
                        }
                    }

                    // Store decoding results for this file
                    results.Add(new DecodedFile
                    {
                        FileName = entry.FullName,
                        Codes = decodedTexts
                    });
                }
            }
        }

        return results;
    }

    // Helper class to hold decoding results per file
    private class DecodedFile
    {
        public string FileName { get; set; }
        public List<string> Codes { get; set; }
    }
}