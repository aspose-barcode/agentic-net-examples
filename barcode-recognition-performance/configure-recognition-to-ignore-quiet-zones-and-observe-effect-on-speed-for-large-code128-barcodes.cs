// Title: Code128 Barcode Generation and Recognition Performance Comparison
// Description: Demonstrates generating large Code128 barcodes, then measuring recognition time with default and high‑performance settings, highlighting the impact of quality presets on speed.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader with QualitySettings for fast recognition. Typical scenarios include bulk barcode processing, performance tuning, and evaluating trade‑offs between accuracy and speed. Developers often need to adjust quality presets, deconvolution modes, and validation options to meet high‑throughput requirements.
/// Prompt: Configure recognition to ignore quiet zones and observe effect on speed for large Code128 barcodes.
/// Tags: code128, barcode, generation, recognition, performance, qualitysettings, aspose.barcode

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates large Code128 barcodes, then compares recognition performance using default
/// and high‑performance quality settings. Demonstrates how quality presets affect processing speed.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Creates temporary barcode images, measures recognition times,
    /// and cleans up generated files.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for this demo
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Prepare a few large Code128 strings (e.g., 500 characters each)
        int barcodeCount = 5;
        int codeLength = 500;
        string[] codeTexts = new string[barcodeCount];
        for (int i = 0; i < barcodeCount; i++)
        {
            codeTexts[i] = new string('A', codeLength);
        }

        // Generate barcode images and store their file paths
        string[] imagePaths = new string[barcodeCount];
        for (int i = 0; i < barcodeCount; i++)
        {
            string imagePath = Path.Combine(tempFolder, $"barcode_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeTexts[i]))
            {
                // No need to set size; default auto-sizing works for large code texts
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }
            imagePaths[i] = imagePath;
        }

        // ------------------------------------------------------------
        // Measure recognition time using default quality settings
        // ------------------------------------------------------------
        Stopwatch swDefault = Stopwatch.StartNew();
        foreach (string path in imagePaths)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($"File not found: {path}");
                continue;
            }

            using (var reader = new BarCodeReader(path, DecodeType.Code128))
            {
                // Default quality settings (no modifications)
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"[Default] Detected: {result.CodeText?.Length} chars");
                }
            }
        }
        swDefault.Stop();
        Console.WriteLine($"Total recognition time (default settings): {swDefault.ElapsedMilliseconds} ms");

        // ------------------------------------------------------------
        // Measure recognition time using high‑performance quality settings
        // ------------------------------------------------------------
        Stopwatch swFast = Stopwatch.StartNew();
        foreach (string path in imagePaths)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($"File not found: {path}");
                continue;
            }

            using (var reader = new BarCodeReader(path, DecodeType.Code128))
            {
                // Apply high‑performance preset to speed up processing
                reader.QualitySettings = QualitySettings.HighPerformance;
                // Reduce deconvolution effort for faster recognition
                reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;
                // Allow incorrect barcodes to avoid extra validation overhead
                reader.QualitySettings.AllowIncorrectBarcodes = true;

                // NOTE: Aspose.BarCode does not expose a property to ignore quiet zones.
                // Quiet zone handling is internal to the recognition engine and cannot be disabled via API.

                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"[Fast] Detected: {result.CodeText?.Length} chars");
                }
            }
        }
        swFast.Stop();
        Console.WriteLine($"Total recognition time (high‑performance settings): {swFast.ElapsedMilliseconds} ms");

        // ------------------------------------------------------------
        // Clean up temporary files and folder
        // ------------------------------------------------------------
        foreach (string path in imagePaths)
        {
            try
            {
                File.Delete(path);
            }
            catch
            {
                // Ignore any deletion errors
            }
        }

        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore any deletion errors
        }
    }
}