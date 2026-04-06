using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode.Common;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Enable use of all processor cores for each BarCodeReader call
        BarCodeReader.ProcessorSettings.UseAllCores = true;

        const int imageCount = 8;
        var images = new List<Bitmap>();

        // Generate sample barcode images in memory
        for (int i = 0; i < imageCount; i++)
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"Code{i}"))
            {
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    ms.Position = 0;
                    var bmp = new Bitmap(ms);
                    images.Add(bmp);
                }
            }
        }

        // Parallel barcode reading
        Parallel.ForEach(images, (bitmap, state, index) =>
        {
            using (var reader = new BarCodeReader())
            {
                // Set the decode type (Code128) for this reader
                reader.SetBarCodeReadType(DecodeType.Code128);
                // Assign the bitmap image to the reader
                reader.SetBarCodeImage(bitmap);
                // Perform recognition
                var results = reader.ReadBarCodes();
                foreach (var result in results)
                {
                    Console.WriteLine($"Image {index}: Type={result.CodeTypeName}, Text={result.CodeText}");
                }
            }
        });

        // Dispose generated bitmaps
        foreach (var bmp in images)
        {
            bmp.Dispose();
        }
    }
}