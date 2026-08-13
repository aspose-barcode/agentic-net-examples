// Title: Apply custom text color to a barcode (Code128)
// Description: Demonstrates how to set a custom color for the human‑readable text of a barcode while keeping bar and background colors at their defaults.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to customize barcode appearance. Developers often need to modify text color for branding or visual integration while preserving standard bar colors. The snippet shows typical steps for creating, customizing, and saving a barcode image.
// Prompt: Apply a custom text color to a barcode while leaving bar and background colors at defaults.
// Tags: barcode, code128, text color, png, aspose.barcode, generation, colortext

using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeExample
{
    /// <summary>
    /// Generates a Code128 barcode with a custom text color while leaving bar and background colors at their defaults.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the example. Creates the barcode, applies the text color, saves the image, and writes the output path to the console.
        /// </summary>
        static void Main()
        {
            // Define the output file name and location
            string outputPath = "custom_text_color_barcode.png";

            // Initialize the barcode generator for Code128 with the desired data
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Set a custom color (blue) for the human‑readable text only
                generator.Parameters.Barcode.CodeTextParameters.Color = Color.Blue;

                // Save the barcode image; bar and background colors remain unchanged (defaults)
                generator.Save(outputPath);
            }

            // Inform the user where the barcode image was saved
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}