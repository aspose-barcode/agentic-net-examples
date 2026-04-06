using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a barcode generator and set the encoded value
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "1234567890";

            // Generate the barcode image as a bitmap
            using (Bitmap bmp = generator.GenerateBarCodeImage())
            {
                // Initialize the reader with the bitmap and request all supported types
                using (var reader = new BarCodeReader(bmp, DecodeType.AllSupportedTypes))
                {
                    // Read and output each detected barcode
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                        Console.WriteLine($"BarCode Text: {result.CodeText}");
                    }
                }
            }
        }
    }
}