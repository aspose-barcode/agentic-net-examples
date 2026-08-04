// Title: Generate Code128 barcode with auto‑sized height
// Description: Demonstrates creating a Code128 barcode where BarCodeHeight is set to zero, allowing the library to auto‑size the height based on the encoded content.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of AutoSizeMode to let the engine determine optimal dimensions. It showcases key classes such as BarcodeGenerator, EncodeTypes, and AutoSizeMode, which developers commonly use when they need dynamic barcode sizing for various output formats like PNG, JPEG, or PDF.
// Prompt: Generate barcode with BarCodeHeight zero to enable auto‑size based on content, using default units.
// Tags: code128, barcode generation, autosize, png, aspose.barcode, csharp

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeExample
{
    /// <summary>
    /// Provides an entry point that generates a Code128 barcode image with automatic height sizing.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Generates a barcode, saves it as a PNG file, and writes the output path to the console.
        /// </summary>
        static void Main()
        {
            // Initialize a BarcodeGenerator for Code128 with the sample text "Sample123".
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Enable auto‑size mode so the library determines the optimal barcode height.
                // Setting BarCodeHeight to zero is implicit; no explicit height assignment is needed.
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

                // Save the generated barcode image to a PNG file named "barcode.png".
                generator.Save("barcode.png");
            }

            // Inform the user that the barcode image has been created.
            Console.WriteLine("Barcode generated: barcode.png");
        }
    }
}