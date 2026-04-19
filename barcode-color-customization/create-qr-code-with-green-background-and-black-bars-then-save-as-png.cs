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
            // Create a QR code generator
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                // Text to encode
                generator.CodeText = "Hello Aspose";

                // Set background to green and bars to black
                generator.Parameters.BackColor = Color.Green;
                generator.Parameters.Barcode.BarColor = Color.Black;

                // Save the QR code as a PNG file
                generator.Save("qr_green_background.png");
            }
        }
    }
}