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
        // Prepare sample barcode image
        string imagePath = Path.Combine(Path.GetTempPath(), "sample.png");
        if (!File.Exists(imagePath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                generator.Save(imagePath);
            }
        }

        const int iterations = 5;

        // Run with hardware acceleration (multi‑core) if available
        long hwTime = RunRecognition(imagePath, iterations, useAllCores: true);
        // Run with software‑only (single core)
        long swTime = RunRecognition(imagePath, iterations, useAllCores: false);

        Console.WriteLine($"Hardware acceleration (multi‑core) total time for {iterations} runs: {hwTime} ms");
        Console.WriteLine($"Software‑only (single core) total time for {iterations} runs: {swTime} ms");
        Console.WriteLine($"Speedup: {(double)swTime / hwTime:F2}x");
    }

    static long RunRecognition(string imagePath, int iterations, bool useAllCores)
    {
        // Configure processor settings
        BarCodeReader.ProcessorSettings.UseAllCores = useAllCores;

        var stopwatch = Stopwatch.StartNew();

        for (int i = 0; i < iterations; i++)
        {
            using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
            {
                // Use normal quality for fair comparison
                reader.QualitySettings = QualitySettings.NormalQuality;

                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // Access result to ensure processing occurs
                    string code = result.CodeText;
                }
            }
        }

        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }
}