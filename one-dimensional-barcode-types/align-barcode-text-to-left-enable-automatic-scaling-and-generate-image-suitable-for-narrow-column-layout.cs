using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeExample
{
    class Program
    {
        static void Main()
        {
            // Create a barcode generator for Code128 with sample text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Align human‑readable text to the left
                generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Left;

                // Enable automatic scaling (interpolation) suitable for narrow columns
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

                // Define the target image size (e.g., 150 pt width, 50 pt height)
                generator.Parameters.ImageWidth.Point = 150f;
                generator.Parameters.ImageHeight.Point = 50f;

                // Optional: reduce padding to fit tighter layout
                generator.Parameters.Barcode.Padding.Left.Point = 2f;
                generator.Parameters.Barcode.Padding.Right.Point = 2f;
                generator.Parameters.Barcode.Padding.Top.Point = 2f;
                generator.Parameters.Barcode.Padding.Bottom.Point = 2f;

                // Save the generated barcode image
                generator.Save("barcode.png");
            }

            Console.WriteLine("Barcode image generated: barcode.png");
        }
    }
}