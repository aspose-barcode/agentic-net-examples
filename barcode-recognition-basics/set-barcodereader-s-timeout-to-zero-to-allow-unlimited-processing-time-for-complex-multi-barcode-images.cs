using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Create a sample barcode image.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            generator.Save("sample.png");
        }

        // Read the barcode with unlimited timeout (0 means no limit).
        using (var reader = new BarCodeReader("sample.png", DecodeType.AllSupportedTypes))
        {
            reader.Timeout = 0; // Unlimited processing time.

            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Barcode Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }
    }
}