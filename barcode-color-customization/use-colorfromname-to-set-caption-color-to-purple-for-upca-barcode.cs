// Title: Set caption color to Purple for a UPC-A barcode using Aspose.BarCode
// Description: Demonstrates how to generate a UPC-A barcode and customize its caption color using Color.FromName.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and caption parameters. Developers often need to modify barcode appearance such as caption visibility, text, and color for branding or UI integration. The snippet shows typical steps: create generator, set caption properties, and save the image.
// Prompt: Use Color.FromName to set caption color to "Purple" for a UPC-A barcode.
// Tags: upc-a, barcode, caption, color, aspnet, aspose.barcode, generation, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeCaptionColorExample
{
    /// <summary>
    /// Demonstrates generating a UPC-A barcode with a purple caption using Aspose.BarCode.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point. Generates the barcode, sets caption properties, and saves as PNG.
        /// </summary>
        static void Main()
        {
            // Determine output file path in the current directory
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "upc_a_caption.png");

            // Create a barcode generator for UPC-A symbology with the specified data
            using (var generator = new BarcodeGenerator(EncodeTypes.UPCA, "123456789012"))
            {
                // Enable the caption above the barcode
                generator.Parameters.CaptionAbove.Visible = true;
                // Set the caption text
                generator.Parameters.CaptionAbove.Text = "Sample Caption";
                // Set the caption text color to purple using Color.FromName
                generator.Parameters.CaptionAbove.TextColor = Color.FromName("Purple");

                // Save the generated barcode as a PNG image
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            // Inform the user where the barcode image was saved
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}