using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Generate a barcode image in memory
        using (MemoryStream barcodeStream = new MemoryStream())
        {
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
            }

            barcodeStream.Position = 0;

            // Read the barcode from the memory stream
            using (BarCodeReader reader = new BarCodeReader())
            {
                reader.SetBarCodeReadType(DecodeType.Code128);
                reader.SetBarCodeImage(barcodeStream);

                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                    Console.WriteLine($"BarCode Text: {result.CodeText}");
                }
            }
        }
    }
}