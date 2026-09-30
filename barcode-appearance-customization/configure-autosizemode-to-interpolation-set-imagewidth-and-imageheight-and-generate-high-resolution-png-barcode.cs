// Title: Generate High‑Resolution PNG Barcode with Interpolation AutoSizeMode
// Description: Demonstrates how to configure Aspose.BarCode to produce a high‑resolution PNG barcode using Interpolation auto‑size mode and fixed image dimensions.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It showcases the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to create barcodes suitable for printing, scanning, and embedding in documents. Developers often need to control resolution, image size, and scaling behavior to meet quality and layout requirements.
// Prompt: Configure AutoSizeMode to Interpolation, set ImageWidth and ImageHeight, and generate a high‑resolution PNG barcode.
// Tags: barcode, code128, highresolution, png, autosizemode, interpolation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a high‑resolution PNG barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a Code128 barcode with specified resolution,
    /// auto‑size mode, and image dimensions, then saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "high_res_barcode.png");

        // Initialize a BarcodeGenerator for the Code128 symbology with sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set the desired resolution (dots per inch) for high‑quality output.
            generator.Parameters.Resolution = 300f;

            // Configure the auto‑size mode to use interpolation scaling.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Specify fixed image dimensions in pixels (width x height).
            generator.Parameters.ImageWidth.Pixels = 2400f;   // Width: 2400 pixels
            generator.Parameters.ImageHeight.Pixels = 1200f; // Height: 1200 pixels

            // Save the generated barcode as a PNG image at the defined path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}