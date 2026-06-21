using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation and recognition benchmarking using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates sample barcode and blank images,
    /// runs recognition benchmarks with different MinimalXDimension settings,
    /// and outputs the results.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary directory for generated images.
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeBenchmark");
        Directory.CreateDirectory(tempDir);

        // Number of sample images to generate for each category.
        int sampleCount = 5;
        List<string> barcodeImages = new List<string>();
        List<string> blankImages = new List<string>();

        // ------------------------------------------------------------
        // Generate barcode images (Code128) and store their file paths.
        // ------------------------------------------------------------
        for (int i = 0; i < sampleCount; i++)
        {
            string codeText = $"Test{i}";
            string filePath = Path.Combine(tempDir, $"barcode_{i}.png");

            // Create a barcode generator for Code128 and save as PNG.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            barcodeImages.Add(filePath);
        }

        // ------------------------------------------------------------
        // Generate blank (white) images and store their file paths.
        // ------------------------------------------------------------
        for (int i = 0; i < sampleCount; i++)
        {
            string filePath = Path.Combine(tempDir, $"blank_{i}.png");

            // Create a blank bitmap, fill it with white, and save as PNG.
            using (var bitmap = new Bitmap(200, 100))
            {
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.Clear(Color.White);
                }

                bitmap.Save(filePath, ImageFormat.Png);
            }

            blankImages.Add(filePath);
        }

        // Run benchmark with MinimalXDimension = 0 pixels.
        Console.WriteLine("Benchmark with MinimalXDimension = 0 pixels");
        RunBenchmark(0f, barcodeImages, blankImages);

        // Run benchmark with MinimalXDimension = 2 pixels.
        Console.WriteLine("\nBenchmark with MinimalXDimension = 2 pixels");
        RunBenchmark(2f, barcodeImages, blankImages);

        // Optional cleanup: delete temporary directory and its contents.
        // Directory.Delete(tempDir, true);
    }

    /// <summary>
    /// Executes the barcode recognition benchmark for a given MinimalXDimension value.
    /// Counts true positives, false negatives, and false positives across provided images.
    /// </summary>
    /// <param name="minimalXDimension">The MinimalXDimension value to apply during recognition.</param>
    /// <param name="barcodeImages">List of file paths containing generated barcodes.</param>
    /// <param name="blankImages">List of file paths containing blank images.</param>
    static void RunBenchmark(float minimalXDimension, List<string> barcodeImages, List<string> blankImages)
    {
        int falsePositives = 0;
        int truePositives = 0;
        int falseNegatives = 0;

        // ------------------------------------------------------------
        // Process barcode images: expect at least one barcode detection.
        // ------------------------------------------------------------
        foreach (var path in barcodeImages)
        {
            using (var reader = new BarCodeReader(path, DecodeType.AllSupportedTypes))
            {
                // Configure quality settings to use the specified MinimalXDimension.
                reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                reader.QualitySettings.MinimalXDimension = minimalXDimension;

                bool found = false;

                // Attempt to read any barcode; stop after the first detection.
                foreach (var result in reader.ReadBarCodes())
                {
                    found = true;
                    break;
                }

                if (found)
                    truePositives++;   // Barcode correctly detected.
                else
                    falseNegatives++; // Barcode missed.
            }
        }

        // ------------------------------------------------------------
        // Process blank images: any detection is a false positive.
        // ------------------------------------------------------------
        foreach (var path in blankImages)
        {
            using (var reader = new BarCodeReader(path, DecodeType.AllSupportedTypes))
            {
                // Apply the same MinimalXDimension setting.
                reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                reader.QualitySettings.MinimalXDimension = minimalXDimension;

                // If any barcode is reported, count it as a false positive.
                foreach (var result in reader.ReadBarCodes())
                {
                    falsePositives++;
                    break;
                }
            }
        }

        // Output benchmark results.
        Console.WriteLine($"True Positives : {truePositives}");
        Console.WriteLine($"False Negatives: {falseNegatives}");
        Console.WriteLine($"False Positives: {falsePositives}");
    }
}