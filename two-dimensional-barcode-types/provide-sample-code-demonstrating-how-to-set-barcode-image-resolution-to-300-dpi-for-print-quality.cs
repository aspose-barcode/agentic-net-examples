// Title: Set Barcode Image Resolution to 300 DPI
// Description: Demonstrates how to generate a barcode image with a resolution of 300 DPI, suitable for high‑quality printing.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure image resolution using the BarcodeGenerator class. Developers often need to produce barcodes for printed materials where DPI impacts clarity; the API allows setting the Resolution property before saving in common formats like PNG.
// Prompt: Provide sample code demonstrating how to set barcode image resolution to 300 DPI for print quality.
// Tags: barcode symbology, resolution, print quality, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a Code128 barcode image with a resolution of 300 DPI and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file in the current directory.
        string outputFile = Path.Combine(Directory.GetCurrentDirectory(), "Barcode300DPI.png");

        // Create a BarcodeGenerator for Code128 symbology with the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the image resolution to 300 DPI for print‑quality output.
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode as a PNG image to the specified path.
            generator.Save(outputFile, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode image saved to: {outputFile}");
    }
}