using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Prepare a folder for generated barcode images
        string folder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        Directory.CreateDirectory(folder);

        // Generate 100 GIF barcode images (Code128)
        for (int i = 0; i < 100; i++)
        {
            string filePath = Path.Combine(folder, $"barcode{i}.gif");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"Test{i}"))
            {
                generator.Save(filePath);
            }
        }

        // Benchmark with HighPerformance preset
        BenchmarkReading(folder, QualitySettings.HighPerformance, "HighPerformance");

        // Benchmark with HighQuality preset
        BenchmarkReading(folder, QualitySettings.HighQuality, "HighQuality");

        // Cleanup generated files (optional)
        // Directory.Delete(folder, true);
    }

    static void BenchmarkReading(string folderPath, QualitySettings preset, string presetName)
    {
        string[] files = Directory.GetFiles(folderPath, "*.gif");
        Stopwatch sw = new Stopwatch();
        sw.Start();

        foreach (string file in files)
        {
            using (var reader = new BarCodeReader(file, DecodeType.Code128))
            {
                reader.QualitySettings = preset;
                // Read barcodes (ignore results for benchmarking)
                reader.ReadBarCodes();
            }
        }

        sw.Stop();
        Console.WriteLine($"{presetName} preset: Processed {files.Length} images in {sw.ElapsedMilliseconds} ms");
    }
}