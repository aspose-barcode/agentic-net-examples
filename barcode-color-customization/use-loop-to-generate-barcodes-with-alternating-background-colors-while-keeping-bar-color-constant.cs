using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeLoopExample
{
    class Program
    {
        static void Main()
        {
            // Constant bar (foreground) color for all barcodes
            var barColor = Color.Black;

            // Two background colors to alternate between
            var bgColor1 = Color.White;
            var bgColor2 = Color.LightGray;

            // Generate 6 barcodes with alternating backgrounds
            for (int i = 0; i < 6; i++)
            {
                // Select background color based on the current index
                var bgColor = (i % 2 == 0) ? bgColor1 : bgColor2;

                // Create a BarcodeGenerator for Code128 with a unique code text
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"CODE{i + 1}"))
                {
                    // Keep bar color constant
                    generator.Parameters.Barcode.BarColor = barColor;

                    // Apply the alternating background color
                    generator.Parameters.BackColor = bgColor;

                    // Optional: set image dimensions
                    generator.Parameters.ImageWidth.Point = 300f;
                    generator.Parameters.ImageHeight.Point = 150f;

                    // Save the barcode image to a file
                    var fileName = $"barcode_{i + 1}.png";
                    generator.Save(fileName);
                }
            }
        }
    }
}