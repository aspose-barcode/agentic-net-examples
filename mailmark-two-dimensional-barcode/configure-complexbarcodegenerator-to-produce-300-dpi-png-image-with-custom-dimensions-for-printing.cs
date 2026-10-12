// Title: Generate a MaxiCode complex barcode with custom size and 300 dpi PNG output
// Description: This example creates a MaxiCode (Mode 3) barcode using Aspose.BarCode's ComplexBarcodeGenerator, sets a 300 dpi resolution, and defines explicit pixel dimensions for printing.
// Category-Description: Shows how to work with Aspose.BarCode's complex barcode APIs, specifically ComplexBarcodeGenerator and MaxiCode codetext classes. Typical use cases include creating high‑resolution, size‑controlled barcodes for packaging and shipping labels. Developers often need to adjust resolution, image dimensions, and appearance before saving to common image formats.
// Prompt: Configure ComplexBarcodeGenerator to produce a 300 dpi PNG image with custom dimensions for printing.
// Tags: maxicode, complex barcode, png, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a MaxiCode complex barcode with custom size and 300 dpi PNG output.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates and saves the barcode image.
    /// </summary>
    static void Main()
    {
        // Prepare complex codetext (MaxiCode example)
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Sample MaxiCode"
        };

        var maxiCodeCodetext = new MaxiCodeCodetextMode3
        {
            PostalCode = "B1050",
            CountryCode = 56,
            ServiceCategory = 999,
            SecondMessage = secondMessage
        };

        // Define output file path
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "maxicode.png");

        // Generate barcode with custom settings
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            // Set resolution to 300 dpi
            generator.Parameters.Resolution = 300f;

            // Use fixed image dimensions (pixels)
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;
            generator.Parameters.ImageWidth.Pixels = 600f;
            generator.Parameters.ImageHeight.Pixels = 400f;

            // Set module size (X dimension)
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Optional appearance settings
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}