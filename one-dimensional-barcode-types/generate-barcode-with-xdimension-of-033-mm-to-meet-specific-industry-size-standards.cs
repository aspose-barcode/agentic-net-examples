// Title: Generate Code128 barcode with precise XDimension
// Description: Demonstrates creating a Code128 barcode image with an XDimension of 0.33 mm, suitable for industry‑specific size requirements.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode parameters such as module size (XDimension) using the BarcodeGenerator class. Typical use cases include producing barcodes that must conform to strict dimensional standards for packaging, logistics, or retail scanning. Developers often need to adjust XDimension, set symbology, and export to common image formats like PNG.
// Prompt: Generate a barcode with XDimension of 0.33 mm to meet specific industry size standards.
// Tags: code128, barcode generation, png, xdimension, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode image with a specific XDimension.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "barcode.png";

        // Initialize a BarcodeGenerator for the Code128 symbology.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the text that will be encoded into the barcode.
            generator.CodeText = "1234567890";

            // Configure the XDimension (module width) to 0.33 millimeters.
            generator.Parameters.Barcode.XDimension.Millimeters = 0.33f;

            // Save the generated barcode as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}