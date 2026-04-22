using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789012"))
        {
            // Set high resolution suitable for label printing (e.g., 300 DPI)
            generator.Parameters.Resolution = 300f;

            // Define image size in pixels (width x height)
            generator.Parameters.ImageWidth.Pixels = 600f;   // 2 inches wide at 300 DPI
            generator.Parameters.ImageHeight.Pixels = 300f;  // 1 inch high at 300 DPI

            // Use explicit sizing (no auto‑size interpolation)
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Set X‑dimension (module width) and bar height for 1D barcode
            generator.Parameters.Barcode.XDimension.Pixels = 2f;   // narrow bar width
            generator.Parameters.Barcode.BarHeight.Pixels = 100f; // bar height

            // Optional: adjust padding around the barcode
            generator.Parameters.Barcode.Padding.Left.Point = 5f;
            generator.Parameters.Barcode.Padding.Top.Point = 5f;
            generator.Parameters.Barcode.Padding.Right.Point = 5f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 5f;

            // Set colors (black bars on white background)
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Enable anti‑aliasing for smoother edges at high DPI
            generator.Parameters.UseAntiAlias = true;

            // Save the barcode image
            generator.Save("highres_code128.png");
        }
    }
}