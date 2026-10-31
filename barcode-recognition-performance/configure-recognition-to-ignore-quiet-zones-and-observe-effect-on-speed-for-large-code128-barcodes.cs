// Title: Code128 Large Barcode Generation and High‑Performance Recognition Demo
// Description: Generates a 200‑character Code128 barcode and measures recognition time using default and high‑performance settings.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It demonstrates how to use BarcodeGenerator to create barcodes and BarCodeReader with QualitySettings to tune performance. Typical use cases include bulk barcode processing, speed optimization for large images, and evaluating trade‑offs between accuracy and throughput. Developers often need to adjust quality presets, X‑dimension handling, and deconvolution modes to meet application requirements.
// Prompt: Configure recognition to ignore quiet zones and observe effect on speed for large Code128 barcodes.
// Tags: code128, barcode, generation, recognition, highperformance, qualitysettings, aspose.barcode

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a large Code128 barcode and comparing recognition performance
/// using default settings versus a high‑performance configuration.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, runs two recognition scenarios,
    /// outputs timing results, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "large_code128.png");

        // Generate a large Code128 barcode (200 characters)
        string longCode = new string('A', 200);
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, longCode))
        {
            // No explicit size settings; default auto-sizing will handle the long text
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine("Barcode generated at: " + barcodePath);
        Console.WriteLine();

        // Read with default settings (NormalQuality)
        var defaultResult = ReadBarcode(barcodePath, DecodeType.Code128, null);
        Console.WriteLine($"Default settings: Time = {defaultResult.ElapsedMilliseconds} ms, Barcodes read = {defaultResult.Count}");

        // Read with HighPerformance preset and minimal XDimension to improve speed
        var fastResult = ReadBarcode(barcodePath, DecodeType.Code128, reader =>
        {
            // Set recognition quality preset to HighPerformance for faster processing
            reader.QualitySettings = QualitySettings.HighPerformance;

            // Use minimal XDimension mode to reduce processing of unnecessary details
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 2f; // minimal element size in pixels

            // Fast deconvolution to speed up processing
            reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;

            // Note: Ignoring quiet zones is not exposed by the API; this setting cannot be applied.
        });
        Console.WriteLine($"HighPerformance preset: Time = {fastResult.ElapsedMilliseconds} ms, Barcodes read = {fastResult.Count}");

        // Clean up temporary files
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }

    /// <summary>
    /// Reads barcodes from an image using the specified decode type and optional configuration.
    /// Returns the elapsed time in milliseconds and the number of barcodes detected.
    /// </summary>
    /// <param name="imagePath">Path to the barcode image.</param>
    /// <param name="decodeType">The barcode symbology to decode.</param>
    /// <param name="configure">Optional action to configure the <see cref="BarCodeReader"/> before reading.</param>
    /// <returns>Tuple containing elapsed milliseconds and count of detected barcodes.</returns>
    private static (long ElapsedMilliseconds, int Count) ReadBarcode(string imagePath, BaseDecodeType decodeType, Action<BarCodeReader> configure)
    {
        var stopwatch = new Stopwatch();
        int count = 0;

        using (var reader = new BarCodeReader(imagePath, decodeType))
        {
            // Apply any custom configuration supplied by the caller
            configure?.Invoke(reader);

            // Measure the time taken to read barcodes
            stopwatch.Start();
            var results = reader.ReadBarCodes();
            stopwatch.Stop();

            // Determine how many barcodes were found
            count = results?.Length ?? 0;
        }

        return (stopwatch.ElapsedMilliseconds, count);
    }
}