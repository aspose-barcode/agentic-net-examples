// Title: Set Barcode Image Resolution to 300 DPI for Print Quality
// Description: Demonstrates how to configure Aspose.BarCode to generate a barcode image with a resolution of 300 DPI, suitable for high‑quality printing.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to adjust rendering parameters such as resolution. It uses the BarcodeGenerator class and its Parameters property to control output quality. Developers often need to set DPI when creating barcodes for print media, labels, or packaging.
// Prompt: Provide sample code demonstrating how to set barcode image resolution to 300 DPI for print quality.
// Tags: barcode, resolution, dpi, print, generation, aspnet, aspose.barcode, code128, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates setting barcode image resolution to 300 DPI using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates a Code128 barcode image with 300 DPI resolution and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Define output file path in the temporary directory
        string outputFile = Path.Combine(Path.GetTempPath(), "barcode_300dpi.png");

        // Create a barcode generator for Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the text to encode
            generator.CodeText = "1234567890";

            // Set the image resolution to 300 DPI for print quality
            generator.Parameters.Resolution = 300f;

            // Save the barcode image as PNG
            generator.Save(outputFile, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode image saved to: {outputFile}");
    }
}