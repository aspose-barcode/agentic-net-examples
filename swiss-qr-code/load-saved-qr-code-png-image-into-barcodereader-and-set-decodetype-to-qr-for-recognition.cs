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
        const string imagePath = "qr.png";

        // Generate a sample QR code if the file does not exist
        if (!File.Exists(imagePath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
            {
                generator.Save(imagePath);
            }
        }

        // Verify the image file exists before attempting to read
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Read the QR code using BarCodeReader with DecodeType set to QR
        using (var reader = new BarCodeReader())
        {
            // Set the decode type to QR
            reader.BarCodeReadType = DecodeType.QR;

            // Load the image file
            reader.SetBarCodeImage(imagePath);

            // Perform recognition
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Decoded Text: {result.CodeText}");
            }
        }
    }
}