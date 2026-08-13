// Title: Generate a purple Code128 barcode using Aspose.BarCode
// Description: Demonstrates how to set a custom foreground color (RGB 128,0,128) for a barcode image, useful for branding.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and color parameters to customize barcode appearance. Typical use cases include creating branded barcodes for product packaging, marketing materials, or internal tracking where brand colors are required. Developers often need to adjust bar and background colors, select symbology, and export to common image formats.
// Prompt: Apply a custom foreground color using RGB (128,0,128) to produce a purple barcode for branding purposes.
// Tags: code128, barcode generation, color customization, png output, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a purple Code128 barcode and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a BarcodeGenerator, sets custom colors, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image
        string outputPath = "purple_barcode.png";

        // Initialize the barcode generator with Code128 symbology
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the text that will be encoded into the barcode
            generator.CodeText = "Brand123";

            // Apply a custom purple color (RGB 128,0,128) to the barcode bars
            generator.Parameters.Barcode.BarColor = Color.FromArgb(128, 0, 128);

            // Ensure the background remains white (default is white, but set explicitly for clarity)
            generator.Parameters.BackColor = Color.White;

            // Save the generated barcode as a PNG image to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}