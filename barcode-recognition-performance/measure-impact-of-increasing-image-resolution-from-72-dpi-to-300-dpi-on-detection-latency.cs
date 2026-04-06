using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Prepare temporary folder
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeResolutionDemo");
        Directory.CreateDirectory(tempFolder);

        // Barcode data
        const string codeText = "1234567890";

        // File paths for low and high resolution images
        string lowResPath = Path.Combine(tempFolder, "barcode_72dpi.png");
        string highResPath = Path.Combine(tempFolder, "barcode_300dpi.png");

        // Generate barcode at 72 DPI
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.Resolution = 72f; // 72 DPI
            generator.Save(lowResPath, BarCodeImageFormat.Png);
        }

        // Generate barcode at 300 DPI
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.Resolution = 300f; // 300 DPI
            generator.Save(highResPath, BarCodeImageFormat.Png);
        }

        // Measure detection latency for low resolution image
        long lowResLatency = MeasureRecognitionLatency(lowResPath);
        // Measure detection latency for high resolution image
        long highResLatency = MeasureRecognitionLatency(highResPath);

        Console.WriteLine($"Detection latency (72 DPI): {lowResLatency} ms");
        Console.WriteLine($"Detection latency (300 DPI): {highResLatency} ms");
        Console.WriteLine($"Latency increase: {highResLatency - lowResLatency} ms");
    }

    static long MeasureRecognitionLatency(string imagePath)
    {
        // Use a high‑performance quality preset for consistent timing
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            reader.QualitySettings = QualitySettings.HighPerformance;
            var stopwatch = Stopwatch.StartNew();
            // Perform recognition
            var results = reader.ReadBarCodes();
            stopwatch.Stop();

            // Output recognized barcodes (optional)
            foreach (var result in results)
            {
                Console.WriteLine($"Detected: {result.CodeTypeName} - {result.CodeText}");
            }

            return stopwatch.ElapsedMilliseconds;
        }
    }
}