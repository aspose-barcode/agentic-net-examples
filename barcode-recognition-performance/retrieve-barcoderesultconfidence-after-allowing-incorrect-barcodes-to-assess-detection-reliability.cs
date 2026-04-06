using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Generate a barcode with an incorrect checksum (EAN13 requires a 13‑digit checksum)
        using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890123"))
        {
            generator.Save("barcode.png");
        }

        // Read the barcode while allowing incorrect barcodes
        using (var reader = new BarCodeReader("barcode.png", DecodeType.EAN13))
        {
            // Enable detection of barcodes with wrong checksum or damaged data
            reader.QualitySettings.AllowIncorrectBarcodes = true;

            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("Code Type: " + result.CodeTypeName);
                Console.WriteLine("Code Text: " + result.CodeText);
                Console.WriteLine("Confidence: " + result.Confidence);
            }
        }
    }
}