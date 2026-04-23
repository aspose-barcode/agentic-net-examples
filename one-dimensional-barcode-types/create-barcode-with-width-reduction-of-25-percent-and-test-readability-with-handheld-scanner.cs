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
        const string filePath = "barcode.png";

        // Create a Code128 barcode with a 25% bar width reduction
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // BarWidthReduction is a Unit; set its value in points (25% of a point)
            generator.Parameters.Barcode.BarWidthReduction.Point = 0.25f;

            // Optional: define image size
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Save the barcode image to a file
            generator.Save(filePath);
        }

        // Ensure the image was created
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Failed to create barcode image at '{filePath}'.");
            return;
        }

        // Simulate a handheld scanner by reading the barcode from the saved image
        using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
        {
            bool anyFound = false;
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");
                Console.WriteLine($"Confidence: {result.Confidence}");
                anyFound = true;
            }

            if (!anyFound)
            {
                Console.WriteLine("No barcode detected.");
            }
        }
    }
}