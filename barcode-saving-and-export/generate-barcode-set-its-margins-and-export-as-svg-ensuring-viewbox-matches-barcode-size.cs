// Title: Generate Code128 Barcode with Margins and Export to SVG
// Description: This example creates a Code128 barcode, applies uniform padding, disables auto‑size mode so the SVG viewBox matches the barcode dimensions, and saves the result as an SVG file.
// Category-Description: Demonstrates Aspose.BarCode barcode generation and image export. It covers the BarcodeGenerator class, setting barcode parameters such as padding and AutoSizeMode, and saving to vector formats like SVG. Useful for developers needing precise control over barcode layout and vector output for web or print.
// Prompt: Generate a barcode, set its margins, and export as SVG ensuring the viewBox matches the barcode size.
// Tags: code128, barcode generation, svg, margins, autosizemode, aspose.barcode, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode, configures its margins,
/// disables automatic sizing, and saves it as an SVG file with a matching viewBox.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output SVG file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.svg");

        // Ensure the target directory exists before attempting to save.
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Initialize the barcode generator with Code128 symbology and sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Configure uniform padding (margins) around the barcode in points.
            generator.Parameters.Barcode.Padding.Left.Point = 5f;
            generator.Parameters.Barcode.Padding.Top.Point = 5f;
            generator.Parameters.Barcode.Padding.Right.Point = 5f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 5f;

            // Disable automatic size adjustment so the SVG viewBox matches the exact barcode size.
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Attempt to save the barcode as an SVG file.
            try
            {
                generator.Save(outputPath, BarCodeImageFormat.Svg);
                Console.WriteLine($"Barcode saved to SVG: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving SVG: {ex.Message}");
            }
        }
    }
}