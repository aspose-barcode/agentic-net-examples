using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeTransparentBackground
{
    class Program
    {
        static void Main()
        {
            // Define output file path
            const string outputPath = "transparent_barcode.png";

            // Create a barcode generator for Code128 with sample text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Set the background color to transparent
                generator.Parameters.BackColor = Color.Transparent;

                // Optionally set the bar (foreground) color
                generator.Parameters.Barcode.BarColor = Color.Black;

                // Save the barcode as PNG preserving the alpha channel
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}