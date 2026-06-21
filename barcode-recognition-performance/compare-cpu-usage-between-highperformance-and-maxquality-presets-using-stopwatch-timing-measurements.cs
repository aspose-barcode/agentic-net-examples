using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation and recognition timing using different quality presets.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode, then measures recognition time
    /// using MaxQuality and HighPerformance quality settings.
    /// </summary>
    static void Main()
    {
        // Generate a sample barcode image in memory.
        byte[] barcodeBytes;
        using (var ms = new MemoryStream())
        {
            // Create a Code128 barcode with the sample text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Save the barcode as PNG into the memory stream.
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Retrieve the generated image bytes.
            barcodeBytes = ms.ToArray();
        }

        // Define the two quality presets to compare.
        var maxQuality = QualitySettings.MaxQuality;
        // HighPerformance preset may not exist in all versions; fallback to MaxQuality if unavailable.
        var highPerformance = GetHighPerformancePreset();

        // Measure processing time with MaxQuality preset.
        long maxQualityTime = MeasureRecognitionTime(barcodeBytes, maxQuality);
        // Measure processing time with HighPerformance preset.
        long highPerformanceTime = MeasureRecognitionTime(barcodeBytes, highPerformance);

        // Output the timing results.
        Console.WriteLine($"MaxQuality processing time: {maxQualityTime} ms");
        Console.WriteLine($"HighPerformance processing time: {highPerformanceTime} ms");
    }

    /// <summary>
    /// Attempts to retrieve a HighPerformance preset; if not available, uses MaxQuality as a fallback.
    /// </summary>
    /// <returns>A <see cref="QualitySettings"/> instance representing the high‑performance preset.</returns>
    private static QualitySettings GetHighPerformancePreset()
    {
        // Some versions expose a HighPerformance static property; use reflection to avoid compile errors if absent.
        var property = typeof(QualitySettings).GetProperty("HighPerformance");
        if (property != null && property.PropertyType == typeof(QualitySettings))
        {
            return (QualitySettings)property.GetValue(null);
        }

        // Fallback to MaxQuality if HighPerformance is not defined.
        return QualitySettings.MaxQuality;
    }

    /// <summary>
    /// Measures the time (in milliseconds) taken to read barcodes from the provided image bytes
    /// using the specified quality preset.
    /// </summary>
    /// <param name="imageBytes">The barcode image data.</param>
    /// <param name="preset">The quality settings to apply during recognition.</param>
    /// <returns>The elapsed time in milliseconds.</returns>
    private static long MeasureRecognitionTime(byte[] imageBytes, QualitySettings preset)
    {
        using (var ms = new MemoryStream(imageBytes))
        {
            // Initialize the barcode reader for all supported decode types.
            using (var reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
            {
                // Apply the desired quality settings.
                reader.QualitySettings = preset;

                // Start timing.
                var stopwatch = Stopwatch.StartNew();

                // Perform the recognition; iterate results to ensure full processing.
                foreach (var result in reader.ReadBarCodes())
                {
                    // Access result properties to avoid compiler optimizations eliminating the loop.
                    var _ = result.CodeText;
                }

                // Stop timing and return elapsed milliseconds.
                stopwatch.Stop();
                return stopwatch.ElapsedMilliseconds;
            }
        }
    }
}