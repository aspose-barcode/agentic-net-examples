// Title: Center Aligned Barcode Text with Automatic Scaling
// Description: Demonstrates how to center the human‑readable text of a barcode and enable automatic scaling to fit a narrow image width using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and image parameter settings. Developers often need to adjust text alignment and auto‑size modes when creating barcodes for limited‑space layouts, such as labels or receipts. The snippet shows configuring TextAlignment, AutoSizeMode, and image dimensions to produce a centered, scaled barcode.
// Prompt: Align barcode text to center and enable automatic scaling to fit within narrow barcode width.
// Tags: barcode, code128, text alignment, autosize, scaling, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a Code128 barcode with centered human‑readable text and automatic scaling to fit a narrow canvas.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output directory, configures the barcode generator,
    /// and saves the resulting image to disk.
    /// </summary>
    static void Main()
    {
        // Define the output folder and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated barcode image
        string outPath = Path.Combine(outputDir, "CenteredScaledBarcode.png");

        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Center align the human‑readable text beneath the barcode
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

            // Enable automatic scaling (interpolation) to adapt the barcode to a narrow width
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set the desired image dimensions (narrow width, optional height)
            generator.Parameters.ImageWidth.Pixels = 150f;   // narrow canvas width
            generator.Parameters.ImageHeight.Pixels = 100f; // optional height

            // Save the barcode image in PNG format
            generator.Save(outPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outPath}");
    }
}