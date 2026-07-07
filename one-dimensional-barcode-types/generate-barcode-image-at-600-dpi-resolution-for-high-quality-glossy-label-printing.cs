// Title: Generate high‑resolution Code128 barcode image
// Description: Demonstrates creating a Code128 barcode and saving it as a PNG with 600 DPI resolution for high‑quality glossy label printing.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure barcode parameters such as resolution using the BarcodeGenerator class. Typical use cases include producing high‑resolution images for printing on glossy labels, packaging, or product identification. Developers often need to set DPI, choose symbology, and export to common image formats.
// Prompt: Generate a barcode image at 600 DPI resolution for high‑quality glossy label printing.
// Tags: code128, barcode generation, resolution, png, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeSample
{
    /// <summary>
    /// Sample program that generates a Code128 barcode image with a resolution of 600 DPI.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the application. Creates a barcode, sets high resolution, and saves it as PNG.
        /// </summary>
        static void Main()
        {
            // Initialize the barcode generator with Code128 symbology and sample data.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Configure the generator to use 600 DPI for high‑quality glossy label printing.
                generator.Parameters.Resolution = 600f;

                // Save the generated barcode as a PNG file.
                generator.Save("barcode.png");
            }

            // Inform the user that the barcode image has been created.
            Console.WriteLine("Barcode image generated at 600 DPI: barcode.png");
        }
    }
}