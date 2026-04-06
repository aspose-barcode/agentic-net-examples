using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Directory containing frame images (simulating a video stream)
        string framesDirectory = "frames";

        if (!Directory.Exists(framesDirectory))
        {
            Console.WriteLine($"Frames directory not found: {framesDirectory}");
            return;
        }

        string[] frameFiles = Directory.GetFiles(framesDirectory, "*.png");
        if (frameFiles.Length == 0)
        {
            Console.WriteLine("No frame images found in the directory.");
            return;
        }

        long totalMilliseconds = 0;
        int processedFrames = 0;

        foreach (string framePath in frameFiles)
        {
            using (var bitmap = new Bitmap(framePath))
            {
                using (var reader = new BarCodeReader())
                {
                    // Set decode types (example: Code39, Code128, QR)
                    reader.BarCodeReadType = new MultiDecodeType(DecodeType.Code39, DecodeType.Code128, DecodeType.QR);
                    reader.SetBarCodeImage(bitmap);

                    var stopwatch = Stopwatch.StartNew();
                    BarCodeResult[] results = reader.ReadBarCodes();
                    stopwatch.Stop();

                    totalMilliseconds += stopwatch.ElapsedMilliseconds;
                    processedFrames++;

                    Console.WriteLine($"Frame: {Path.GetFileName(framePath)} - Processed in {stopwatch.ElapsedMilliseconds} ms");
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                    }
                }
            }
        }

        if (processedFrames > 0)
        {
            double averageTime = (double)totalMilliseconds / processedFrames;
            Console.WriteLine($"Average processing time per frame: {averageTime:F2} ms");
        }
    }
}