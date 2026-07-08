// Title: Barcode width reduction example using Aspose.BarCode
// Description: Demonstrates how to reduce the bar width of a Code128 barcode by 20 percent to fit narrow label spaces.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize barcode appearance using the BarcodeGenerator class and its Parameters.Barcode properties. Typical use cases include adjusting bar dimensions for limited label real‑estate, ensuring readability while meeting size constraints. Developers often need to modify bar width, height, margins, or other visual parameters before saving the barcode to an image or document.
// Prompt: Set barcode width reduction to 20 percent to fit narrow label spaces.
// Tags: barcode, width reduction, code128, png, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an entry point that generates a Code128 barcode with a 20 percent width reduction
/// and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Creates a <see cref="BarcodeGenerator"/> for Code128, applies a 20 percent bar width reduction,
    /// saves the barcode to a file, and writes a completion message to the console.
    /// </summary>
    static void Main()
    {
        // Initialize the barcode generator with the desired symbology (Code128) and data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Apply a 20 percent reduction to the bar width (specified in points).
            generator.Parameters.Barcode.BarWidthReduction.Point = 20f;

            // Save the generated barcode as a PNG image file.
            generator.Save("barcode.png");
        }

        // Output a simple confirmation that the barcode was generated successfully.
        Console.WriteLine("Barcode generated with 20% width reduction.");
    }
}