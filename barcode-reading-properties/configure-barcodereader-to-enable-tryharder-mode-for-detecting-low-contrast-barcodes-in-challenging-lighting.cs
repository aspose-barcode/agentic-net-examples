// Title: Enable TryHarder Mode for Low‑Contrast Barcode Detection
// Description: Demonstrates configuring BarCodeReader with high‑quality (tryHarder) settings to read low‑contrast barcodes generated in challenging lighting conditions.
// Category-Description: This example belongs to the Aspose.BarCode reading category, showcasing how to use BarCodeReader, QualitySettings, and related classes to improve detection of difficult images. Typical use cases include scanning barcodes in low‑light environments, on faded labels, or when contrast is poor. Developers often need to enable tryHarder mode to boost recognition accuracy for such scenarios.
// Prompt: Configure BarCodeReader to enable tryHarder mode for detecting low‑contrast barcodes in challenging lighting.
// Tags: code128, detection, low-contrast, png, barcodereader, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a low‑contrast barcode image and reads it using
/// high‑quality (tryHarder) settings to demonstrate robust detection in challenging lighting.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a low‑contrast Code128 barcode, saves it,
    /// and then reads it with BarCodeReader configured for high‑quality detection.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare output directory
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Path for the sample barcode image
        string barcodePath = Path.Combine(outputDir, "low_contrast_barcode.png");

        // --------------------------------------------------------------------
        // Generate a low‑contrast barcode (dark gray bars on slightly lighter gray background)
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Parameters.Barcode.BarColor = Color.FromArgb(80, 80, 80); // dark gray bars
            generator.Parameters.BackColor = Color.FromArgb(120, 120, 120);   // lighter gray background
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify the image was created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // --------------------------------------------------------------------
        // Read the barcode using high‑quality (try‑harder) settings
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            // Apply the HighQuality preset which is designed for low‑quality / low‑contrast images
            reader.QualitySettings = QualitySettings.HighQuality;

            // Optional: further enhance detection for challenging images
            reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;
            reader.QualitySettings.AllowIncorrectBarcodes = true;

            // Iterate through all detected barcodes and output details
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");
                Console.WriteLine($"Confidence: {result.Confidence}");
                Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                var bounds = result.Region.Rectangle;
                Console.WriteLine($"Region: X={bounds.X}, Y={bounds.Y}, Width={bounds.Width}, Height={bounds.Height}");
                Console.WriteLine();
            }
        }
    }
}