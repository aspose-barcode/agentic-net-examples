// Title: Generate Center-Aligned Code128 Barcode for Receipt Printing
// Description: Creates a Code128 barcode with centered human‑readable text, automatic scaling, and high resolution suitable for narrow receipt printers.
// Category-Description: This example demonstrates basic barcode generation using Aspose.BarCode. It shows how to configure barcode parameters such as text alignment, X‑dimension, and image resolution, then save the result as a PNG. Developers working with point‑of‑sale or receipt printing often need to produce compact, high‑quality barcodes; the BarcodeGenerator class and its Parameters property provide the necessary controls.
// Prompt: Align barcode text to center, enable automatic scaling, and generate image suitable for narrow receipt printing.
// Tags: code128, barcode generation, receipt printing, text alignment, scaling, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode with centered text for narrow receipt printing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, configures parameters, and saves the image.
    /// </summary>
    static void Main()
    {
        // Determine output directory and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting PNG file
        string outPath = Path.Combine(outputDir, "receipt_barcode.png");

        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Center align the human‑readable text beneath the barcode
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

            // Set a small XDimension (pixel width of the narrowest bar) for narrow receipt printing
            generator.Parameters.Barcode.XDimension.Pixels = 1f;

            // Use a high resolution to ensure crisp output on printers
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode as a PNG image
            generator.Save(outPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved
        Console.WriteLine($"Barcode image saved to: {outPath}");
    }
}