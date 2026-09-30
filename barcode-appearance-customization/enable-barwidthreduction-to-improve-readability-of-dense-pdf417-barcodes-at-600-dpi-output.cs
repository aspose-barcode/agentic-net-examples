// Title: PDF417 Barcode Generation with Bar Width Reduction at 600 DPI
// Description: Demonstrates generating a high‑density PDF417 barcode, applying bar‑width reduction to improve readability, and saving the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on PDF417 symbology and image rendering settings. It showcases key API classes such as BarcodeGenerator, EncodeTypes, and BarCodeImageFormat, illustrating how to configure resolution, bar‑width reduction, and image dimensions—common tasks for developers creating printable or high‑resolution barcodes.
// Prompt: Enable BarWidthReduction to improve readability of dense PDF417 barcodes at 600 dpi output.
// Tags: pdf417, barwidthreduction, resolution, png, aspose.barcode, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a PDF417 barcode with bar‑width reduction at 600 dpi and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates and saves the barcode image.
    /// </summary>
    static void Main()
    {
        // Define the data to encode in the PDF417 barcode.
        string codeText = "Sample PDF417 Data for high‑density barcode";

        // Determine a temporary file path for the output PNG image.
        string outputPath = Path.Combine(Path.GetTempPath(), "Pdf417BarWidthReduction.png");

        // Ensure the target directory exists before attempting to write the file.
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Initialize the barcode generator for PDF417 with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, codeText))
        {
            // Set the rendering resolution to 600 DPI for high‑quality output.
            generator.Parameters.Resolution = 600f;

            // Reduce the bar width by 20 % to improve readability of dense barcodes.
            generator.Parameters.Barcode.BarWidthReduction.Point = 0.2f;

            // Optionally enlarge the image canvas to accommodate the higher resolution.
            generator.Parameters.ImageWidth.Pixels = 2400f;   // 4 inches at 600 DPI
            generator.Parameters.ImageHeight.Pixels = 1200f; // 2 inches at 600 DPI

            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"PDF417 barcode saved to: {outputPath}");
    }
}