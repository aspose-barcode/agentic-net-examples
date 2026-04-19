using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Prepare output directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Paths for two barcode images
        string barcodePath1 = Path.Combine(outputDir, "barcode1.png");
        string barcodePath2 = Path.Combine(outputDir, "barcode2.png");

        // Generate first barcode (Code128) and save
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "First123"))
        {
            generator.Save(barcodePath1);
        }

        // Generate second barcode (QR) and save
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Second456"))
        {
            generator.Save(barcodePath2);
        }

        // Use BarCodeReader with default constructor
        using (var reader = new BarCodeReader())
        {
            // Set first image as source
            if (File.Exists(barcodePath1))
            {
                reader.SetBarCodeImage(barcodePath1);
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"First Image - Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }
            else
            {
                Console.WriteLine("First barcode image not found.");
            }

            // Change source image to the second one
            if (File.Exists(barcodePath2))
            {
                reader.SetBarCodeImage(barcodePath2);
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Second Image - Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }
            else
            {
                Console.WriteLine("Second barcode image not found.");
            }
        }
    }
}