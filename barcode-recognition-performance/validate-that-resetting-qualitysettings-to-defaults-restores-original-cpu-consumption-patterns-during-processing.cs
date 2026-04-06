using System;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        const string filePath = "barcode.png";

        // Generate a simple Code128 barcode image.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(filePath);
        }

        // Measure processing time with default (NormalQuality) settings.
        long defaultTime = MeasureReadTime(filePath, QualitySettings.NormalQuality);
        Console.WriteLine($"Default (NormalQuality) read time: {defaultTime} ms");

        // Measure with HighPerformance setting.
        long highPerfTime = MeasureReadTime(filePath, QualitySettings.HighPerformance);
        Console.WriteLine($"HighPerformance read time: {highPerfTime} ms");

        // Measure with HighQuality setting.
        long highQualTime = MeasureReadTime(filePath, QualitySettings.HighQuality);
        Console.WriteLine($"HighQuality read time: {highQualTime} ms");

        // Measure with MaxQuality setting.
        long maxQualTime = MeasureReadTime(filePath, QualitySettings.MaxQuality);
        Console.WriteLine($"MaxQuality read time: {maxQualTime} ms");

        // Reset to defaults (NormalQuality) and measure again.
        long resetTime = MeasureReadTime(filePath, QualitySettings.NormalQuality);
        Console.WriteLine($"After reset (NormalQuality) read time: {resetTime} ms");

        // Simple validation: check if reset time is within 10% of the original default time.
        double tolerance = 0.10;
        bool isRestored = Math.Abs(resetTime - defaultTime) <= defaultTime * tolerance;
        Console.WriteLine(isRestored
            ? "QualitySettings reset restored original processing pattern."
            : "Reset did not restore original processing pattern.");
    }

    static long MeasureReadTime(string imagePath, QualitySettings settings)
    {
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            reader.QualitySettings = settings;
            var stopwatch = Stopwatch.StartNew();
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Access result to ensure processing occurs.
                var _ = result.CodeText;
            }
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }
    }
}