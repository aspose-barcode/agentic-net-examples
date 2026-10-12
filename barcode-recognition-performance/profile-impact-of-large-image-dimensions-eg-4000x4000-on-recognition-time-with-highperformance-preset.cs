// Title: Barcode Recognition Performance with Large Images and HighPerformance Settings
// Description: Demonstrates generating a 4000x4000 barcode image and measuring the time required to recognize it using the HighPerformance quality preset.
// Category-Description: This example belongs to the Aspose.BarCode performance profiling category, illustrating how to use BarcodeGenerator for image creation and BarCodeReader with QualitySettings to evaluate recognition speed. Developers often need to benchmark barcode scanning on high‑resolution images, adjust AutoSizeMode, and select appropriate DecodeType for optimal throughput.
// Prompt: Profile the impact of large image dimensions (e.g., 4000x4000) on recognition time with HighPerformance preset.
// Tags: barcode, performance, highresolution, code128, generation, recognition, highperformance, aspose.barcode, c#

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates a large barcode image and profiles its recognition time using the HighPerformance preset.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a 4000x4000 Code128 barcode, measures recognition time, and cleans up temporary files.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // --------------------------------------------------------------------
        // Create a unique temporary folder to store the generated barcode image.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodePerf_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "large.png");

        // ---------------------------------------------------------------
        // Generate a barcode image with large dimensions (4000x4000 pixels).
        // ---------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Parameters.ImageWidth.Pixels = 4000f;
            generator.Parameters.ImageHeight.Pixels = 4000f;
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created successfully.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // ---------------------------------------------------------------
        // Measure recognition time using the HighPerformance quality preset.
        // ---------------------------------------------------------------
        var stopwatch = Stopwatch.StartNew();
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            reader.QualitySettings = QualitySettings.HighPerformance;
            BarCodeResult[] results = reader.ReadBarCodes();
            stopwatch.Stop();

            Console.WriteLine($"Recognition time: {stopwatch.ElapsedMilliseconds} ms");
            Console.WriteLine($"Barcodes found: {results.Length}");
            foreach (var result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // ------------------------------
        // Cleanup temporary files/folders.
        // ------------------------------
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored – cleanup failures are non‑critical for this demo.
        }
    }
}