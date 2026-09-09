// Title: Demonstrate MinimalXDimension effect on sub‑pixel Code128 barcode detection
// Description: This example generates a Code128 barcode with a sub‑pixel XDimension of 0.5 point, saves it as PNG, and compares detection results using normal and minimal XDimension modes.
// Category-Description: Shows how to work with Aspose.BarCode generation and recognition APIs to control XDimension settings. It covers BarcodeGenerator for creating barcodes, BarCodeReader with QualitySettings for adjusting XDimensionMode, and typical use cases such as fine‑tuning detection of low‑resolution or sub‑pixel barcodes. Developers often need to experiment with MinimalXDimension to improve read accuracy on small or compressed images.
// Prompt: Test the effect of setting MinimalXDimension to 0.5 pixels on detection of sub‑pixel barcode elements.
// Tags: code128, xdimension, minimalxdimension, barcode-generation, barcode-recognition, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a sub‑pixel Code128 barcode and evaluates detection using different XDimension settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a barcode image, reads it with normal and minimal XDimension modes,
    /// and outputs the detection counts.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the generated image
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the barcode PNG file
        string barcodePath = Path.Combine(tempFolder, "subpixel_code128.png");

        // Generate a Code128 barcode with a sub‑pixel XDimension (0.5 point)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "SUBPIXEL"))
        {
            // Set XDimension to 0.5 point (sub‑pixel)
            generator.Parameters.Barcode.XDimension.Point = 0.5f;

            // Reduce padding to keep the image compact
            generator.Parameters.Barcode.Padding.Left.Point = 2f;
            generator.Parameters.Barcode.Padding.Top.Point = 2f;
            generator.Parameters.Barcode.Padding.Right.Point = 2f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 2f;

            // Save the barcode image as PNG
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to generate barcode image.");
            return;
        }

        // -----------------------------------------------------------------
        // Read the barcode using the default XDimension mode (Normal)
        // -----------------------------------------------------------------
        int countNormal = 0;
        using (var readerNormal = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            readerNormal.QualitySettings.XDimension = XDimensionMode.Normal;
            BarCodeResult[] results = readerNormal.ReadBarCodes();
            countNormal = results.Length;
            Console.WriteLine($"Normal XDimension mode detected {countNormal} barcode(s).");
        }

        // -----------------------------------------------------------------
        // Read the barcode using UseMinimalXDimension mode with MinimalXDimension = 0.5 point
        // -----------------------------------------------------------------
        int countMinimal = 0;
        using (var readerMinimal = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            readerMinimal.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            readerMinimal.QualitySettings.MinimalXDimension = 0.5f;
            BarCodeResult[] results = readerMinimal.ReadBarCodes();
            countMinimal = results.Length;
            Console.WriteLine($"UseMinimalXDimension mode (0.5pt) detected {countMinimal} barcode(s).");
        }

        // Output a simple summary
        Console.WriteLine("Test completed.");
    }
}