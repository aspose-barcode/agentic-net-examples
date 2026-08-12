// Title: Export MaxiCode barcode to lossless TIFF image
// Description: Demonstrates generating a MaxiCode barcode and saving it as a TIFF file with lossless compression, suitable for high‑resolution printing.
// Category-Description: Shows how to use Aspose.BarCode to create a barcode, configure resolution and colors, and export the image using Aspose.Drawing. This belongs to the barcode generation and image export category, where developers commonly need to adjust DPI, module size, and output format for print‑ready assets.
// Prompt: Export MaxiCode barcode as TIFF image with lossless compression for high‑resolution printing.
// Tags: maxicode, barcode, export, tiff, lossless, resolution, aspose.barcode, aspose.drawing, image-generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace MaxiCodeExport
{
    /// <summary>
    /// Generates a MaxiCode barcode and saves it as a TIFF image with lossless compression.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the example. Creates the barcode, configures rendering options, and writes the image to disk.
        /// </summary>
        static void Main()
        {
            // Define the output file path (TIFF with lossless compression) in the current directory.
            string outputPath = Path.Combine(Environment.CurrentDirectory, "maxicode.tiff");

            // Create a MaxiCode barcode generator with sample codetext.
            using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Sample MaxiCode"))
            {
                // Set a high resolution (e.g., 300 DPI) for print‑quality output.
                generator.Parameters.Resolution = 300f;

                // Adjust the module size (X dimension) for better visual clarity.
                generator.Parameters.Barcode.XDimension.Point = 2f;

                // Set foreground (barcode) and background colors; defaults are black on white.
                generator.Parameters.Barcode.BarColor = Color.Black;
                generator.Parameters.BackColor = Color.White;

                // Save the generated barcode as a TIFF image using lossless compression.
                generator.Save(outputPath, BarCodeImageFormat.Tiff);
            }

            // Inform the user where the file was saved.
            Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
        }
    }
}