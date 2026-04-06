using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        const int frameCount = 10;
        var barcodes = new List<Bitmap>();

        // Generate barcode images for each frame
        for (int i = 0; i < frameCount; i++)
        {
            string codeText = $"FRAME{i:D2}";
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Optional: set size parameters
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 150f;
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.Barcode.BarHeight.Point = 40f;

                // Generate bitmap and store a clone (to keep after disposing generator)
                Bitmap bmp = generator.GenerateBarCodeImage();
                barcodes.Add((Bitmap)bmp.Clone());
                bmp.Dispose();
            }
        }

        // Prepare benchmark
        var cpuTimes = new List<TimeSpan>();
        var totalStopwatch = Stopwatch.StartNew();

        // Use all processor cores for recognition (optional performance boost)
        BarCodeReader.ProcessorSettings.UseAllCores = true;

        foreach (var frame in barcodes)
        {
            // Measure CPU time for scanning this frame
            var process = Process.GetCurrentProcess();
            TimeSpan cpuStart = process.TotalProcessorTime;

            using (var reader = new BarCodeReader(frame, DecodeType.Code128))
            {
                // High performance mode for faster scanning
                reader.QualitySettings = QualitySettings.HighPerformance;
                // Optional timeout to avoid hangs
                reader.Timeout = 5000;

                // Perform recognition
                foreach (var result in reader.ReadBarCodes())
                {
                    // Access result to ensure processing occurs
                    string _ = result.CodeText;
                }
            }

            TimeSpan cpuEnd = process.TotalProcessorTime;
            cpuTimes.Add(cpuEnd - cpuStart);
        }

        totalStopwatch.Stop();

        // Output benchmark results
        Console.WriteLine($"Scanned {frameCount} frames in {totalStopwatch.Elapsed.TotalMilliseconds:F2} ms (wall-clock).");
        for (int i = 0; i < cpuTimes.Count; i++)
        {
            Console.WriteLine($"Frame {i:D2}: CPU time = {cpuTimes[i].TotalMilliseconds:F2} ms");
        }

        // Cleanup bitmaps
        foreach (var bmp in barcodes)
        {
            bmp.Dispose();
        }
    }
}