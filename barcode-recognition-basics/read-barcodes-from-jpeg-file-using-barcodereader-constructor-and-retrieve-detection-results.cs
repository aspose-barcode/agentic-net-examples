using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

namespace BarcodeReadExample
{
    class Program
    {
        static void Main()
        {
            // Create a sample barcode image and save it as JPEG
            const string imagePath = "sample.jpg";
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
            {
                // Save the generated barcode as JPEG
                generator.Save(imagePath);
            }

            // Read barcodes from the JPEG file using BarCodeReader constructor
            using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                // Iterate through all detected barcodes
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine("BarCode Type: " + result.CodeTypeName);
                    Console.WriteLine("BarCode Text: " + result.CodeText);
                    Console.WriteLine("Confidence: " + result.Confidence);
                    // Retrieve detection region rectangle
                    var rect = result.Region.Rectangle;
                    Console.WriteLine($"Region - X:{rect.X}, Y:{rect.Y}, Width:{rect.Width}, Height:{rect.Height}");
                    Console.WriteLine();
                }
            }
        }
    }
}