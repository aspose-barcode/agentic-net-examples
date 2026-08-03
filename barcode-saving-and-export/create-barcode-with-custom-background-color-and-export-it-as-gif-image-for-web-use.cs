// Title: Generate a Code128 barcode with custom background color and save as GIF
// Description: Demonstrates creating a Code128 barcode, applying a light gray background, and exporting it as a GIF image suitable for web pages.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to customize barcode appearance using the BarcodeGenerator class. Typical use cases include branding, UI integration, and web-friendly image output. Developers often need to adjust colors, formats, and symbologies when embedding barcodes in web applications.
// Prompt: Create a barcode with custom background color and export it as a GIF image for web use.
// Tags: code128, barcode generation, gif, background color, aspose.barcode, aspnet

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeExample
{
    /// <summary>
    /// Provides an example that creates a Code128 barcode, sets a custom background color,
    /// and saves the result as a GIF image for web usage.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the example. Generates the barcode and writes the output path to the console.
        /// </summary>
        static void Main()
        {
            // Define the output file name and format
            string outputPath = "barcode.gif";

            // Initialize the barcode generator with Code128 symbology and sample data
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Apply a light gray background color to the generated image
                generator.Parameters.BackColor = Color.LightGray;

                // Save the barcode as a GIF image, which is optimal for web delivery
                generator.Save(outputPath, BarCodeImageFormat.Gif);
            }

            // Inform the user where the barcode image has been saved
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}