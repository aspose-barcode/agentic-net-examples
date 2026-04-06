using System;
using System.Drawing;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Generate a Code39 barcode that contains spaces (white space)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "A B C"))
        {
            // Optional: set image size
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 100f;
            generator.Save("code39.png");
        }

        // Read the barcode while ignoring white space
        using (var reader = new BarCodeReader())
        {
            // Set the decode type to Code39
            reader.BarCodeReadType = DecodeType.Code39;

            // Load the generated image
            reader.SetBarCodeImage("code39.png");

            // Instruct the reader to ignore white space characters in Code39
            // (Aspose.BarCode provides this via the StripFNC property for Code39)
            reader.BarcodeSettings.StripFNC = true;

            // Perform recognition
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Decoded Text: {result.CodeText}");
            }
        }
    }
}