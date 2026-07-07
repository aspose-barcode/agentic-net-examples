// Title: Apply LightCoral Background to Barcode Image
// Description: Demonstrates setting a custom background color for a generated barcode and saving it as PNG.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to customize visual appearance using BarcodeGenerator and its Parameters. Typical use cases include branding, UI theming, and improving readability by adjusting background and bar colors. Developers often need to modify colors, sizes, and formats when integrating barcodes into applications.
// Prompt: Apply a custom background color named “LightCoral” to match UI theme requirements.
// Tags: barcode symbology, image generation, png output, background color, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode with a LightCoral background and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a barcode, applies custom colors, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Initialize the barcode generator with Code128 symbology and sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set the background color to LightCoral to match the UI theme.
            generator.Parameters.BackColor = Color.LightCoral;

            // Set the bar (foreground) color to Black for good contrast.
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Define the output file path for the PNG image.
            string outputPath = "barcode_lightcoral.png";

            // Save the generated barcode image to the specified file.
            generator.Save(outputPath);

            // Inform the user where the file was saved.
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}