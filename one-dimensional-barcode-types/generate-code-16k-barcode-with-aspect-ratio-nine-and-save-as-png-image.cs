// Title: Generate Code 16K barcode with aspect ratio 9 and save as PNG
// Description: Demonstrates creating a Code 16K barcode, configuring its aspect ratio to nine, and exporting it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.Code16K. It shows setting barcode parameters such as XDimension and specific symbology options (AspectRatio) before saving the result. Developers working with barcode creation, custom sizing, and image output will find this pattern useful for generating high‑density Code 16K barcodes in .NET applications.
// Prompt: Generate a Code 16K barcode with aspect ratio nine and save as PNG image.
// Tags: code16k, barcode generation, aspect ratio, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code 16K barcode with a nine‑to‑one aspect ratio and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, configures dimensions, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Build the full path for the output PNG file in the current directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "Code16KAspectRatio9.png");

        // Create a BarcodeGenerator for Code 16K symbology with the desired text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code16K, "Aspose.Barcode"))
        {
            // Set the X-dimension (module width) in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Configure the Code 16K specific aspect ratio to 9 (height = 9 × X-dimension).
            generator.Parameters.Barcode.Code16K.AspectRatio = 9f;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved barcode image.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}