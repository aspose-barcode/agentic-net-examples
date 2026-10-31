// Title: Generate PDF417 barcode with automatic font sizing using FontMode.Auto
// Description: This example shows how to enable FontMode.Auto so Aspose.BarCode automatically determines the optimal font size for each barcode symbol.
// Category-Description: Demonstrates Aspose.BarCode generation features, focusing on the CodeTextParameters.FontMode property. Typical use cases include creating barcodes with readable text where the library adjusts font size to fit the symbol dimensions. Developers working with PDF417, QR, or other symbologies often need to control text appearance without manual size calculations.
// Prompt: Apply FontMode.Auto to barcode text so the library automatically calculates optimal font size for each symbol.
// Tags: pdf417, fontmode, auto, barcode, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a PDF417 barcode with automatic font sizing using FontMode.Auto.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output folder, configures barcode generator, and saves the image.
    /// </summary>
    static void Main()
    {
        // Determine the output directory relative to the current working folder.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Ensure the output directory exists.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Full path for the generated barcode image.
        string outputPath = Path.Combine(outputDir, "FontModeAuto.png");

        // Initialize the barcode generator for PDF417 with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Sample Text for Auto Font"))
        {
            // Configure PDF417 specific parameters.
            generator.Parameters.Barcode.Pdf417.Rows = 12;
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Enable automatic font sizing for the code text.
            generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Auto;

            // Set additional font properties (family, style, and a base size).
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Lucida Handwriting";
            generator.Parameters.Barcode.CodeTextParameters.Font.Style = FontStyle.Underline;
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 10f;

            // Save the barcode image as PNG.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}