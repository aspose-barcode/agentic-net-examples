// Title: Effect of MinimalXDimension on Barcode Detection
// Description: Demonstrates how setting MinimalXDimension to 0.5 pixels influences the detection of sub‑pixel barcode elements using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating the use of QualitySettings, XDimensionMode, and MinimalXDimension to fine‑tune detection sensitivity. Developers working with low‑resolution or sub‑pixel barcodes often adjust these settings to improve read rates. The sample shows generating a Code128 barcode, then reading it with default settings and with MinimalXDimension applied.
// Prompt: Test the effect of setting MinimalXDimension to 0.5 pixels on detection of sub‑pixel barcode elements.
// Tags: code128, minimalxdimension, barcode detection, qualitysettings, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates the impact of MinimalXDimension on barcode detection.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, then reads it with default and minimal X dimension settings.
    /// </summary>
    static void Main()
    {
        // Create a temporary output directory
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        string barcodePath = Path.Combine(outputDir, "sample.png");

        // Generate a simple Code128 barcode
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            // Set a modest XDimension to ensure the barcode is renderable
            generator.Parameters.Barcode.XDimension.Point = 1f;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode with default quality settings
        int defaultCount = 0;
        using (var readerDefault = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            BarCodeResult[] results = readerDefault.ReadBarCodes();
            defaultCount = results.Length;
        }

        // Read the barcode with MinimalXDimension set to 0.5 pixels
        int minimalCount = 0;
        using (var readerMinimal = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            // Enable minimal X dimension mode and specify the value
            readerMinimal.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            readerMinimal.QualitySettings.MinimalXDimension = 0.5f;
            BarCodeResult[] results = readerMinimal.ReadBarCodes();
            minimalCount = results.Length;
        }

        // Output the detection results for comparison
        Console.WriteLine($"Barcodes detected with default XDimension mode: {defaultCount}");
        Console.WriteLine($"Barcodes detected with MinimalXDimension = 0.5px: {minimalCount}");
    }
}