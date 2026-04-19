using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        string qrText = "https://example.com";

        using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
        {
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                string filePath = "qr.png";
                bitmap.Save(filePath, ImageFormat.Png);
            }
        }
    }
}