using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Generate sample barcode images
        List<string> imagePaths = new List<string>();
        for (int i = 1; i <= 5; i++)
        {
            string filePath = Path.Combine(outputFolder, $"code{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"CODE{i}"))
            {
                generator.Save(filePath);
            }
            imagePaths.Add(filePath);
        }

        // Warm‑up to avoid JIT impact
        ProcessBatch(imagePaths, useMinimal: false);

        // Measure without UseMinimalXDimension
        var timeWithout = MeasureProcessingTime(imagePaths, useMinimal: false);
        // Measure with UseMinimalXDimension
        var timeWith = MeasureProcessingTime(imagePaths, useMinimal: true);

        Console.WriteLine($"Processing time without UseMinimalXDimension: {timeWithout.TotalMilliseconds} ms");
        Console.WriteLine($"Processing time with UseMinimalXDimension:    {timeWith.TotalMilliseconds} ms");
    }

    static TimeSpan MeasureProcessingTime(List<string> imagePaths, bool useMinimal)
    {
        Stopwatch sw = Stopwatch.StartNew();
        ProcessBatch(imagePaths, useMinimal);
        sw.Stop();
        return sw.Elapsed;
    }

    static void ProcessBatch(List<string> imagePaths, bool useMinimal)
    {
        // Enable multi‑core usage for each reader call
        BarCodeReader.ProcessorSettings.UseAllCores = true;

        foreach (string path in imagePaths)
        {
            using (var reader = new BarCodeReader(path, DecodeType.Code128))
            {
                // Use a high‑performance preset for consistency
                reader.QualitySettings = QualitySettings.HighPerformance;

                // Configure XDimension mode
                reader.QualitySettings.XDimension = useMinimal
                    ? XDimensionMode.UseMinimalXDimension
                    : XDimensionMode.Auto;

                // Optional: set a minimal XDimension value when using the mode
                if (useMinimal)
                {
                    reader.QualitySettings.MinimalXDimension = 1f;
                }

                // Perform recognition (results are not used further)
                foreach (var result in reader.ReadBarCodes())
                {
                    // No operation; just iterate to ensure full processing
                }
            }
        }
    }
}