// Title: Override Barcode Image Size with Explicit Width and Height
// Description: Demonstrates how to set ImageWidth and ImageHeight while keeping AutoSizeMode set to Interpolation, producing a PNG barcode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to create custom-sized barcodes. Developers often need to control barcode dimensions for layout consistency in reports, labels, or UI elements. The snippet illustrates typical steps: initializing the generator, configuring sizing parameters, and saving the image.
// Prompt: Override default sizing by setting explicit ImageHeight and ImageWidth while AutoSizeMode remains Interpolation.
// Tags: barcode symbology, generation, image size, autosizemode, png, aspose.barcode, code128

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates overriding default barcode image sizing by setting explicit width and height while keeping AutoSizeMode set to Interpolation.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Code128 barcode with custom dimensions and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define a temporary output folder and ensure it exists
        string outputFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Build the full path for the resulting PNG file
        string outputPath = Path.Combine(outputFolder, "barcode.png");

        // Initialize the barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Keep AutoSizeMode as Interpolation and set explicit image dimensions
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Pixels = 300f;   // explicit width in pixels
            generator.Parameters.ImageHeight.Pixels = 150f;  // explicit height in pixels

            // Save the generated barcode image to the specified path in PNG format
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}