// Title: Barcode decoding speed comparison using DecodeType vs AllSupportedTypes
// Description: Demonstrates how limiting the DecodeType to a specific symbology (QR) versus using DecodeType.AllSupportedTypes affects overall barcode recognition performance.
// Category-Description: This example belongs to the Aspose.BarCode recognition performance category, showcasing the use of BarCodeReader, DecodeType, and QualitySettings classes. Developers often need to optimize scanning speed by restricting supported symbologies or using high‑performance presets, especially in bulk processing scenarios.
// Prompt: Measure the impact of limiting DecodeType versus using MultyDecodeType on overall recognition speed.
// Tags: barcode, decoding, speed, performance, decode type, allsupportedtypes, qrcode, code128, datamatrix, aspose.barcode, barcodereader, qualitysettings

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Provides a performance test comparing barcode decoding with all supported types versus a limited decode type.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, measures decoding times with different DecodeType settings, and outputs the results.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for sample barcodes
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeSpeedTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample barcodes (QR, Code128, DataMatrix)
        string[] symbologies = { "QR", "Code128", "DataMatrix" };
        int perSymbology = 5; // small safe sample size
        var barcodeFiles = new System.Collections.Generic.List<string>();

        foreach (string sym in symbologies)
        {
            // Resolve the symbology name to the corresponding encode type
            BaseEncodeType encodeType = ResolveEncodeType(sym);
            for (int i = 0; i < perSymbology; i++)
            {
                string filePath = Path.Combine(tempFolder, $"{sym}_{i}.png");
                using (var generator = new BarcodeGenerator(encodeType, $"Sample_{sym}_{i}"))
                {
                    // Save the generated barcode as PNG
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }
                barcodeFiles.Add(filePath);
            }
        }

        // Measure reading with all supported decode types
        BaseDecodeType allDecode = DecodeType.AllSupportedTypes;
        double timeAll = MeasureReadingTime(barcodeFiles.ToArray(), allDecode);

        // Measure reading with limited decode type (only QR)
        BaseDecodeType limitedDecode = DecodeType.QR;
        double timeLimited = MeasureReadingTime(barcodeFiles.ToArray(), limitedDecode);

        // Output the timing results
        Console.WriteLine($"Reading {barcodeFiles.Count} barcodes with DecodeType.AllSupportedTypes took {timeAll:F2} ms");
        Console.WriteLine($"Reading {barcodeFiles.Count} barcodes with DecodeType.QR (limited) took {timeLimited:F2} ms");

        // Clean up temporary files
        try
        {
            foreach (var file in barcodeFiles)
            {
                File.Delete(file);
            }
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }

    // Resolve a symbology name to a BaseEncodeType using reflection (EncodeTypes is a static class)
    static BaseEncodeType ResolveEncodeType(string name)
    {
        var field = typeof(EncodeTypes).GetField(name);
        if (field == null)
            throw new ArgumentException($"Unknown symbology: {name}");
        return (BaseEncodeType)field.GetValue(null);
    }

    // Measure total time to read all files with the specified decode type
    static double MeasureReadingTime(string[] files, BaseDecodeType decodeType)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        foreach (var file in files)
        {
            if (!File.Exists(file))
                continue;

            using (var reader = new BarCodeReader(file, decodeType))
            {
                // Use high performance preset for fair comparison
                reader.QualitySettings = QualitySettings.HighPerformance;

                // Read all barcodes in the image
                BarCodeResult[] results = reader.ReadBarCodes();

                // Optionally process results (here we just count them)
                int count = results.Length;
                // Console.WriteLine($"Found {count} barcode(s) in {Path.GetFileName(file)}");
            }
        }

        stopwatch.Stop();
        return stopwatch.Elapsed.TotalMilliseconds;
    }
}