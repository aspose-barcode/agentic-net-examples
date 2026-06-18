using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeExample
{
    /// <summary>
    /// Demonstrates generating a DataMatrix barcode using Aspose.BarCode.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the application. Generates a DataMatrix barcode,
        /// customizes the human‑readable text color, saves the image, and
        /// writes a confirmation to the console.
        /// </summary>
        static void Main()
        {
            // Initialize a BarcodeGenerator for DataMatrix with the provided text.
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Sample123"))
            {
                // Set the color of the human‑readable text to orange.
                generator.Parameters.Barcode.CodeTextParameters.Color = Color.Orange;

                // Save the generated barcode as a PNG file (default background retained).
                generator.Save("datamatrix.png");

                // Output a confirmation message to the console.
                Console.WriteLine("Barcode saved to datamatrix.png");
            } // The using block disposes the generator automatically.
        }
    }
}