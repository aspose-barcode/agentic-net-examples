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
        string imagePath = "barcode.png";

        // Create a barcode image
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(imagePath);
        }

        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Barcode image was not created.");
            return;
        }

        BarCodeResult result = null;
        double readingQuality = 0;
        int maxAttempts = 3;

        for (int attempt = 1; attempt <= maxAttempts; attempt++)
        {
            using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
            {
                // Increase quality settings on subsequent attempts
                if (attempt == 2)
                    reader.QualitySettings = QualitySettings.HighQuality;
                else if (attempt == 3)
                    reader.QualitySettings = QualitySettings.MaxQuality;

                foreach (var res in reader.ReadBarCodes())
                {
                    result = res;
                    readingQuality = res.ReadingQuality;
                    break; // take the first detected barcode
                }
            }

            if (result != null && readingQuality > 0)
            {
                Console.WriteLine($"Successfully read on attempt {attempt}.");
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"ReadingQuality: {readingQuality}");
                break;
            }
            else
            {
                Console.WriteLine($"Attempt {attempt} failed (ReadingQuality={readingQuality}).");
                if (attempt == maxAttempts)
                {
                    Console.WriteLine("All attempts exhausted without successful read.");
                }
            }
        }
    }
}