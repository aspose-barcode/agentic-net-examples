// Title: Generate Code128 Barcode with Margins and Export to SVG
// Description: This example creates a Code128 barcode, applies uniform padding, and saves it as an SVG file where the viewBox matches the barcode dimensions.
// Category-Description: Demonstrates Aspose.BarCode barcode generation and SVG export. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce vector graphics with precise sizing. Common scenarios include creating printable barcodes for web or print, customizing layout with padding, and ensuring scalable SVG output for responsive designs.
// Prompt: Generate a barcode, set its margins, and export as SVG ensuring the viewBox matches the barcode size.
// Tags: code128, barcode generation, svg export, margins, aspose.barcode, barcodegenerator, padding

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode, applies padding, and saves it as an SVG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the output path for the SVG file in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.svg");

        // Initialize a BarcodeGenerator for Code128 with the sample text "1234567890".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Configure uniform padding (margins) around the barcode in points.
            generator.Parameters.Barcode.Padding.Left.Point = 10f;
            generator.Parameters.Barcode.Padding.Top.Point = 10f;
            generator.Parameters.Barcode.Padding.Right.Point = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

            // Attempt to save the barcode as an SVG file.
            // SVG export may require a valid license in evaluation mode, so we catch potential exceptions.
            try
            {
                generator.Save(outputPath, BarCodeImageFormat.Svg);
                Console.WriteLine($"Barcode SVG saved to: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save SVG: {ex.Message}");
            }
        }
    }
}