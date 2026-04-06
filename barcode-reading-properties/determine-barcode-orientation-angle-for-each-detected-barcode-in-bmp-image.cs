using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        const string filePath = "barcode.bmp";

        // Create a barcode image and rotate it to demonstrate angle detection
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            // Rotate the barcode image by 45 degrees
            generator.Parameters.RotationAngle = 45f;
            // Save the image as BMP
            generator.Save(filePath, BarCodeImageFormat.Bmp);
        }

        // Read all supported barcodes from the BMP image
        using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
        {
            // Iterate through each detected barcode and output its angle
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"Angle: {result.Region.Angle}");
            }
        }
    }
}