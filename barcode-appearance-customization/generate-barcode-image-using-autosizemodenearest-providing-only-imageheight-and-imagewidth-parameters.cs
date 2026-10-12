// Title: Generate DataMatrix barcode using AutoSizeMode.Nearest
// Description: Demonstrates how to create a DataMatrix barcode image with Aspose.BarCode by specifying only the image width and height. AutoSizeMode.Nearest automatically adjusts the barcode size to fit the given dimensions.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and AutoSizeMode to produce barcode images. Developers often need to generate barcodes with specific image dimensions while letting the library handle optimal scaling, a common requirement for printing, labeling, and UI display.
// Prompt: Generate a barcode image using AutoSizeMode.Nearest, providing only ImageHeight and ImageWidth parameters.
// Tags: datamatrix, autosizemode, nearest, imagegeneration, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a DataMatrix barcode image using AutoSizeMode.Nearest.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a barcode with specified image dimensions and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "Barcode_Nearest.png");

        // Initialize the barcode generator with DataMatrix symbology and sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "ASPOSE"))
        {
            // Configure AutoSizeMode to automatically fit the nearest size based on dimensions
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;

            // Set only the image width and height; other size parameters are auto‑adjusted
            generator.Parameters.ImageWidth.Pixels = 300f;
            generator.Parameters.ImageHeight.Pixels = 300f;

            // Save the generated barcode as a PNG image to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}