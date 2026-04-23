using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Directory containing sample images
        string imagesDir = "Images";

        if (!Directory.Exists(imagesDir))
        {
            Console.WriteLine($"Images directory \"{imagesDir}\" not found.");
            return;
        }

        // Collect image files (limit to common formats)
        var supportedExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            ".png", ".jpg", ".jpeg", ".bmp", ".tiff"
        };

        var allFiles = Directory.GetFiles(imagesDir);
        var imageFiles = new List<string>();
        foreach (var file in allFiles)
        {
            if (supportedExtensions.Contains(Path.GetExtension(file)))
                imageFiles.Add(file);
        }

        if (imageFiles.Count == 0)
        {
            Console.WriteLine("No image files found in the directory.");
            return;
        }

        // Process a safe sample size (max 5 images)
        int filesToProcess = Math.Min(5, imageFiles.Count);

        // Enable multi‑core processing for BarCodeReader
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2;

        Process currentProcess = Process.GetCurrentProcess();
        TimeSpan totalCpuTime = TimeSpan.Zero;
        var totalStopwatch = Stopwatch.StartNew();

        for (int i = 0; i < filesToProcess; i++)
        {
            string filePath = imageFiles[i];
            Console.WriteLine($"Processing \"{Path.GetFileName(filePath)}\"");

            TimeSpan cpuBefore = currentProcess.TotalProcessorTime;
            var wallStopwatch = Stopwatch.StartNew();

            using (var bitmap = new Bitmap(filePath))
            using (var reader = new BarCodeReader())
            {
                // Load image into the reader
                reader.SetBarCodeImage(bitmap);

                // Read all barcodes in the image
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }

            wallStopwatch.Stop();
            TimeSpan cpuAfter = currentProcess.TotalProcessorTime;
            TimeSpan cpuUsed = cpuAfter - cpuBefore;
            totalCpuTime += cpuUsed;

            Console.WriteLine($"  Wall time: {wallStopwatch.Elapsed.TotalSeconds:F2}s, CPU time: {cpuUsed.TotalSeconds:F2}s");
        }

        totalStopwatch.Stop();

        Console.WriteLine();
        Console.WriteLine($"Processed {filesToProcess} image(s).");
        Console.WriteLine($"Total wall time: {totalStopwatch.Elapsed.TotalSeconds:F2}s");
        Console.WriteLine($"Total CPU time: {totalCpuTime.TotalSeconds:F2}s");
    }
}