using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Generate a sample barcode image to be read later.
        const string imagePath = "sample.png";
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            generator.Save(imagePath);
        }

        // Initialize the reader to detect all supported barcode types.
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Read all barcodes from the image.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine("BarCode Type: " + result.CodeTypeName);
                Console.WriteLine("BarCode CodeText: " + result.CodeText);
            }
        }
    }
}