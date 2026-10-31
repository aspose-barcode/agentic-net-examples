// Title: Code128 Checksum Validation Confidence Comparison
// Description: Demonstrates generating a Code 128 barcode, then reading it twice—once with default checksum validation and once with checksum validation turned off—to compare the recognition confidence and reading quality.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to create a barcode image with BarcodeGenerator and evaluate it using BarCodeReader. It focuses on checksum validation settings (Default vs Off) and how they affect confidence scores, a common requirement for developers optimizing barcode scanning performance and accuracy. Key API classes include BarcodeGenerator, BarCodeReader, BarCodeResult, BarcodeSettings, and ChecksumValidation.
// Prompt: Compare recognition confidence between default and forced checksum validation for Code 128 barcodes in a performance benchmark.
// Tags: code128, checksumvalidation, confidence, barcode, generation, recognition, aspose.barcode, performance, benchmark

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a Code128 barcode, reads it with different checksum validation settings,
/// and outputs the confidence and reading quality for comparison.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes barcode generation, recognition, and result comparison.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare a temporary folder and file path for the barcode image
        // ------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "Code128Benchmark_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "code128.png");

        // ------------------------------------------------------------
        // Generate a Code128 barcode and save it as PNG
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "AsposeCode128"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to generate barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // Read the barcode with default checksum validation
        // ------------------------------------------------------------
        BaseDecodeType decodeType = DecodeType.Code128;
        BarCodeResult defaultResult = null;
        using (var reader = new BarCodeReader(barcodePath, decodeType))
        {
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Default;
            var results = reader.ReadBarCodes();
            if (results.Length > 0)
                defaultResult = results[0];
        }

        // ------------------------------------------------------------
        // Read the barcode with checksum validation forced off
        // ------------------------------------------------------------
        BarCodeResult offResult = null;
        using (var reader = new BarCodeReader(barcodePath, decodeType))
        {
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;
            var results = reader.ReadBarCodes();
            if (results.Length > 0)
                offResult = results[0];
        }

        // ------------------------------------------------------------
        // Output the comparison of confidence and reading quality
        // ------------------------------------------------------------
        Console.WriteLine("Code128 Barcode Recognition Confidence Comparison");
        Console.WriteLine($"Barcode file: {barcodePath}");
        Console.WriteLine();

        if (defaultResult != null)
        {
            Console.WriteLine("Default ChecksumValidation:");
            Console.WriteLine($"  Confidence      : {defaultResult.Confidence}");
            Console.WriteLine($"  ReadingQuality  : {defaultResult.ReadingQuality}");
        }
        else
        {
            Console.WriteLine("Default ChecksumValidation: No barcode detected.");
        }

        Console.WriteLine();

        if (offResult != null)
        {
            Console.WriteLine("ChecksumValidation.Off:");
            Console.WriteLine($"  Confidence      : {offResult.Confidence}");
            Console.WriteLine($"  ReadingQuality  : {offResult.ReadingQuality}");
        }
        else
        {
            Console.WriteLine("ChecksumValidation.Off: No barcode detected.");
        }

        // ------------------------------------------------------------
        // Cleanup temporary files and directory
        // ------------------------------------------------------------
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}