// Title: Effect of Deconvolution Mode on Low‑Contrast Barcode Recognition
// Description: Demonstrates how disabling deconvolution (Fast mode) versus enabling it (Slow mode) impacts the detection count of a low‑contrast Code128 barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, illustrating the use of BarCodeReader, QualitySettings, and DeconvolutionMode to adjust image processing for low‑quality barcodes. Developers often need to tune deconvolution and quality settings to improve recognition accuracy in challenging imaging conditions.
// Prompt: Evaluate the effect of disabling deconvolution on recognition accuracy for low‑contrast barcodes.
// Tags: code128, low-contrast, deconvolution, barcode-recognition, quality-settings, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Sample program that generates a low‑contrast barcode and compares recognition results
/// with deconvolution disabled (Fast mode) and enabled (Slow mode).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode, runs recognition in two modes,
    /// outputs the detection counts, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "DeconvTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a simple Code128 barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "LowContrastTest"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode with deconvolution disabled (Fast mode)
        int countFast = ReadBarcode(barcodePath, DeconvolutionMode.Fast);
        // Read the barcode with deconvolution enabled (Slow mode)
        int countSlow = ReadBarcode(barcodePath, DeconvolutionMode.Slow);

        // Output the detection results for both modes
        Console.WriteLine($"Deconvolution Fast (disabled): {countFast} barcode(s) detected.");
        Console.WriteLine($"Deconvolution Slow (enabled): {countSlow} barcode(s) detected.");

        // Clean up temporary files and folder
        try { File.Delete(barcodePath); } catch { }
        try { Directory.Delete(tempFolder, true); } catch { }
    }

    /// <summary>
    /// Reads barcodes from the specified image using the given deconvolution mode.
    /// </summary>
    /// <param name="imagePath">Path to the barcode image file.</param>
    /// <param name="deconvMode">Deconvolution mode to apply during recognition.</param>
    /// <returns>The number of barcodes detected in the image.</returns>
    static int ReadBarcode(string imagePath, DeconvolutionMode deconvMode)
    {
        // Initialize the barcode reader for all supported types
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Simulate low‑contrast conditions by setting low quality mode
            reader.QualitySettings.BarcodeQuality = BarcodeQualityMode.Low;
            // Apply the specified deconvolution mode
            reader.QualitySettings.Deconvolution = deconvMode;

            // Perform the recognition operation
            BarCodeResult[] results = reader.ReadBarCodes();

            // Return the count of detected barcodes (0 if none)
            return results?.Length ?? 0;
        }
    }
}