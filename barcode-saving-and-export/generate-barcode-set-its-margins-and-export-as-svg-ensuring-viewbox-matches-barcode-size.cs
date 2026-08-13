// Title: Generate Code39 barcode with margins and export to SVG
// Description: Demonstrates creating a Code39 barcode, applying custom margins, and saving it as an SVG file where the viewBox matches the barcode dimensions.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode parameters such as padding, colors, and AutoSizeMode using the BarcodeGenerator class. Typical use cases include generating barcodes for web or print with precise layout control. Developers often need to export barcodes to vector formats like SVG while preserving exact sizing for responsive designs.
// Prompt: Generate a barcode, set its margins, and export as SVG ensuring the viewBox matches the barcode size.
// Tags: code39, barcode, margin, svg, autosizemode, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeSvgExample
{
    /// <summary>
    /// Provides an entry point that generates a Code39 barcode, applies padding,
    /// and saves the result as an SVG file with a viewBox that matches the barcode size.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Generates the barcode, configures its appearance, and writes the SVG output.
        /// </summary>
        static void Main()
        {
            // Define the output SVG file path
            string outputPath = "barcode.svg";

            // Initialize a Code39 barcode generator with the sample text "123ABC"
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "123ABC"))
            {
                // Configure padding (margins) in points for each side
                generator.Parameters.Barcode.Padding.Left.Point = 10f;
                generator.Parameters.Barcode.Padding.Top.Point = 10f;
                generator.Parameters.Barcode.Padding.Right.Point = 10f;
                generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

                // Optional: set foreground (barcode) and background colors
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Ensure the SVG viewBox matches the exact barcode size (no auto‑scaling)
                generator.Parameters.AutoSizeMode = AutoSizeMode.None;

                // Attempt to save the barcode as an SVG file and report the result
                try
                {
                    generator.Save(outputPath, BarCodeImageFormat.Svg);
                    Console.WriteLine($"Barcode saved to {Path.GetFullPath(outputPath)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving SVG: {ex.Message}");
                }
            }
        }
    }
}