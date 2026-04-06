using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Create a QR code with a specific version and error correction level
        string imagePath = "qr.png";
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            // Set QR version (e.g., Version5) and error correction level (e.g., LevelQ)
            generator.Parameters.Barcode.QR.Version = QRVersion.Version05;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelQ;
            generator.Save(imagePath);
        }

        // Read the QR code and output its version and error correction level
        using (var reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("Barcode Type: " + result.CodeTypeName);
                Console.WriteLine("Code Text: " + result.CodeText);

                // Access extended QR parameters
                var qrParams = result.Extended.QR;
                Console.WriteLine("QR Version: " + qrParams.Version);
                Console.WriteLine("QR Error Level: " + qrParams.ErrorLevel);
            }
        }
    }
}