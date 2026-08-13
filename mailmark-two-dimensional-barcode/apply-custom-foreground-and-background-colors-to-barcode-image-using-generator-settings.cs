// Title: Custom Foreground and Background Colors for a Code128 Barcode
// Description: Demonstrates how to set custom bar (foreground) and background colors when generating a Code128 barcode image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize visual appearance of barcodes via the BarcodeGenerator and its Parameters API. Developers often need to match corporate branding or improve scan reliability by adjusting bar and background colors. Typical use cases include creating PNG, JPEG, or PDF barcode assets with specific color schemes.
// Prompt: Apply custom foreground and background colors to the barcode image using generator settings.
// Tags: code128, color, png, barcodegenerator, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeColorExample
{
    /// <summary>
    /// Generates a Code128 barcode image with custom foreground (bar) and background colors.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the example. Creates a barcode, applies color settings, and saves it as a PNG file.
        /// </summary>
        static void Main()
        {
            // Output file path for the generated barcode image
            const string outputFile = "barcode.png";

            // Initialize a BarcodeGenerator for the Code128 symbology with sample data
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Set the color of the barcode bars (foreground)
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;

                // Set the background color of the image
                generator.Parameters.BackColor = Aspose.Drawing.Color.Yellow;

                // Render and save the barcode as a PNG image
                generator.Save(outputFile, BarCodeImageFormat.Png);
            }

            // Output the location of the saved barcode image
            Console.WriteLine($"Barcode image saved to: {outputFile}");
        }
    }
}