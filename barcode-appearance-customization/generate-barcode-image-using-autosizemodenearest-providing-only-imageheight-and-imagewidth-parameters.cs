// Title: Generate DataMatrix barcode with AutoSizeMode.Nearest using Aspose.BarCode
// Description: Demonstrates how to create a DataMatrix barcode image by setting AutoSizeMode to Nearest and specifying only image width and height.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and AutoSizeMode for sizing barcodes. Developers often need to generate barcode images with precise dimensions without manually calculating module sizes; this snippet shows the typical workflow for such scenarios.
// Prompt: Generate a barcode image using AutoSizeMode.Nearest, providing only ImageHeight and ImageWidth parameters.
// Tags: datamatrix, autosizemode, nearest, imagegeneration, png, aspose.barcode, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a DataMatrix barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a barcode, configures auto‑size mode, sets image dimensions, and saves the result as a PNG file.
    /// </summary>
    static void Main()
    {
        // Text to encode in the barcode
        string codeText = "ASPOSE";

        // Build the full output file path in the current directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "AutoSizeNearest.png");

        // Initialize the barcode generator with DataMatrix symbology and the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Set auto‑size mode to Nearest so the generator chooses the closest module size
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;

            // Specify the desired image dimensions (in pixels)
            generator.Parameters.ImageWidth.Pixels = 300f;
            generator.Parameters.ImageHeight.Pixels = 300f;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}