// Title: Effect of Deconvolution Settings on Low‑Contrast Barcode Recognition
// Description: Demonstrates how disabling deconvolution (Fast mode) influences the recognition accuracy of a low‑contrast Code128 barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing the use of BarCodeReader, QualitySettings, and DeconvolutionMode to adjust image preprocessing. Developers often need to fine‑tune deconvolution for challenging images such as low‑contrast or noisy barcodes to improve detection speed and accuracy.
// Prompt: Evaluate the effect of disabling deconvolution on recognition accuracy for low‑contrast barcodes.
// Tags: code128, low-contrast, deconvolution, barcode recognition, qualitysettings, aspose.barcode, image preprocessing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates the impact of deconvolution settings on the recognition of a low‑contrast barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates a low‑contrast barcode, reads it with default and fast deconvolution settings,
    /// and prints the recognition results to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeDeconvDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "lowcontrast.png");

        // ------------------------------------------------------------
        // Generate a low‑contrast Code128 barcode image
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "LowContrastTest"))
        {
            // Set dark gray bars on a light gray background to simulate low contrast
            generator.Parameters.Barcode.BarColor = Color.FromArgb(100, 100, 100);
            generator.Parameters.BackColor = Color.FromArgb(200, 200, 200);
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Read the barcode with default deconvolution (Normal mode)
        // ------------------------------------------------------------
        using (var readerDefault = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            BarCodeResult[] results = readerDefault.ReadBarCodes();
            Console.WriteLine("=== Default Deconvolution (Normal) ===");
            PrintResults(results);
        }

        // ------------------------------------------------------------
        // Read the barcode with deconvolution minimized (Fast mode)
        // ------------------------------------------------------------
        using (var readerFast = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            // Fast deconvolution reduces processing; useful for low‑contrast images
            readerFast.QualitySettings.Deconvolution = DeconvolutionMode.Fast;
            BarCodeResult[] results = readerFast.ReadBarCodes();
            Console.WriteLine("=== Deconvolution set to Fast (minimized) ===");
            PrintResults(results);
        }

        // ------------------------------------------------------------
        // Cleanup temporary files (optional)
        // ------------------------------------------------------------
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir);
        }
        catch
        {
            // Ignore cleanup errors in demo
        }
    }

    /// <summary>
    /// Prints barcode recognition results to the console.
    /// </summary>
    /// <param name="results">Array of <see cref="BarCodeResult"/> objects returned by the reader.</param>
    static void PrintResults(BarCodeResult[] results)
    {
        if (results.Length == 0)
        {
            Console.WriteLine("No barcode detected.");
            return;
        }

        foreach (var result in results)
        {
            Console.WriteLine($"CodeText: {result.CodeText}");
            Console.WriteLine($"Symbology: {result.CodeTypeName}");
            Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");
            Console.WriteLine();
        }
    }
}