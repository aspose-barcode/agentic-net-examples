using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

namespace CodablockFExample
{
    class Program
    {
        static void Main()
        {
            // Multiline data to encode – each line separated by a newline character.
            string multilineText = "First line\nSecond line\nThird line";

            // Create a BarcodeGenerator for Codablock‑F symbology and assign the multiline text.
            using (var generator = new BarcodeGenerator(EncodeTypes.CodablockF, multilineText))
            {
                // Optional: set image resolution (dpi) if desired.
                generator.Parameters.Resolution = 300;

                // Save the generated barcode as a BMP file.
                generator.Save("codablockf.bmp");
            }

            // Indicate completion.
            Console.WriteLine("Codablock‑F barcode saved as 'codablockf.bmp'.");
        }
    }
}