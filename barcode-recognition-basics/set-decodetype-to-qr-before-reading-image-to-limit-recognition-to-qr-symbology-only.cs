using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main(string[] args)
    {
        // Path for the generated QR code image
        string imagePath = "qr.png";

        // Create a QR code and save it to a file
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            generator.Save(imagePath);
        }

        // Read the image, limiting recognition to QR symbology only
        using (var reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Type: {result.CodeTypeName}");
                Console.WriteLine($"Text: {result.CodeText}");
                Console.WriteLine($"Confidence: {result.Confidence}");
            }
        }
    }
}