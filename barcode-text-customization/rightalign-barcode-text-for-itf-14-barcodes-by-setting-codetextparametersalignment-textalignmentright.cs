// Title: Right-align text for ITF‑14 barcode
// Description: Demonstrates how to generate an ITF‑14 barcode with the human‑readable text aligned to the right side of the image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to control text alignment. Typical use cases include customizing the appearance of linear barcodes for packaging labels where right‑aligned text is required. Developers often need to adjust X‑dimension, symbology type, and text positioning to meet labeling standards.
// Prompt: Right-align barcode text for ITF‑14 barcodes by setting CodetextParameters.Alignment = TextAlignment.Right.
// Tags: itf14, text alignment, barcode generation, png output, aspose.barcode, codetextparameters

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates an ITF‑14 barcode with right‑aligned human‑readable text and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output directory, configures the barcode generator,
    /// sets the text alignment to right, saves the image, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Determine output folder relative to current directory and ensure it exists.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting PNG file.
        string outPath = Path.Combine(outputDir, "ITF14_RightAligned.png");

        // Initialize generator for ITF‑14 symbology with sample code text.
        using (var generator = new BarcodeGenerator(EncodeTypes.ITF14, "12345678901231"))
        {
            // Set the X‑dimension (module width) to 2 pixels for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Align the human‑readable text to the right side of the barcode.
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Right;

            // Save the generated barcode as a PNG image.
            generator.Save(outPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to {outPath}");
    }
}