// Title: Generate Postnet 2‑state barcode with custom bar height
// Description: Demonstrates creating a Postnet 2‑state postal barcode and saving it as a PNG image, with the bar height overridden to 30 points.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure barcode parameters such as bar height for postal symbologies. It uses the BarcodeGenerator class together with EncodeTypes and BarCodeImageFormat to produce image files. Developers often need to customize visual properties of generated barcodes for printing or embedding in documents.
// Prompt: Generate a Postnet 2‑state postal barcode with overridden BarHeight set to 30 points.
// Tags: postnet, barcode, generation, barheight, png, aspnet, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Postnet 2‑state barcode with a custom bar height and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, sets bar height, saves the image, and writes the output path.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file
        string outputPath = Path.Combine(Environment.CurrentDirectory, "PostnetBarcode.png");

        // Initialize the barcode generator with Postnet symbology and the data to encode
        using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, "123456"))
        {
            // Override the default bar height to 30 points
            generator.Parameters.Barcode.BarHeight.Point = 30f;

            // Save the generated barcode as a PNG image to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved barcode image
        Console.WriteLine($"Postnet barcode saved to: {outputPath}");
    }
}