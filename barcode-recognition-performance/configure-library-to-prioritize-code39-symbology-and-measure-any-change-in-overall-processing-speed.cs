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
        // Prepare sample data
        string[] samples = { "ABC123", "CODE39", "TEST01", "HELLO", "WORLD" };
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(tempDir);

        // Generate Code39 barcodes
        for (int i = 0; i < samples.Length; i++)
        {
            string filePath = Path.Combine(tempDir, $"code39_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39, samples[i]))
            {
                generator.Save(filePath);
            }
        }

        // Measure recognition time with NormalQuality
        long normalTime = MeasureRecognitionTime(tempDir, QualitySettings.NormalQuality);
        // Measure recognition time with HighPerformance
        long highPerfTime = MeasureRecognitionTime(tempDir, QualitySettings.HighPerformance);

        Console.WriteLine($"Recognition time (NormalQuality): {normalTime} ms");
        Console.WriteLine($"Recognition time (HighPerformance): {highPerfTime} ms");
    }

    static long MeasureRecognitionTime(string folderPath, QualitySettings quality)
    {
        string[] files = Directory.GetFiles(folderPath, "*.png");
        Stopwatch sw = new Stopwatch();
        sw.Start();

        foreach (string file in files)
        {
            using (var reader = new BarCodeReader(file, DecodeType.Code39))
            {
                reader.QualitySettings = quality;
                foreach (var result in reader.ReadBarCodes())
                {
                    // Access result to ensure processing
                    string codeText = result.CodeText;
                }
            }
        }

        sw.Stop();
        return sw.ElapsedMilliseconds;
    }
}