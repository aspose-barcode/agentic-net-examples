using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path to the image containing the QR code
        string imagePath = "sample.png";

        // Verify that the file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Initialize the reader with QR decode type to limit recognition to QR only
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            // Perform recognition
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Barcode Text: {result.CodeText}");
            }
        }
    }
}