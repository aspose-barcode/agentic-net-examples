// Title: Generate High‑Resolution DataMatrix PNG with Interpolation AutoSizeMode
// Description: Demonstrates how to configure Aspose.BarCode to produce a high‑resolution PNG image of a DataMatrix barcode by setting AutoSizeMode to Interpolation and specifying image dimensions.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to create printable barcodes. Developers often need to control image resolution, size, and scaling behavior for high‑quality output in documents, labels, or packaging. The snippet shows typical steps such as setting resolution, auto‑size mode, and fixed pixel dimensions, which are common requirements when integrating barcode generation into .NET applications.
// Prompt: Configure AutoSizeMode to Interpolation, set ImageWidth and ImageHeight, and generate a high‑resolution PNG barcode.
// Tags: datamatrix, barcode generation, high resolution, png, autosizemode, interpolation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a high‑resolution DataMatrix barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a PNG barcode with specific resolution and size settings.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "HighResBarcode.png");

        // Ensure the target directory exists; create it if necessary.
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Initialize the barcode generator for a DataMatrix symbology with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "ASPOSE"))
        {
            // Set the image resolution to 300 DPI for high‑quality output.
            generator.Parameters.Resolution = 300f;

            // Use Interpolation mode to let the generator scale the barcode smoothly.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Specify fixed image dimensions in pixels (1200×1200).
            generator.Parameters.ImageWidth.Pixels = 1200f;
            generator.Parameters.ImageHeight.Pixels = 1200f;

            // Optionally increase the X‑dimension (module size) for better visual clarity.
            generator.Parameters.Barcode.XDimension.Pixels = 3f;

            // Save the generated barcode as a PNG file at the specified location.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}