// Title: Reset barcode background color from gray to white
// Description: Demonstrates how to change the background color of a generated barcode and then restore it to the default white.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of the BarcodeGenerator class and its Parameters property to modify visual attributes such as background color. Typical scenarios include customizing barcode appearance for branding or readability, then resetting to default for standard output. Developers often need to adjust colors dynamically before saving barcodes in various image formats.
// Prompt: Reset the barcode background to default white after previously setting it to gray.
// Tags: barcode symbology, background color, image output, aspose.barcode, barcode generation, png

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode, first with a gray background, then resets the background to white and saves both images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a barcode, changes its background color, and saves the results.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 symbology with sample text "123456"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set the barcode background to gray and save the image
            generator.Parameters.BackColor = Color.Gray;
            generator.Save("barcode_gray.png");

            // Reset the background color to the default white and save the second image
            generator.Parameters.BackColor = Color.White;
            generator.Save("barcode_white.png");
        }

        // Inform the user about the generated files
        Console.WriteLine("Barcodes generated: barcode_gray.png (gray background), barcode_white.png (white background).");
    }
}