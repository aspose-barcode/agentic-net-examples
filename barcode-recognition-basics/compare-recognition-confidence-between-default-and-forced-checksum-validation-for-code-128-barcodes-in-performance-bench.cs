// Title: Compare checksum validation confidence for Code 128 barcodes
// Description: Demonstrates how default and forced checksum validation affect recognition confidence and performance for a Code 128 barcode.
// Category-Description: This example belongs to the Aspose.BarCode recognition performance benchmarks. It shows usage of BarcodeGenerator, BarCodeReader, and ChecksumValidation settings to compare confidence levels and processing time, a common task for developers optimizing barcode scanning reliability and speed.
// Prompt: Compare recognition confidence between default and forced checksum validation for Code 128 barcodes in a performance benchmark.
// Tags: code128, checksumvalidation, confidence, performance, benchmark, generation, recognition, aspose.barcode

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a Code 128 barcode, then measures and compares recognition confidence
/// and execution time using default checksum validation versus forced checksum validation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes barcode generation, recognition, and timing.
    /// </summary>
    static void Main()
    {
        // Path for the generated barcode image
        const string imagePath = "code128.png";

        // ------------------------------------------------------------
        // Generate a Code128 barcode and save it to a file
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(imagePath);
        }

        // Verify that the image was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Variables to hold confidence values for each scenario
        BarCodeConfidence defaultConfidence = BarCodeConfidence.None;
        BarCodeConfidence forcedConfidence = BarCodeConfidence.None;

        // ------------------------------------------------------------
        // Measure default recognition (checksum validation follows default behavior)
        // ------------------------------------------------------------
        var defaultStopwatch = Stopwatch.StartNew();
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                defaultConfidence = result.Confidence;
                break; // Only one barcode expected
            }
        }
        defaultStopwatch.Stop();

        // ------------------------------------------------------------
        // Measure recognition with forced checksum validation (ChecksumValidation.On)
        // ------------------------------------------------------------
        var forcedStopwatch = Stopwatch.StartNew();
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Force checksum validation for this read operation
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            foreach (var result in reader.ReadBarCodes())
            {
                forcedConfidence = result.Confidence;
                break; // Only one barcode expected
            }
        }
        forcedStopwatch.Stop();

        // ------------------------------------------------------------
        // Output the comparison results
        // ------------------------------------------------------------
        Console.WriteLine($"Default checksum validation confidence: {defaultConfidence}");
        Console.WriteLine($"Forced checksum validation confidence: {forcedConfidence}");
        Console.WriteLine($"Default recognition time: {defaultStopwatch.ElapsedMilliseconds} ms");
        Console.WriteLine($"Forced checksum recognition time: {forcedStopwatch.ElapsedMilliseconds} ms");
    }
}