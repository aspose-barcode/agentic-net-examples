// Title: Effect of Deconvolution Modes on Low‑Contrast Barcode Recognition
// Description: Demonstrates how disabling deconvolution impacts the ability of Aspose.BarCode to read a low‑contrast Code128 barcode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and the QualitySettings.Deconvolution property to control deconvolution levels. Developers often need to adjust deconvolution when processing low‑contrast or noisy images to balance speed and accuracy.
// Prompt: Evaluate the effect of disabling deconvolution on recognition accuracy for low‑contrast barcodes.
// Tags: low-contrast, deconvolution, barcode-recognition, code128, aspose-barcode, generation, quality-settings

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates the impact of different deconvolution settings on recognizing a low‑contrast barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a low‑contrast barcode, reads it with various deconvolution modes, and reports results.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the sample barcode
        string tempFolder = Path.Combine(Path.GetTempPath(), "DeconvTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "lowcontrast.png");

        // Generate a low‑contrast Code128 barcode (black bars on a light‑gray background)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.FromArgb(200, 200, 200); // light gray background
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Read with deconvolution disabled (Fast mode)
        ReadAndReport(barcodePath, DeconvolutionMode.Fast, "Deconvolution Fast (disabled)");

        // Read with default deconvolution (Normal mode)
        ReadAndReport(barcodePath, DeconvolutionMode.Normal, "Deconvolution Normal (enabled)");

        // Optional: read with heavy deconvolution (Slow mode) for comparison
        ReadAndReport(barcodePath, DeconvolutionMode.Slow, "Deconvolution Slow (max)");

        // Cleanup temporary files (optional)
        try { Directory.Delete(tempFolder, true); } catch { }
    }

    /// <summary>
    /// Reads a barcode image using the specified deconvolution mode and writes the results to the console.
    /// </summary>
    /// <param name="imagePath">Path to the barcode image.</param>
    /// <param name="deconvMode">Deconvolution mode to apply during recognition.</param>
    /// <param name="label">Label used in console output to identify the test case.</param>
    static void ReadAndReport(string imagePath, DeconvolutionMode deconvMode, string label)
    {
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Simulate low‑quality conditions for the recognition engine
            reader.QualitySettings.BarcodeQuality = BarcodeQualityMode.Low;
            reader.QualitySettings.Deconvolution = deconvMode;

            // Perform barcode detection
            BarCodeResult[] results = reader.ReadBarCodes();
            Console.WriteLine($"{label}: Detected {results.Length} barcode(s)");
            foreach (var result in results)
            {
                Console.WriteLine($"  CodeText: {result.CodeText}");
                Console.WriteLine($"  Type: {result.CodeTypeName}");
                Console.WriteLine($"  ReadingQuality: {result.ReadingQuality}");
            }
        }
    }
}