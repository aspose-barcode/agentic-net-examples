using System;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        const string filePath = "code39.png";
        const string codeText = "CODE39TEST";

        // Generate Code39 barcode and save to file
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, codeText))
        {
            generator.Save(filePath);
        }

        // Measure recognition time with default quality settings
        long defaultTimeMs;
        using (var reader = new BarCodeReader(filePath, DecodeType.Code39FullASCII))
        {
            var sw = Stopwatch.StartNew();
            foreach (var result in reader.ReadBarCodes())
            {
                // Access result to ensure processing
                var txt = result.CodeText;
            }
            sw.Stop();
            defaultTimeMs = sw.ElapsedMilliseconds;
        }

        // Measure recognition time with HighPerformance quality preset
        long highPerfTimeMs;
        using (var reader = new BarCodeReader(filePath, DecodeType.Code39FullASCII))
        {
            reader.QualitySettings = QualitySettings.HighPerformance;
            var sw = Stopwatch.StartNew();
            foreach (var result in reader.ReadBarCodes())
            {
                var txt = result.CodeText;
            }
            sw.Stop();
            highPerfTimeMs = sw.ElapsedMilliseconds;
        }

        Console.WriteLine($"Default quality recognition time: {defaultTimeMs} ms");
        Console.WriteLine($"HighPerformance quality recognition time: {highPerfTimeMs} ms");
    }
}