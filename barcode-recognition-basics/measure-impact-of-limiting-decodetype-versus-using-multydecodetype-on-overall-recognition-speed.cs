// Title: Barcode recognition speed comparison: limited DecodeType vs MultiDecodeType
// Description: Demonstrates how to generate sample barcodes and measure the time required to read them using a limited set of DecodeTypes versus using MultiDecodeType.
// Category-Description: This example belongs to the Aspose.BarCode recognition performance category. It showcases the use of BarCodeGenerator for creating barcodes, BarCodeReader for decoding, and the DecodeType and MultiDecodeType classes to control which symbologies are processed. Developers often need to benchmark or optimize barcode scanning speed in bulk processing scenarios, and this snippet provides a baseline for such measurements.
// Prompt: Measure the impact of limiting DecodeType versus using MultyDecodeType on overall recognition speed.
// Tags: barcode symbology, performance, speed, decode type, multidecodetype, aspose.barcode, generation, recognition

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates measuring barcode recognition speed when limiting DecodeType versus using MultiDecodeType.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, measures reading time with two approaches, and outputs results.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for sample barcodes
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeSpeedTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define symbologies to generate and test
        BaseEncodeType[] encodeTypes = new BaseEncodeType[]
        {
            EncodeTypes.Code128,
            EncodeTypes.QR,
            EncodeTypes.DataMatrix
        };

        // Generate sample barcode images and collect file paths
        List<string> barcodeFiles = new List<string>();
        for (int i = 0; i < encodeTypes.Length; i++)
        {
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.png");
            var generator = new BarcodeGenerator(encodeTypes[i], $"Test{i}");
            generator.Save(filePath, BarCodeImageFormat.Png);
            barcodeFiles.Add(filePath);
        }

        // Measure reading time with limited DecodeType (SetBarCodeReadType)
        Stopwatch swLimited = new Stopwatch();
        swLimited.Start();
        foreach (string file in barcodeFiles)
        {
            using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
            {
                // Restrict reader to specific symbologies
                reader.SetBarCodeReadType(DecodeType.Code128, DecodeType.QR, DecodeType.DataMatrix);
                BarCodeResult[] results = reader.ReadBarCodes();
                // Optionally process results
            }
        }
        swLimited.Stop();

        // Measure reading time with MultiDecodeType
        Stopwatch swMulti = new Stopwatch();
        var multiDecode = new MultiDecodeType(DecodeType.Code128, DecodeType.QR, DecodeType.DataMatrix);
        swMulti.Start();
        foreach (string file in barcodeFiles)
        {
            using (var reader = new BarCodeReader(file, multiDecode))
            {
                BarCodeResult[] results = reader.ReadBarCodes();
                // Optionally process results
            }
        }
        swMulti.Stop();

        // Output the timing results
        Console.WriteLine($"Reading with limited DecodeType (SetBarCodeReadType) took: {swLimited.ElapsedMilliseconds} ms");
        Console.WriteLine($"Reading with MultiDecodeType took: {swMulti.ElapsedMilliseconds} ms");

        // Clean up temporary files and folder
        foreach (string file in barcodeFiles)
        {
            try { File.Delete(file); } catch { }
        }
        try { Directory.Delete(tempFolder, true); } catch { }
    }
}