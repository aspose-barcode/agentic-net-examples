using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

namespace BarcodeReaderExample
{
    class Program
    {
        static void Main()
        {
            // Generate a sample QR code image.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR"))
            {
                generator.Save("sample_qr.png");
            }

            // Read only 2D barcodes from the generated image.
            using (var reader = new BarCodeReader())
            {
                // Configure the reader to detect only 2D symbologies.
                reader.BarCodeReadType = DecodeType.Types2D;

                // Assign the image to the reader.
                reader.SetBarCodeImage("sample_qr.png");

                // Perform recognition and output results.
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                    Console.WriteLine($"Decoded Text: {result.CodeText}");
                }
            }
        }
    }
}