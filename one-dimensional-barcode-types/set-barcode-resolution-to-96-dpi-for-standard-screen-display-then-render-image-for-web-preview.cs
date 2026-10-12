// Title: Set Barcode Resolution to 96 DPI and Save as PNG
// Description: Demonstrates how to configure a barcode image's resolution to 96 DPI for optimal screen display and generate a PNG file for web preview.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Typical scenarios include creating barcodes for e‑commerce sites, digital tickets, or any web‑based application where a standard screen resolution is required. Developers often need to adjust resolution, format, and symbology to meet UI and performance requirements.
// Prompt: Set barcode resolution to 96 DPI for standard screen display, then render image for web preview.
// Tags: barcode, code128, resolution, png, aspose.barcode, image-generation, web-preview

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a Code128 barcode, sets its resolution to 96 DPI, and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures resolution, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output PNG file in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // Initialize the barcode generator with Code128 symbology and the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            // Set the image resolution to 96 DPI, suitable for standard screen display.
            generator.Parameters.Resolution = 96f;

            // Save the generated barcode as a PNG file at the specified location.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}