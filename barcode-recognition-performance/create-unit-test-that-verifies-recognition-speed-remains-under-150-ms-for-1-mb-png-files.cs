// Title: Barcode Recognition Speed Test for Large PNG Files
// Description: Demonstrates generating a ~1 MB QR code PNG and measuring the time required to recognize it using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition performance category. It shows how to use BarCodeGenerator to create a high‑resolution barcode image and BarCodeReader to decode it, measuring execution time. Developers often need to benchmark recognition speed for large images to ensure UI responsiveness or meet service‑level agreements.
// Prompt: Create a unit test that verifies recognition speed remains under 150 ms for 1‑MB PNG files.
// Tags: qr,barcode,generation,recognition,performance,benchmark,aspose.barcode,png

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a large QR code image and measuring barcode recognition speed.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a barcode image, measures recognition time, and validates performance criteria.
    /// </summary>
    static void Main()
    {
        // Define temporary directory and output image path
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeSpeedTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string imagePath = Path.Combine(tempDir, "largeBarcode.png");

        // Generate a large PNG barcode (~1 MB)
        GenerateLargeBarcode(imagePath);

        // Verify the file was created and report its size
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("FAILED: Barcode image was not created.");
            return;
        }

        long fileSize = new FileInfo(imagePath).Length;
        Console.WriteLine($"Generated image size: {fileSize} bytes.");

        // Measure recognition speed using BarCodeReader
        BaseDecodeType decodeType = DecodeType.AllSupportedTypes;
        using (var reader = new BarCodeReader(imagePath, decodeType))
        {
            var stopwatch = Stopwatch.StartNew();
            var results = reader.ReadBarCodes();
            stopwatch.Stop();

            long elapsedMs = stopwatch.ElapsedMilliseconds;
            bool success = results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText);

            Console.WriteLine($"Recognition time: {elapsedMs} ms.");
            Console.WriteLine(success ? "Recognition succeeded." : "Recognition failed (no result).");

            if (elapsedMs <= 150 && success)
            {
                Console.WriteLine("PASSED: Recognition speed is within the 150 ms limit.");
            }
            else
            {
                Console.WriteLine("FAILED: Recognition speed exceeds the 150 ms limit or no barcode detected.");
            }
        }

        // Clean up temporary files (best‑effort)
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignored – cleanup is best‑effort
        }
    }

    // Generates a QR barcode saved as a PNG file with dimensions large enough to exceed ~1 MB.
    private static void GenerateLargeBarcode(string outputPath)
    {
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "SampleCodeText"))
        {
            // Set a high resolution and explicit canvas size to increase file size.
            generator.Parameters.Resolution = 300f;
            generator.Parameters.ImageWidth.Pixels = 2000f;
            generator.Parameters.ImageHeight.Pixels = 2000f;

            // Optional: set a modest module size.
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save as PNG.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }
}