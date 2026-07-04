// Title: PDF417 Barcode Generation with Bar Width Reduction
// Description: Demonstrates generating a dense PDF417 barcode, applying bar‑width reduction to improve readability at 600 dpi, and saving it as PNG.
// Prompt: Enable BarWidthReduction to improve readability of dense PDF417 barcodes at 600 dpi output.
// Tags: pdf417, barcode, bar width reduction, resolution, png, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a PDF417 barcode with bar‑width reduction enabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a dense PDF417 barcode, configures high‑resolution settings,
    /// enables bar‑width reduction, and saves the result as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the barcode content – dense data benefits most from bar‑width reduction.
        string codeText = "Sample PDF417 dense data for testing bar width reduction.";
        // Output file path.
        string outputPath = "pdf417.png";

        // Create a BarcodeGenerator for PDF417 with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, codeText))
        {
            // Set the image resolution to 600 dpi for high‑quality output.
            generator.Parameters.Resolution = 600;

            // Reduce the bar width by 0.2 points to compensate for ink spread at high DPI.
            generator.Parameters.Barcode.BarWidthReduction.Point = 0.2f;

            // Use interpolation mode so the barcode size is controlled via image dimensions.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Save the generated barcode image as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"PDF417 barcode saved to {outputPath}");
    }
}