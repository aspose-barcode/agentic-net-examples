using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeToPdfExample
{
    class Program
    {
        static void Main()
        {
            // Create a barcode generator for Code128 with sample text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Configure image size (points) – preserve exact dimensions
                generator.Parameters.ImageWidth.Point = 300f;   // Width = 300 points
                generator.Parameters.ImageHeight.Point = 150f; // Height = 150 points

                // Set resolution (dpi) – higher resolution keeps quality when exported to PDF
                generator.Parameters.Resolution = 300f; // 300 DPI

                // Optional: adjust barcode-specific dimensions while keeping size
                generator.Parameters.Barcode.XDimension.Point = 2f;   // Smallest bar width
                generator.Parameters.Barcode.BarHeight.Point = 40f;   // Bar height for 1D barcode

                // Optional: set colors
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Export the barcode directly to PDF while preserving size and resolution
                generator.Save("barcode.pdf");
            }
        }
    }
}