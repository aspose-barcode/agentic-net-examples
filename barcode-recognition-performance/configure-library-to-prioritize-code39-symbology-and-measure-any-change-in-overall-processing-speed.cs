// Title: Barcode Recognition Speed Test with Code39 Prioritization
// Description: Demonstrates generating barcodes of several symbologies, then measuring the recognition time with and without restricting the reader to Code39. Shows the performance impact of prioritizing a specific symbology.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It uses BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and related classes such as EncodeTypes, DecodeType, and BarCodeImageFormat. Typical use cases include batch processing of mixed‑symbology images, performance tuning, and benchmarking different decoding strategies. Developers often need to restrict decoding to a single symbology to improve speed in high‑throughput scenarios.
// Prompt: Configure the library to prioritize Code39 symbology and measure any change in overall processing speed.
// Tags: barcode symbology, speed measurement, code39, generation, recognition, aspose.barcode

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation and recognition speed measurement, focusing on Code39 prioritization.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, benchmarks recognition with and without Code39 restriction, and outputs timing results.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for sample barcodes
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeSpeedTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // List to hold paths of generated barcode files
        List<string> barcodeFiles = new List<string>();

        // Generate a Code39 barcode
        GenerateBarcode(Path.Combine(tempFolder, "code39.png"), EncodeTypes.Code39, "CODE39");
        barcodeFiles.Add(Path.Combine(tempFolder, "code39.png"));

        // Generate a Code128 barcode
        GenerateBarcode(Path.Combine(tempFolder, "code128.png"), EncodeTypes.Code128, "CODE128");
        barcodeFiles.Add(Path.Combine(tempFolder, "code128.png"));

        // Generate a QR code
        GenerateBarcode(Path.Combine(tempFolder, "qr.png"), EncodeTypes.QR, "https://example.com");
        barcodeFiles.Add(Path.Combine(tempFolder, "qr.png"));

        // Benchmark default recognition (no symbology restriction)
        long defaultTime = MeasureRecognition(barcodeFiles, null);

        // Benchmark recognition with Code39 restriction (prioritize Code39)
        BaseDecodeType code39Decode = DecodeType.Code39;
        long code39Time = MeasureRecognition(barcodeFiles, code39Decode);

        // Output timing results
        Console.WriteLine($"Default recognition time: {defaultTime} ms");
        Console.WriteLine($"Code39-restricted recognition time: {code39Time} ms");

        // Clean up temporary files and folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }

    /// <summary>
    /// Generates a barcode image using the specified encoding type and text.
    /// </summary>
    /// <param name="filePath">Full path where the image will be saved.</param>
    /// <param name="encodeType">Symbology to encode.</param>
    /// <param name="codeText">Text or data to encode.</param>
    static void GenerateBarcode(string filePath, BaseEncodeType encodeType, string codeText)
    {
        using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Save the generated barcode as a PNG image
            generator.Save(filePath, BarCodeImageFormat.Png);
        }
    }

    /// <summary>
    /// Measures the time required to recognize barcodes in the provided files.
    /// </summary>
    /// <param name="files">List of image file paths to process.</param>
    /// <param name="decodeType">
    /// Optional symbology restriction. If null, the reader attempts all supported types.
    /// </param>
    /// <returns>Total elapsed time in milliseconds.</returns>
    static long MeasureRecognition(List<string> files, BaseDecodeType decodeType)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        foreach (string file in files)
        {
            if (!File.Exists(file))
                continue;

            using (BarCodeReader reader = new BarCodeReader(file))
            {
                if (decodeType != null)
                {
                    // Restrict to a specific symbology to prioritize speed
                    reader.BarCodeReadType = decodeType;
                }

                try
                {
                    // Read all barcodes from the image
                    BarCodeResult[] results = reader.ReadBarCodes();
                    foreach (BarCodeResult result in results)
                    {
                        // Placeholder processing – retrieve text and symbology
                        string text = result.CodeText;
                        string symbology = result.CodeTypeName;
                    }
                }
                catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
                {
                    // Log a warning if the image cannot be loaded
                    Console.WriteLine($"Warning: Unable to load image {file}");
                }
            }
        }

        sw.Stop();
        return sw.ElapsedMilliseconds;
    }
}