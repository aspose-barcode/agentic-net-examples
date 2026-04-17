using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main(string[] args)
    {
        // Path to the 1‑MB PNG file (can be passed as argument)
        string imagePath = args.Length > 0 ? args[0] : "sample.png";

        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        const int maxAllowedMs = 150;
        var stopwatch = new Stopwatch();

        try
        {
            using (var reader = new BarCodeReader(imagePath))
            {
                // Use high‑performance mode to speed up recognition
                reader.QualitySettings = QualitySettings.HighPerformance;
                // Set a generous timeout to avoid aborting the test
                reader.Timeout = 5000;

                stopwatch.Start();
                var results = reader.ReadBarCodes();
                stopwatch.Stop();

                Console.WriteLine($"Recognition time: {stopwatch.ElapsedMilliseconds} ms");
                Console.WriteLine($"Found {results.Length} barcode(s).");

                if (stopwatch.ElapsedMilliseconds <= maxAllowedMs)
                {
                    Console.WriteLine("Test passed: recognition within allowed time.");
                }
                else
                {
                    Console.WriteLine("Test failed: recognition exceeded allowed time.");
                }
            }
        }
        catch (RecognitionAbortedException ex)
        {
            Console.WriteLine($"Recognition aborted: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}