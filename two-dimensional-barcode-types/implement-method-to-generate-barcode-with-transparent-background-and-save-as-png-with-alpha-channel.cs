// Title: Generate a Code128 barcode with transparent background and save as PNG
// Description: Demonstrates how to create a barcode image with a transparent background using Aspose.BarCode and save it as a PNG file that retains the alpha channel.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Typical scenarios include creating overlay‑friendly barcodes for UI designs, reports, or web pages where the background must blend with surrounding content. Developers often need to control background transparency and output formats when integrating barcodes into graphics pipelines.
// Prompt: Implement method to generate barcode with transparent background and save as PNG with alpha channel.
// Tags: barcode, code128, transparent background, png, alpha channel, aspose.barcode, image generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeTransparentExample
{
    /// <summary>
    /// Provides an entry point that generates a barcode with a transparent background and saves it as a PNG file.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main method – defines input data, invokes the generation routine, and reports the output location.
        /// </summary>
        static void Main()
        {
            // Sample barcode text to encode
            string codeText = "Sample123";

            // Build a temporary file path for the resulting PNG image
            string outputPath = Path.Combine(Path.GetTempPath(), "transparent_barcode.png");

            // Generate the barcode with a transparent background
            GenerateTransparentBarcode(codeText, outputPath);

            // Inform the user where the file was saved
            Console.WriteLine($"Barcode saved to: {outputPath}");
        }

        /// <summary>
        /// Generates a barcode image with a transparent background and saves it as PNG.
        /// </summary>
        /// <param name="codeText">The text to encode in the barcode.</param>
        /// <param name="outputPath">The full file path where the PNG will be saved.</param>
        static void GenerateTransparentBarcode(string codeText, string outputPath)
        {
            // Ensure the output directory exists
            string directory = Path.GetDirectoryName(outputPath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Create a BarcodeGenerator for Code128 (symbology can be changed as needed)
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Set the background color to transparent so the PNG retains an alpha channel
                generator.Parameters.BackColor = Aspose.Drawing.Color.Transparent;

                // Optionally, set the bar (foreground) color; default is black
                // generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

                // Save the barcode as PNG, which supports transparency
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }
        }
    }
}