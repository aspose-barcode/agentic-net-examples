using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeExample
{
    class Program
    {
        static void Main()
        {
            // Create a barcode generator for Code128
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                // Set the text to encode
                generator.CodeText = "123ABC";

                // Rotate the barcode image by 90 degrees
                generator.Parameters.RotationAngle = 90f;

                // Save the barcode as a BMP file preserving the rotation
                generator.Save("barcode.bmp");
            }
        }
    }
}