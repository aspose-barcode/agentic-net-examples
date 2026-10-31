// Title: Create a Code128 barcode with a custom background color and save as GIF
// Description: Demonstrates how to generate a Code128 barcode, apply a light blue background, and export it as a GIF image suitable for web pages.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to customize barcode appearance and output format. Developers often need to adjust visual properties such as background color and choose web‑friendly image formats like GIF when embedding barcodes in HTML or email content.
// Prompt: Create a barcode with custom background color and export it as a GIF image for web use.
// Tags: code128, background-color, gif, generation, aspose.barcode, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode with a custom background color and saves it as a GIF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, applies styling, and writes the file to disk.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output GIF file.
        string outputFile = Path.Combine(Directory.GetCurrentDirectory(), "custom_background_barcode.gif");

        // Ensure the target directory exists.
        Directory.CreateDirectory(Path.GetDirectoryName(outputFile));

        // Initialize the barcode generator with Code128 symbology and the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Set a light blue background color for the barcode image.
            generator.Parameters.BackColor = Color.LightBlue;

            // Save the generated barcode as a GIF file.
            generator.Save(outputFile, BarCodeImageFormat.Gif);
        }

        // Output the location of the saved barcode to the console.
        Console.WriteLine($"Barcode saved to: {outputFile}");
    }
}