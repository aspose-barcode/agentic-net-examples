// Title: Barcode Decode Type Speed Comparison
// Description: This example measures the impact on recognition speed when limiting the decoder to a single DecodeType versus using MultiDecodeType with Aspose.BarCode.
// Category-Description: The sample belongs to the Aspose.BarCode generation and recognition category, showcasing how to generate barcodes (using BarcodeGenerator) and read them (using BarCodeReader). Developers often need to benchmark performance for different decoding configurations, such as single‑symbology versus multi‑symbology decoding, to optimize processing time in high‑throughput scenarios.
// Prompt: Measure the impact of limiting DecodeType versus using MultyDecodeType on overall recognition speed.
// Tags: barcode, decode, speed, performance, aspose.barcode, generation, recognition, multidecode, singledecode, csharp

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates measuring barcode recognition speed using single and multi decode types.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, reads them with different decode configurations,
    /// and outputs the elapsed time for each approach.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeSpeedTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define barcode specifications to generate (type, text, file name)
        var barcodeSpecs = new List<(BaseEncodeType encodeType, string codeText, string fileName)>
        {
            (EncodeTypes.Code128, "CODE128-12345", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png"),
            (EncodeTypes.DataMatrix, "DM-98765", "datamatrix.png")
        };

        var imagePaths = new List<string>();

        // Generate barcode images and collect their file paths
        foreach (var spec in barcodeSpecs)
        {
            string filePath = Path.Combine(tempFolder, spec.fileName);
            using (var generator = new BarcodeGenerator(spec.encodeType, spec.codeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            imagePaths.Add(filePath);
        }

        // Prepare decode configurations
        BaseDecodeType singleDecode = DecodeType.Code128; // limit decoding to Code128 only
        var multiDecode = new MultiDecodeType(DecodeType.Code128, DecodeType.QR, DecodeType.DataMatrix); // allow multiple symbologies

        // Measure reading speed with single DecodeType
        var stopwatchSingle = Stopwatch.StartNew();
        foreach (string path in imagePaths)
        {
            using (var reader = new BarCodeReader(path, singleDecode))
            {
                // ReadBarCodes() returns an array; we invoke it to include decoding time
                BarCodeResult[] results = reader.ReadBarCodes();
                // Results are not processed to keep the measurement focused on decoding performance
            }
        }
        stopwatchSingle.Stop();

        // Measure reading speed with MultiDecodeType
        var stopwatchMulti = Stopwatch.StartNew();
        foreach (string path in imagePaths)
        {
            using (var reader = new BarCodeReader(path, multiDecode))
            {
                BarCodeResult[] results = reader.ReadBarCodes();
            }
        }
        stopwatchMulti.Stop();

        // Output the timing results
        Console.WriteLine($"Reading with single DecodeType (Code128 only) took: {stopwatchSingle.ElapsedMilliseconds} ms");
        Console.WriteLine($"Reading with MultiDecodeType (Code128, QR, DataMatrix) took: {stopwatchMulti.ElapsedMilliseconds} ms");

        // Clean up temporary files
        foreach (string path in imagePaths)
        {
            try
            {
                File.Delete(path);
            }
            catch
            {
                // Ignore cleanup errors
            }
        }

        // Remove the temporary folder
        try
        {
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}