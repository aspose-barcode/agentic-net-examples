// Title: Barcode Confidence Level Reader
// Description: Demonstrates reading barcodes from image files, applying default checksum validation, and displaying each barcode's confidence (reading quality) level.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing the use of BarCodeReader, BarcodeSettings, and ChecksumValidation to evaluate detection confidence. Typical use cases include batch processing of scanned documents, validating barcode integrity, and extracting quality metrics for downstream analysis. Developers often need to iterate over image files, configure decoding options, and retrieve reading quality scores.
// Prompt: Create a console utility that accepts a directory path, applies ChecksumValidation.Default, and outputs each barcode's confidence level.
// Tags: barcode, checksumvalidation, confidence, readingquality, aspose.barcode, console, batch-processing

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Console utility that reads barcode images from a directory, applies default checksum validation,
/// and prints each barcode's confidence (reading quality) level.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts an optional directory path argument; if omitted, generates sample barcodes in a temporary folder.
    /// Processes each PNG image, reads barcodes with default checksum validation, and writes confidence values to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments; the first argument may be a folder path.</param>
    static void Main(string[] args)
    {
        // Determine the folder to process: use argument if valid, otherwise create a temporary folder with sample barcodes.
        string folderPath;
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
        {
            folderPath = args[0];
            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"Directory does not exist: {folderPath}");
                return;
            }
        }
        else
        {
            // Create a temporary folder and generate sample barcode images.
            folderPath = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(folderPath);
            GenerateSampleBarcodes(folderPath);
        }

        // Collect all PNG files in the target folder.
        List<string> barcodeFiles = new List<string>();
        foreach (string file in Directory.GetFiles(folderPath, "*.png"))
        {
            barcodeFiles.Add(file);
        }

        if (barcodeFiles.Count == 0)
        {
            Console.WriteLine("No barcode images found to process.");
            return;
        }

        // Process each image file.
        foreach (string filePath in barcodeFiles)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Initialize the reader for all supported barcode types.
            using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                // Apply the default checksum validation setting.
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Default;

                bool anyResult = false;
                // Iterate through all detected barcodes.
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    anyResult = true;
                    Console.WriteLine($"{Path.GetFileName(filePath)}: Confidence = {result.ReadingQuality}");
                }

                if (!anyResult)
                {
                    Console.WriteLine($"{Path.GetFileName(filePath)}: No barcode detected.");
                }
            }
        }
    }

    // Generates a few sample barcode images in the specified folder.
    private static void GenerateSampleBarcodes(string folder)
    {
        var samples = new List<(BaseEncodeType type, string text, string fileName)>
        {
            (EncodeTypes.Code128, "1234567890", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png"),
            (EncodeTypes.DataMatrix, "SampleDM", "datamatrix.png")
        };

        foreach (var (type, text, fileName) in samples)
        {
            string filePath = Path.Combine(folder, fileName);
            using (BarcodeGenerator generator = new BarcodeGenerator(type, text))
            {
                // Save the generated barcode as a PNG image.
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }
    }
}