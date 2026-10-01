// Title: Impact of Checksum Validation on Code 39 Recognition Speed
// Description: Demonstrates how disabling checksum verification affects the processing time when recognizing a batch of Code 39 barcodes using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode recognition performance category. It shows how to configure BarCodeReader settings, specifically the ChecksumValidation property, to compare default (On) versus disabled (Off) checksum verification. Developers working with high‑volume barcode scanning, especially Code 39, can use this pattern to benchmark and optimize throughput.
// Prompt: Test impact of disabling checksum verification on recognition speed for high‑volume Code 39 scans.
// Tags: code39, checksumvalidation, performance, recognition, aspose.barcode, benchmark

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a set of Code 39 barcode images and benchmarks the recognition speed
/// with checksum validation enabled and disabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary barcode images, runs two
    /// recognition benchmarks (checksum on/off), outputs timing results, and
    /// cleans up the temporary files.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Number of sample barcodes to generate (kept small for CI)
        const int sampleCount = 10;

        // Create a unique temporary folder for the generated images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Code39Test_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample Code 39 barcode images and store their file paths
        var imagePaths = new string[sampleCount];
        for (int i = 0; i < sampleCount; i++)
        {
            string codeText = $"CODE{i:D4}";
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39, codeText))
            {
                // Save the barcode as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            imagePaths[i] = filePath;
        }

        // Benchmark with checksum validation ENABLED (default behavior)
        Stopwatch swOn = Stopwatch.StartNew();
        foreach (string path in imagePaths)
        {
            if (!File.Exists(path))
                continue;

            using (BarCodeReader reader = new BarCodeReader(path, DecodeType.Code39))
            {
                // Explicitly enable checksum validation (default)
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                // Read all barcodes in the image (results are ignored for timing)
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // No operation; iteration forces processing
                }
            }
        }
        swOn.Stop();

        // Benchmark with checksum validation DISABLED
        Stopwatch swOff = Stopwatch.StartNew();
        foreach (string path in imagePaths)
        {
            if (!File.Exists(path))
                continue;

            using (BarCodeReader reader = new BarCodeReader(path, DecodeType.Code39))
            {
                // Disable checksum validation to potentially improve speed
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;

                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // No operation; iteration forces processing
                }
            }
        }
        swOff.Stop();

        // Output the timing results to the console
        Console.WriteLine($"Processed {sampleCount} Code 39 images with checksum validation ON  : {swOn.ElapsedMilliseconds} ms");
        Console.WriteLine($"Processed {sampleCount} Code 39 images with checksum validation OFF : {swOff.ElapsedMilliseconds} ms");

        // Cleanup temporary files and folder
        try
        {
            foreach (string file in imagePaths)
            {
                if (File.Exists(file))
                    File.Delete(file);
            }
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // If cleanup fails, ignore – not critical for the demo
        }
    }
}