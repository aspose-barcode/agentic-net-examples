// Title: Generate Code39 Barcode with Margins and Export as SVG
// Description: Creates a Code39 barcode, applies uniform padding, and saves it as an SVG file with a viewBox that matches the barcode dimensions.
// Prompt: Generate a barcode, set its margins, and export as SVG ensuring the viewBox matches the barcode size.
// Tags: code39, barcode, margin, svg, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates barcode generation, margin configuration, and SVG export using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Code39 barcode, sets padding, and saves it as an SVG file.
    /// </summary>
    static void Main()
    {
        // Initialize the barcode generator with Code39 symbology and sample data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39, "Sample123"))
        {
            // Set uniform padding (10 points) on all sides of the barcode
            generator.Parameters.Barcode.Padding.Left.Point = 10f;
            generator.Parameters.Barcode.Padding.Top.Point = 10f;
            generator.Parameters.Barcode.Padding.Right.Point = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

            // Ensure the generated image size matches the barcode dimensions
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Save the barcode as an SVG file; the viewBox will correspond to the barcode size
            generator.Save("barcode.svg");
        }

        // Inform the user that the barcode has been successfully generated and saved
        Console.WriteLine("Barcode generated and saved as barcode.svg");
    }
}