using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Sample image files (adjust paths as needed)
        string[] imageFiles = new[]
        {
            "sample1.png",
            "sample2.png",
            "sample3.png"
        };

        // Define the quality presets to test
        var presets = new (string Name, QualitySettings Settings)[]
        {
            ("HighPerformance", QualitySettings.HighPerformance),
            ("NormalQuality", QualitySettings.NormalQuality),
            ("HighQuality", QualitySettings.HighQuality),
            ("MaxQuality", QualitySettings.MaxQuality)
        };

        foreach (string imagePath in imageFiles)
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                continue;
            }

            foreach (var preset in presets)
            {
                var stopwatch = Stopwatch.StartNew();

                using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code128))
                {
                    // Apply the current quality preset
                    reader.QualitySettings = preset.Settings;

                    // Perform recognition
                    BarCodeResult[] results = reader.ReadBarCodes();

                    stopwatch.Stop();

                    Console.WriteLine($"Image: {Path.GetFileName(imagePath)}, Preset: {preset.Name}, Time: {stopwatch.ElapsedMilliseconds} ms, Barcodes found: {results.Length}");
                }
            }
        }
    }
}