using System;
using Aspose.BarCode.Generation;

namespace BarcodeExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a QR Code generator with the desired text
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
            {
                // Set all padding (margin) values to zero
                generator.Parameters.Barcode.Padding.Left.Point = 0f;
                generator.Parameters.Barcode.Padding.Top.Point = 0f;
                generator.Parameters.Barcode.Padding.Right.Point = 0f;
                generator.Parameters.Barcode.Padding.Bottom.Point = 0f;

                // Save the barcode as a JPEG image
                generator.Save("qr.jpg");
            }
        }
    }
}