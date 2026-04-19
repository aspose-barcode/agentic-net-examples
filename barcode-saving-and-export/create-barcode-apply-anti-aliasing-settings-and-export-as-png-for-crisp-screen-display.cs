using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeSample
{
    class Program
    {
        static void Main()
        {
            // Create a barcode generator for Code128 with sample text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Enable anti-aliasing for smoother rendering on screen
                generator.Parameters.UseAntiAlias = true;

                // Set a higher resolution (e.g., 300 DPI) for crisp display
                generator.Parameters.Resolution = 300f;

                // Optionally, define image size in points for consistent dimensions
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 100f;

                // Save the barcode as a PNG file
                generator.Save("barcode.png");
            }
        }
    }
}