using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Prepare sample barcode data
        var texts = new[] { "ABC123", "DEF456", "GHI789", "JKL012", "MNO345" };
        var images = new List<byte[]>();

        // Generate barcode images and store them in memory
        foreach (var txt in texts)
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, txt))
            {
                // Optional: set image size
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 150f;

                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    images.Add(ms.ToArray());
                }
            }
        }

        // Enable multi‑core processing for BarCodeReader
        BarCodeReader.ProcessorSettings.UseAllCores = true;

        var results = new ConcurrentBag<string>();

        // Read barcodes from all images in parallel
        Parallel.ForEach(images, imageData =>
        {
            using (var ms = new MemoryStream(imageData))
            using (var reader = new BarCodeReader())
            {
                // Configure decode type
                reader.BarCodeReadType = new MultiDecodeType(DecodeType.Code128);
                // Assign image
                reader.SetBarCodeImage(ms);
                // Perform recognition
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    results.Add(result.CodeText);
                }
            }
        });

        // Output aggregated results
        Console.WriteLine("Aggregated barcode texts:");
        foreach (var code in results)
        {
            Console.WriteLine(code);
        }
    }
}