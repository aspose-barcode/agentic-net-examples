// Title: Generate a PNG barcode image with dimensions specified in inches
// Description: Demonstrates how to create a barcode using Aspose.BarCode, set its size in inches, and save it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and image parameter settings. Developers often need to control barcode dimensions and resolution for printing or UI display, and this snippet shows typical steps for setting measurement units, size, and exporting to common image formats.
// Prompt: Instantiate BarcodeGenerator, set unit to Inches, specify width and height, and generate a PNG image.
// Tags: barcode, code128, generation, inches, width, height, png, aspose.barcode, image, resolution

using System;
using System.IO;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode image with custom dimensions in inches and saving it as PNG.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a barcode, configures size in inches, sets resolution, and writes the PNG file.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // Initialize the barcode generator with Code128 symbology and sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set the barcode image width and height using inches as the measurement unit
            generator.Parameters.ImageWidth.Inches = 2.0f;   // 2 inches wide
            generator.Parameters.ImageHeight.Inches = 1.0f;  // 1 inch tall

            // Optionally define the image resolution (dots per inch)
            generator.Parameters.Resolution = 300;

            // Save the generated barcode as a PNG image to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}