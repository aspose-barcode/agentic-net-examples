// Title: Generate barcode with AutoSizeMode.Nearest using only image dimensions
// Description: Demonstrates creating a Code128 barcode image by setting AutoSizeMode to Nearest and specifying only ImageWidth and ImageHeight. Useful for quickly generating barcodes with desired size without manual scaling.
// Prompt: Generate a barcode image using AutoSizeMode.Nearest, providing only ImageHeight and ImageWidth parameters.
// Tags: code128, barcode, autosizemode, nearest, imagegeneration, aspnet, csharp

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode image using AutoSizeMode.Nearest.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a barcode generator, configures size, saves the image, and writes a confirmation to console.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set AutoSizeMode to Nearest to let the library choose the best size.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;

            // Define only the image dimensions (width and height) in points.
            generator.Parameters.ImageWidth.Point = 300f;   // Width in points.
            generator.Parameters.ImageHeight.Point = 150f; // Height in points.

            // Save the generated barcode image to a PNG file.
            generator.Save("barcode.png");
        }

        // Inform the user that the barcode image has been created.
        Console.WriteLine("Barcode image generated: barcode.png");
    }
}