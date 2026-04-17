using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

class Program
{
    // Number of sample images to generate (kept small for safe execution)
    private const int SampleCount = 5;

    static void Main()
    {
        // Prepare a temporary folder for the sample GIF images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeBenchmark");
        if (!Directory.Exists(tempFolder))
        {
            Directory.CreateDirectory(tempFolder);
        }

        // Generate sample barcode images
        string[] imageFiles = GenerateSampleBarcodes(tempFolder, SampleCount);

        // Benchmark with HighPerformance preset
        long highPerfMs = BenchmarkReading(imageFiles, QualitySettings.HighPerformance);
        Console.WriteLine($"HighPerformance reading time: {highPerfMs} ms");

        // Benchmark with HighQuality preset
        long highQualMs = BenchmarkReading(imageFiles, QualitySettings.HighQuality);
        Console.WriteLine($"HighQuality reading time: {highQualMs} ms");
    }

    // Generates 'count' barcode images in GIF format and returns their file paths
    private static string[] GenerateSampleBarcodes(string folder, int count)
    {
        string[] files = new string[count];
        for (int i = 0; i < count; i++)
        {
            string codeText = $"CODE{i + 1:D3}";
            string filePath = Path.Combine(folder, $"barcode_{i + 1}.gif");

            // Create a barcode generator for Code128
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Save as GIF
                generator.Save(filePath, BarCodeImageFormat.Gif);
            }

            files[i] = filePath;
        }
        return files;
    }

    // Reads all provided barcode images using the specified quality preset and returns elapsed milliseconds
    private static long BenchmarkReading(string[] files, QualitySettings preset)
    {
        Stopwatch sw = Stopwatch.StartNew();

        foreach (string file in files)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"Warning: File not found - {file}");
                continue;
            }

            // Use Code128 as the expected decode type
            using (var reader = new BarCodeReader(file, DecodeType.Code128))
            {
                // Apply the quality preset
                reader.QualitySettings = preset;

                // Perform recognition (results are ignored for benchmarking)
                BarCodeResult[] results = reader.ReadBarCodes();

                // Optional: output result count for verification
                Console.WriteLine($"{Path.GetFileName(file)}: {results.Length} barcode(s) detected.");
            }
        }

        sw.Stop();
        return sw.ElapsedMilliseconds;
    }
}