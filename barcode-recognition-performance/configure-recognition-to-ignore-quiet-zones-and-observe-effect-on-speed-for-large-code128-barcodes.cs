using System;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Generate a large Code128 barcode (200 characters)
        string longText = new string('A', 200);
        string imagePath = "code128.png";

        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, longText))
        {
            // Adjust XDimension to ensure the barcode is readable
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Save(imagePath);
        }

        // Recognize with default (NormalQuality) settings and measure time
        var stopwatch = Stopwatch.StartNew();
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine("Default Quality - CodeText: " + result.CodeText);
            }
        }
        stopwatch.Stop();
        Console.WriteLine($"Default Quality elapsed: {stopwatch.ElapsedMilliseconds} ms");

        // Recognize with HighPerformance quality (ignores quiet zones for speed) and measure time
        stopwatch.Restart();
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            reader.QualitySettings = QualitySettings.HighPerformance;
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine("HighPerformance - CodeText: " + result.CodeText);
            }
        }
        stopwatch.Stop();
        Console.WriteLine($"HighPerformance elapsed: {stopwatch.ElapsedMilliseconds} ms");
    }
}