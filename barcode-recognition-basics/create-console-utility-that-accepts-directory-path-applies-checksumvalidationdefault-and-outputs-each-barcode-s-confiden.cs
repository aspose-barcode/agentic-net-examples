// Title: Barcode Confidence Level Scanner
// Description: Scans a directory for supported image and PDF files, reads barcodes with default checksum validation, and prints each barcode's confidence level.
// Prompt: Create a console utility that accepts a directory path, applies ChecksumValidation.Default, and outputs each barcode's confidence level.
// Tags: barcode, checksumvalidation, console, confidence, aspose.barcode, file-processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Console utility that scans a directory for barcode images/PDFs,
/// applies default checksum validation, and outputs each barcode's confidence level.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts an optional directory path argument,
    /// processes supported files, and writes barcode details to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument may be a directory path.</param>
    static void Main(string[] args)
    {
        // Determine directory to scan; fallback to current directory if none provided.
        string directoryPath = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

        // Verify that the directory exists before proceeding.
        if (!Directory.Exists(directoryPath))
        {
            Console.WriteLine($"Directory not found: {directoryPath}");
            return;
        }

        // File extensions that Aspose.BarCode can read.
        string[] extensions = new[] { ".png", ".jpg", ".jpeg", ".bmp", ".tif", ".tiff", ".gif", ".pdf" };

        // Enumerate all files in the target directory.
        foreach (string filePath in Directory.GetFiles(directoryPath))
        {
            // Process only supported image/pdf files.
            if (Array.IndexOf(extensions, Path.GetExtension(filePath).ToLowerInvariant()) < 0)
                continue;

            // Double‑check file existence (defensive programming).
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found (skipped): {filePath}");
                continue;
            }

            // Open the barcode reader for the current file.
            using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                // Apply default checksum validation as required.
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Default;

                // Read all barcodes in the file.
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // Output file name, barcode type, and confidence level.
                    Console.WriteLine($"File: {Path.GetFileName(filePath)} | Type: {result.CodeTypeName} | Confidence: {result.Confidence}");
                }
            }
        }
    }
}