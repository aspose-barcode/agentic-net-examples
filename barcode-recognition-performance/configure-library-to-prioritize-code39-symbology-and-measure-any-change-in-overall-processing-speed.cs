// Title: Code39 Barcode Generation and Speed Comparison
// Description: Generates a set of Code39 barcodes, then measures the time required to read them with default settings versus prioritizing the Code39 symbology.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Typical scenarios include performance testing, batch processing, and validating barcode symbology handling in applications. Developers often need to generate barcodes, read them efficiently, and compare processing times across different decode configurations.
// Prompt: Configure the library to prioritize Code39 symbology and measure any change in overall processing speed.
// Tags: barcode symbology, performance, code39, generation, recognition, aspose.barcode

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating Code39 barcodes and comparing read performance with and without prioritizing the Code39 symbology.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates barcodes, measures reading speed, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Code39Demo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate a collection of sample Code39 barcode images
        List<string> barcodeFiles = GenerateBarcodes(tempFolder, 5);

        // Measure reading speed using the default decoder (all symbologies)
        TimeSpan defaultTime = MeasureReadingSpeed(barcodeFiles, null);
        Console.WriteLine($"Reading all symbologies time: {defaultTime.TotalMilliseconds} ms");

        // Measure reading speed when prioritizing Code39 symbology
        BaseDecodeType code39Decode = DecodeType.Code39;
        TimeSpan code39Time = MeasureReadingSpeed(barcodeFiles, code39Decode);
        Console.WriteLine($"Reading only Code39 time: {code39Time.TotalMilliseconds} ms");

        // Clean up generated files and temporary directory
        foreach (string file in barcodeFiles)
        {
            try { File.Delete(file); } catch { }
        }
        try { Directory.Delete(tempFolder); } catch { }
    }

    /// <summary>
    /// Generates a specified number of Code39 barcode images in the given folder.
    /// </summary>
    /// <param name="folder">The directory where barcode images will be saved.</param>
    /// <param name="count">The number of barcode images to generate.</param>
    /// <returns>A list of file paths for the generated barcode images.</returns>
    static List<string> GenerateBarcodes(string folder, int count)
    {
        var files = new List<string>();
        for (int i = 1; i <= count; i++)
        {
            // Create barcode text and generator for Code39 symbology
            string codeText = "CODE39-" + i;
            var generator = new BarcodeGenerator(EncodeTypes.Code39, codeText);

            // Define output file path and save the barcode as PNG
            string filePath = Path.Combine(folder, $"code39_{i}.png");
            generator.Save(filePath, BarCodeImageFormat.Png);
            files.Add(filePath);
        }
        return files;
    }

    /// <summary>
    /// Measures the time required to read a collection of barcode images using the specified decode type.
    /// </summary>
    /// <param name="files">List of barcode image file paths to read.</param>
    /// <param name="decodeType">Optional decode type to prioritize; if null, all symbologies are considered.</param>
    /// <returns>The elapsed time taken to process all images.</returns>
    static TimeSpan MeasureReadingSpeed(List<string> files, BaseDecodeType decodeType)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        foreach (string file in files)
        {
            try
            {
                // Choose appropriate BarCodeReader constructor based on decode type
                if (decodeType == null)
                {
                    using (var reader = new BarCodeReader(file))
                    {
                        reader.ReadBarCodes();
                    }
                }
                else
                {
                    using (var reader = new BarCodeReader(file, decodeType))
                    {
                        reader.ReadBarCodes();
                    }
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine($"Failed to load image: {file}");
            }
        }

        sw.Stop();
        return sw.Elapsed;
    }
}