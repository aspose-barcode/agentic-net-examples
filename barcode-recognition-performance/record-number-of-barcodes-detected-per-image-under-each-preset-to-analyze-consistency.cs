using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Sample image files (place them in the same folder as the executable or adjust the paths)
        string[] imageFiles = new[]
        {
            "sample1.png",
            "sample2.png",
            "sample3.png"
        };

        // Define the recognition presets to evaluate
        var presets = new Dictionary<string, QualitySettings>
        {
            { "NormalQuality", QualitySettings.NormalQuality },
            { "HighPerformance", QualitySettings.HighPerformance },
            { "HighQuality", QualitySettings.HighQuality },
            { "MaxQuality", QualitySettings.MaxQuality }
        };

        // Iterate over each preset
        foreach (var preset in presets)
        {
            Console.WriteLine($"--- Preset: {preset.Key} ---");

            // Process each image under the current preset
            foreach (string imagePath in imageFiles)
            {
                if (!File.Exists(imagePath))
                {
                    Console.WriteLine($"File not found: {imagePath}");
                    continue;
                }

                // Initialize the reader for the image file
                using (var reader = new BarCodeReader(imagePath))
                {
                    // Apply the current quality preset
                    reader.QualitySettings = preset.Value;

                    // Perform recognition
                    BarCodeResult[] results = reader.ReadBarCodes();

                    int count = results?.Length ?? 0;
                    Console.WriteLine($"{Path.GetFileName(imagePath)}: {count} barcode(s) detected");
                }
            }

            Console.WriteLine(); // Blank line for readability between presets
        }
    }
}