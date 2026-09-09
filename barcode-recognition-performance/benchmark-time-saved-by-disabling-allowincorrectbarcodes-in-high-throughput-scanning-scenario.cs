// Title: Benchmarking AllowIncorrectBarcodes Impact on Scan Performance
// Description: Demonstrates measuring the time difference when the AllowIncorrectBarcodes setting is toggled while scanning a set of corrupted barcodes.
// Category-Description: This example belongs to the Aspose.BarCode scanning and quality settings category. It showcases the BarCodeReader class with its QualitySettings, particularly the AllowIncorrectBarcodes property, which controls whether the reader tolerates damaged or partially unreadable barcodes. Developers often benchmark such settings to optimize high‑throughput scanning pipelines, ensuring maximum speed without sacrificing accuracy.
// Prompt: Benchmark the time saved by disabling AllowIncorrectBarcodes in a high‑throughput scanning scenario.
// Tags: barcode, scanning, performance, benchmark, allowincorrectbarcodes, aspose.barcode, qr, pdf417, c#

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates benchmarking the effect of the AllowIncorrectBarcodes setting on barcode scanning performance.
/// </summary>
class Program
{
    /// <summary>
    /// Runs the benchmark and outputs timing results.
    /// </summary>
    static void Main()
    {
        // Create a temporary directory to store generated and corrupted barcode images
        string tempDir = Path.Combine(Path.GetTempPath(), "Benchmark_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        var imagePaths = new List<string>();

        try
        {
            // ------------------------------------------------------------
            // Generate sample QR and PDF417 barcodes, then corrupt them
            // ------------------------------------------------------------
            for (int i = 0; i < 5; i++)
            {
                // Generate QR code image
                string qrPath = Path.Combine(tempDir, $"qr_{i}.png");
                using (var generator = new BarcodeGenerator(EncodeTypes.QR, $"SampleQR{i}"))
                {
                    generator.Save(qrPath, BarCodeImageFormat.Png);
                }
                // Introduce visual corruption
                CorruptImage(qrPath);
                imagePaths.Add(qrPath);

                // Generate PDF417 code image
                string pdfPath = Path.Combine(tempDir, $"pdf_{i}.png");
                using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, $"SamplePDF417{i}"))
                {
                    generator.Save(pdfPath, BarCodeImageFormat.Png);
                }
                // Introduce visual corruption
                CorruptImage(pdfPath);
                imagePaths.Add(pdfPath);
            }

            // ------------------------------------------------------------
            // Benchmark scanning with AllowIncorrectBarcodes set to false
            // ------------------------------------------------------------
            var swFalse = new Stopwatch();
            int countFalse = 0;
            swFalse.Start();
            foreach (var path in imagePaths)
            {
                using (var reader = new BarCodeReader(path, DecodeType.AllSupportedTypes))
                {
                    reader.QualitySettings.AllowIncorrectBarcodes = false;
                    var results = reader.ReadBarCodes();
                    countFalse += results.Length;
                }
            }
            swFalse.Stop();

            // ------------------------------------------------------------
            // Benchmark scanning with AllowIncorrectBarcodes set to true
            // ------------------------------------------------------------
            var swTrue = new Stopwatch();
            int countTrue = 0;
            swTrue.Start();
            foreach (var path in imagePaths)
            {
                using (var reader = new BarCodeReader(path, DecodeType.AllSupportedTypes))
                {
                    reader.QualitySettings.AllowIncorrectBarcodes = true;
                    var results = reader.ReadBarCodes();
                    countTrue += results.Length;
                }
            }
            swTrue.Stop();

            // Output benchmark results
            Console.WriteLine($"AllowIncorrectBarcodes = false: Time = {swFalse.ElapsedMilliseconds} ms, Barcodes read = {countFalse}");
            Console.WriteLine($"AllowIncorrectBarcodes = true : Time = {swTrue.ElapsedMilliseconds} ms, Barcodes read = {countTrue}");
        }
        finally
        {
            // ------------------------------------------------------------
            // Clean up temporary files and directory
            // ------------------------------------------------------------
            if (Directory.Exists(tempDir))
            {
                try
                {
                    Directory.Delete(tempDir, true);
                }
                catch
                {
                    // Suppress any cleanup exceptions
                }
            }
        }
    }

    /// <summary>
    /// Corrupts an image by drawing a diagonal black line across it and overwriting the original file.
    /// </summary>
    /// <param name="imagePath">Full path to the image to corrupt.</param>
    static void CorruptImage(string imagePath)
    {
        using (var bitmap = new Bitmap(imagePath))
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                using (var pen = new Pen(Color.Black))
                {
                    // Draw a line from the top‑left to the bottom‑right corner
                    graphics.DrawLine(pen, 0, 0, bitmap.Width, bitmap.Height);
                }
            }

            // Save the corrupted bitmap back to the original file
            using (var stream = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
            {
                bitmap.Save(stream, ImageFormat.Png);
            }
        }
    }
}