// Title: Apply LightCoral background to a Code128 barcode image
// Description: Demonstrates how to set a custom LightCoral background color for a Code128 barcode and save it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and Parameters to customize barcode appearance. Typical use cases include branding, UI theming, and visual consistency across applications. Developers often need to adjust colors, sizes, and formats when integrating barcodes into user interfaces or printed materials.
// Prompt: Apply a custom background color named “LightCoral” to match UI theme requirements.
// Tags: barcode symbology, background color, png, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode with a LightCoral background and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, applies the custom background, and writes the file.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "barcode.png";

        // Initialize a BarcodeGenerator for the Code128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the text that the barcode will encode.
            generator.CodeText = "123ABC";

            // Apply a custom LightCoral background color (RGB 240,128,128, fully opaque).
            generator.Parameters.BackColor = Color.FromArgb(255, 240, 128, 128);

            // Save the generated barcode as a PNG file to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}