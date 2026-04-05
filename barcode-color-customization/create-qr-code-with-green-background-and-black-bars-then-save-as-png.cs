using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeExample
{
    class Program
    {
        static void Main()
        {
            // Create a QR code generator with the desired symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                // Set the data to be encoded
                generator.CodeText = "Hello, Aspose!";

                // Set barcode (bars) color to black
                generator.Parameters.Barcode.BarColor = Color.Black;

                // Set background color to green
                generator.Parameters.BackColor = Color.Green;

                // Save the generated QR code as a PNG file
                generator.Save("qr_green_bg_black_bars.png");
            }
        }
    }
}