using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        const int barcodeCount = 1000;
        var barcodeImages = new List<MemoryStream>(barcodeCount);

        // Generate barcodes with an incorrect checksum (EAN13 example)
        for (int i = 0; i < barcodeCount; i++)
        {
            // 13 digits where the last digit is intentionally wrong
            string incorrectEan13 = "1234567890123";

            using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, incorrectEan13))
            {
                var ms = new MemoryStream();
                // Save to memory stream in PNG format
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;
                barcodeImages.Add(ms);
            }
        }

        // Benchmark with AllowIncorrectBarcodes = false (default)
        long timeWithValidation = BenchmarkReading(barcodeImages, allowIncorrect: false);
        // Benchmark with AllowIncorrectBarcodes = true
        long timeWithoutValidation = BenchmarkReading(barcodeImages, allowIncorrect: true);

        Console.WriteLine($"Reading with AllowIncorrectBarcodes = false: {timeWithValidation} ms");
        Console.WriteLine($"Reading with AllowIncorrectBarcodes = true : {timeWithoutValidation} ms");
        Console.WriteLine($"Time saved: {timeWithValidation - timeWithoutValidation} ms");

        // Clean up memory streams
        foreach (var ms in barcodeImages)
        {
            ms.Dispose();
        }
    }

    static long BenchmarkReading(List<MemoryStream> images, bool allowIncorrect)
    {
        var stopwatch = Stopwatch.StartNew();

        foreach (var ms in images)
        {
            ms.Position = 0;
            using (var reader = new BarCodeReader(ms, DecodeType.EAN13))
            {
                // High‑performance mode
                reader.QualitySettings = QualitySettings.HighPerformance;
                // Toggle the AllowIncorrectBarcodes flag
                reader.QualitySettings.AllowIncorrectBarcodes = allowIncorrect;

                // Perform recognition (results are ignored for the benchmark)
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