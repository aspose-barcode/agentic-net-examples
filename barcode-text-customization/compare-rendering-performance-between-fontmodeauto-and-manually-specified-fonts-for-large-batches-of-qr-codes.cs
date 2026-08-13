// Title: Benchmark FontMode.Auto vs Manual Font Settings for QR Code Generation
// Description: Demonstrates how to measure rendering performance when using FontMode.Auto compared with manually specified fonts while generating a batch of QR codes.
// Category-Description: This example belongs to the Aspose.BarCode performance benchmarking category. It shows how to use BarcodeGenerator, EncodeTypes, and CodeTextParameters to render QR codes, adjust FontMode, and evaluate rendering speed. Developers often need to compare automatic font sizing with explicit font settings to optimize batch barcode generation.
// Prompt: Compare rendering performance between FontMode.Auto and manually specified fonts for large batches of QR codes.
// Tags: qr code, performance, fontmode, automatic font, manual font, aspose.barcode, barcode generation, png output

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Provides a simple benchmark that compares the rendering time of QR codes
/// when using FontMode.Auto versus manually specified font settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a batch of QR codes with two different
    /// font configurations and measures the elapsed time for each approach.
    /// </summary>
    static void Main()
    {
        const int batchSize = 5;

        // Determine output folder and ensure it exists
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "output");
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Prepare sample data for the QR codes
        string[] sampleTexts = new string[batchSize];
        for (int i = 0; i < batchSize; i++)
        {
            sampleTexts[i] = $"Sample QR {i + 1}";
        }

        // -------------------- Benchmark FontMode.Auto --------------------
        Stopwatch swAuto = new Stopwatch();
        swAuto.Start();

        for (int i = 0; i < batchSize; i++)
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, sampleTexts[i]))
            {
                // Enable automatic font sizing
                generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Auto;

                // Optional: set QR error correction level
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                // Save the generated QR code image
                string filePath = Path.Combine(outputFolder, $"auto_{i + 1}.png");
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        swAuto.Stop();

        // -------------------- Benchmark Manual Font Specification --------------------
        Stopwatch swManual = new Stopwatch();
        swManual.Start();

        for (int i = 0; i < batchSize; i++)
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, sampleTexts[i]))
            {
                // Switch to manual font settings
                generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Manual;
                generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica";
                generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

                // Optional: set QR error correction level
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                // Save the generated QR code image
                string filePath = Path.Combine(outputFolder, $"manual_{i + 1}.png");
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        swManual.Stop();

        // Output benchmark results
        Console.WriteLine($"FontMode.Auto rendering time for {batchSize} QR codes: {swAuto.ElapsedMilliseconds} ms");
        Console.WriteLine($"Manual font rendering time for {batchSize} QR codes: {swManual.ElapsedMilliseconds} ms");
    }
}