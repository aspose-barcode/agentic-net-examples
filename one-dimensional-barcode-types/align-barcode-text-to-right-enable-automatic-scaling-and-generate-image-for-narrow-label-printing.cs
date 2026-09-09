// Title: Generate Right-Aligned Code128 Barcode with Automatic Scaling for Narrow Labels
// Description: Demonstrates how to create a Code128 barcode, align its text to the right, enable automatic scaling, and set a narrow X dimension for label printing, then save as PNG.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating common tasks such as configuring barcode text alignment, auto‑size modes, and dimension settings using the BarcodeGenerator class. Developers creating product labels, shipping tags, or narrow‑width barcodes often need to adjust text positioning and scaling to fit limited space. The snippet shows typical usage of EncodeTypes, TextAlignment, AutoSizeMode, and XDimension properties for generating printable barcode images.
// Prompt: Align barcode text to right, enable automatic scaling, and generate image for narrow label printing.
// Tags: code128, text alignment, auto scaling, narrow label, png, aspose.barcode, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode with right‑aligned text,
/// automatic scaling, and a narrow X dimension suitable for label printing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates the output folder, configures the barcode,
    /// saves it as a PNG image, and writes the result path to the console.
    /// </summary>
    static void Main()
    {
        // Determine a temporary folder for the output image
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo");
        Directory.CreateDirectory(outputFolder);

        // Full path for the generated PNG file
        string outputPath = Path.Combine(outputFolder, "narrow_label.png");

        // Initialize the barcode generator with Code128 symbology and the desired data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "NARROW"))
        {
            // Align the human‑readable text to the right side of the barcode
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Right;

            // Enable automatic scaling using interpolation to fit the image dimensions
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set a narrow X dimension (module width) for high‑density label printing
            generator.Parameters.Barcode.XDimension.Point = 0.5f;

            // Save the configured barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}