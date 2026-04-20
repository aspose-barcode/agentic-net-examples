using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        const string filePath = "barcode.png";

        // Create a Code128 barcode with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Apply a 30‑point bar width reduction (approx. 30 %)
            generator.Parameters.Barcode.BarWidthReduction.Point = 30f;

            // Save the generated barcode image
            generator.Save(filePath);
        }

        // Verify that the barcode can be read by a scanner
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Barcode image was not created.");
            return;
        }

        using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
        {
            bool anyFound = false;
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected CodeText: {result.CodeText}");
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