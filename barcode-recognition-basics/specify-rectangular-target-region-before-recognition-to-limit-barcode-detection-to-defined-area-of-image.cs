using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Generate a sample barcode image
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save("barcode.png");
        }

        // Load the image and define a target region
        using (var bmp = new Bitmap("barcode.png"))
        {
            // Define a rectangle that covers the central part of the image
            var region = new Rectangle(bmp.Width / 4, bmp.Height / 4, bmp.Width / 2, bmp.Height / 2);

            // Initialize the reader with the image, region and decode type
            using (var reader = new BarCodeReader(bmp, region, DecodeType.AllSupportedTypes))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine("Type: " + result.CodeTypeName);
                    Console.WriteLine("Text: " + result.CodeText);
                    var rect = result.Region.Rectangle;
                    Console.WriteLine($"Detected Region: X={rect.X}, Y={rect.Y}, Width={rect.Width}, Height={rect.Height}");
                }
            }
        }
    }
}