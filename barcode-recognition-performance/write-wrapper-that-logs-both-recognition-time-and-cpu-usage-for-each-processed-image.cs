using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Determine image files to process
        string[] imageFiles = args.Length > 0
            ? args
            : new[] { "sample1.png", "sample2.png", "sample3.png" };

        // Get the current process for CPU usage measurement
        Process currentProcess = Process.GetCurrentProcess();

        foreach (string imagePath in imageFiles)
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                continue;
            }

            // Measure CPU time before recognition
            TimeSpan cpuStart = currentProcess.TotalProcessorTime;
            Stopwatch sw = Stopwatch.StartNew();

            // Perform barcode recognition
            using (BarCodeReader reader = new BarCodeReader(imagePath))
            {
                // Optional: set a timeout to avoid hangs
                reader.Timeout = 5000; // 5 seconds

                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Image: {Path.GetFileName(imagePath)}");
                    Console.WriteLine($"  Barcode Type : {result.CodeTypeName}");
                    Console.WriteLine($"  Code Text    : {result.CodeText}");
                    Console.WriteLine($"  Confidence   : {result.Confidence}");
                }
            }

            // Stop timing
            sw.Stop();
            TimeSpan cpuEnd = currentProcess.TotalProcessorTime;
            TimeSpan cpuUsed = cpuEnd - cpuStart;

            // Log performance data
            Console.WriteLine($"Image: {Path.GetFileName(imagePath)}");
            Console.WriteLine($"  Recognition Time (ms): {sw.ElapsedMilliseconds}");
            Console.WriteLine($"  CPU Time (ms)        : {cpuUsed.TotalMilliseconds}");
            Console.WriteLine();
        }
    }
}