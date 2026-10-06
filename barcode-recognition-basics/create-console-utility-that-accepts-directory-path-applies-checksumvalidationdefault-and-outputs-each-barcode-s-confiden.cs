// Title: Barcode Confidence Level Reader with Checksum Validation
// Description: Demonstrates reading barcode images from a directory, applying default checksum validation, and printing each barcode's confidence level.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create sample barcodes and BarCodeReader with BarcodeSettings.ChecksumValidation to decode them. Typical scenarios include batch processing of scanned images, validating data integrity, and retrieving confidence metrics for quality assessment. Developers often need to iterate over files, configure decoding options, and extract result details such as code type and confidence.
// Prompt: Create a console utility that accepts a directory path, applies ChecksumValidation.Default, and outputs each barcode's confidence level.
// Tags: barcode symbology, checksum validation, confidence level, console utility, aspose.barcode, generation, recognition

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Console utility that generates sample barcodes, reads them from a directory,
/// applies default checksum validation, and writes each barcode's confidence level to the console.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts an optional directory path argument, creates sample barcodes if needed,
    /// and processes each image file to display barcode type and confidence.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument is the target directory.</param>
    static void Main(string[] args)
    {
        // Determine working directory: use supplied argument or create a temporary folder
        string baseDir = args.Length > 0
            ? args[0]
            : Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));

        // Ensure the base directory exists
        if (!Directory.Exists(baseDir))
        {
            Directory.CreateDirectory(baseDir);
        }

        // Create a dedicated subfolder for sample barcodes
        string sampleDir = Path.Combine(baseDir, "Samples_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(sampleDir);

        // Define sample barcodes to generate (type, text, file name)
        var samples = new List<(BaseEncodeType type, string text, string fileName)>
        {
            (EncodeTypes.Code128, "ABC123", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png"),
            (EncodeTypes.DataMatrix, "DataMatrixSample", "datamatrix.png"),
            (EncodeTypes.Pdf417, "Pdf417SampleText", "pdf417.png"),
            (EncodeTypes.Code39, "CODE39", "code39.png")
        };

        // Generate sample barcode images and save them as PNG files
        foreach (var (type, text, fileName) in samples)
        {
            string filePath = Path.Combine(sampleDir, fileName);
            using (var generator = new BarcodeGenerator(type, text))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // Supported image extensions for barcode scanning
        string[] imageExtensions = new[] { ".png", ".jpg", ".jpeg", ".bmp", ".gif" };

        // Process each image file in the sample directory
        foreach (string file in Directory.GetFiles(sampleDir))
        {
            // Skip files that are not supported image types
            if (Array.IndexOf(imageExtensions, Path.GetExtension(file).ToLowerInvariant()) < 0)
                continue;

            // Initialize the barcode reader for all supported symbologies
            using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
            {
                // Apply default checksum validation as required
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Default;

                // Read all barcodes found in the image and output their confidence levels
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"{Path.GetFileName(file)} - {result.CodeTypeName}: Confidence={result.Confidence}");
                }
            }
        }
    }
}