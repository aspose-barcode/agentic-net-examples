// Title: Generate a Code128 barcode with lime foreground color and save as high‑quality TIFF
// Description: This example creates a Code128 barcode, applies a custom lime (#00FF00) bar color, sets a high resolution, and saves the result as a TIFF file.
// Category-Description: Aspose.BarCode generation examples showing how to customize barcode appearance and output format. It covers using BarcodeGenerator, setting Parameters such as Resolution and BarColor, and saving to image formats like TIFF. Developers often need to produce high‑resolution barcodes for print media, requiring precise color and DPI control.
// Prompt: Generate a barcode with custom foreground color #00FF00 (lime) and save as a high‑quality TIFF file.
// Tags: code128, barcode-generation, tiff, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeExample
{
    /// <summary>
    /// Demonstrates generating a Code128 barcode with a custom lime foreground color and saving it as a high‑quality TIFF image.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the example. Creates the barcode, configures appearance, and writes the image to disk.
        /// </summary>
        static void Main()
        {
            // Define the output file name
            string outputFile = "barcode.tiff";

            // Initialize a BarcodeGenerator for Code128 with sample data
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Configure high resolution (e.g., 300 DPI) for print‑quality output
                generator.Parameters.Resolution = 300f;

                // Set the bar (foreground) color to lime (#00FF00)
                generator.Parameters.Barcode.BarColor = Color.FromArgb(0, 255, 0);

                // Save the generated barcode as a TIFF image
                generator.Save(outputFile, BarCodeImageFormat.Tiff);
            }

            // Inform the user where the file was saved
            Console.WriteLine($"Barcode saved to {Path.GetFullPath(outputFile)}");
        }
    }
}