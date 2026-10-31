// Title: Barcode detection performance comparison with and without Gaussian blur removal
// Description: Demonstrates generating sample barcodes, then recognizing them using Aspose.BarCode with default settings and with deconvolution (Gaussian blur removal) to measure impact on detection speed and accuracy.
// Category-Description: This example belongs to the Aspose.BarCode image preprocessing and recognition category. It shows how to use BarcodeGenerator to create barcodes, BarCodeReader with QualitySettings.Deconvolution to apply Gaussian blur removal, and Stopwatch for performance measurement. Developers often need to evaluate preprocessing techniques to improve barcode detection in low‑quality images, making this a useful reference for performance testing and image enhancement scenarios.
// Prompt: Preprocess input images with Gaussian blur removal before barcode detection to assess performance impact.
// Tags: barcode, gaussian blur, deconvolution, performance, recognition, Aspose.BarCode, image preprocessing, barcode generation

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation and recognition with and without Gaussian blur removal (deconvolution) to compare performance.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates sample barcodes, runs recognition in two modes, and reports timing results.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample barcode data: type, text, and output file name
        var samples = new List<(BaseEncodeType encodeType, string codeText, string fileName)>
        {
            (EncodeTypes.QR, "SampleQR123", "qr.png"),
            (EncodeTypes.Code128, "CODE128TEST", "code128.png"),
            (EncodeTypes.DataMatrix, "DMATRIX", "datamatrix.png")
        };

        // Generate barcode images and save them as PNG files
        foreach (var (encodeType, codeText, fileName) in samples)
        {
            string filePath = Path.Combine(tempFolder, fileName);
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        Console.WriteLine("Barcode images generated in: " + tempFolder);
        Console.WriteLine();

        // Process each image: compare default recognition vs. deconvolution (blur removal)
        foreach (var (_, _, fileName) in samples)
        {
            string filePath = Path.Combine(tempFolder, fileName);
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            // ----- Default recognition (no preprocessing) -----
            string defaultResult = "";
            long defaultTimeMs = 0;
            using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                var sw = Stopwatch.StartNew();
                var barcodes = reader.ReadBarCodes();
                sw.Stop();
                defaultTimeMs = sw.ElapsedMilliseconds;

                // Capture the first detected barcode (if any)
                foreach (var code in barcodes)
                {
                    defaultResult = $"Type: {code.CodeTypeName}, Text: {code.CodeText}";
                    break;
                }
            }

            // ----- Recognition with deconvolution (Gaussian blur removal) -----
            string deconvResult = "";
            long deconvTimeMs = 0;
            using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                // Enable deconvolution to mitigate blur effects
                reader.QualitySettings.Deconvolution = DeconvolutionMode.Normal;

                var sw = Stopwatch.StartNew();
                var barcodes = reader.ReadBarCodes();
                sw.Stop();
                deconvTimeMs = sw.ElapsedMilliseconds;

                // Capture the first detected barcode (if any)
                foreach (var code in barcodes)
                {
                    deconvResult = $"Type: {code.CodeTypeName}, Text: {code.CodeText}";
                    break;
                }
            }

            // Output comparison results for the current file
            Console.WriteLine($"File: {fileName}");
            Console.WriteLine($"  Default  -> {defaultResult} (Time: {defaultTimeMs} ms)");
            Console.WriteLine($"  Deconv   -> {deconvResult} (Time: {deconvTimeMs} ms)");
            Console.WriteLine();
        }

        // Cleanup: delete temporary folder and all generated files
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Cleanup failed: " + ex.Message);
        }
    }
}