// Title: Generate Code128 Barcode Image with Specified Size in Inches
// Description: This example creates a Code128 barcode, sets its dimensions in inches, and saves it as a PNG file.
// Category-Description: Demonstrates Aspose.BarCode generation API usage for creating barcodes with custom image size and resolution. It covers the BarcodeGenerator class, EncodeTypes enumeration, and image format options, which are common tasks for developers needing to produce printable or displayable barcodes in .NET applications.
// Prompt: Instantiate BarcodeGenerator, set unit to Inches, specify width and height, and generate a PNG image.
// Tags: barcode, code128, generation, inches, image size, png, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode image with custom dimensions in inches.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, configures size and resolution, and saves as PNG.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output PNG file
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // Initialize the barcode generator with Code128 symbology and the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set image width to 2 inches
            generator.Parameters.ImageWidth.Inches = 2f;

            // Set image height to 1 inch
            generator.Parameters.ImageHeight.Inches = 1f;

            // Optional: increase resolution to 300 DPI for higher quality output
            generator.Parameters.Resolution = 300;

            // Save the generated barcode as a PNG image to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}