// Title: Generate non-square barcode using interpolation mode
// Description: Demonstrates how to create a barcode image with a rectangular aspect ratio by setting ImageHeight lower than ImageWidth and using the Interpolation auto‑size mode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to customize barcode dimensions with the BarcodeGenerator class. It covers setting AutoSizeMode, image width/height, and X‑dimension for common symbologies like Code128. Developers often need to produce barcodes that fit specific layout constraints or non‑standard aspect ratios, and this snippet illustrates the typical API usage for such scenarios.
// Prompt: Generate a barcode with a non‑square aspect ratio by setting ImageHeight lower than ImageWidth in Interpolation mode.
// Tags: barcode, code128, non-square, interpolation, autosizemode, image, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a Code128 barcode with a non‑square aspect ratio
/// using the Interpolation auto‑size mode and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "NonSquareBarcode.png");

        // Create a BarcodeGenerator for Code128 with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the auto‑size mode to Interpolation to allow custom image dimensions.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Configure the image width and height to achieve a rectangular shape.
            generator.Parameters.ImageWidth.Pixels = 300f;   // Width in pixels
            generator.Parameters.ImageHeight.Pixels = 150f;  // Height in pixels (half of width)

            // Adjust the X‑dimension (module width) for visual clarity.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Save the generated barcode as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}