using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a memory stream that will hold the TIFF image
        using (var tiffStream = new MemoryStream())
        {
            // Generate a sample barcode and save it as TIFF into the memory stream
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ABC123"))
            {
                generator.Save(tiffStream, BarCodeImageFormat.Tiff);
            }

            // Reset stream position before reading
            tiffStream.Position = 0;

            // Read barcodes from the TIFF stream
            using (var reader = new BarCodeReader())
            {
                // Detect all supported barcode types
                reader.BarCodeReadType = new MultiDecodeType(DecodeType.AllSupportedTypes);
                // Assign the image stream to the reader
                reader.SetBarCodeImage(tiffStream);

                // Iterate through all detected barcodes
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected barcode: Type = {result.CodeTypeName}, Value = {result.CodeText}");
                }
            }
        }
    }
}