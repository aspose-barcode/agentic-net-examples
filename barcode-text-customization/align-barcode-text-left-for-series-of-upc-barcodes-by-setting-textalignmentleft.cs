// Title: Align UPC‑A barcode text to the left
// Description: Demonstrates how to set the human‑readable text alignment to the left for a series of UPC‑A barcodes and save them as PNG images.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and TextAlignment to customize barcode appearance. Developers often need to adjust text positioning for branding or layout requirements when generating barcodes programmatically.
// Prompt: Align barcode text left for a series of UPC‑A barcodes by setting TextAlignment.Left.
// Tags: upc-a, text alignment, left, png, barcode generation, aspose.barcode, c#

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeExample
{
    /// <summary>
    /// Demonstrates aligning the human‑readable text of UPC‑A barcodes to the left and saving them as PNG files.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point. Generates several UPC‑A barcodes with left‑aligned text and writes each to a PNG file.
        /// </summary>
        static void Main()
        {
            // Define a collection of sample UPC‑A codes (each code must contain 12 digits)
            string[] upcCodes = new string[]
            {
                "012345678905",
                "123456789012",
                "234567890123",
                "345678901234",
                "456789012345"
            };

            // Iterate over each code, generate a barcode, and save it as a PNG image
            for (int i = 0; i < upcCodes.Length; i++)
            {
                string code = upcCodes[i];
                string fileName = $"upc_a_{i + 1}.png";

                // Create a BarcodeGenerator for the UPC‑A symbology with the current code
                using (var generator = new BarcodeGenerator(EncodeTypes.UPCA, code))
                {
                    // Set the human‑readable text alignment to the left side of the barcode
                    generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Left;

                    // Save the generated barcode image to a PNG file
                    generator.Save(fileName);
                }

                // Output the name of the saved file to the console for verification
                Console.WriteLine($"Saved {fileName}");
            }
        }
    }
}