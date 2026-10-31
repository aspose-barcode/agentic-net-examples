// Title: Generate Code128 barcode with custom corporate foreground color
// Description: Demonstrates how to create a Code128 barcode and apply a custom orange foreground color using a hexadecimal value, then save it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and color customization. Developers often need to brand barcodes with corporate colors for printed materials, packaging, or digital displays. The snippet shows typical steps: configure barcode parameters, set bar color, and export the image.
// Prompt: Apply custom foreground color using hexadecimal value #FF6600 to match corporate branding.
// Tags: code128, barcode generation, custom color, png output, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode with a custom foreground color.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, applies the color, saves the image, and writes the output path.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the barcode
        string codeText = "123456789";

        // Determine the full path for the output PNG file
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "custom_color_barcode.png");

        // Create a BarcodeGenerator for Code128 with the specified text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set custom foreground (bar) color using hexadecimal #FF6600 (orange)
            generator.Parameters.Barcode.BarColor = Color.FromArgb(255, 102, 0);

            // Save the generated barcode image as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}