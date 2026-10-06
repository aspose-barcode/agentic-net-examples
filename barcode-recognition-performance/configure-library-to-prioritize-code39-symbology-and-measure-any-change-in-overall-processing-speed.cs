// Title: Code39 Symbology Prioritization Speed Test
// Description: Demonstrates generating Code39 and Code128 barcodes, then measuring the read performance when scanning all symbologies versus Code39 only.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Typical scenarios include performance benchmarking, batch processing, and selective symbology scanning. Developers often need to prioritize a specific symbology to improve throughput, and this snippet illustrates how to do so while measuring processing time.
// Prompt: Configure the library to prioritize Code39 symbology and measure any change in overall processing speed.
// Tags: barcode symbology, speed test, code39, code128, generation, recognition, aspose.barcode, performance

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates barcode images and compares scanning performance
/// when using a full symbology scan versus a Code39‑only scan.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, measures read times with and without
    /// prioritizing Code39, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for generated images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Code39SpeedTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample barcode images and collect their file paths
        List<string> barcodeFiles = new List<string>();
        GenerateBarcode(Path.Combine(tempFolder, "code39_1.png"), EncodeTypes.Code39, "CODE39A");
        barcodeFiles.Add(Path.Combine(tempFolder, "code39_1.png"));
        GenerateBarcode(Path.Combine(tempFolder, "code39_2.png"), EncodeTypes.Code39, "CODE39B");
        barcodeFiles.Add(Path.Combine(tempFolder, "code39_2.png"));
        GenerateBarcode(Path.Combine(tempFolder, "code128_1.png"), EncodeTypes.Code128, "CODE128A");
        barcodeFiles.Add(Path.Combine(tempFolder, "code128_1.png"));
        GenerateBarcode(Path.Combine(tempFolder, "code128_2.png"), EncodeTypes.Code128, "CODE128B");
        barcodeFiles.Add(Path.Combine(tempFolder, "code128_2.png"));
        GenerateBarcode(Path.Combine(tempFolder, "code39_3.png"), EncodeTypes.Code39, "CODE39C");
        barcodeFiles.Add(Path.Combine(tempFolder, "code39_3.png"));

        // Measure reading time without specifying symbology (full scan)
        TimeSpan fullScanTime = MeasureReading(barcodeFiles, null);
        Console.WriteLine($"Full scan time (all symbologies): {fullScanTime.TotalMilliseconds} ms");

        // Measure reading time with Code39 only (prioritized)
        BaseDecodeType code39Decode = DecodeType.Code39;
        TimeSpan code39OnlyTime = MeasureReading(barcodeFiles, code39Decode);
        Console.WriteLine($"Code39‑only scan time: {code39OnlyTime.TotalMilliseconds} ms");

        // Cleanup temporary folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors (e.g., files in use)
        }
    }

    /// <summary>
    /// Generates a barcode image using the specified encoding type and text.
    /// </summary>
    /// <param name="filePath">Full path where the image will be saved.</param>
    /// <param name="encodeType">Symbology to encode (e.g., Code39, Code128).</param>
    /// <param name="codeText">Text to encode in the barcode.</param>
    static void GenerateBarcode(string filePath, BaseEncodeType encodeType, string codeText)
    {
        // Create a generator with the desired symbology and text
        BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText);
        // Save the generated barcode as a PNG image
        generator.Save(filePath, BarCodeImageFormat.Png);
    }

    /// <summary>
    /// Measures the time required to read a collection of barcode images.
    /// </summary>
    /// <param name="files">List of image file paths to process.</param>
    /// <param name="decodeType">
    /// Optional specific symbology to decode. If null, all supported symbologies are scanned.
    /// </param>
    /// <returns>Elapsed time as a <see cref="TimeSpan"/>.</returns>
    static TimeSpan MeasureReading(List<string> files, BaseDecodeType decodeType)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        foreach (string file in files)
        {
            if (!File.Exists(file))
                continue;

            // Choose reader constructor based on whether a specific symbology is requested
            if (decodeType == null)
            {
                using (BarCodeReader reader = new BarCodeReader(file))
                {
                    try
                    {
                        BarCodeResult[] results = reader.ReadBarCodes();
                        // Results can be processed here if needed
                    }
                    catch (ArgumentException)
                    {
                        // Skip files that cannot be loaded as images
                    }
                }
            }
            else
            {
                using (BarCodeReader reader = new BarCodeReader(file, decodeType))
                {
                    try
                    {
                        BarCodeResult[] results = reader.ReadBarCodes();
                        // Results can be processed here if needed
                    }
                    catch (ArgumentException)
                    {
                        // Skip files that cannot be loaded as images
                    }
                }
            }
        }

        sw.Stop();
        return sw.Elapsed;
    }
}