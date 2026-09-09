// Title: Code 39 checksum validation impact on recognition speed
// Description: Demonstrates how disabling checksum verification affects the time required to recognize multiple Code 39 barcodes using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition performance category. It shows how to generate Code 39 barcodes, configure checksum settings, and measure recognition speed using the BarCodeReader, BarcodeGenerator, and related settings such as ChecksumValidation and QualitySettings. Developers looking to optimize high‑volume scanning workflows can use this pattern to benchmark and tune barcode processing.
// Prompt: Test impact of disabling checksum verification on recognition speed for high‑volume Code 39 scans.
// Tags: code39, checksum, performance, recognition, aspnet, aspose.barcode, barcodegeneration, barcoderecognition

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates the effect of checksum validation on the recognition speed of Code 39 barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Generates sample Code 39 barcodes, measures recognition time with checksum validation enabled and disabled, and outputs the results.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for generated barcodes
        string tempFolder = Path.Combine(Path.GetTempPath(), "ChecksumSpeedTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Number of sample barcodes to generate
        int sampleCount = 5;
        List<string> barcodeFiles = new List<string>();

        // Generate Code39 barcodes with optional checksum enabled
        for (int i = 0; i < sampleCount; i++)
        {
            string codeText = "CODE" + i;
            string filePath = Path.Combine(tempFolder, $"code_{i}.png");

            using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.Code39, codeText))
            {
                gen.Parameters.Barcode.XDimension.Pixels = 2;
                // Enable optional checksum for demonstration purposes
                gen.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
                gen.Save(filePath, BarCodeImageFormat.Png);
            }

            barcodeFiles.Add(filePath);
        }

        // Measure recognition time with default checksum validation
        long timeDefault = MeasureRecognitionTime(barcodeFiles, ChecksumValidation.Default);
        // Measure recognition time with checksum validation turned off
        long timeOff = MeasureRecognitionTime(barcodeFiles, ChecksumValidation.Off);

        // Output benchmark results
        Console.WriteLine($"Recognition time with ChecksumValidation.Default: {timeDefault} ms");
        Console.WriteLine($"Recognition time with ChecksumValidation.Off   : {timeOff} ms");

        // Cleanup generated files and temporary folder
        try
        {
            foreach (var file in barcodeFiles)
            {
                if (File.Exists(file))
                    File.Delete(file);
            }
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect the demo
        }
    }

    /// <summary>
    /// Measures the total time required to read a collection of barcode images using the specified checksum setting.
    /// </summary>
    /// <param name="files">List of barcode image file paths.</param>
    /// <param name="checksumSetting">Checksum validation mode to apply during recognition.</param>
    /// <returns>Total elapsed time in milliseconds.</returns>
    static long MeasureRecognitionTime(List<string> files, ChecksumValidation checksumSetting)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        // Set decode type to Code39 for all reads
        BaseDecodeType decodeType = DecodeType.Code39;

        foreach (string file in files)
        {
            using (BarCodeReader reader = new BarCodeReader(file, decodeType))
            {
                // Apply the requested checksum validation mode
                reader.BarcodeSettings.ChecksumValidation = checksumSetting;
                // Use high performance preset to focus on speed
                reader.QualitySettings = QualitySettings.HighPerformance;

                // Read all barcodes in the image (single barcode per image in this demo)
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // Access result properties to ensure full processing
                    string text = result.CodeText;
                    string type = result.CodeTypeName;
                    double quality = result.ReadingQuality;
                    // Output suppressed to keep benchmark clean
                }
            }
        }

        sw.Stop();
        return sw.ElapsedMilliseconds;
    }
}