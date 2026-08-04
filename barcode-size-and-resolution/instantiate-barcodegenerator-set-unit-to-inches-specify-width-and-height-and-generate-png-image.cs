// Title: Generate Code128 Barcode Image with Specified Size in Inches
// Description: Demonstrates creating a Code128 barcode, setting its dimensions in inches, and saving it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator, set image size units, and export to common image formats. Developers often need to create barcodes with precise physical dimensions for printing on labels, packaging, or documents. The key API classes include BarcodeGenerator, EncodeTypes, and the Parameters.ImageWidth/Height properties.
// Prompt: Instantiate BarcodeGenerator, set unit to Inches, specify width and height, and generate a PNG image.
// Tags: code128, barcode generation, png output, inches, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeExample
{
    /// <summary>
    /// Provides an entry point that generates a Code128 barcode image with dimensions defined in inches.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Creates a BarcodeGenerator, configures size in inches, and saves the barcode as a PNG file.
        /// </summary>
        static void Main()
        {
            // Initialize the barcode generator for Code128 symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                // Set the data to be encoded in the barcode
                generator.CodeText = "1234567890";

                // Define the image width and height using inches as the unit
                generator.Parameters.ImageWidth.Inches = 3f;   // 3 inches wide
                generator.Parameters.ImageHeight.Inches = 1f;  // 1 inch tall

                // Save the generated barcode to a PNG file
                generator.Save("barcode.png");
            }

            // Inform the user that the image has been created
            Console.WriteLine("Barcode image generated: barcode.png");
        }
    }
}