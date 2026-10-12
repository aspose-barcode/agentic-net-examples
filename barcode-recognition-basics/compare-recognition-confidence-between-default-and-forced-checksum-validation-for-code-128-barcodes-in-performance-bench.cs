// Title: Code128 checksum validation performance benchmark
// Description: Demonstrates how to compare barcode recognition confidence and timing when using default versus forced checksum validation for Code 128 barcodes.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition performance category. It shows how to configure the ChecksumValidation property of BarCodeReader, generate a Code128 barcode, and measure decoding speed and confidence. Developers working with barcode validation, quality assessment, or high‑throughput scanning can use these patterns to fine‑tune recognition settings.
// Prompt: Compare recognition confidence between default and forced checksum validation for Code 128 barcodes in a performance benchmark.
// Tags: code128, checksumvalidation, performance, benchmark, confidence, aspose.barcode, barcoderecognition

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides a benchmark that compares default and forced checksum validation
/// for Code 128 barcode recognition using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Code 128 barcode, runs two recognition benchmarks
    /// (default and forced checksum validation), and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the generated image
        string tempFolder = Path.Combine(Path.GetTempPath(), "Code128Benchmark_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "code128.png");

        // Generate a Code128 barcode and save it as PNG
        string codeText = "AsposeCode128";
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Run benchmark with default checksum validation
        BenchmarkRead(imagePath, ChecksumValidation.Default, "Default");

        // Run benchmark with forced checksum validation (On)
        BenchmarkRead(imagePath, ChecksumValidation.On, "On");

        // Attempt to delete the generated image and temporary folder
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Suppress any cleanup exceptions
        }
    }

    /// <summary>
    /// Reads a barcode from the specified image using the given checksum validation mode,
    /// measures execution time, and outputs confidence and other result details.
    /// </summary>
    /// <param name="imagePath">Path to the barcode image file.</param>
    /// <param name="validationMode">Checksum validation mode to apply.</param>
    /// <param name="label">Label used in console output to identify the benchmark run.</param>
    static void BenchmarkRead(string imagePath, ChecksumValidation validationMode, string label)
    {
        // Verify that the image file exists before attempting to read
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Set the decode type to Code128
        BaseDecodeType decodeType = DecodeType.Code128;
        using (BarCodeReader reader = new BarCodeReader(imagePath, decodeType))
        {
            // Apply the requested checksum validation setting
            reader.BarcodeSettings.ChecksumValidation = validationMode;

            // Start timing the read operation
            Stopwatch sw = Stopwatch.StartNew();
            BarCodeResult[] results = reader.ReadBarCodes();
            sw.Stop();

            // Output results if a barcode was detected
            if (results.Length > 0)
            {
                BarCodeResult result = results[0];
                Console.WriteLine($"ChecksumValidation: {label}");
                Console.WriteLine($"  Time (ms): {sw.ElapsedMilliseconds}");
                Console.WriteLine($"  Confidence: {result.Confidence}");
                Console.WriteLine($"  ReadingQuality: {result.ReadingQuality}");
                Console.WriteLine($"  CodeText: {result.CodeText}");
                Console.WriteLine($"  CodeType: {result.CodeTypeName}");
            }
            else
            {
                // No barcode detected; still report elapsed time
                Console.WriteLine($"ChecksumValidation: {label} - No barcode detected.");
                Console.WriteLine($"  Time (ms): {sw.ElapsedMilliseconds}");
            }
        }
    }
}