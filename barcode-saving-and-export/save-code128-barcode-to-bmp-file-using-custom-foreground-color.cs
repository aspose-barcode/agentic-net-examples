// Title: Save Code128 barcode as BMP with custom foreground color
// Description: Demonstrates generating a Code128 barcode and saving it as a BMP image using a custom foreground color.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes, BarCodeImageFormat, and color customization. Typical use cases include creating barcodes for product labeling, inventory tracking, and packaging where specific visual styling is required. Developers often need to adjust barcode colors and output formats to match branding or printing specifications.
// Prompt: Save a Code128 barcode to a BMP file using a custom foreground color.
// Tags: code128, barcode, bmp, color, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode, applies a custom foreground color, and saves it as a BMP file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output BMP file.
        string outputPath = Path.Combine(Environment.CurrentDirectory, "Code128_CustomColor.bmp");

        // Initialize the barcode generator with Code128 symbology and the desired data.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Set the barcode's foreground (bar) color to red.
            generator.Parameters.Barcode.BarColor = Color.Red;

            // Save the generated barcode as a BMP image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Output the location of the saved barcode image.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}