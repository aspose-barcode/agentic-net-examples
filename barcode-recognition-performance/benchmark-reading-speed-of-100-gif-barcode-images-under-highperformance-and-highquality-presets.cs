using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates benchmarking of Aspose.BarCode barcode recognition performance
/// using different quality settings on GIF images located in a folder.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Benchmarks HighPerformance and HighQuality quality settings on a sample of GIF barcode images.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the folder that contains GIF barcode images.
        string folderPath = "Barcodes";

        // Verify that the folder exists; exit if it does not.
        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Retrieve all GIF files from the folder.
        string[] gifFiles = Directory.GetFiles(folderPath, "*.gif");
        if (gifFiles.Length == 0)
        {
            Console.WriteLine("No GIF barcode images found in the folder.");
            return;
        }

        // Limit the benchmark to a maximum of 5 images to keep the run time reasonable.
        int sampleCount = Math.Min(gifFiles.Length, 5);
        Console.WriteLine($"Benchmarking {sampleCount} GIF images (out of {gifFiles.Length})");

        // --------------------------------------------------------------------
        // Benchmark using the HighPerformance quality preset.
        // --------------------------------------------------------------------
        var stopwatch = Stopwatch.StartNew(); // Start timing.

        for (int i = 0; i < sampleCount; i++)
        {
            string imagePath = gifFiles[i];

            // Create a barcode reader for the current image.
            using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                // Apply the HighPerformance quality settings.
                reader.QualitySettings = QualitySettings.HighPerformance;

                // Perform barcode recognition; results are not used for timing.
                var results = reader.ReadBarCodes();
            }
        }

        stopwatch.Stop(); // Stop timing for HighPerformance.
        Console.WriteLine($"HighPerformance: {stopwatch.ElapsedMilliseconds} ms for {sampleCount} images");

        // --------------------------------------------------------------------
        // Benchmark using the HighQuality quality preset.
        // --------------------------------------------------------------------
        stopwatch.Restart(); // Reset and start timing again.

        for (int i = 0; i < sampleCount; i++)
        {
            string imagePath = gifFiles[i];

            // Create a barcode reader for the current image.
            using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                // Apply the HighQuality quality settings.
                reader.QualitySettings = QualitySettings.HighQuality;

                // Perform barcode recognition; results are not used for timing.
                var results = reader.ReadBarCodes();
            }
        }

        stopwatch.Stop(); // Stop timing for HighQuality.
        Console.WriteLine($"HighQuality: {stopwatch.ElapsedMilliseconds} ms for {sampleCount} images");
    }
}