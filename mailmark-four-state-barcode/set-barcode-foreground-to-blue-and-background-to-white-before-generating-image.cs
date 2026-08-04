// Title: Generate Code128 barcode with custom colors
// Description: Demonstrates how to create a Code128 barcode image with a blue foreground and white background using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure visual properties such as bar color and background color before rendering. It showcases the BarcodeGenerator class, its Parameters property, and common use cases like customizing barcode appearance for branding or readability. Developers often need to adjust colors, sizes, and formats when integrating barcodes into documents, labels, or web pages.
// Prompt: Set barcode foreground to blue and background to white before generating the image.
// Tags: code128, color, png, generation, barcodegenerator, parameters

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeSample
{
    /// <summary>
    /// Provides an entry point that generates a Code128 barcode image with custom colors.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Generates a barcode, applies blue foreground and white background, and saves it as a PNG file.
        /// </summary>
        static void Main()
        {
            // Define the output file path for the generated barcode image.
            string outputPath = "barcode.png";

            // Ensure a clean start by deleting any existing file with the same name.
            if (File.Exists(outputPath))
            {
                File.Delete(outputPath);
            }

            // Initialize the barcode generator for the Code128 symbology.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                // Specify the text that the barcode will encode.
                generator.CodeText = "123456";

                // Set the barcode's foreground (bars) color to blue.
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;

                // Set the image background color to white.
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Render and save the barcode image in PNG format.
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            // Output the full path of the saved barcode image for verification.
            Console.WriteLine($"Barcode image saved to {Path.GetFullPath(outputPath)}");
        }
    }
}