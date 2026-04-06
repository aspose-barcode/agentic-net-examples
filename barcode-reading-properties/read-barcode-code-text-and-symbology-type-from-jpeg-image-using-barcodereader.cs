using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Create a sample barcode image (Code128) and save it as JPEG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            generator.Save("sample.jpg");
        }

        // Read the barcode from the JPEG image
        using (var reader = new BarCodeReader("sample.jpg", DecodeType.AllSupportedTypes))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine("Detected Barcode Type: " + result.CodeTypeName);
                Console.WriteLine("Detected Barcode Text: " + result.CodeText);
            }
        }
    }
}