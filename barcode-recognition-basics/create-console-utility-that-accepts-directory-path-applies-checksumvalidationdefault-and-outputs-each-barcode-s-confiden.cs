// Title: Barcode Confidence Reader Console Utility
// Description: Demonstrates reading barcodes from images in a directory, applying default checksum validation, and printing each barcode's confidence level.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing the use of BarCodeReader, BarcodeSettings, and DecodeType to process multiple image formats. Typical use cases include batch processing of scanned documents, validating barcode integrity, and extracting confidence metrics for quality control. Developers often need to configure checksum validation and iterate over detection results, as illustrated here.
// Prompt: Create a console utility that accepts a directory path, applies ChecksumValidation.Default, and outputs each barcode's confidence level.
// Tags: barcode, checksumvalidation, confidence, console, aspnet, aspnetcore, aspnet-barcode, aspose.barcode, barcode-recognition, image-processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Console utility that reads barcodes from image files in a specified directory,
/// applies default checksum validation, and outputs each barcode's confidence level.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts an optional directory path argument, generates sample barcodes if needed,
    /// and processes each image file to display barcode information.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument may be a directory path.</param>
    static void Main(string[] args)
    {
        // Resolve the target folder: use the first argument if provided, otherwise create a temporary folder.
        string folderPath;
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
        {
            folderPath = args[0];
        }
        else
        {
            // Create a temporary folder named "BarcodesSample" in the current working directory.
            folderPath = Path.Combine(Directory.GetCurrentDirectory(), "BarcodesSample");
            Directory.CreateDirectory(folderPath);
        }

        // Verify that the folder exists before proceeding.
        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine($"Directory does not exist: {folderPath}");
            return;
        }

        // If the folder is empty, generate a few sample barcode images for demonstration.
        var sampleFiles = Directory.GetFiles(folderPath, "*.png");
        if (sampleFiles.Length == 0)
        {
            GenerateSampleBarcodes(folderPath);
        }

        // Define the image file patterns to process (PNG, JPG, BMP).
        string[] patterns = new[] { "*.png", "*.jpg", "*.bmp" };

        // Iterate over each pattern and process matching files.
        foreach (var pattern in patterns)
        {
            foreach (var filePath in Directory.GetFiles(folderPath, pattern))
            {
                // Guard against missing files (should not happen, but defensive programming).
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"File not found: {filePath}");
                    continue;
                }

                // Open the image with BarCodeReader, requesting all supported barcode types.
                using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
                {
                    // Apply the default checksum validation setting.
                    reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Default;

                    bool anyFound = false;

                    // Enumerate all detected barcodes in the image.
                    foreach (var result in reader.ReadBarCodes())
                    {
                        anyFound = true;
                        Console.WriteLine($"File: {Path.GetFileName(filePath)}");
                        Console.WriteLine($"  Type: {result.CodeTypeName}");
                        Console.WriteLine($"  CodeText: {result.CodeText}");
                        Console.WriteLine($"  Confidence: {result.Confidence}");
                    }

                    // If no barcodes were detected, inform the user.
                    if (!anyFound)
                    {
                        Console.WriteLine($"No barcode detected in file: {Path.GetFileName(filePath)}");
                    }
                }
            }
        }
    }

    // Generates a few barcode images into the specified folder for demonstration purposes.
    private static void GenerateSampleBarcodes(string folder)
    {
        // Code128 barcode.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            string path = Path.Combine(folder, "code128.png");
            generator.Save(path);
        }

        // QR code.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "QR Sample Text"))
        {
            string path = Path.Combine(folder, "qr.png");
            generator.Save(path);
        }

        // EAN13 barcode (requires a valid 12‑digit code; checksum is added automatically).
        using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, "590123412345"))
        {
            string path = Path.Combine(folder, "ean13.png");
            generator.Save(path);
        }
    }
}