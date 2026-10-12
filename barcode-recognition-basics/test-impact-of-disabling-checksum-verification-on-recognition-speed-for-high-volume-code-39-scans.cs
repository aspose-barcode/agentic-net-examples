// Title: Impact of Checksum Validation on Code 39 Recognition Speed
// Description: Demonstrates how disabling checksum verification affects the time required to recognize a set of Code 39 barcodes using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition performance category. It showcases the use of BarcodeGenerator for creating Code 39 images and BarCodeReader with ChecksumValidation settings to measure processing speed. Developers often need to benchmark recognition throughput when handling high‑volume scans, adjusting checksum validation to balance accuracy and performance.
// Prompt: Test impact of disabling checksum verification on recognition speed for high‑volume Code 39 scans.
// Tags: code39, checksum, recognition, performance, barcode, aspose.barcode

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates measuring the effect of checksum validation on Code 39 barcode recognition speed.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample Code 39 barcodes, measures recognition time with checksum on/off, and cleans up.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "ChecksumTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample Code 39 barcodes and store file paths
        List<string> barcodeFiles = new List<string>();
        int sampleCount = 5; // safe sample size
        for (int i = 0; i < sampleCount; i++)
        {
            string filePath = Path.Combine(tempFolder, $"code39_{i}.png");
            using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.Code39FullASCII, $"CODE{i}"))
            {
                // Set barcode module size
                gen.Parameters.Barcode.XDimension.Pixels = 2;
                // Save barcode image as PNG
                gen.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Measure recognition time with checksum validation ON (default)
        long timeWithChecksum = MeasureRecognitionTime(barcodeFiles, ChecksumValidation.On);
        Console.WriteLine($"Recognition time with checksum ON: {timeWithChecksum} ms");

        // Measure recognition time with checksum validation OFF
        long timeWithoutChecksum = MeasureRecognitionTime(barcodeFiles, ChecksumValidation.Off);
        Console.WriteLine($"Recognition time with checksum OFF: {timeWithoutChecksum} ms");

        // Clean up temporary files
        foreach (string file in barcodeFiles)
        {
            try { File.Delete(file); } catch { }
        }
        try { Directory.Delete(tempFolder); } catch { }
    }

    /// <summary>
    /// Measures the total time required to read a collection of barcode images using the specified checksum validation setting.
    /// </summary>
    /// <param name="files">List of barcode image file paths.</param>
    /// <param name="validation">Checksum validation mode (On or Off).</param>
    /// <returns>Total elapsed time in milliseconds.</returns>
    static long MeasureRecognitionTime(List<string> files, ChecksumValidation validation)
    {
        Stopwatch sw = Stopwatch.StartNew();

        // Iterate through each barcode image and read its content
        foreach (string file in files)
        {
            using (BarCodeReader reader = new BarCodeReader(file, DecodeType.Code39FullASCII))
            {
                // Apply the requested checksum validation setting
                reader.BarcodeSettings.ChecksumValidation = validation;

                // Read all barcodes in the image
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // Access result to ensure processing (e.g., retrieve the decoded text)
                    string codeText = result.CodeText;
                }
            }
        }

        sw.Stop();
        return sw.ElapsedMilliseconds;
    }
}