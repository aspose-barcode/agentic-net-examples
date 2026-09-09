// Title: Generate Code 16K barcode with aspect ratio 9 and save as PNG
// Description: Demonstrates creating a Code 16K barcode, setting its aspect ratio to nine, and saving the result as a PNG image file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure barcode parameters such as X‑dimension and specific symbology options (Code 16K) before rendering. It uses the BarcodeGenerator class together with EncodeTypes and BarCodeImageFormat to produce image output. Developers often need to customize dimensions, aspect ratios, or other symbology settings when integrating barcodes into documents, labels, or web applications.
// Prompt: Generate a Code 16K barcode with aspect ratio nine and save as PNG image.
// Tags: code16k, aspectratio, png, barcode generation, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code 16K barcode with a custom aspect ratio and saving it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures parameters, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output PNG file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "Code16KAspectRatio9.png");

        // Initialize the barcode generator with Code16K symbology and the desired text.
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.Code16K, "Aspose.BarCode"))
        {
            // Set the X-dimension (pixel width of the narrow bar) to 2 pixels.
            gen.Parameters.Barcode.XDimension.Pixels = 2;

            // Configure the aspect ratio specific to the Code16K symbology to 9.
            gen.Parameters.Barcode.Code16K.AspectRatio = 9;

            // Save the generated barcode as a PNG image to the specified path.
            gen.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved barcode image.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}