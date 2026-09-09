// Title: Generate DataMatrix barcode with AutoSizeMode.Nearest using Aspose.BarCode
// Description: Demonstrates how to create a DataMatrix barcode image by setting AutoSizeMode to Nearest and specifying only the image width and height. The resulting PNG file is saved to the local directory.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and AutoSizeMode to control barcode sizing. Developers often need to generate barcodes with precise dimensions without manually calculating module sizes, and this pattern shows the typical workflow for creating and saving barcode images in PNG format.
// Prompt: Generate a barcode image using AutoSizeMode.Nearest, providing only ImageHeight and ImageWidth parameters.
// Tags: datamatrix, generate, png, barcodelibrary, autosizemode, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a DataMatrix barcode image using AutoSizeMode.Nearest.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a barcode, configures sizing, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file.
        string outputPath = Path.Combine(Environment.CurrentDirectory, "AutoSizeModeNearest.png");

        // Initialize the barcode generator with DataMatrix symbology and the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "ASPOSE"))
        {
            // Set the auto-size mode to automatically choose the nearest size that fits the content.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;

            // Specify the desired image dimensions (width and height) in pixels.
            generator.Parameters.ImageWidth.Pixels = 300f;
            generator.Parameters.ImageHeight.Pixels = 300f;

            // Save the generated barcode image to the specified path in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}