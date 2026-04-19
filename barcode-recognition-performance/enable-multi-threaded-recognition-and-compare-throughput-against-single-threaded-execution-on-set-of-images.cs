using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Folder containing sample images (relative to executable)
        string imagesFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
        if (!Directory.Exists(imagesFolder))
        {
            Console.WriteLine($"Images folder not found: {imagesFolder}");
            return;
        }

        // Get up to 5 image files (png, jpg, bmp)
        string[] allFiles = Directory.GetFiles(imagesFolder, "*.*", SearchOption.TopDirectoryOnly);
        string[] imageFiles = Array.FindAll(allFiles, f =>
            f.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
            f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
            f.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
            f.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase));

        if (imageFiles.Length == 0)
        {
            Console.WriteLine("No image files found in the Images folder.");
            return;
        }

        // Limit to a safe sample size
        int sampleSize = Math.Min(5, imageFiles.Length);
        string[] sampleFiles = new string[sampleSize];
        Array.Copy(imageFiles, sampleFiles, sampleSize);

        // Single‑threaded run
        Console.WriteLine("Running single‑threaded recognition...");
        BarCodeReader.ProcessorSettings.UseAllCores = false;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 1;
        TimeSpan singleTime = RunRecognition(sampleFiles);
        Console.WriteLine($"Single‑threaded total time: {singleTime.TotalMilliseconds} ms");

        // Multi‑threaded run
        Console.WriteLine("\nRunning multi‑threaded recognition...");
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2;
        TimeSpan multiTime = RunRecognition(sampleFiles);
        Console.WriteLine($"Multi‑threaded total time: {multiTime.TotalMilliseconds} ms");

        // Comparison
        double speedup = singleTime.TotalMilliseconds / multiTime.TotalMilliseconds;
        Console.WriteLine($"\nSpeed‑up factor: {speedup:F2}x");
    }

    static TimeSpan RunRecognition(string[] files)
    {
        Stopwatch sw = Stopwatch.StartNew();

        foreach (string file in files)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            // Using BarCodeReader with common decode types (Code128 and QR)
            using (BarCodeReader reader = new BarCodeReader(file, DecodeType.Code128, DecodeType.QR))
            {
                // Perform recognition
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(file)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                }
            }
        }

        sw.Stop();
        return sw.Elapsed;
    }
}