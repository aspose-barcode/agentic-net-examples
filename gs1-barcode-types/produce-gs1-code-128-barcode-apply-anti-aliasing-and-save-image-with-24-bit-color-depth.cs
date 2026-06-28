using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeExample
{
    /// <summary>
    /// Demonstrates generating a GS1 Code128 barcode using Aspose.BarCode.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the application. Generates a barcode image and saves it to disk.
        /// </summary>
        static void Main()
        {
            // Define the GS1 Code128 data (Application Identifier 01 - GTIN)
            string codeText = "(01)12345678901231";

            // Specify the output file name and location
            string outputPath = "gs1code128.png";

            // Initialize the barcode generator with GS1 Code128 symbology and the data
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
            {
                // Turn on anti‑aliasing to improve visual quality of the rendered image
                generator.Parameters.UseAntiAlias = true;

                // Save the generated barcode as a PNG file (default 24‑bit color depth)
                generator.Save(outputPath);
            }

            // Inform the user where the barcode image was saved
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}