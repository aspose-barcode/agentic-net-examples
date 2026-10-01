// Title: Read barcodes from a zip archive and aggregate metadata
// Description: Generates sample barcode images, zips them, extracts the archive, reads each barcode, and outputs aggregated metadata as CSV.
// Category-Description: This example demonstrates Aspose.BarCode generation and recognition APIs. It shows how to create barcodes with BarcodeGenerator, package them using System.IO.Compression, and then read them with BarCodeReader. Typical scenarios include batch processing of scanned documents, automated inventory checks, and bulk data extraction where developers need to handle multiple images stored in archives.
// Prompt: Read barcodes from a zip archive containing multiple image files and aggregate metadata.
// Tags: barcode generation, barcode recognition, zip archive, metadata aggregation, csv output, aspose.barcode, c#

using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to generate barcodes, package them into a zip file,
/// extract the archive, read each barcode, and aggregate its metadata.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the full workflow from generation to cleanup.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Create a temporary folder for generated barcode images.
        // --------------------------------------------------------------------
        string generateFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(generateFolder);

        // --------------------------------------------------------------------
        // 2. Define sample barcodes to be generated.
        // --------------------------------------------------------------------
        var samples = new List<(BaseEncodeType encodeType, string codeText, string fileName)>
        {
            (EncodeTypes.Code128, "ABC123456", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png"),
            (EncodeTypes.DataMatrix, "DM12345", "datamatrix.png")
        };

        // --------------------------------------------------------------------
        // 3. Generate barcode images and save them as PNG files.
        // --------------------------------------------------------------------
        foreach (var (encodeType, codeText, fileName) in samples)
        {
            string filePath = Path.Combine(generateFolder, fileName);
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Save the generated barcode image.
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // --------------------------------------------------------------------
        // 4. Create a zip archive that contains the generated images.
        // --------------------------------------------------------------------
        string zipPath = Path.Combine(Path.GetTempPath(),
            "BarcodesArchive_" + Guid.NewGuid().ToString("N") + ".zip");
        ZipFile.CreateFromDirectory(generateFolder, zipPath);

        // --------------------------------------------------------------------
        // 5. Extract the zip archive to a new temporary folder for reading.
        // --------------------------------------------------------------------
        string extractFolder = Path.Combine(Path.GetTempPath(),
            "Extracted_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(extractFolder);
        ZipFile.ExtractToDirectory(zipPath, extractFolder);

        // --------------------------------------------------------------------
        // 6. Prepare a collection to hold aggregated barcode metadata.
        // --------------------------------------------------------------------
        var aggregated = new List<BarcodeMetadata>();

        // --------------------------------------------------------------------
        // 7. Iterate over each extracted file and attempt to read barcodes.
        // --------------------------------------------------------------------
        string[] files = Directory.GetFiles(extractFolder);
        foreach (string file in files)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                // Initialize the reader for all supported barcode types.
                using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
                {
                    // Read all barcodes present in the image.
                    BarCodeResult[] results = reader.ReadBarCodes();
                    foreach (var result in results)
                    {
                        // Capture required metadata for each barcode.
                        var meta = new BarcodeMetadata
                        {
                            FileName = Path.GetFileName(file),
                            CodeText = result.CodeText,
                            Symbology = result.CodeTypeName,
                            ReadingQuality = result.ReadingQuality,
                            RegionX = result.Region.Rectangle.X,
                            RegionY = result.Region.Rectangle.Y,
                            RegionWidth = result.Region.Rectangle.Width,
                            RegionHeight = result.Region.Rectangle.Height,
                            Angle = result.Region.Angle
                        };
                        aggregated.Add(meta);
                    }
                }
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                // Skip files that cannot be loaded as images (e.g., non‑image entries).
                Console.WriteLine($"Skipping unsupported file: {file}");
            }
            catch (Exception ex)
            {
                // Log any unexpected errors but continue processing remaining files.
                Console.WriteLine($"Error processing file '{file}': {ex.Message}");
            }
        }

        // --------------------------------------------------------------------
        // 8. Output the aggregated metadata as CSV to the console.
        // --------------------------------------------------------------------
        Console.WriteLine("FileName,CodeText,Symbology,ReadingQuality,RegionX,RegionY,RegionWidth,RegionHeight,Angle");
        foreach (var meta in aggregated)
        {
            Console.WriteLine($"{meta.FileName},{meta.CodeText},{meta.Symbology},{meta.ReadingQuality}," +
                              $"{meta.RegionX},{meta.RegionY},{meta.RegionWidth},{meta.RegionHeight},{meta.Angle}");
        }

        // --------------------------------------------------------------------
        // 9. Clean up temporary directories and the zip file.
        // --------------------------------------------------------------------
        try
        {
            Directory.Delete(generateFolder, true);
            Directory.Delete(extractFolder, true);
            if (File.Exists(zipPath))
                File.Delete(zipPath);
        }
        catch
        {
            // Ignored – cleanup failures should not crash the program.
        }
    }

    // Simple DTO for aggregated barcode information.
    class BarcodeMetadata
    {
        public string FileName { get; set; }
        public string CodeText { get; set; }
        public string Symbology { get; set; }
        public double ReadingQuality { get; set; }
        public float RegionX { get; set; }
        public float RegionY { get; set; }
        public float RegionWidth { get; set; }
        public float RegionHeight { get; set; }
        public double Angle { get; set; }
    }
}