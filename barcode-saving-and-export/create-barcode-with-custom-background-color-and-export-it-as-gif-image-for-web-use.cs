using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace BarcodeSample
{
    class Program
    {
        static void Main()
        {
            // Define output file path
            string outputPath = "barcode.gif";

            // Create a barcode generator for Code128
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                // Set the text to encode
                generator.CodeText = "1234567890";

                // Set a custom background color (e.g., light yellow)
                generator.Parameters.BackColor = Color.LightYellow;

                // Optionally, set the bar (foreground) color
                generator.Parameters.Barcode.BarColor = Color.DarkBlue;

                // Save the barcode as a GIF image suitable for web use
                generator.Save(outputPath, BarCodeImageFormat.Gif);
            }

            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}