// Title: Generate QR Code with Custom Green Foreground and Black Background
// Description: This example creates a QR Code barcode, applies a green foreground and black background, and saves it as a PNG image.
// Category-Description: Demonstrates Aspose.BarCode generation of QR Code symbology using BarcodeGenerator. Shows how to customize barcode colors via Parameters.Barcode.BarColor and Parameters.BackColor. Ideal for developers needing styled QR codes for branding or UI integration, covering common tasks like setting foreground/background colors and exporting to PNG.
// Prompt: Generate QR Code barcode and apply custom color palette with green foreground and black background.
// Tags: qr code, color palette, png, generation, aspose.barcode

using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing;

namespace BarcodeExample
{
    /// <summary>
    /// Provides an entry point that generates a QR Code with a custom color palette.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Generates a QR Code barcode, applies green foreground and black background colors,
        /// saves the image as PNG, and writes the output path to the console.
        /// </summary>
        static void Main()
        {
            // Define the output file name and location
            string outputPath = "qr_green.png";

            // Initialize the QR Code generator with the desired text
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
            {
                // Set the barcode (foreground) color to green
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Green;

                // Set the image background color to black
                generator.Parameters.BackColor = Aspose.Drawing.Color.Black;

                // Render and save the barcode as a PNG file
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            // Inform the user where the file was saved
            Console.WriteLine($"QR Code saved to {outputPath}");
        }
    }
}