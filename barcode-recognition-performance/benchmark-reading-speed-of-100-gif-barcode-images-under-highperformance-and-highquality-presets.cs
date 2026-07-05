// Title: Benchmark barcode reading speed for GIF images
// Description: Demonstrates measuring the time required to read 100 GIF barcode images using HighPerformance and HighQuality quality presets.
// Prompt: Benchmark reading speed of 100 GIF barcode images under HighPerformance and HighQuality presets.
// Tags: barcode, reading, performance, gif, highperformance, highquality, aspnet.barcode, qualitysettings

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates sample GIF barcode images and benchmarks the reading speed using
/// different <see cref="QualitySettings"/> presets (HighPerformance and HighQuality).
/// </summary>
class Program
{
    // Number of sample images to generate (kept small for runnable example)
    const int SampleCount = 5;

    // Folder to store generated barcode images
    const string ImagesFolder = "Barcodes";

    /// <summary>
    /// Entry point of the program. Creates sample barcode images, then measures
    /// the time required to read them with two quality presets.
    /// </summary>
    static void Main()
    {
        // Ensure the output folder exists
        if (!Directory.Exists(ImagesFolder))
        {
            Directory.CreateDirectory(ImagesFolder);
        }

        // Generate sample GIF barcode images
        GenerateSampleBarcodes();

        // Retrieve all generated GIF files
        string[] imageFiles = Directory.GetFiles(ImagesFolder, "*.gif");
        if (imageFiles.Length == 0)
        {
            Console.WriteLine("No barcode images found for benchmarking.");
            return;
        }

        // Benchmark reading with the HighPerformance preset
        BenchmarkReading("HighPerformance", QualitySettings.HighPerformance, imageFiles);

        // Benchmark reading with the HighQuality preset
        BenchmarkReading("HighQuality", QualitySettings.HighQuality, imageFiles);
    }

    // Generates SampleCount barcode images in GIF format
    static void GenerateSampleBarcodes()
    {
        for (int i = 0; i < SampleCount; i++)
        {
            // Build the file path for the current barcode image
            string filePath = Path.Combine(ImagesFolder, $"barcode_{i + 1}.gif");

            // Use Code128 symbology with a simple numeric text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"Sample{i + 1}"))
            {
                // Save the generated barcode as a GIF file
                generator.Save(filePath, BarCodeImageFormat.Gif);
            }
        }
    }

    // Benchmarks reading speed using the specified QualitySettings preset
    static void BenchmarkReading(string presetName, QualitySettings preset, string[] files)
    {
        Stopwatch sw = new Stopwatch();
        int totalBarcodesDetected = 0;

        // Start timing the read operation
        sw.Start();

        foreach (string file in files)
        {
            // Verify that the file exists before attempting to read
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            // Initialize the barcode reader for the current image file
            using (var reader = new BarCodeReader(file))
            {
                // Apply the selected quality preset
                reader.QualitySettings = preset;

                // Iterate through all barcodes detected in the image
                foreach (var result in reader.ReadBarCodes())
                {
                    totalBarcodesDetected++;

                    // Optionally, output each detected code text (commented out for speed)
                    // Console.WriteLine($"Detected: {result.CodeText}");
                }
            }
        }

        // Stop timing after all images have been processed
        sw.Stop();

        // Output benchmark results
        Console.WriteLine($"Preset: {presetName}");
        Console.WriteLine($"Processed {files.Length} images in {sw.Elapsed.TotalMilliseconds:F2} ms");
        Console.WriteLine($"Total barcodes detected: {totalBarcodesDetected}");
        Console.WriteLine();
    }
}