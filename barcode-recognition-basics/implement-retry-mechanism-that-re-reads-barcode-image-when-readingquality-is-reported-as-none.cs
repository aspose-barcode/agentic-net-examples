using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        const string filePath = "barcode.png";

        // Generate a simple barcode image
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            generator.Save(filePath);
        }

        BarCodeResult finalResult = null;
        const int maxAttempts = 2;

        for (int attempt = 1; attempt <= maxAttempts; attempt++)
        {
            // Create a reader; default quality is NormalQuality
            using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
            {
                // On retries, increase the quality preset before reading
                if (attempt > 1)
                {
                    reader.QualitySettings = QualitySettings.HighQuality;
                }

                BarCodeResult[] results = reader.ReadBarCodes();

                if (results.Length == 0)
                {
                    Console.WriteLine($"Attempt {attempt}: No barcode detected.");
                    continue;
                }

                // Assume first result is the target
                BarCodeResult result = results[0];
                Console.WriteLine($"Attempt {attempt}: ReadingQuality = {result.ReadingQuality}");

                // ReadingQuality of 0 means None
                if (result.ReadingQuality > 0)
                {
                    finalResult = result;
                    break;
                }
            }
        }

        if (finalResult != null && finalResult.ReadingQuality > 0)
        {
            Console.WriteLine($"Successfully read barcode: {finalResult.CodeText}");
        }
        else
        {
            Console.WriteLine("Failed to read barcode with sufficient quality after retries.");
        }
    }
}