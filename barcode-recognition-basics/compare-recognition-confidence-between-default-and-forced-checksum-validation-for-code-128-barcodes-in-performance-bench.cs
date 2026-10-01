// Title: Compare Code128 checksum validation impact on reading quality
// Description: Demonstrates how default and forced checksum validation affect the ReadingQuality metric for Code 128 barcodes.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition performance benchmarks. It shows how to generate Code 128 barcodes, read them with different checksum validation settings, and compare the ReadingQuality values. Key API classes include BarcodeGenerator, BarCodeReader, QualitySettings, and ChecksumValidation. Developers use such benchmarks to fine‑tune barcode scanning accuracy and speed in high‑throughput applications.
// Prompt: Compare recognition confidence between default and forced checksum validation for Code 128 barcodes in a performance benchmark.
// Tags: code128, checksumvalidation, readingquality, performance, benchmark, barcode, generation, recognition, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates comparison of reading quality for Code 128 barcodes with default and forced checksum validation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample Code 128 barcodes, reads them under two checksum settings, and outputs the confidence scores.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Code128Benchmark_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Sample code texts for Code128 barcodes
        List<string> codeTexts = new List<string>
        {
            "ABC123",
            "9876543210",
            "Aspose2024",
            "CODE128TEST",
            "12345XYZ"
        };

        // Generate barcode images and collect their file paths
        List<string> imagePaths = new List<string>();
        foreach (string text in codeTexts)
        {
            string imagePath = Path.Combine(tempFolder, $"{text}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                // No special settings; use defaults
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }
            imagePaths.Add(imagePath);
        }

        Console.WriteLine("Recognition confidence comparison (ReadingQuality) for Code128 barcodes:");
        Console.WriteLine();

        // Process each generated image
        foreach (string path in imagePaths)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($"File not found: {path}");
                continue;
            }

            // Default reader (checksum validation uses default setting)
            double defaultQuality = ReadBarcode(path, applyForcedChecksum: false, out string decodedTextDefault);

            // Forced checksum validation (explicitly set to On)
            double forcedQuality = ReadBarcode(path, applyForcedChecksum: true, out string decodedTextForced);

            Console.WriteLine($"File: {Path.GetFileName(path)}");
            Console.WriteLine($"  Decoded Text (default): {decodedTextDefault}");
            Console.WriteLine($"  ReadingQuality (default): {defaultQuality}");
            Console.WriteLine($"  Decoded Text (forced checksum): {decodedTextForced}");
            Console.WriteLine($"  ReadingQuality (forced checksum): {forcedQuality}");
            Console.WriteLine();
        }

        // Cleanup temporary files and folder
        try
        {
            foreach (string file in imagePaths)
            {
                File.Delete(file);
            }
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }

    /// <summary>
    /// Reads a barcode image and returns the ReadingQuality.
    /// If <paramref name="applyForcedChecksum"/> is true, checksum validation is explicitly set to On.
    /// </summary>
    /// <param name="imagePath">Path to the barcode image.</param>
    /// <param name="applyForcedChecksum">Whether to force checksum validation.</param>
    /// <param name="decodedText">Outputs the decoded barcode text.</param>
    /// <returns>The ReadingQuality value; 0.0 if no barcode is found.</returns>
    private static double ReadBarcode(string imagePath, bool applyForcedChecksum, out string decodedText)
    {
        decodedText = string.Empty;
        BaseDecodeType decodeType = DecodeType.Code128; // Code128 decode type

        using (var reader = new BarCodeReader(imagePath, decodeType))
        {
            // Optionally enforce checksum validation
            if (applyForcedChecksum)
            {
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
            }

            // Use a high-performance quality preset for speed
            reader.QualitySettings = QualitySettings.HighPerformance;

            // Read all barcodes; return the first result's quality
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Reaching this point means checksum passed (if validation is On)
                decodedText = result.CodeText;
                return result.ReadingQuality;
            }
        }

        // If no result was found, return 0 quality
        return 0.0;
    }
}