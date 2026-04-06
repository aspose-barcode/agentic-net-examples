using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Generate a simple Code128 barcode and save it to a file.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            generator.Save("barcode.png");
        }

        // Read the barcode from the saved image.
        using (var reader = new BarCodeReader("barcode.png", DecodeType.Code128))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Type: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"Confidence: {result.Confidence}");
                Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");

                // Apply custom threshold: flag barcodes with ReadingQuality below 50.
                if (result.ReadingQuality < 50.0)
                {
                    Console.WriteLine("=> Barcode flagged for manual review (ReadingQuality below 50).");
                }

                Console.WriteLine();
            }
        }
    }
}