// Title: Compare FontMode.Auto vs Manual Font Rendering Performance for QR Codes
// Description: Demonstrates measuring the time required to generate a batch of QR codes using Aspose.BarCode with FontMode.Auto and with a manually specified font. Shows practical performance comparison for large barcode generation scenarios.
// Category-Description: This example belongs to the Aspose.BarCode performance testing category, illustrating how to use the BarcodeGenerator class, EncodeTypes, and CodeTextParameters to control font rendering. Developers often need to benchmark barcode generation for high‑volume applications, such as batch printing or real‑time scanning systems, and compare automatic font handling with custom font settings.
// Prompt: Compare rendering performance between FontMode.Auto and manually specified fonts for large batches of QR codes.
// Tags: qr, fontmode, performance, batch, aspnet, aspose.barcode, generation, png

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates performance comparison between automatic and manual font modes when generating QR codes.
/// </summary>
class Program
{
    /// <summary>
    /// Executes the performance test and outputs elapsed times.
    /// </summary>
    static void Main()
    {
        // Number of QR codes to generate in each test batch.
        const int batchSize = 5;

        // Create a temporary folder to store generated barcode images.
        string tempFolder = Path.Combine(Path.GetTempPath(), "FontModePerf_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Text to encode in each QR code.
        string codeText = "Sample QR Code Text";

        // ------------------------------------------------------------
        // Test 1: Generate QR codes using FontMode.Auto (automatic font handling).
        // ------------------------------------------------------------
        Stopwatch swAuto = Stopwatch.StartNew();
        for (int i = 0; i < batchSize; i++)
        {
            // Build the output file path for the current barcode image.
            string filePath = Path.Combine(tempFolder, $"Auto_{i}.png");

            // Create a barcode generator for QR code with the specified text.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                // Set module size (pixel dimension) for the QR code.
                generator.Parameters.Barcode.XDimension.Pixels = 4;

                // Use automatic font mode for the code text.
                generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Auto;

                // Save the generated barcode as a PNG image.
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }
        swAuto.Stop();
        Console.WriteLine($"FontMode.Auto total time: {swAuto.ElapsedMilliseconds} ms");

        // ------------------------------------------------------------
        // Test 2: Generate QR codes using FontMode.Manual with a custom font.
        // ------------------------------------------------------------
        Stopwatch swManual = Stopwatch.StartNew();
        for (int i = 0; i < batchSize; i++)
        {
            // Build the output file path for the current barcode image.
            string filePath = Path.Combine(tempFolder, $"Manual_{i}.png");

            // Create a barcode generator for QR code with the specified text.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                // Set module size (pixel dimension) for the QR code.
                generator.Parameters.Barcode.XDimension.Pixels = 4;

                // Use manual font mode and specify a custom font.
                generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Manual;
                generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Lucida Handwriting";
                generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

                // Save the generated barcode as a PNG image.
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }
        swManual.Stop();
        Console.WriteLine($"FontMode.Manual total time: {swManual.ElapsedMilliseconds} ms");
    }
}