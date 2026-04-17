using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeExample
{
    class Program
    {
        static void Main()
        {
            // Create a barcode generator for Code128 with sample text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
            {
                // Set image size using millimeters
                generator.Parameters.ImageHeight.Millimeters = 30f;
                generator.Parameters.ImageWidth.Millimeters = 50f;

                // Save the barcode as a JPEG image
                generator.Save("barcode.jpg");
            }
        }
    }
}