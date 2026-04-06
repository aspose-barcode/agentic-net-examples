using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path for the generated barcode image
        const string filePath = "barcode.png";

        // Generate a simple Code128 barcode and save it
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            generator.Save(filePath);
        }

        // Read the barcode from the saved image
        using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                double readingQuality = result.ReadingQuality;
                Console.WriteLine($"ReadingQuality: {readingQuality}");

                // Map values 1‑99 to moderate quality and log a warning
                if (readingQuality >= 1 && readingQuality <= 99)
                {
                    Console.WriteLine("Warning: Moderate quality detected (ReadingQuality 1‑99).");
                }
                else
                {
                    Console.WriteLine("Info: High quality detected.");
                }
            }
        }
    }
}