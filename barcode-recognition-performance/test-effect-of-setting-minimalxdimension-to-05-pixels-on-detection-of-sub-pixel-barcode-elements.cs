// Title: Demonstrate MinimalXDimension effect on barcode detection
// Description: Shows how setting MinimalXDimension to 0.5 pixels influences detection of sub‑pixel Code128 barcodes. Generates a barcode with sub‑pixel module size and compares default vs minimal X dimension recognition counts.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It illustrates using BarcodeGenerator to create a barcode with a custom XDimension and BarCodeReader with QualitySettings to adjust XDimension detection. Developers working with low‑resolution or sub‑pixel barcodes often need to tweak MinimalXDimension to improve read accuracy. The snippet highlights key classes such as BarcodeGenerator, BarCodeReader, and QualitySettings for practical use cases like testing scanner sensitivity.
// Prompt: Test the effect of setting MinimalXDimension to 0.5 pixels on detection of sub‑pixel barcode elements.
// Tags: code128, minimalxdimension, subpixel, barcode generation, barcode recognition, qualitysettings, aspnet, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Program demonstrating the impact of MinimalXDimension on barcode detection.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a sub‑pixel Code128 barcode, reads it with default settings,
    /// then reads it with MinimalXDimension set to 0.5 px, and prints detection counts.
    /// </summary>
    static void Main(string[] args)
    {
        // Create a temporary folder for the barcode image
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "barcode.png");

        // Generate a Code128 barcode with a very small XDimension (sub‑pixel)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            generator.Parameters.Barcode.XDimension.Point = 0.5f; // sub‑pixel module size
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Read the barcode with default recognition settings
        int defaultCount = 0;
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            var results = reader.ReadBarCodes();
            defaultCount = results.Length;
        }

        // Read the barcode using MinimalXDimension = 0.5 pixels
        int minimalCount = 0;
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            // Enable use of MinimalXDimension and set it to 0.5 px
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 0.5f;
            var results = reader.ReadBarCodes();
            minimalCount = results.Length;
        }

        // Output the detection results for comparison
        Console.WriteLine($"Default detection count: {defaultCount}");
        Console.WriteLine($"UseMinimalXDimension (0.5 px) detection count: {minimalCount}");
    }
}