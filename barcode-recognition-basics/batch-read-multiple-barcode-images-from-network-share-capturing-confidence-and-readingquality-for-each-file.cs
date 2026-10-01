// Title: Batch barcode reading with confidence and quality metrics
// Description: Demonstrates how to read multiple barcode images from a folder, extracting the decoded text along with confidence and reading quality values.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing the BarCodeReader class for bulk processing of images. Typical use cases include scanning batches of scanned documents or network‑shared files to retrieve barcode data and quality metrics, helping developers assess detection reliability. Developers often need to iterate over files, handle unsupported formats, and capture confidence scores for downstream validation.
// Prompt: Batch read multiple barcode images from a network share, capturing Confidence and ReadingQuality for each file.
// Tags: barcode, batch processing, confidence, readingquality, barcodereader, aspose.barcode, csharp, image recognition

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch reading of barcode images, extracting code text, confidence, and reading quality.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, reads them, and outputs detection details.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BatchRead_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample barcode images and collect their file paths
        List<string> barcodeFiles = new List<string>();
        GenerateSampleBarcodes(tempFolder, barcodeFiles);

        // Iterate over each generated file and attempt to read barcodes
        foreach (string filePath in barcodeFiles)
        {
            // Verify that the file exists before processing
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Initialize the reader for all supported barcode types
                using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
                {
                    // Read all barcodes present in the image
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // If no barcodes were detected, report and move to the next file
                    if (results.Length == 0)
                    {
                        Console.WriteLine($"No barcode detected in file: {Path.GetFileName(filePath)}");
                        continue;
                    }

                    // Output details for each detected barcode
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"File: {Path.GetFileName(filePath)}");
                        Console.WriteLine($"  CodeText        : {result.CodeText}");
                        Console.WriteLine($"  Confidence      : {result.Confidence}");
                        Console.WriteLine($"  ReadingQuality  : {result.ReadingQuality}");
                    }
                }
            }
            // Handle cases where the image cannot be loaded (e.g., unsupported format)
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                Console.WriteLine($"Skipping unsupported file '{Path.GetFileName(filePath)}': {ex.Message}");
            }
            // Catch any other unexpected errors during processing
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file '{Path.GetFileName(filePath)}': {ex.Message}");
            }
        }

        // Optional cleanup of temporary files
        // Directory.Delete(tempFolder, true);
    }

    /// <summary>
    /// Generates a set of sample barcode images of various symbologies and records their file paths.
    /// </summary>
    /// <param name="folderPath">The directory where barcode images will be saved.</param>
    /// <param name="fileList">A list that will be populated with the full paths of the generated images.</param>
    private static void GenerateSampleBarcodes(string folderPath, List<string> fileList)
    {
        // Define sample data: (symbology, encoded text, output file name)
        var samples = new (BaseEncodeType encodeType, string codeText, string fileName)[]
        {
            (EncodeTypes.Code128, "Sample123", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png"),
            (EncodeTypes.DataMatrix, "DM12345", "datamatrix.png"),
            (EncodeTypes.Pdf417, "PDF417 Sample Text", "pdf417.png"),
            (EncodeTypes.Aztec, "AztecDemo", "aztec.png")
        };

        // Generate each barcode image and add its path to the list
        foreach (var (encodeType, codeText, fileName) in samples)
        {
            string filePath = Path.Combine(folderPath, fileName);
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Save the generated barcode as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            fileList.Add(filePath);
        }
    }
}