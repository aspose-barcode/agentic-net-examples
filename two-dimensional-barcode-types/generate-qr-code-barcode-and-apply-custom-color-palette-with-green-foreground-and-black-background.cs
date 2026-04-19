using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a QR Code generator with sample text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose!"))
        {
            // Set foreground (bars) to green
            generator.Parameters.Barcode.BarColor = Color.Green;

            // Set background to black
            generator.Parameters.BackColor = Color.Black;

            // Optional: set error correction level to high
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the barcode image as PNG
            generator.Save("qr_green_on_black.png");
        }

        Console.WriteLine("QR Code generated and saved as 'qr_green_on_black.png'.");
    }
}