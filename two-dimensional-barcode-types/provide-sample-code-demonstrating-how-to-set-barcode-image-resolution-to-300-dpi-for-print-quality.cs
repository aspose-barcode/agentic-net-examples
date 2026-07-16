// Title: Set barcode image resolution to 300 DPI for high‑quality printing
// Description: Demonstrates how to configure Aspose.BarCode to generate a barcode image with a resolution of 300 DPI, suitable for print media.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator and its Parameters property to control output quality. Developers often need to adjust resolution, size, and format when creating barcodes for labels, packaging, or printed documents. The key classes shown are BarcodeGenerator, EncodeTypes, and the Parameters.Resolution setting, which are common in print‑ready barcode generation scenarios.
// Prompt: Provide sample code demonstrating how to set barcode image resolution to 300 DPI for print quality.
// Tags: code128, resolution, png, barcode, generation, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates setting barcode image resolution to 300 DPI using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode saved as a PNG with 300 DPI resolution.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for the Code128 symbology with sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Configure the image resolution to 300 DPI for high‑quality print output.
            generator.Parameters.Resolution = 300f;

            // Persist the generated barcode as a PNG file.
            generator.Save("barcode_300dpi.png");
        }

        // Inform the user that the barcode has been created.
        Console.WriteLine("Barcode generated with 300 DPI resolution.");
    }
}