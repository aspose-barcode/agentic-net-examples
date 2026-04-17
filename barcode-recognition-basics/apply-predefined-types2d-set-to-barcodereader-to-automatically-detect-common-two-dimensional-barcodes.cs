using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Generate a sample QR code image in memory
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Text"))
        {
            // Optional: set QR error correction level
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            using (Bitmap qrImage = generator.GenerateBarCodeImage())
            {
                // Use BarCodeReader with the predefined Types2D set to detect any 2D barcode
                using (var reader = new BarCodeReader(qrImage, DecodeType.Types2D))
                {
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine("Detected Type: " + result.CodeTypeName);
                        Console.WriteLine("Decoded Text : " + result.CodeText);
                    }
                }
            }
        }
    }
}