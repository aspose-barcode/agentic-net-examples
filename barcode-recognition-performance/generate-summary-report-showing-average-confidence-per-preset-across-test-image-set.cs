using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Sample image set (adjust paths as needed)
        string[] imagePaths = new string[]
        {
            "TestImages/image1.png",
            "TestImages/image2.png",
            "TestImages/image3.png",
            "TestImages/image4.png",
            "TestImages/image5.png"
        };

        // Define the quality presets to evaluate
        var presets = new Dictionary<string, QualitySettings>
        {
            { "NormalQuality", QualitySettings.NormalQuality },
            { "HighPerformance", QualitySettings.HighPerformance },
            { "HighQuality", QualitySettings.HighQuality },
            { "MaxQuality", QualitySettings.MaxQuality }
        };

        // Store results: preset name -> (total confidence, barcode count)
        var results = new Dictionary<string, (int totalConfidence, int count)>();

        foreach (var preset in presets)
        {
            int sumConfidence = 0;
            int barcodeCount = 0;

            foreach (string path in imagePaths)
            {
                if (!File.Exists(path))
                {
                    Console.WriteLine($"Warning: File not found - {path}");
                    continue;
                }

                // Use BarCodeReader to read barcodes with the current preset
                using (BarCodeReader reader = new BarCodeReader(path))
                {
                    reader.QualitySettings = preset.Value;

                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        // Cast enum to its underlying int value (0, 80, 100)
                        sumConfidence += (int)result.Confidence;
                        barcodeCount++;
                    }
                }
            }

            results[preset.Key] = (sumConfidence, barcodeCount);
        }

        // Output the average confidence per preset
        Console.WriteLine("Average Confidence per Quality Preset:");
        foreach (var kvp in results)
        {
            string presetName = kvp.Key;
            int total = kvp.Value.totalConfidence;
            int count = kvp.Value.count;

            double average = count > 0 ? (double)total / count : 0.0;
            Console.WriteLine($"{presetName}: {average:F2}");
        }
    }
}