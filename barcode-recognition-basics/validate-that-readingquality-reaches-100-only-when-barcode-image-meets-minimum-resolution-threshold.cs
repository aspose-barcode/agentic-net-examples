// Title: Validate ReadingQuality based on barcode image resolution
// Description: Demonstrates generating low‑ and high‑resolution Code128 barcodes and checking that the ReadingQuality reaches 100 only when the image meets a minimum DPI.
// Category-Description: This example belongs to the Aspose.BarCode image generation and recognition category. It shows how to use BarcodeGenerator to set image resolution and BarCodeReader to evaluate ReadingQuality, a metric useful for assessing scan reliability. Developers working with barcode scanning often need to ensure sufficient image DPI to achieve optimal recognition quality.
// Prompt: Validate that ReadingQuality reaches 100 only when the barcode image meets a minimum resolution threshold.
// Tags: code128, generation, recognition, readingquality, resolution, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates barcode images at different resolutions and validates that
/// the <c>ReadingQuality</c> reported by <c>BarCodeReader</c> reaches 100
/// only when the image DPI meets the defined minimum threshold.
/// </summary>
class Program
{
    // Minimum DPI required for a ReadingQuality of 100
    const float MinResolutionDpi = 200f;

    /// <summary>
    /// Entry point of the example. Creates low‑ and high‑resolution barcodes,
    /// evaluates their reading quality, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Paths for temporary barcode images
        string lowResPath = "barcode_low.png";
        string highResPath = "barcode_high.png";

        // Generate a low‑resolution barcode (100 DPI)
        GenerateBarcode("1234567890", 100f, lowResPath);

        // Generate a high‑resolution barcode (300 DPI)
        GenerateBarcode("1234567890", 300f, highResPath);

        // Evaluate low‑resolution image
        EvaluateBarcode(lowResPath, "Low resolution");

        // Evaluate high‑resolution image
        EvaluateBarcode(highResPath, "High resolution");

        // Clean up temporary files
        try { if (File.Exists(lowResPath)) File.Delete(lowResPath); } catch { }
        try { if (File.Exists(highResPath)) File.Delete(highResPath); } catch { }
    }

    /// <summary>
    /// Generates a barcode image with the specified DPI resolution.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="resolutionDpi">Desired image resolution in dots per inch.</param>
    /// <param name="outputPath">File path where the image will be saved.</param>
    static void GenerateBarcode(string codeText, float resolutionDpi, string outputPath)
    {
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set image resolution (dots per inch)
            generator.Parameters.Resolution = resolutionDpi;

            // Keep image size reasonable
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 100f;

            // Save the generated image to the specified path
            generator.Save(outputPath);
        }
    }

    /// <summary>
    /// Reads a barcode image, prints its inferred resolution and <c>ReadingQuality</c>,
    /// and validates that full quality (100) is reported only when the resolution
    /// meets or exceeds <see cref="MinResolutionDpi"/>.
    /// </summary>
    /// <param name="imagePath">Path to the barcode image file.</param>
    /// <param name="label">Label used in console output to identify the test case.</param>
    static void EvaluateBarcode(string imagePath, string label)
    {
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"{label}: Image file not found.");
            return;
        }

        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Read all barcodes present in the image
            foreach (var result in reader.ReadBarCodes())
            {
                // ReadingQuality is a double representing a percentage
                double quality = result.ReadingQuality;

                // The reader does not expose the source resolution directly,
                // so we infer it from the file name for demonstration purposes.
                float usedResolution = imagePath.Contains("high") ? 300f : 100f;

                bool meetsThreshold = usedResolution >= MinResolutionDpi;
                bool qualityIsFull = Math.Abs(quality - 100.0) < 0.0001;

                Console.WriteLine($"{label}: Used DPI = {usedResolution}, ReadingQuality = {quality}");

                if (meetsThreshold && qualityIsFull)
                {
                    Console.WriteLine($"{label}: PASS – High resolution yields full quality.");
                }
                else if (!meetsThreshold && !qualityIsFull)
                {
                    Console.WriteLine($"{label}: PASS – Low resolution yields reduced quality.");
                }
                else
                {
                    Console.WriteLine($"{label}: FAIL – Unexpected quality for the given resolution.");
                }
            }
        }
    }
}