// Title: Automatic Font Size for Barcode Text using FontMode.Auto
// Description: Demonstrates how to enable FontMode.Auto so Aspose.BarCode automatically determines the optimal font size for each barcode symbol.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and FontMode to control text rendering. Developers often need to adjust barcode text appearance for readability and layout constraints; FontMode.Auto provides a convenient way to let the library calculate the best font size per symbol, useful in dynamic image generation and reporting scenarios.
// Prompt: Apply FontMode.Auto to barcode text so the library automatically calculates optimal font size for each symbol.
// Tags: barcode, fontmode, auto, code128, generation, image, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeFontModeExample
{
    /// <summary>
    /// Demonstrates applying FontMode.Auto to barcode text for automatic font sizing.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point that creates a Code128 barcode with automatic font size and saves it as PNG.
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
                // Set the text that will be encoded into the barcode.
                generator.CodeText = "123ABC";

                // Enable automatic font size calculation for the barcode text.
                generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Auto;

                // Save the generated barcode image to the specified path.
                generator.Save(outputPath);
            }

            // Inform the user where the barcode image has been saved.
            Console.WriteLine($"Barcode image saved to: {Path.GetFullPath(outputPath)}");
        }
    }
}