// Title: Barcode Generation and Confidence Level Reporting Utility
// Description: Generates sample Code128 and QR barcodes, then reads them applying default checksum validation and prints each barcode's confidence level.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It demonstrates using BarcodeGenerator to create barcodes, BarCodeReader with ChecksumValidation.Default to decode them, and accessing the Confidence property of BarCodeResult. Developers often need to generate barcodes, validate them during recognition, and assess detection confidence for quality control or logging purposes.
// Prompt: Create a console utility that accepts a directory path, applies ChecksumValidation.Default, and outputs each barcode's confidence level.
// Tags: barcode generation, barcode recognition, checksumvalidation, confidence, console utility, aspose.barcode, code128, qr

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating barcodes, reading them with default checksum validation,
/// and outputting each barcode's confidence level.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts an optional directory path, generates sample barcodes,
    /// reads them, and writes confidence information to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument may be a folder path.</param>
    static void Main(string[] args)
    {
        // Determine the working folder: use provided path if valid, otherwise create a temporary folder.
        string folderPath;
        if (args.Length > 0 && Directory.Exists(args[0]))
        {
            folderPath = args[0];
        }
        else
        {
            folderPath = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(folderPath);
        }

        // Collect paths of generated barcode images.
        var generatedFiles = new List<string>();

        // Generate a Code128 barcode and save it as PNG.
        string code128Path = Path.Combine(folderPath, "code128.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            generator.Save(code128Path, BarCodeImageFormat.Png);
        }
        generatedFiles.Add(code128Path);

        // Generate a QR barcode and save it as PNG.
        string qrPath = Path.Combine(folderPath, "qr.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "QRText"))
        {
            generator.Save(qrPath, BarCodeImageFormat.Png);
        }
        generatedFiles.Add(qrPath);

        // Iterate over each generated file, read the barcode, and output confidence information.
        foreach (string filePath in generatedFiles)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Initialize the reader for all supported barcode types.
                using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
                {
                    // Apply default checksum validation during decoding.
                    reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Default;

                    // Read all barcodes in the image and display their confidence levels.
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"File: {Path.GetFileName(filePath)} | Type: {result.CodeTypeName} | Confidence: {result.Confidence}");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                // Handle cases where the file cannot be processed as a barcode.
                Console.WriteLine($"Failed to read '{filePath}': {ex.Message}");
            }
        }
    }
}