using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeExample
{
    /// <summary>
    /// Demonstrates generating a Code128 barcode and saving it as an image file.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the application. Generates a barcode and writes it to disk.
        /// </summary>
        static void Main()
        {
            // Define the output file path for the generated barcode image
            string outputPath = "barcode.png";

            // Initialize a BarcodeGenerator with Code128 symbology and the desired text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Set a soft pastel background color for the barcode image
                generator.Parameters.BackColor = Color.MistyRose;

                // Save the generated barcode image to the specified file path
                generator.Save(outputPath);
            }

            // Inform the user that the barcode has been saved
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}