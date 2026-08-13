// Title: Batch barcode reading with confidence and quality metrics
// Description: Demonstrates how to read multiple barcode images from a folder (or network share) and capture each barcode's confidence and reading quality.
// Category-Description: This example belongs to the Aspose.BarCode reading category, showcasing the use of BarCodeReader, DecodeType, and related result properties. Typical scenarios include bulk processing of scanned documents, inventory verification, and quality assessment of barcode captures. Developers often need to iterate over image collections, extract barcode data, and evaluate confidence and reading quality to ensure reliable downstream processing.
// Prompt: Batch read multiple barcode images from a network share, capturing Confidence and ReadingQuality for each file.
// Tags: code128, qr, datamatrix, batch-read, confidence, readingquality, console-output, barcodereader, barcodegenerator, decodetype, encodetypes

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that batch‑reads barcode images from a folder (or network share),
/// printing each barcode's type, text, confidence, and reading quality.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts an optional folder path argument; otherwise uses a default "Barcodes" folder.
    /// Generates sample barcodes if none are found, then reads all supported images.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument may specify the barcode folder path.</param>
    static void Main(string[] args)
    {
        // Determine the folder containing barcode images.
        // In production replace this with a UNC path, e.g. @"\\Server\Share\Barcodes".
        string folderPath = args.Length > 0 ? args[0] : "Barcodes";

        // Ensure the target folder exists.
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // Check whether the folder already contains supported image files.
        bool hasImages = Directory.GetFiles(folderPath, "*.png").Length > 0 ||
                         Directory.GetFiles(folderPath, "*.jpg").Length > 0 ||
                         Directory.GetFiles(folderPath, "*.bmp").Length > 0;

        // If no images are present, generate a few sample barcode files.
        if (!hasImages)
        {
            GenerateSampleBarcodes(folderPath);
        }

        // Gather all supported image files (PNG, JPG, BMP) into a single array.
        string[] pngFiles = Directory.GetFiles(folderPath, "*.png");
        string[] jpgFiles = Directory.GetFiles(folderPath, "*.jpg");
        string[] bmpFiles = Directory.GetFiles(folderPath, "*.bmp");

        string[] allFiles = new string[pngFiles.Length + jpgFiles.Length + bmpFiles.Length];
        pngFiles.CopyTo(allFiles, 0);
        jpgFiles.CopyTo(allFiles, pngFiles.Length);
        bmpFiles.CopyTo(allFiles, pngFiles.Length + jpgFiles.Length);

        // Iterate over each image file and attempt to read any barcodes it contains.
        foreach (string filePath in allFiles)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Use BarCodeReader with AllSupportedTypes to detect any barcode format.
            using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                // ReadBarCodes returns an enumerable of detection results.
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(filePath)}");
                    Console.WriteLine($"  Type: {result.CodeTypeName}");
                    Console.WriteLine($"  CodeText: {result.CodeText}");
                    Console.WriteLine($"  Confidence: {result.Confidence}");
                    Console.WriteLine($"  ReadingQuality: {result.ReadingQuality}");
                }
            }
        }
    }

    /// <summary>
    /// Generates a set of sample barcode images (Code128, QR, DataMatrix) in the specified folder.
    /// </summary>
    /// <param name="folderPath">The directory where sample images will be saved.</param>
    private static void GenerateSampleBarcodes(string folderPath)
    {
        // Sample 1: Code128 barcode.
        string code128Path = Path.Combine(folderPath, "sample_code128.png");
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            generator.Save(code128Path);
        }

        // Sample 2: QR Code.
        string qrPath = Path.Combine(folderPath, "sample_qr.png");
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            generator.Save(qrPath);
        }

        // Sample 3: DataMatrix barcode.
        string dmPath = Path.Combine(folderPath, "sample_datamatrix.png");
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "DM123456"))
        {
            generator.Save(dmPath);
        }
    }
}