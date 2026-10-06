// Title: Generate a narrow-label Code128 barcode with right-aligned text
// Description: Demonstrates how to create a Code128 barcode, align its human‑readable text to the right, and configure a small X‑dimension for narrow label printing, then save it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat for creating barcodes. Typical scenarios include label printing, inventory tagging, and point‑of‑sale applications where precise text alignment and size scaling are required. Developers often need to adjust text alignment, X‑dimension, and image output settings to meet specific label dimensions and printing hardware constraints.
// Prompt: Align barcode text to right, enable automatic scaling, and generate image for narrow label printing.
// Tags: code128, barcode generation, text alignment, right alignment, narrow label, png, aspose.barcode, xdimension, autoscaling

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a narrow‑label Code128 barcode with right‑aligned text and saving it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output folder, configures the barcode generator, and saves the image.
    /// </summary>
    static void Main()
    {
        // Determine the output directory relative to the current working directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Full path for the generated barcode image
        string outputPath = Path.Combine(outputDir, "NarrowLabelBarcode.png");

        // Initialize the barcode generator with Code128 symbology and the desired data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Align the human‑readable text to the right side of the barcode
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Right;

            // Set a small XDimension (module width) suitable for narrow label printing
            generator.Parameters.Barcode.XDimension.Point = 0.5f;

            // No explicit AutoSizeMode is required; the generator automatically scales the barcode to fit the content

            // Save the generated barcode as a PNG image to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image has been saved
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}