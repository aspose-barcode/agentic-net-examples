using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set a light background to improve contrast
            generator.Parameters.BackColor = Color.White;

            // Set dark bar color for strong detection
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Increase resolution for clearer image
            generator.Parameters.Resolution = 300f;

            // Optional: increase padding to avoid edge clipping
            generator.Parameters.Barcode.Padding.Top.Point = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f;
            generator.Parameters.Barcode.Padding.Left.Point = 10f;
            generator.Parameters.Barcode.Padding.Right.Point = 10f;

            // Save the barcode image
            generator.Save("custom_color_barcode.png");
        }

        Console.WriteLine("Barcode generated with custom color scheme.");
    }
}