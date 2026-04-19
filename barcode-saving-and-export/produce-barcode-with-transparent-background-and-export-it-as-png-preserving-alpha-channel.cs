using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeTransparentBackground
{
    class Program
    {
        static void Main()
        {
            // Output file path
            string outputPath = "transparent_barcode.png";

            // Create a barcode generator (Code128) with sample text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
            {
                // Set the background to transparent
                generator.Parameters.BackColor = Color.Transparent;

                // Set the bar (foreground) color if desired
                generator.Parameters.Barcode.BarColor = Color.Black;

                // Save the barcode as PNG; the transparent background is preserved
                generator.Save(outputPath);
            }

            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}