using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        const string imagePath = "sample.png";

        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        var totalTimer = Stopwatch.StartNew();

        // Loading stage
        var loadTimer = Stopwatch.StartNew();
        using (Bitmap bitmap = new Bitmap(imagePath))
        {
            loadTimer.Stop();
            Console.WriteLine($"Loading time: {loadTimer.ElapsedMilliseconds} ms");

            // Preprocessing stage
            var preprocessTimer = Stopwatch.StartNew();
            using (BarCodeReader reader = new BarCodeReader())
            {
                // Example preprocessing: set high performance quality preset
                reader.QualitySettings = QualitySettings.HighPerformance;
                preprocessTimer.Stop();
                Console.WriteLine($"Preprocessing time: {preprocessTimer.ElapsedMilliseconds} ms");

                // Set image for recognition
                reader.SetBarCodeImage(bitmap);

                // Detection stage
                var detectionTimer = Stopwatch.StartNew();
                BarCodeResult[] results = reader.ReadBarCodes();
                detectionTimer.Stop();
                Console.WriteLine($"Detection time: {detectionTimer.ElapsedMilliseconds} ms");

                // Decoding stage
                var decodingTimer = Stopwatch.StartNew();
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
                decodingTimer.Stop();
                Console.WriteLine($"Decoding time: {decodingTimer.ElapsedMilliseconds} ms");
            }
        }

        totalTimer.Stop();
        Console.WriteLine($"Total execution time: {totalTimer.ElapsedMilliseconds} ms");
    }
}