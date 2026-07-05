// Title: Barcode recognition speed test for 1‑MB PNG
// Description: Generates a large PNG barcode, then measures recognition time ensuring it stays under 150 ms.
// Prompt: Create a unit test that verifies recognition speed remains under 150 ms for 1‑MB PNG files.
// Tags: barcode, performance, png, recognition, unit-test, aspose.barcode

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demo program that creates a large barcode image and measures recognition speed.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that runs the performance test.
    /// </summary>
    static void Main()
    {
        // Path for temporary barcode image
        string imagePath = Path.Combine(Path.GetTempPath(), "large_barcode.png");

        // Ensure any previous file is removed
        if (File.Exists(imagePath))
        {
            File.Delete(imagePath);
        }

        // Generate a barcode image large enough to be at least 1 MB
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "SampleCodeForPerformanceTest1234567890"))
        {
            // Set a large image size to increase file size
            generator.Parameters.ImageWidth.Point = 2500f;
            generator.Parameters.ImageHeight.Point = 2500f;
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Save the image as PNG
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify the generated file exists
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Verify the file size is at least 1 MB (1 048 576 bytes)
        long fileSize = new FileInfo(imagePath).Length;
        if (fileSize < 1_048_576)
        {
            Console.WriteLine($"Generated image is smaller than 1 MB (size: {fileSize} bytes).");
            return;
        }

        // Prepare the barcode reader
        using (var reader = new BarCodeReader())
        {
            // Use high‑performance settings for speed
            reader.QualitySettings = QualitySettings.HighPerformance;

            // Set the image for recognition
            reader.SetBarCodeImage(imagePath);

            // Measure recognition time
            var stopwatch = Stopwatch.StartNew();
            var results = reader.ReadBarCodes();
            stopwatch.Stop();

            // Check if any barcode was detected
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
                return;
            }

            // Output the recognized code text
            foreach (var result in results)
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }

            // Verify the recognition time is under 150 ms
            long elapsedMs = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"Recognition time: {elapsedMs} ms");
            if (elapsedMs <= 150)
            {
                Console.WriteLine("Test passed: recognition speed is within the required limit.");
            }
            else
            {
                Console.WriteLine("Test failed: recognition took longer than 150 ms.");
            }
        }

        // Clean up the temporary file
        try
        {
            File.Delete(imagePath);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}