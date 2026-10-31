// Title: Compare recognition speed of 1D vs 2D barcodes
// Description: Demonstrates generating Code128 and QR barcodes, then measuring average recognition time using identical QualitySettings.
// Category-Description: This example belongs to the Aspose.BarCode recognition performance category. It shows how to use BarcodeGenerator to create barcodes, BarCodeReader with QualitySettings to decode them, and Stopwatch to benchmark decoding speed. Developers often need to compare processing times of different symbologies for optimization in scanning applications.
// Prompt: Compare recognition speed of 1D barcodes versus 2D barcodes under identical QualitySettings.
// Tags: barcode, recognition, performance, speed, 1d, 2d, code128, qr, qualitysettings, aspose.barcode, stopwatch

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates sample 1D (Code128) and 2D (QR) barcodes, then measures and compares their recognition speeds
/// using identical <see cref="QualitySettings"/>. Useful for performance benchmarking of different symbologies.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates temporary barcode images, measures average decode time for each symbology,
    /// outputs the results, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeSpeedTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Prepare containers for file paths of generated barcodes
        int sampleCount = 5;
        string[] oneDPaths = new string[sampleCount];
        string[] twoDPaths = new string[sampleCount];

        // -------------------- Generate 1D barcodes (Code128) --------------------
        for (int i = 0; i < sampleCount; i++)
        {
            string codeText = "CODE128_" + i;
            string filePath = Path.Combine(tempFolder, $"code128_{i}.png");

            // Use BarcodeGenerator to create a Code128 image
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            oneDPaths[i] = filePath;
        }

        // -------------------- Generate 2D barcodes (QR) --------------------
        for (int i = 0; i < sampleCount; i++)
        {
            string codeText = "QR_" + i;
            string filePath = Path.Combine(tempFolder, $"qr_{i}.png");

            // Use BarcodeGenerator to create a QR image
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            twoDPaths[i] = filePath;
        }

        // -------------------- Helper to measure recognition time --------------------
        double MeasureRecognitionTime(string[] paths, BaseDecodeType decodeType)
        {
            Stopwatch sw = new Stopwatch();
            int totalRead = 0;

            foreach (string path in paths)
            {
                if (!File.Exists(path))
                {
                    Console.WriteLine($"File not found: {path}");
                    continue;
                }

                // Initialize BarCodeReader with the specified decode type
                using (var reader = new BarCodeReader(path, decodeType))
                {
                    // Apply identical QualitySettings for a fair comparison
                    reader.QualitySettings = QualitySettings.NormalQuality;

                    sw.Start();
                    var results = reader.ReadBarCodes();
                    sw.Stop();

                    if (results.Length > 0)
                    {
                        totalRead++;
                        Console.WriteLine($"Decoded: {results[0].CodeText}");
                    }
                    else
                    {
                        Console.WriteLine("No barcode detected.");
                    }
                }
            }

            // Return average time per successfully decoded barcode (ms)
            if (totalRead == 0)
                return 0;

            return sw.Elapsed.TotalMilliseconds / totalRead;
        }

        // -------------------- Measure and display average recognition speeds --------------------
        double oneDAvgMs = MeasureRecognitionTime(oneDPaths, DecodeType.Code128);
        double twoDAvgMs = MeasureRecognitionTime(twoDPaths, DecodeType.QR);

        Console.WriteLine();
        Console.WriteLine($"Average recognition time (1D Code128): {oneDAvgMs:F2} ms");
        Console.WriteLine($"Average recognition time (2D QR): {twoDAvgMs:F2} ms");

        // -------------------- Cleanup temporary files --------------------
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors (e.g., files in use)
        }
    }
}