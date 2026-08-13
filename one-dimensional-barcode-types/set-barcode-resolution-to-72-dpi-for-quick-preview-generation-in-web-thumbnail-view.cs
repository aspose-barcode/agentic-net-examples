// Title: Generate a low‑resolution barcode thumbnail (72 DPI) using Aspose.BarCode
// Description: Demonstrates how to create a Code128 barcode image with a resolution of 72 DPI, suitable for quick preview thumbnails in web applications.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and resolution settings. Developers often need to produce low‑resolution barcode images for thumbnails, previews, or email attachments, where speed and file size are more important than print quality. The snippet shows the typical workflow of configuring barcode parameters and saving the result.
// Prompt: Set barcode resolution to 72 DPI for quick preview generation in a web thumbnail view.
// Tags: barcode, code128, resolution, thumbnail, preview, aspose.barcode, image generation, png

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode image with a low resolution (72 DPI) for use as a web thumbnail.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for the Code128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Define the data to encode in the barcode.
            generator.CodeText = "123456";

            // Configure the image resolution to 72 DPI to keep the file lightweight for quick previews.
            generator.Parameters.Resolution = 72f;

            // Render the barcode and write it to a PNG file.
            generator.Save("barcode_thumbnail.png");
        }
    }
}