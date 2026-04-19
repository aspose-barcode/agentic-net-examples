using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.BarCodeRecognition; // for DecodeType
using Aspose.Drawing; // required for BarCodeImageFormat

class Program
{
    static void Main()
    {
        // Generate a simple barcode image in memory
        using (var ms = new MemoryStream())
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Ensure the stream is ready for reading
            ms.Position = 0;

            // Measure default (NormalQuality) processing time
            long defaultTime = MeasureReadingTime(ms, null);
            Console.WriteLine($"Default (NormalQuality) processing time: {defaultTime} ms");

            // Measure HighPerformance processing time
            long highPerfTime = MeasureReadingTime(ms, QualitySettings.HighPerformance);
            Console.WriteLine($"HighPerformance processing time: {highPerfTime} ms");

            // Reset to defaults (NormalQuality) and measure again
            long resetTime = MeasureReadingTime(ms, QualitySettings.NormalQuality);
            Console.WriteLine($"After reset to NormalQuality processing time: {resetTime} ms");

            // Compare default and reset times
            double tolerance = 0.1 * defaultTime; // 10% tolerance
            if (Math.Abs(defaultTime - resetTime) <= tolerance)
            {
                Console.WriteLine("Reset restored original performance within tolerance.");
            }
            else
            {
                Console.WriteLine("Performance after reset differs from original.");
            }
        }
    }

    // Measures the time taken to read barcodes using the specified QualitySettings.
    // If quality is null, the reader uses its default settings (NormalQuality).
    private static long MeasureReadingTime(MemoryStream sourceStream, QualitySettings quality)
    {
        // Create a fresh copy of the stream for each measurement
        using (var streamCopy = new MemoryStream())
        {
            sourceStream.Position = 0;
            sourceStream.CopyTo(streamCopy);
            streamCopy.Position = 0;

            using (var reader = new BarCodeReader(streamCopy, DecodeType.Code128))
            {
                if (quality != null)
                {
                    reader.QualitySettings = quality;
                }

                var stopwatch = Stopwatch.StartNew();
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // Access result properties to ensure full processing
                    Console.WriteLine($"Detected: {result.CodeTypeName} - {result.CodeText}");
                }
                stopwatch.Stop();
                return stopwatch.ElapsedMilliseconds;
            }
        }
    }
}