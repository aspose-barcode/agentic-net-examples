// Title: Benchmark FontMode.Auto vs Manual for QR code generation
// Description: Demonstrates measuring rendering performance when using FontMode.Auto versus manually specified fonts while generating a batch of QR codes.
// Category-Description: This example belongs to the Aspose.BarCode performance benchmarking category, illustrating how to use BarcodeGenerator, EncodeTypes, and FontMode settings. Developers often need to compare automatic font handling with explicit font specifications to optimize rendering speed for large barcode batches. The snippet shows typical usage of barcode parameters, image saving, and timing with Stopwatch, useful for performance testing scenarios.
// Prompt: Compare rendering performance between FontMode.Auto and manually specified fonts for large batches of QR codes.
// Tags: qr code, fontmode, performance, benchmark, aspose.barcode, generation, png

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates performance comparison between automatic and manual font modes when generating QR codes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates two batches of QR codes using different FontMode settings and measures the elapsed time.
    /// </summary>
    static void Main()
    {
        // Define the number of QR codes to generate in each batch
        const int batchSize = 5;

        // Create a temporary folder to store generated images
        string tempFolder = Path.Combine(Path.GetTempPath(), "FontModeBenchmark_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        Console.WriteLine($"Benchmark folder: {tempFolder}");
        Console.WriteLine();

        // ------------------------------
        // Auto font mode benchmark
        // ------------------------------
        Stopwatch swAuto = Stopwatch.StartNew(); // start timing for Auto mode
        for (int i = 0; i < batchSize; i++)
        {
            // Prepare unique text and output path for each QR code
            string codeText = $"AutoMode_{i}";
            string filePath = Path.Combine(tempFolder, $"Auto_{i}.png");

            // Generate QR code with FontMode.Auto
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 4;
                generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Auto;
                generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Lucida Handwriting";
                generator.Parameters.Barcode.CodeTextParameters.Font.Style = FontStyle.Underline;
                generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 10f; // ignored in Auto mode
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }
        swAuto.Stop(); // stop timing for Auto mode

        // ------------------------------
        // Manual font mode benchmark
        // ------------------------------
        Stopwatch swManual = Stopwatch.StartNew(); // start timing for Manual mode
        for (int i = 0; i < batchSize; i++)
        {
            // Prepare unique text and output path for each QR code
            string codeText = $"ManualMode_{i}";
            string filePath = Path.Combine(tempFolder, $"Manual_{i}.png");

            // Generate QR code with FontMode.Manual
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 4;
                generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Manual;
                generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Lucida Handwriting";
                generator.Parameters.Barcode.CodeTextParameters.Font.Style = FontStyle.Underline;
                generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f; // manual size
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }
        swManual.Stop(); // stop timing for Manual mode

        // Output benchmark results
        Console.WriteLine($"Generated {batchSize} QR codes with FontMode.Auto in {swAuto.ElapsedMilliseconds} ms");
        Console.WriteLine($"Generated {batchSize} QR codes with FontMode.Manual in {swManual.ElapsedMilliseconds} ms");
        Console.WriteLine();
        Console.WriteLine("Benchmark completed.");
    }
}