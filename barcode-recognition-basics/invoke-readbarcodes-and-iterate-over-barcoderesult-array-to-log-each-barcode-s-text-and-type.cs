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

        // Create a reader for the saved image, specifying the decode type(s) to look for.
        using (var reader = new BarCodeReader("barcode.png", DecodeType.Code128))
        {
            // Read all barcodes in the image and iterate over the results.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine("BarCode Type: " + result.CodeTypeName);
                Console.WriteLine("BarCode Text: " + result.CodeText);
            }
        }
    }
}