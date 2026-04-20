using System;
using Aspose.BarCode.Generation;

namespace BarcodeDemo
{
    class Program
    {
        static void Main()
        {
            // Create a barcode generator for Code128 with sample text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Set the image resolution to 600 DPI for high‑quality printing
                generator.Parameters.Resolution = 600f;

                // Save the barcode image as PNG
                generator.Save("barcode.png");
            }
        }
    }
}