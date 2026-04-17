using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Prepare a small set of barcode images with an incorrect checksum (EAN13)
        const int sampleCount = 5;
        var barcodeImages = new List<MemoryStream>();

        for (int i = 0; i < sampleCount; i++)
        {
            // Incorrect EAN13 code (last digit is wrong)
            string incorrectCode = "1234567890123";

            using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, incorrectCode))
            {
                // Save to a memory stream in PNG format
                var ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;
                barcodeImages.Add(ms);
            }
        }

        // Benchmark without AllowIncorrectBarcodes (default = false)
        long timeWithout = MeasureReadingTime(barcodeImages, allowIncorrect: false);

        // Benchmark with AllowIncorrectBarcodes = true
        long timeWith = MeasureReadingTime(barcodeImages, allowIncorrect: true);

        Console.WriteLine($"Reading time without AllowIncorrectBarcodes: {timeWithout} ms");
        Console.WriteLine($"Reading time with AllowIncorrectBarcodes   : {timeWith} ms");
    }

    static long MeasureReadingTime(List<MemoryStream> images, bool allowIncorrect)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        foreach (var originalStream in images)
        {
            // Each read needs a fresh stream positioned at the beginning
            originalStream.Position = 0;
            using (var reader = new BarCodeReader(originalStream, DecodeType.EAN13))
            {
                // Configure QualitySettings
                reader.QualitySettings.AllowIncorrectBarcodes = allowIncorrect;

                // Perform recognition (results are ignored for benchmarking)
                foreach (var result in reader.ReadBarCodes())
                {
                    // No operation; just iterate to ensure full processing
                }
            }
        }

        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }
}