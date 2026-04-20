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
        // Sample size (use a small number for a runnable example)
        const int sampleSize = 10;

        // Prepare in‑memory PNG images of Planet barcodes
        var streams = new List<MemoryStream>(sampleSize);
        for (int i = 0; i < sampleSize; i++)
        {
            // Create a Planet barcode with a simple numeric code
            using (var generator = new BarcodeGenerator(EncodeTypes.Planet, $"12345{i:D2}"))
            {
                var ms = new MemoryStream();
                // Save the barcode image to the memory stream in PNG format
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset for reading
                streams.Add(ms);
            }
        }

        // Benchmark decoding of the prepared barcodes
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        foreach (var ms in streams)
        {
            // Decode the image using the Planet decode type
            using (var reader = new BarCodeReader(ms, DecodeType.Planet))
            {
                // Perform the recognition
                var results = reader.ReadBarCodes();

                // Optional: verify that a barcode was found
                if (results.Length == 0)
                {
                    Console.WriteLine("No barcode detected in one of the images.");
                }
            }

            // Dispose the stream after it has been processed
            ms.Dispose();
        }

        stopwatch.Stop();

        // Output benchmark results
        Console.WriteLine($"Decoded {sampleSize} Planet barcodes in {stopwatch.Elapsed.TotalMilliseconds} ms.");
        Console.WriteLine($"Average time per barcode: {stopwatch.Elapsed.TotalMilliseconds / sampleSize:F2} ms.");
    }
}