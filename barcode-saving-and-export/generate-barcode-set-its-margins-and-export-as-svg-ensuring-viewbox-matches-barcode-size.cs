// Title: Generate Code128 barcode with custom margins and export to SVG
// Description: Demonstrates creating a Code128 barcode, applying uniform padding, and saving it as an SVG file where the viewBox matches the barcode dimensions.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode parameters such as padding and export options using the BarcodeGenerator class. Typical use cases include creating barcodes for web or print with precise layout control. Developers often need to adjust margins and output formats like SVG for scalable graphics.
// Prompt: Generate a barcode, set its margins, and export as SVG ensuring the viewBox matches the barcode size.
// Tags: code128, barcode, margin, svg, export, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a Code128 barcode, sets custom margins, and saves it as an SVG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output directory and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting SVG file
        string svgPath = Path.Combine(outputDir, "barcode.svg");

        // Initialize the barcode generator with Code128 symbology and the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Configure uniform padding (margins) around the barcode in points
            generator.Parameters.Barcode.Padding.Left.Point = 10f;
            generator.Parameters.Barcode.Padding.Top.Point = 10f;
            generator.Parameters.Barcode.Padding.Right.Point = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

            try
            {
                // Save the barcode as an SVG file; the viewBox will match the barcode size including padding
                generator.Save(svgPath, BarCodeImageFormat.Svg);
                Console.WriteLine($"SVG barcode saved to: {svgPath}");
            }
            catch (Exception ex)
            {
                // Output any errors that occur during the save operation
                Console.WriteLine($"Error saving SVG: {ex.Message}");
            }
        }
    }
}