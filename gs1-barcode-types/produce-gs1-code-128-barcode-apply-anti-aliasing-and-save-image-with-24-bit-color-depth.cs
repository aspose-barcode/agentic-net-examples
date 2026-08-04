// Title: Generate GS1 Code 128 Barcode with Anti-Aliasing and 24‑Bit PNG Output
// Description: Demonstrates how to create a GS1 Code 128 barcode, enable anti‑aliasing for smooth rendering, and save it as a 24‑bit PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and rendering parameters. Typical use cases include creating GS1‑compliant product barcodes for packaging, applying anti‑aliasing for high‑quality visuals, and exporting to common image formats with specific color depth. Developers often need to customize colors, rendering options, and output formats when integrating barcode generation into .NET applications.
// Prompt: Produce a GS1 Code 128 barcode, apply anti‑aliasing, and save the image with 24‑bit color depth.
// Tags: gs1, code128, barcode, generation, anti-aliasing, png, 24-bit, aspnet, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeSample
{
    /// <summary>
    /// Sample program that generates a GS1 Code 128 barcode, applies anti‑aliasing,
    /// and saves the result as a 24‑bit PNG image.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the application.
        /// </summary>
        static void Main()
        {
            // Initialize a barcode generator for GS1 Code 128 with a sample GTIN (01) value.
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, "(01)01234567890123"))
            {
                // Enable anti‑aliasing to improve visual quality of the rendered barcode.
                generator.Parameters.UseAntiAlias = true;

                // Set the bar (foreground) color to black.
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

                // Set the background color to white.
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Save the generated barcode as a PNG file.
                // PNG format uses 24‑bit color depth by default, meeting the requirement.
                generator.Save("gs1code128.png");
            }

            // Inform the user that the barcode has been generated.
            Console.WriteLine("Barcode generated and saved as gs1code128.png");
        }
    }
}