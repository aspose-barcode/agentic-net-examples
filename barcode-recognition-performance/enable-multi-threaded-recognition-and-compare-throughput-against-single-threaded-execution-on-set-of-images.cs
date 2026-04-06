using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Prepare a temporary folder for barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(tempFolder);

        // Number of test images
        int imageCount = 20;
        // Generate test barcode images
        string[] imagePaths = GenerateBarcodes(tempFolder, imageCount);

        // Single‑threaded recognition (default settings)
        BarCodeReader.ProcessorSettings.UseAllCores = false;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 0;
        TimeSpan singleThreadTime = RecognizeImages(imagePaths);
        Console.WriteLine($"Single‑threaded total time: {singleThreadTime.TotalMilliseconds} ms");

        // Multi‑threaded recognition (enable all cores)
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2;
        TimeSpan multiThreadTime = RecognizeImages(imagePaths);
        Console.WriteLine($"Multi‑threaded total time: {multiThreadTime.TotalMilliseconds} ms");

        // Clean up generated images
        foreach (string file in imagePaths)
        {
            try { File.Delete(file); } catch { /* ignore */ }
        }
        try { Directory.Delete(tempFolder, true); } catch { /* ignore */ }
    }

    // Generates a set of barcode PNG files and returns their paths
    static string[] GenerateBarcodes(string folder, int count)
    {
        var paths = new List<string>();
        var rand = new Random();

        for (int i = 0; i < count; i++)
        {
            string text = $"CODE{i:D4}_{rand.Next(1000, 9999)}";
            string filePath = Path.Combine(folder, $"barcode_{i}.png");

            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            paths.Add(filePath);
        }

        return paths.ToArray();
    }

    // Recognizes barcodes in the provided image files and returns the elapsed time
    static TimeSpan RecognizeImages(string[] imagePaths)
    {
        var stopwatch = Stopwatch.StartNew();

        foreach (string path in imagePaths)
        {
            using (var reader = new BarCodeReader(path, DecodeType.Code128))
            {
                // Iterate through results to ensure full processing
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // For demo purposes we just access properties
                    string codeText = result.CodeText;
                    string codeType = result.CodeTypeName;
                }
            }
        }

        stopwatch.Stop();
        return stopwatch.Elapsed;
    }
}