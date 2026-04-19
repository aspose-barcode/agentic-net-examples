using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string imagePath = "code128.png";

        // Generate a large Code128 barcode (200 characters)
        string longCode = new string('A', 100) + new string('1', 100);
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, longCode))
        {
            // Optional: set image size to accommodate long code
            generator.Parameters.ImageWidth.Point = 1200f;
            generator.Parameters.ImageHeight.Point = 200f;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Measure recognition time with default (NormalQuality) settings
        TimeSpan normalTime;
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            var sw = Stopwatch.StartNew();
            foreach (var result in reader.ReadBarCodes())
            {
                // Output result once
                Console.WriteLine($"[Normal] Detected: {result.CodeText}");
                break;
            }
            sw.Stop();
            normalTime = sw.Elapsed;
        }

        // Measure recognition time with HighPerformance settings (ignores quiet zones implicitly)
        TimeSpan highPerfTime;
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // HighPerformance preset speeds up processing; it also reduces checks that involve quiet zones
            reader.QualitySettings = QualitySettings.HighPerformance;

            var sw = Stopwatch.StartNew();
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"[HighPerformance] Detected: {result.CodeText}");
                break;
            }
            sw.Stop();
            highPerfTime = sw.Elapsed;
        }

        Console.WriteLine($"Recognition time (NormalQuality): {normalTime.TotalMilliseconds} ms");
        Console.WriteLine($"Recognition time (HighPerformance): {highPerfTime.TotalMilliseconds} ms");
    }
}