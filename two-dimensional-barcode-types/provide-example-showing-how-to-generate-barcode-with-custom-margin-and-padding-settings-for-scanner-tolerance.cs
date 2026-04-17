using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a Code128 barcode generator with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ABC123456"))
        {
            // Set custom image size to provide extra margin around the barcode
            generator.Parameters.ImageWidth.Point = 300f;   // total image width
            generator.Parameters.ImageHeight.Point = 150f;  // total image height

            // Configure barcode paddings (quiet zones) for scanner tolerance
            generator.Parameters.Barcode.Padding.Left.Point = 10f;   // left padding
            generator.Parameters.Barcode.Padding.Top.Point = 5f;     // top padding
            generator.Parameters.Barcode.Padding.Right.Point = 10f; // right padding
            generator.Parameters.Barcode.Padding.Bottom.Point = 5f; // bottom padding

            // Optional: set background color to white and bar color to black (default)
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

            // Save the barcode image
            generator.Save("custom_margin_padding.png");
        }

        Console.WriteLine("Barcode generated successfully.");
    }
}