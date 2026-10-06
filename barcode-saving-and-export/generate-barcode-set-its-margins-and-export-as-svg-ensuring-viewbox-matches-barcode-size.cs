// Title: Generate Code128 barcode with margins and export to SVG with viewBox matching size
// Description: Demonstrates creating a Code128 barcode, applying uniform padding, and saving it as an SVG file where the viewBox aligns with the barcode dimensions.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode parameters such as padding and export options using the BarcodeGenerator class. Typical use cases include creating barcodes for web or print with precise layout control, where developers need to export vector graphics (SVG) that preserve exact sizing. The snippet shows common steps: instantiate BarcodeGenerator, adjust Parameters, and call Save with BarCodeImageFormat.Svg.
// Prompt: Generate a barcode, set its margins, and export as SVG ensuring the viewBox matches the barcode size.
// Tags: code128, barcode, margin, svg, viewbox, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a Code128 barcode, applies padding, and saves it as an SVG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, configures margins, and writes the SVG output.
    /// </summary>
    static void Main()
    {
        // Define the temporary output path for the SVG file
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode.svg");
        // Text to encode in the barcode
        string codeText = "1234567890";

        // Initialize the barcode generator with Code128 symbology and the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set uniform margins (padding) of 10 points on each side of the barcode
            generator.Parameters.Barcode.Padding.Left.Point = 10f;
            generator.Parameters.Barcode.Padding.Top.Point = 10f;
            generator.Parameters.Barcode.Padding.Right.Point = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

            // Attempt to save the barcode as an SVG file; the viewBox will match the barcode size
            try
            {
                generator.Save(outputPath, BarCodeImageFormat.Svg);
                Console.WriteLine($"Barcode saved to: {outputPath}");
            }
            catch (Exception ex)
            {
                // Provide a clearer message if the exception is related to evaluation licensing
                if (ex.Message != null && ex.Message.Contains("evaluation"))
                {
                    Console.WriteLine("A valid license is required for SVG export in evaluation mode.");
                }
                else
                {
                    Console.WriteLine($"Error saving barcode: {ex.Message}");
                }
            }
        }
    }
}