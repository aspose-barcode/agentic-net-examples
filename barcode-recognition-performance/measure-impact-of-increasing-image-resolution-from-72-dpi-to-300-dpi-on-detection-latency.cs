using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        const string barcodeText = "1234567890";

        // Generate barcode at 72 DPI
        byte[] image72 = GenerateBarcode(barcodeText, EncodeTypes.Code128, 72f);
        // Generate barcode at 300 DPI
        byte[] image300 = GenerateBarcode(barcodeText, EncodeTypes.Code128, 300f);

        // Measure detection latency for 72 DPI image
        long latency72 = MeasureDetectionLatency(image72, DecodeType.Code128);
        // Measure detection latency for 300 DPI image
        long latency300 = MeasureDetectionLatency(image300, DecodeType.Code128);

        Console.WriteLine($"Detection latency at 72 DPI : {latency72} ms");
        Console.WriteLine($"Detection latency at 300 DPI: {latency300} ms");
        Console.WriteLine($"Latency increase: {latency300 - latency72} ms");
    }

    // Generates a barcode image with the specified resolution and returns it as a byte array.
    private static byte[] GenerateBarcode(string text, BaseEncodeType type, float resolution)
    {
        using (var generator = new BarcodeGenerator(type, text))
        {
            generator.Parameters.Resolution = resolution;

            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                return ms.ToArray();
            }
        }
    }

    // Measures the time (in milliseconds) required to detect barcodes in the given image data.
    private static long MeasureDetectionLatency(byte[] imageData, BaseDecodeType decodeType)
    {
        using (var ms = new MemoryStream(imageData))
        {
            using (var reader = new BarCodeReader(ms, decodeType))
            {
                var stopwatch = Stopwatch.StartNew();
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    var _ = result.CodeText;
                }
                stopwatch.Stop();
                return stopwatch.ElapsedMilliseconds;
            }
        }
    }
}