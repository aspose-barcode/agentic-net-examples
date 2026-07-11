// Title: Generate OneCode barcode with custom DPI and save as PNG
// Description: Demonstrates creating a OneCode barcode, setting a 300 DPI resolution, and saving it as a high‑resolution PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on OneCode symbology. It showcases the use of BarcodeGenerator, EncodeTypes, and resolution settings to produce high‑quality images. Developers often need to generate barcodes for printing or digital display with specific DPI requirements, and this snippet illustrates the typical workflow.
// Prompt: Generate a OneCode barcode with custom DPI of 300 and save as high‑resolution PNG.
// Tags: onecode, barcode, generation, dpi, png, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace OneCodeExample
{
    /// <summary>
    /// Provides an entry point that generates a OneCode barcode,
    /// configures a custom DPI of 300, and saves the result as a PNG file.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main method that performs the barcode generation and saving process.
        /// </summary>
        static void Main()
        {
            // OneCode requires a numeric codetext of length 20, 25, 29, or 31.
            const string codeText = "12345678901234567890";

            // Initialize the barcode generator with OneCode symbology and the specified codetext.
            using (var generator = new BarcodeGenerator(EncodeTypes.OneCode, codeText))
            {
                // Set the image resolution to 300 DPI for high‑resolution output.
                generator.Parameters.Resolution = 300f;

                // Save the generated barcode as a PNG image file.
                generator.Save("OneCode.png");
            }

            // Inform the user that the barcode has been saved.
            Console.WriteLine("OneCode barcode saved to OneCode.png");
        }
    }
}