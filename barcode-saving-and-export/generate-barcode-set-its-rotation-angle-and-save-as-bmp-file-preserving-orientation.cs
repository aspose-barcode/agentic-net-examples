using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeRotationExample
{
    class Program
    {
        static void Main()
        {
            // Create a barcode generator for Code128 with sample text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
            {
                // Set the rotation angle (in degrees)
                generator.Parameters.RotationAngle = 90f;

                // Save the barcode image as BMP, preserving the rotation
                generator.Save("barcode.bmp");
            }
        }
    }
}