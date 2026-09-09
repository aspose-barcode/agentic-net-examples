// Title: Generate Code128 barcode image with custom colors
// Description: Demonstrates how to set the barcode foreground to blue and background to white using Aspose.BarCode before saving the image as PNG.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize visual appearance of barcodes. It shows usage of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to create a barcode, adjust BarColor and BackColor properties, and export the result. Developers often need to modify colors to match branding or UI themes, and this snippet provides a quick reference for such scenarios.
// Prompt: Set barcode foreground to blue and background to white before generating the image.
// Tags: code128, barcode, color, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode with a blue foreground and white background,
/// then saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates the barcode, applies color settings, and writes the file.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output PNG file in the current directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // Initialize the barcode generator with Code128 symbology and the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            // Set the barcode (foreground) color to blue.
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the background color of the image to white.
            generator.Parameters.BackColor = Color.White;

            // Save the generated barcode as a PNG file to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}