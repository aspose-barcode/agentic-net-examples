// Title: Low‑contrast Code128 barcode recognition with deconvolution settings
// Description: Demonstrates how disabling or reducing deconvolution affects recognition accuracy for a low‑contrast barcode image.
// Prompt: Evaluate the effect of disabling deconvolution on recognition accuracy for low‑contrast barcodes.
// Tags: barcode, low-contrast, deconvolution, recognition, code128, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a low‑contrast Code128 barcode, saves it as an image,
/// and then reads it using different deconvolution settings to
/// illustrate the impact on recognition accuracy.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a barcode image and
    /// performs two recognition attempts: one with default high‑quality
    /// settings (including deconvolution) and one with deconvolution
    /// minimized (Fast mode).
    /// </summary>
    static void Main()
    {
        // Path for the sample barcode image
        const string imagePath = "lowcontrast.png";

        // ------------------------------------------------------------
        // Create a low‑contrast Code128 barcode (light gray bars on white background)
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "LowContrastTest"))
        {
            // Set bar color close to background to simulate low contrast
            generator.Parameters.Barcode.BarColor = Color.FromArgb(200, 200, 200); // light gray
            generator.Parameters.BackColor = Color.White;

            // Optional: reduce XDimension to make bars thinner
            generator.Parameters.Barcode.XDimension.Point = 0.5f;

            // Save the barcode image to the specified path
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify the image file exists before processing
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Barcode image '{imagePath}' not found.");
            return;
        }

        // ------------------------------------------------------------
        // Local function to read the barcode with a configurable QualitySettings
        // ------------------------------------------------------------
        void ReadBarcode(string description, Action<BarCodeReader> configureReader)
        {
            using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
            {
                // Apply custom configuration (e.g., deconvolution mode)
                configureReader(reader);

                // Perform recognition and output results
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"{description} - CodeText: {result.CodeText}");
                    Console.WriteLine($"{description} - Confidence: {result.Confidence}");
                    Console.WriteLine($"{description} - ReadingQuality: {result.ReadingQuality}");
                }
            }
        }

        // ------------------------------------------------------------
        // 1. Recognition with default HighQuality settings (includes deconvolution)
        // ------------------------------------------------------------
        ReadBarcode("HighQuality (default deconvolution)", reader =>
        {
            reader.QualitySettings = QualitySettings.HighQuality;
        });

        // ------------------------------------------------------------
        // 2. Recognition with deconvolution minimized (Fast mode)
        // ------------------------------------------------------------
        ReadBarcode("HighQuality with Deconvolution.Fast (reduced deconvolution)", reader =>
        {
            reader.QualitySettings = QualitySettings.HighQuality;
            // DeconvolutionMode.Fast is the minimal deconvolution option
            reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;
        });
    }
}