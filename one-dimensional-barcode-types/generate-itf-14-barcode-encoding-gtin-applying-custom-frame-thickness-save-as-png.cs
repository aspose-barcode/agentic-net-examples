// Title: Generate ITF-14 barcode with custom frame thickness and save as PNG
// Description: Demonstrates encoding a 14‑digit GTIN into an ITF‑14 barcode, applying a custom frame border thickness, and saving the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.ITF14. It shows configuring ITF‑14 specific parameters such as border type and thickness, a common requirement for packaging and logistics applications where GTIN‑14 codes must be printed with a visible frame. Developers looking for barcode creation, format customization, and image export can reference this pattern across similar symbologies.
// Prompt: Generate ITF‑14 barcode encoding GTIN, applying custom frame thickness, save as PNG.
// Tags: itf-14, barcode, generation, frame thickness, png, aspose.barcode, encode-types, gtin

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeExample
{
    /// <summary>
    /// Entry point for the ITF‑14 barcode generation example.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Generates an ITF‑14 barcode for a given GTIN, applies a custom frame border, and saves it as a PNG file.
        /// </summary>
        static void Main()
        {
            // GTIN for ITF‑14 must be exactly 14 digits
            string gtin = "12345678901231";

            // Desired frame thickness in points
            float frameThickness = 5f;

            // Output file path
            string outputPath = "itf14.png";

            // Create the barcode generator for ITF‑14 using the specified GTIN
            using (var generator = new BarcodeGenerator(EncodeTypes.ITF14, gtin))
            {
                // Apply custom frame thickness to the ITF‑14 border
                generator.Parameters.Barcode.ITF.BorderThickness.Point = frameThickness;

                // Set the border type to a full frame around the barcode
                generator.Parameters.Barcode.ITF.BorderType = ITF14BorderType.Frame;

                // Save the generated barcode image as a PNG file
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            // Inform the user where the barcode image was saved
            Console.WriteLine($"ITF‑14 barcode saved to {outputPath}");
        }
    }
}