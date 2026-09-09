// Title: Center Align Barcode Text with Automatic Scaling for Narrow Width
// Description: Demonstrates how to center the human‑readable text of a Code128 barcode and enable automatic scaling to fit a narrow image width using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, showcasing how to configure barcode appearance with the BarcodeGenerator class. It covers setting text alignment, location, auto‑size mode, and image dimensions—common tasks when creating barcodes for labels, receipts, or UI elements where space is limited. Developers often need to adjust these parameters to ensure readability and proper fit across various output formats.
// Prompt: Align barcode text to center and enable automatic scaling to fit within narrow barcode width.
// Tags: code128, text alignment, auto scaling, png, aspose.barcode, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a Code128 barcode with centered human‑readable text and automatic scaling to fit a narrow canvas.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output folder, configures the barcode, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary output directory.
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "CenteredScaledBarcode.png");

        // Initialize the barcode generator for Code128 with the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            // Center align the human‑readable text and place it below the barcode.
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Enable automatic scaling (interpolation) to adapt the barcode to a narrow width.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Pixels = 200f;   // Narrow canvas width.
            generator.Parameters.ImageHeight.Pixels = 100f; // Canvas height.

            // Reduce module (X) dimension to improve fitting within the constrained width.
            generator.Parameters.Barcode.XDimension.Pixels = 1f;

            // Save the generated barcode image as PNG.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine("Barcode generated at: " + outputPath);
    }
}