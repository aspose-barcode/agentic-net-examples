// Title: Generate multiple barcode images with different color schemes using a single generator
// Description: Demonstrates how to produce several barcode PNG files, each with its own foreground and background colors, while reusing one BarcodeGenerator instance.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode appearance such as bar color and background using the BarcodeGenerator class. Typical use cases include creating branded or visually distinct barcodes for marketing, labeling, or UI display. Developers often need to adjust colors, fonts, and image formats when generating barcodes programmatically.
// Prompt: Produce multiple barcode images with varying color schemes using a single BarcodeGenerator instance.
// Tags: code128, color, png, generation, aspose.barcode, aspose.drawing, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeColorDemo
{
    /// <summary>
    /// Demonstrates generating multiple barcode images with different color schemes using a single <see cref="BarcodeGenerator"/> instance.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the demo. Creates an output folder, defines color schemes, and saves PNG barcodes with varying colors.
        /// </summary>
        static void Main()
        {
            // Create output directory for generated barcode images
            string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
            Directory.CreateDirectory(outputDir);

            // Define a set of foreground/background color pairs to apply to each barcode
            var colorSchemes = new (Color fore, Color back)[]
            {
                (Color.Blue, Color.White),
                (Color.Green, Color.LightGray),
                (Color.Red, Color.Yellow)
            };

            // Initialize a single BarcodeGenerator for Code128 symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Optional: set a common font size for the code text
                generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 14f;

                int index = 1;
                foreach (var scheme in colorSchemes)
                {
                    // Apply the current color scheme to the barcode
                    generator.Parameters.Barcode.BarColor = scheme.fore;
                    generator.Parameters.BackColor = scheme.back;

                    // Optionally change the code text for each image
                    generator.CodeText = $"Sample{index}";

                    // Build the file path and save the barcode as a PNG image
                    string filePath = Path.Combine(outputDir, $"barcode_{index}.png");
                    generator.Save(filePath, BarCodeImageFormat.Png);

                    Console.WriteLine($"Saved barcode {index} to {filePath}");
                    index++;
                }
            }

            Console.WriteLine("Barcode generation completed.");
        }
    }
}