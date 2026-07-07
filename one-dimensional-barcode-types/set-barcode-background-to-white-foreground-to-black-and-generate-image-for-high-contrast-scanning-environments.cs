// Title: Generate high‑contrast barcode image (white background, black foreground)
// Description: Demonstrates setting barcode colors for optimal scanning and saving as PNG.
// Category-Description: This example belongs to Aspose.BarCode generation examples, showing how to configure visual properties such as BarColor and BackColor using BarcodeGenerator. Typical use cases include creating high‑contrast barcodes for industrial scanners. Developers often need to adjust colors, size, and output format when integrating barcode generation into .NET applications.
// Prompt: Set barcode background to white, foreground to black, and generate image for high‑contrast scanning environments.
// Tags: barcode, generation, high-contrast, png, code128, aspnet, aspose.barcode, color

using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeExample
{
    /// <summary>
    /// Demonstrates generating a Code128 barcode with white background and black foreground,
    /// suitable for high‑contrast scanning environments.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point. Generates the barcode image and saves it to disk.
        /// </summary>
        static void Main()
        {
            // Define the output file path for the generated PNG image
            string outputPath = "high_contrast_barcode.png";

            // Initialize a BarcodeGenerator for Code128 symbology with sample data
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Set the barcode (bars) color to black for maximum contrast
                generator.Parameters.Barcode.BarColor = Color.Black;

                // Set the image background color to white
                generator.Parameters.BackColor = Color.White;

                // Optionally specify the image dimensions (width and height in points)
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 150f;

                // Save the generated barcode as a PNG file at the specified path
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            // Inform the user where the image has been saved
            Console.WriteLine($"Barcode image saved to {outputPath}");
        }
    }
}