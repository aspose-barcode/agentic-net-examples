// Title: Barcode recognition performance comparison between HighPerformance and MaxQuality presets
// Description: Demonstrates how to generate a Code128 barcode, then measures the time required to recognize it using Aspose.BarCode's HighPerformance and MaxQuality quality settings.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing the use of BarcodeGenerator for creating barcodes and BarCodeReader with QualitySettings for decoding. Developers often need to balance speed and accuracy when processing barcodes at scale, and this snippet illustrates typical performance tuning by comparing HighPerformance and MaxQuality presets. Ideal for scenarios such as batch scanning, real‑time validation, and quality‑sensitive applications.
// Prompt: Compare CPU usage between HighPerformance and MaxQuality presets using Stopwatch timing measurements.
// Tags: barcode symbology, generation, recognition, performance, qualitysettings, code128, png, stopwatch

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates performance measurement of barcode recognition using different quality presets.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, measures recognition times for HighPerformance and MaxQuality presets, and outputs the results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to store the sample barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodePerf_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "sample.png");

        try
        {
            // Generate a simple Code128 barcode image and save it as PNG
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }

            // Measure recognition time using the HighPerformance preset
            long highPerfMs = MeasureRecognitionTime(imagePath, QualitySettings.HighPerformance);

            // Measure recognition time using the MaxQuality preset
            long maxQualityMs = MeasureRecognitionTime(imagePath, QualitySettings.MaxQuality);

            // Output the timing results
            Console.WriteLine($"Recognition time (HighPerformance): {highPerfMs} ms");
            Console.WriteLine($"Recognition time (MaxQuality): {maxQualityMs} ms");
        }
        finally
        {
            // Clean up temporary files and folder
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
    }

    /// <summary>
    /// Measures the time required for the BarCodeReader to decode the specified image using a given quality preset.
    /// </summary>
    /// <param name="imagePath">Path to the barcode image file.</param>
    /// <param name="preset">QualitySettings preset to apply (e.g., HighPerformance or MaxQuality).</param>
    /// <returns>Elapsed time in milliseconds.</returns>
    static long MeasureRecognitionTime(string imagePath, QualitySettings preset)
    {
        // Initialize the reader for Code128 barcodes
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Apply the selected quality preset
            reader.QualitySettings = preset;

            // Start timing, perform the read operation, then stop timing
            Stopwatch sw = Stopwatch.StartNew();
            reader.ReadBarCodes();
            sw.Stop();

            // Return the elapsed milliseconds
            return sw.ElapsedMilliseconds;
        }
    }
}