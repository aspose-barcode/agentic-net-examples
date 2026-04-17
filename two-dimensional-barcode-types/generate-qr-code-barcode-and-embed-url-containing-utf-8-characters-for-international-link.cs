using System;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // International URL containing UTF‑8 characters
        string url = "https://例子.测试/路径?查询=值";

        // Create QR Code generator
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Encode the URL using UTF‑8
            generator.SetCodeText(url, Encoding.UTF8);

            // Set high error correction level
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the QR Code image as PNG
            generator.Save("qr_utf8.png");
        }
    }
}