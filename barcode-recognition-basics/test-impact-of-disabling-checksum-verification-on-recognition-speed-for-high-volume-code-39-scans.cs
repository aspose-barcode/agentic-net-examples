// Title: Impact of Checksum Validation on Code 39 Recognition Speed
// Description: Demonstrates how disabling checksum verification affects the time required to recognize a batch of Code 39 barcodes.
// Category-Description: This example belongs to the Aspose.BarCode recognition performance category. It shows how to configure the BarCodeReader's ChecksumValidation property (On/Off) while processing multiple images, a common scenario for high‑volume scanning applications where speed is critical. Developers often need to balance validation accuracy against throughput using classes like BarCodeReader, BarcodeGenerator, and Stopwatch.
// Prompt: Test impact of disabling checksum verification on recognition speed for high‑volume Code 39 scans.
// Tags: code39, checksum, performance, recognition, aspose.barcode, barcodegenerator, barcodereader, stopwatch

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that measures the effect of enabling or disabling checksum validation
/// on the recognition speed of a set of Code 39 barcode images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample Code 39 barcodes, then times recognition with checksum
    /// validation turned on and off, outputting the results.
    /// </summary>
    static void Main()
    {
        const int sampleCount = 10; // Number of barcode images to generate
        string folder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        Directory.CreateDirectory(folder); // Ensure output directory exists

        // Generate sample Code 39 barcode images
        for (int i = 0; i < sampleCount; i++)
        {
            string text = $"CODE{i:D4}";
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39, text))
            {
                string filePath = Path.Combine(folder, $"code{i}.png");
                generator.Save(filePath); // Save PNG image to disk
            }
        }

        // Measure recognition time with checksum validation enabled
        long timeWithChecksum = MeasureRecognitionTime(folder, ChecksumValidation.On);

        // Measure recognition time with checksum validation disabled
        long timeWithoutChecksum = MeasureRecognitionTime(folder, ChecksumValidation.Off);

        // Output timing results
        Console.WriteLine($"Recognition time with checksum ON : {timeWithChecksum} ms");
        Console.WriteLine($"Recognition time with checksum OFF: {timeWithoutChecksum} ms");
    }

    /// <summary>
    /// Scans all PNG files in the specified folder using the given checksum setting
    /// and returns the elapsed time in milliseconds.
    /// </summary>
    /// <param name="folderPath">Path to the folder containing barcode images.</param>
    /// <param name="checksumSetting">Checksum validation mode (On or Off).</param>
    /// <returns>Elapsed time in milliseconds for processing all images.</returns>
    static long MeasureRecognitionTime(string folderPath, ChecksumValidation checksumSetting)
    {
        string[] files = Directory.GetFiles(folderPath, "*.png");
        Stopwatch sw = Stopwatch.StartNew(); // Start timing

        foreach (string file in files)
        {
            using (BarCodeReader reader = new BarCodeReader(file, DecodeType.Code39))
            {
                // Apply the requested checksum validation setting
                reader.BarcodeSettings.ChecksumValidation = checksumSetting;

                // ReadBarCodes returns an array; results are ignored for timing purposes
                _ = reader.ReadBarCodes();
            }
        }

        sw.Stop(); // Stop timing
        return sw.ElapsedMilliseconds;
    }
}