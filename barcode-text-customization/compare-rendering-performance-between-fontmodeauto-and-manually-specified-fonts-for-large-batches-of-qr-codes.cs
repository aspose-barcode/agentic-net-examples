// Title: QR Code FontMode Performance Benchmark
// Description: Demonstrates measuring rendering time of QR codes using FontMode.Auto versus manually specified fonts.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to configure barcode text rendering via FontMode (Auto or Manual). It illustrates typical use cases such as batch barcode creation, performance comparison, and font customization using the BarcodeGenerator, CodeTextParameters, and BarCodeImageFormat classes. Developers often need to benchmark rendering speed when switching between automatic font handling and explicit font settings for large barcode batches.
// Prompt: Compare rendering performance between FontMode.Auto and manually specified fonts for large batches of QR codes.
// Tags: qr code, fontmode, performance, benchmark, aspose.barcode, generation, png

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides a simple benchmark comparing QR code rendering performance
/// when using FontMode.Auto versus manually specified fonts.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the benchmark application.
    /// Generates two batches of QR codes with different FontMode settings
    /// and reports the elapsed time for each batch.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for output images
        string basePath = Path.Combine(Path.GetTempPath(), "QrFontModeBenchmark_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(basePath);

        // Prepare sample texts for the QR codes
        int batchSize = 5;
        string[] texts = new string[batchSize];
        for (int i = 0; i < batchSize; i++)
        {
            texts[i] = "SampleText_" + i;
        }

        // -------------------- Auto FontMode --------------------
        Stopwatch swAuto = Stopwatch.StartNew();
        for (int i = 0; i < batchSize; i++)
        {
            string filePath = Path.Combine(basePath, $"Auto_{i}.png");
            GenerateQr(texts[i], FontMode.Auto, filePath);
        }
        swAuto.Stop();

        // -------------------- Manual FontMode --------------------
        Stopwatch swManual = Stopwatch.StartNew();
        for (int i = 0; i < batchSize; i++)
        {
            string filePath = Path.Combine(basePath, $"Manual_{i}.png");
            GenerateQr(texts[i], FontMode.Manual, filePath);
        }
        swManual.Stop();

        // Output benchmark results
        Console.WriteLine($"Generated {batchSize} QR codes with FontMode.Auto in {swAuto.ElapsedMilliseconds} ms");
        Console.WriteLine($"Generated {batchSize} QR codes with FontMode.Manual in {swManual.ElapsedMilliseconds} ms");
        Console.WriteLine($"Output folder: {basePath}");
    }

    /// <summary>
    /// Generates a QR code image with the specified text, font mode, and output path.
    /// </summary>
    /// <param name="codeText">The text to encode in the QR code.</param>
    /// <param name="mode">The FontMode to apply to the code text.</param>
    /// <param name="outputPath">The file path where the PNG image will be saved.</param>
    static void GenerateQr(string codeText, FontMode mode, string outputPath)
    {
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Set common barcode parameters
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Parameters.Barcode.CodeTextParameters.FontMode = mode;
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Lucida Handwriting";

            if (mode == FontMode.Manual)
            {
                // Explicitly set font size for manual mode
                generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;
            }
            else // Auto mode
            {
                // Font size is ignored in Auto mode; set a placeholder value
                generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 10f;
            }

            // Save the generated QR code as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }
}