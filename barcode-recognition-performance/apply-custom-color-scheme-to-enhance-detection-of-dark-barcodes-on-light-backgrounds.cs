using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace BarcodeColorSchemeDemo
{
    class Program
    {
        static void Main()
        {
            // Output file for the generated barcode
            string outputPath = "custom_color_barcode.png";

            // Create a barcode generator for Code128 with sample text
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Set a dark foreground color to improve detection on light backgrounds
                generator.Parameters.Barcode.BarColor = Color.DarkBlue;

                // Set a light background color
                generator.Parameters.BackColor = Color.LightYellow;

                // Increase resolution for sharper output (optional)
                generator.Parameters.Resolution = 300;

                // Disable anti-aliasing to keep high contrast edges (optional)
                generator.Parameters.UseAntiAlias = false;

                // Save the barcode image as PNG
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}