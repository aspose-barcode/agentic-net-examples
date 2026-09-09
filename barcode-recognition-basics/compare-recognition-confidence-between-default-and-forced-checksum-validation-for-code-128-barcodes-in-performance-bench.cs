// Title: Code128 checksum validation benchmark comparing confidence levels
// Description: Demonstrates generating a Code 128 barcode, then measuring recognition confidence and performance with default checksum validation versus checksum validation turned off.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them, focusing on checksum validation settings. Developers often need to evaluate how checksum handling impacts detection confidence and processing speed in high‑throughput or quality‑critical applications.
// Prompt: Compare recognition confidence between default and forced checksum validation for Code 128 barcodes in a performance benchmark.
// Tags: code128, checksumvalidation, benchmark, confidence, barcode, generation, recognition, aspose.barcode

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that benchmarks barcode recognition confidence for Code 128
/// with different checksum validation settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code 128 barcode, then runs two benchmarks:
    /// one with default checksum validation and one with checksum validation disabled.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the benchmark files
        string tempFolder = Path.Combine(Path.GetTempPath(), "Code128Benchmark_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the output image path and the barcode text
        string barcodePath = Path.Combine(tempFolder, "code128.png");
        string codeText = "Aspose123";

        // Generate a Code128 barcode image
        BaseEncodeType encodeType = EncodeTypes.Code128;
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Set X-dimension to improve image clarity
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to generate barcode image.");
            return;
        }

        // Benchmark reading with default checksum validation
        BenchmarkRead(barcodePath, ChecksumValidation.Default, "Default");

        // Benchmark reading with checksum validation forced off
        BenchmarkRead(barcodePath, ChecksumValidation.Off, "Off");
    }

    /// <summary>
    /// Executes a read benchmark for a given checksum validation mode and reports confidence, quality, and elapsed time.
    /// </summary>
    /// <param name="imagePath">Path to the barcode image.</param>
    /// <param name="validationMode">Checksum validation setting to apply.</param>
    /// <param name="modeName">Friendly name for the mode (used in output).</param>
    static void BenchmarkRead(string imagePath, ChecksumValidation validationMode, string modeName)
    {
        // Specify that we are decoding Code128 barcodes
        BaseDecodeType decodeType = DecodeType.Code128;
        var stopwatch = new Stopwatch();

        using (var reader = new BarCodeReader(imagePath, decodeType))
        {
            // Apply the requested checksum validation mode
            reader.BarcodeSettings.ChecksumValidation = validationMode;

            // Start timing the read operation
            stopwatch.Start();
            BarCodeResult[] results = reader.ReadBarCodes();
            stopwatch.Stop();

            // Handle case where no barcode was detected
            if (results.Length == 0)
            {
                Console.WriteLine($"[{modeName}] No barcode detected.");
                return;
            }

            // Output confidence, reading quality, and elapsed time
            BarCodeResult result = results[0];
            Console.WriteLine($"[{modeName}] Confidence: {result.Confidence}");
            Console.WriteLine($"[{modeName}] ReadingQuality: {result.ReadingQuality}");
            Console.WriteLine($"[{modeName}] Elapsed ms: {stopwatch.ElapsedMilliseconds}");
        }
    }
}