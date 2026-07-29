// Title: Generate barcode with AutoSizeMode.Nearest using image dimensions
// Description: Demonstrates creating a Code128 barcode image by specifying only the image height and width while using AutoSizeMode.Nearest for automatic sizing.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure AutoSizeMode and image dimensions. It uses BarcodeGenerator, EncodeTypes, AutoSizeMode, and BarCodeImageFormat classes. Typical use cases include generating barcode images for labels, invoices, or web applications where precise image size is required. Developers often need to control output size while letting the library adjust barcode scaling automatically.
// Prompt: Generate a barcode image using AutoSizeMode.Nearest, providing only ImageHeight and ImageWidth parameters.
// Tags: code128, autosizemode, nearest, imagewidth, imageheight, png, generation, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode image using AutoSizeMode.Nearest,
/// specifying only the desired image width and height.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a barcode, configures sizing, and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        const string outputPath = "barcode.png";

        // Initialize the barcode generator with Code128 symbology and sample data.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the automatic sizing mode to Nearest so the library adjusts the barcode
            // to best fit the specified image dimensions.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;

            // Specify the desired image width and height in points (1 point = 1/72 inch).
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Save the generated barcode image to the specified path in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user that the barcode image has been saved.
        Console.WriteLine($"Barcode image saved to '{outputPath}'.");
    }
}