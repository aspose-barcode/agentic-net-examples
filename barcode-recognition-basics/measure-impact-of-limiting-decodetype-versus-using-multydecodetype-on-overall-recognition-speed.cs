// Title: Measure barcode decoding speed with limited vs all decode types
// Description: Demonstrates how to generate sample barcodes, then compare the time required to decode them using DecodeType.AllSupportedTypes versus a MultiDecodeType with a limited set of symbologies.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition performance category. It shows how to use BarcodeGenerator to create barcodes, BarCodeReader with DecodeType or MultiDecodeType to read them, and Stopwatch to measure execution time. Developers often need to benchmark decoding speed when restricting supported symbologies to improve performance in high‑throughput scenarios.
// Prompt: Measure the impact of limiting DecodeType versus using MultyDecodeType on overall recognition speed.
// Tags: barcode, decoding, performance, speed, decode-type, multidecode, aspose.barcode, barcodereader, barcodegenerator

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates measuring barcode decoding speed when using all supported decode types versus a limited set via MultiDecodeType.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, measures decoding times, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for sample barcodes
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeSpeedTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample barcode images and collect their file paths
        List<string> barcodeFiles = new List<string>();
        GenerateBarcode(EncodeTypes.Code128, "CODE128_SAMPLE", Path.Combine(tempFolder, "code128.png"), barcodeFiles);
        GenerateBarcode(EncodeTypes.QR, "QR_SAMPLE", Path.Combine(tempFolder, "qr.png"), barcodeFiles);
        GenerateBarcode(EncodeTypes.DataMatrix, "DM_SAMPLE", Path.Combine(tempFolder, "datamatrix.png"), barcodeFiles);

        // Measure reading time when using all supported decode types
        long allSupportedTicks = MeasureReadingAllSupported(barcodeFiles);
        Console.WriteLine($"Reading with DecodeType.AllSupportedTypes took {allSupportedTicks} ms");

        // Measure reading time when limiting decode types via MultiDecodeType
        BaseDecodeType[] limitedTypes = new BaseDecodeType[] { DecodeType.Code128, DecodeType.QR, DecodeType.DataMatrix };
        long limitedTicks = MeasureReadingWithMultiDecode(barcodeFiles, limitedTypes);
        Console.WriteLine($"Reading with MultiDecodeType (limited) took {limitedTicks} ms");

        // Cleanup temporary files and folder
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
            // Ignored - cleanup failure should not affect program outcome
        }
    }

    /// <summary>
    /// Generates a barcode image using the specified encode type and text, saves it to the given path, and records the file path.
    /// </summary>
    static void GenerateBarcode(BaseEncodeType encodeType, string text, string outputPath, List<string> list)
    {
        using (var generator = new BarcodeGenerator(encodeType, text))
        {
            generator.Save(outputPath, BarCodeImageFormat.Png);
            list.Add(outputPath);
        }
    }

    /// <summary>
    /// Measures the total time required to read all barcodes using DecodeType.AllSupportedTypes.
    /// </summary>
    static long MeasureReadingAllSupported(List<string> files)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        foreach (var file in files)
        {
            using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
            {
                BarCodeResult[] results = reader.ReadBarCodes();
                // No further processing needed; just ensure reading occurs
            }
        }

        sw.Stop();
        return sw.ElapsedMilliseconds;
    }

    /// <summary>
    /// Measures the total time required to read all barcodes using a MultiDecodeType with a limited set of symbologies.
    /// </summary>
    static long MeasureReadingWithMultiDecode(List<string> files, BaseDecodeType[] types)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        foreach (var file in files)
        {
            var multiDecode = new MultiDecodeType(types);
            using (var reader = new BarCodeReader(file, multiDecode))
            {
                BarCodeResult[] results = reader.ReadBarCodes();
            }
        }

        sw.Stop();
        return sw.ElapsedMilliseconds;
    }
}