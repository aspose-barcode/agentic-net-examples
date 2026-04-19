using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Create a memory stream that will hold a sample TIFF barcode image
        using (var tiffStream = new MemoryStream())
        {
            // Generate a simple Code128 barcode and save it as TIFF into the stream
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                generator.Save(tiffStream, BarCodeImageFormat.Tiff);
            }

            // Reset stream position before reading
            tiffStream.Position = 0;

            // Initialize the reader to detect all supported barcode types from the TIFF stream
            using (var reader = new BarCodeReader(tiffStream, DecodeType.AllSupportedTypes))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine("Detected barcode value: " + result.CodeText);
                }
            }
        }
    }
}