// Title: Compare CPU usage of barcode recognition presets
// Description: Demonstrates measuring the time taken to recognize a barcode using HighPerformance and MaxQuality quality settings.
// Prompt: Compare CPU usage between HighPerformance and MaxQuality presets using Stopwatch timing measurements.
// Tags: barcode, recognition, performance, stopwatch, aspose.barcode, csharp

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates measuring CPU usage of barcode recognition with different quality presets.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, then measures recognition time using HighPerformance and MaxQuality presets.
    /// </summary>
    static void Main()
    {
        // Define the file path for the generated barcode image
        string imagePath = "sample.png";

        // Generate a simple Code128 barcode and save it to the specified path
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(imagePath);
        }

        // Verify that the barcode image was successfully created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // ------------------------------
        // Measure recognition time using the HighPerformance preset
        // ------------------------------
        long highPerfMs;
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Apply the HighPerformance quality setting for faster processing
            reader.QualitySettings = QualitySettings.HighPerformance;

            // Start timing
            var stopwatch = Stopwatch.StartNew();

            // Read all barcodes in the image
            foreach (var result in reader.ReadBarCodes())
            {
                // Access the decoded text to ensure the result is processed
                var _ = result.CodeText;
            }

            // Stop timing and record elapsed milliseconds
            stopwatch.Stop();
            highPerfMs = stopwatch.ElapsedMilliseconds;
        }

        // ------------------------------
        // Measure recognition time using the MaxQuality preset
        // ------------------------------
        long maxQualityMs;
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Apply the MaxQuality setting for highest accuracy (potentially slower)
            reader.QualitySettings = QualitySettings.MaxQuality;

            // Start timing
            var stopwatch = Stopwatch.StartNew();

            // Read all barcodes in the image
            foreach (var result in reader.ReadBarCodes())
            {
                // Access the decoded text to ensure the result is processed
                var _ = result.CodeText;
            }

            // Stop timing and record elapsed milliseconds
            stopwatch.Stop();
            maxQualityMs = stopwatch.ElapsedMilliseconds;
        }

        // Output the timing results for both presets
        Console.WriteLine($"HighPerformance preset elapsed time: {highPerfMs} ms");
        Console.WriteLine($"MaxQuality preset elapsed time: {maxQualityMs} ms");
    }
}