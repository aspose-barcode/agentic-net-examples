// Title: QR Code Rendering Performance Comparison between FontMode.Auto and Manual Fonts
// Description: Demonstrates how to measure the generation time of QR codes using Aspose.BarCode with automatic font selection versus manually specified fonts.
// Category-Description: This example belongs to the Aspose.BarCode rendering performance category, showcasing the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters.FontMode. Developers often need to benchmark barcode generation for large batches to choose optimal settings for speed and quality. The snippet illustrates typical use cases such as bulk QR code creation for marketing or inventory systems.
// Prompt: Compare rendering performance between FontMode.Auto and manually specified fonts for large batches of QR codes.
// Tags: qr code, performance, fontmode, automatic, manual, aspnet, aspose.barcode, generation, benchmarking

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates performance comparison of QR code generation using FontMode.Auto versus manual font settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a set of QR codes with automatic and manual font modes, measures elapsed time, and outputs results.
    /// </summary>
    static void Main()
    {
        // Number of QR codes to generate for each test case (kept small for safe execution)
        const int sampleCount = 5;
        // Sample text to encode (same for all barcodes)
        const string sampleText = "https://www.example.com/performance-test";

        // Prepare timers for each font mode
        var autoTimer = new Stopwatch();
        var manualTimer = new Stopwatch();

        // Directory to store generated images
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "QrPerformance");
        if (!Directory.Exists(outputDir))
        {
            // Create the output folder if it does not exist
            Directory.CreateDirectory(outputDir);
        }

        // ---------- Test FontMode.Auto ----------
        for (int i = 0; i < sampleCount; i++)
        {
            // Build file path for the current QR code image
            string filePath = Path.Combine(outputDir, $"qr_auto_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, sampleText))
            {
                // Ensure FontMode is Auto (default, but set explicitly for clarity)
                generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Auto;

                // Optional: set QR error correction level
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                // Start timing, generate the barcode, then stop timing
                autoTimer.Start();
                generator.Save(filePath);
                autoTimer.Stop();
            }
        }

        // ---------- Test FontMode.Manual ----------
        for (int i = 0; i < sampleCount; i++)
        {
            // Build file path for the current QR code image
            string filePath = Path.Combine(outputDir, $"qr_manual_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, sampleText))
            {
                // Switch to manual font mode and specify font details
                generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Manual;
                generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
                generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f; // 12pt font

                // Optional: set QR error correction level
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                // Start timing, generate the barcode, then stop timing
                manualTimer.Start();
                generator.Save(filePath);
                manualTimer.Stop();
            }
        }

        // Output aggregated timing results to the console
        Console.WriteLine($"FontMode.Auto:   Total time = {autoTimer.ElapsedMilliseconds} ms for {sampleCount} QR codes.");
        Console.WriteLine($"FontMode.Manual: Total time = {manualTimer.ElapsedMilliseconds} ms for {sampleCount} QR codes.");
        Console.WriteLine($"Average per QR (Auto):   {(double)autoTimer.ElapsedMilliseconds / sampleCount:F2} ms");
        Console.WriteLine($"Average per QR (Manual): {(double)manualTimer.ElapsedMilliseconds / sampleCount:F2} ms");
    }
}