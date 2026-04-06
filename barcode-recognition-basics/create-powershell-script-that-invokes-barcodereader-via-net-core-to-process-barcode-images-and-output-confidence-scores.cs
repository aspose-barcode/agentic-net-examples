using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path for the temporary barcode image
        string imagePath = "sample.png";

        // Generate a sample Code128 barcode and save it to a file
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            generator.Save(imagePath);
        }

        // Read the barcode image and output confidence information
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                Console.WriteLine($"BarCode CodeText: {result.CodeText}");
                Console.WriteLine($"BarCode Confidence: {result.Confidence}");
                Console.WriteLine($"BarCode ReadingQuality: {result.ReadingQuality}");
                var bounds = result.Region.Rectangle;
                Console.WriteLine($"BarCode Region: X={bounds.X}, Y={bounds.Y}, Width={bounds.Width}, Height={bounds.Height}");
                Console.WriteLine();
            }
        }

        // Cleanup (optional)
        // System.IO.File.Delete(imagePath);
    }
}