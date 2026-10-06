// Title: Compare recognition speed of 1D vs 2D barcodes
// Description: Demonstrates measuring and comparing the recognition performance of Code128 (1D) and QR (2D) barcodes using identical QualitySettings.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition performance category. It shows how to generate barcodes with BarcodeGenerator, configure QualitySettings for high‑performance scanning, and use BarCodeReader to read barcodes while timing the operation. Developers often need to benchmark different symbologies to choose the optimal barcode type for fast scanning in high‑throughput applications.
// Prompt: Compare recognition speed of 1D barcodes versus 2D barcodes under identical QualitySettings.
// Tags: barcode, recognition, performance, 1d, 2d, code128, qr, qualitysettings, aspose.barcode

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates measuring and comparing recognition speed of 1D (Code128) and 2D (QR) barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, measures recognition time with high‑performance QualitySettings, and outputs average timings.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeSpeedTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Sample data for 1D and 2D barcodes
        string[] oneDTexts = { "1234567890", "ABCDEFGHIJ", "9876543210", "CODE128TEST", "0011223344" };
        string[] twoDTexts = { "HelloWorld", "https://example.com", "Aspose.BarCode", "12345ABCDE", "QRTestData" };

        // -------------------- Generate 1D barcodes (Code128) --------------------
        var oneDFiles = new string[oneDTexts.Length];
        for (int i = 0; i < oneDTexts.Length; i++)
        {
            string filePath = Path.Combine(tempFolder, $"code128_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, oneDTexts[i]))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            oneDFiles[i] = filePath;
        }

        // -------------------- Generate 2D barcodes (QR) --------------------
        var twoDFiles = new string[twoDTexts.Length];
        for (int i = 0; i < twoDTexts.Length; i++)
        {
            string filePath = Path.Combine(tempFolder, $"qr_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, twoDTexts[i]))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            twoDFiles[i] = filePath;
        }

        // Use the same quality preset for both recognition tests
        QualitySettings preset = QualitySettings.HighPerformance;

        // -------------------- Measure recognition speed for 1D barcodes --------------------
        long oneDTotalTicks = 0;
        int oneDRecognized = 0;
        foreach (string file in oneDFiles)
        {
            if (!File.Exists(file)) continue;

            using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
            {
                reader.QualitySettings = preset;
                Stopwatch sw = Stopwatch.StartNew();
                var results = reader.ReadBarCodes();
                sw.Stop();

                oneDTotalTicks += sw.ElapsedTicks;
                if (results != null && results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText))
                {
                    oneDRecognized++;
                }
            }
        }

        // -------------------- Measure recognition speed for 2D barcodes --------------------
        long twoDTotalTicks = 0;
        int twoDRecognized = 0;
        foreach (string file in twoDFiles)
        {
            if (!File.Exists(file)) continue;

            using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
            {
                reader.QualitySettings = preset;
                Stopwatch sw = Stopwatch.StartNew();
                var results = reader.ReadBarCodes();
                sw.Stop();

                twoDTotalTicks += sw.ElapsedTicks;
                if (results != null && results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText))
                {
                    twoDRecognized++;
                }
            }
        }

        // -------------------- Output average recognition times --------------------
        double oneDAverageMs = (oneDTotalTicks * 1000.0) / Stopwatch.Frequency / oneDFiles.Length;
        double twoDAverageMs = (twoDTotalTicks * 1000.0) / Stopwatch.Frequency / twoDFiles.Length;

        Console.WriteLine($"1D Barcodes (Code128) - Recognized: {oneDRecognized}/{oneDFiles.Length}, Avg. time: {oneDAverageMs:F3} ms");
        Console.WriteLine($"2D Barcodes (QR)      - Recognized: {twoDRecognized}/{twoDFiles.Length}, Avg. time: {twoDAverageMs:F3} ms");

        // -------------------- Clean up temporary files --------------------
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}