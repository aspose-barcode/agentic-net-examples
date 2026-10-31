// Title: Impact of Checksum Validation on Code 39 Recognition Speed
// Description: Demonstrates how disabling checksum verification affects the time required to read multiple Code 39 barcodes using Aspose.BarCode.
// Category-Description: This example belongs to the barcode generation and recognition category of Aspose.BarCode. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader with QualitySettings and ChecksumValidation to control recognition behavior. Developers often need to balance accuracy and performance when processing high‑volume scans, making checksum settings a common tuning point.
// Prompt: Test impact of disabling checksum verification on recognition speed for high‑volume Code 39 scans.
// Tags: code39, checksum, performance, barcode, generation, recognition, aspose.barcode

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Provides an example that measures the effect of enabling or disabling checksum validation
/// on the speed of reading Code 39 barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates sample Code 39 barcodes, measures reading times with checksum
    /// validation on and off, and outputs the results.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "ChecksumTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample Code39 barcodes and store file paths
        List<string> barcodeFiles = new List<string>();
        for (int i = 1; i <= 5; i++)
        {
            string text = "CODE" + i;
            string filePath = Path.Combine(tempFolder, $"Code39_{i}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39, text))
            {
                // Set X-dimension to 2 pixels for a compact barcode
                generator.Parameters.Barcode.XDimension.Pixels = 2;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Measure reading time with checksum validation enabled (default)
        TimeSpan timeOn = MeasureReading(barcodeFiles, ChecksumValidation.On);
        Console.WriteLine($"Reading with ChecksumValidation.On took: {timeOn.TotalMilliseconds} ms");

        // Measure reading time with checksum validation disabled
        TimeSpan timeOff = MeasureReading(barcodeFiles, ChecksumValidation.Off);
        Console.WriteLine($"Reading with ChecksumValidation.Off took: {timeOff.TotalMilliseconds} ms");

        // Cleanup temporary files and folder
        foreach (var file in barcodeFiles)
        {
            try { File.Delete(file); } catch { }
        }
        try { Directory.Delete(tempFolder, true); } catch { }
    }

    /// <summary>
    /// Reads a collection of barcode image files using the specified checksum validation setting
    /// and returns the elapsed time.
    /// </summary>
    /// <param name="files">List of barcode image file paths.</param>
    /// <param name="validation">Checksum validation mode (On or Off).</param>
    /// <returns>TimeSpan representing the total reading duration.</returns>
    static TimeSpan MeasureReading(List<string> files, ChecksumValidation validation)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        // Iterate through each file and perform barcode recognition
        foreach (string file in files)
        {
            if (!File.Exists(file))
                continue;

            using (BarCodeReader reader = new BarCodeReader(file, DecodeType.Code39))
            {
                // Apply the requested checksum validation setting
                reader.BarcodeSettings.ChecksumValidation = validation;

                // Use a high‑performance preset to focus on speed comparison
                reader.QualitySettings = QualitySettings.HighPerformance;

                // Perform the read operation
                BarCodeResult[] results = reader.ReadBarCodes();

                // Access result properties to ensure the read operation is exercised
                foreach (BarCodeResult result in results)
                {
                    string codeText = result.CodeText;
                    string codeType = result.CodeTypeName;
                }
            }
        }

        sw.Stop();
        return sw.Elapsed;
    }
}