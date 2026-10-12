// Title: Barcode recognition speed test for 1‑MB PNG
// Description: Demonstrates generating a ~1 MB PNG barcode image and measuring the time required to recognize it, ensuring the operation stays under a performance threshold.
// Category-Description: This example belongs to the Aspose.BarCode performance testing category, illustrating how to use BarcodeGenerator to create barcodes, BarCodeReader for decoding, and ProcessorSettings to control threading. Developers often need to benchmark recognition speed for large images in CI pipelines or real‑time applications.
// Prompt: Create a unit test that verifies recognition speed remains under 150 ms for 1‑MB PNG files.
// Tags: code128, speed, recognition, png, barcodegenerator, barcodereader

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a large barcode image and measuring recognition speed.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a barcode, pads it to ~1 MB, configures single‑threaded recognition,
    /// measures the decoding time, and validates it against a 150 ms threshold.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the test files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeSpeedTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "test.png");

        // Generate a barcode image large enough to be ~1 MB
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Increase image dimensions to enlarge file size
            generator.Parameters.ImageWidth.Point = 2000f;
            generator.Parameters.ImageHeight.Point = 2000f;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Ensure the file is at least 1 MB (pad if necessary)
        const long oneMb = 1024 * 1024;
        FileInfo info = new FileInfo(imagePath);
        if (info.Length < oneMb)
        {
            long padding = oneMb - info.Length;
            using (var stream = new FileStream(imagePath, FileMode.Append, FileAccess.Write, FileShare.None))
            {
                byte[] zeros = new byte[8192];
                while (padding > 0)
                {
                    int write = (int)Math.Min(padding, zeros.Length);
                    stream.Write(zeros, 0, write);
                    padding -= write;
                }
            }
        }

        // Verify file size
        info.Refresh();
        Console.WriteLine($"Generated PNG size: {info.Length} bytes");

        // Configure single‑threaded recognition for consistent timing
        BarCodeReader.ProcessorSettings.UseAllCores = false;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 1;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 0;

        // Measure recognition time
        long elapsedMs;
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            Stopwatch watch = Stopwatch.StartNew();
            BarCodeResult[] results = reader.ReadBarCodes();
            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;

            bool success = results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText);
            Console.WriteLine($"Recognition success: {success}");
            if (success)
            {
                Console.WriteLine($"Detected type: {results[0].CodeTypeName}");
                Console.WriteLine($"Detected text: {results[0].CodeText}");
            }
        }

        // Verify speed requirement
        if (elapsedMs <= 150)
        {
            Console.WriteLine($"PASSED: Recognition time {elapsedMs} ms ≤ 150 ms");
        }
        else
        {
            Console.WriteLine($"FAILED: Recognition time {elapsedMs} ms > 150 ms");
        }

        // Clean up temporary files
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored – cleanup failure should not affect test result
        }
    }
}