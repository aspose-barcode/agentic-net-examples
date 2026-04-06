using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Generate a sample QR code image
        const string imagePath = "sample_qr.png";
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello 2D"))
        {
            generator.Save(imagePath);
        }

        // Read all 2D barcodes from the generated image using Types2D preset
        using (var reader = new BarCodeReader(imagePath, DecodeType.Types2D))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");
            }
        }
    }
}