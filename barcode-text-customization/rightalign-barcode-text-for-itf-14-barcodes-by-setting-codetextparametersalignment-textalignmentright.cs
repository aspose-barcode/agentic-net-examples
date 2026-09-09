// Title: Right-align text for ITF‑14 barcode using Aspose.BarCode
// Description: Demonstrates generating an ITF‑14 barcode with the human‑readable text aligned to the right side of the image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode appearance using the BarcodeGenerator class and its Parameters, such as XDimension and CodeTextParameters. Typical use cases include product labeling, packaging, and inventory systems where ITF‑14 barcodes are required. Developers often need to adjust text alignment, size, and image format to meet branding or regulatory guidelines.
// Prompt: Right-align barcode text for ITF‑14 barcodes by setting CodetextParameters.Alignment = TextAlignment.Right.
// Tags: itf-14, barcode, text alignment, right align, aspose.barcode, generation, png, codetextparameters

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates an ITF‑14 barcode image with right‑aligned human‑readable text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates an output folder, configures the barcode generator,
    /// aligns the code text to the right, saves the image, and writes the file path to the console.
    /// </summary>
    static void Main()
    {
        // Determine a temporary directory for the output file.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarCodeDemo");

        // Ensure the directory exists.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Full path for the generated PNG image.
        string outputPath = Path.Combine(outputDir, "ITF14_RightAlign.png");

        // Initialize the barcode generator for ITF‑14 with sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.ITF14, "12345678901231"))
        {
            // Set the X‑dimension (module width) in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Align the human‑readable text to the right side of the barcode.
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Right;

            // Save the barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}