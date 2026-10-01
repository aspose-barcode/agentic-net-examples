// Title: Profiling the impact of AllowIncorrectBarcodes on barcode scanning performance
// Description: Demonstrates how to measure CPU time differences when scanning corrupted barcodes with AllowIncorrectBarcodes set to true or false.
// Category-Description: This example belongs to the Aspose.BarCode scanning and performance profiling category. It showcases the BarCodeReader, BarcodeGenerator, and QualitySettings APIs for generating, corrupting, and repeatedly decoding barcodes. Developers use these patterns to benchmark decoding settings, evaluate error‑tolerance options, and optimize CPU usage in high‑throughput scanning scenarios.
// Prompt: Profile the impact of AllowIncorrectBarcodes on overall CPU load during continuous scanning.
// Tags: barcode, code128, scanning, performance, profiling, allowincorrectbarcodes, barcodereader, barcodegenerator, qualitysettings

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates profiling of the AllowIncorrectBarcodes setting during repeated barcode scanning.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, creates a corrupted version, profiles scanning with and without AllowIncorrectBarcodes, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for sample barcodes
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeProfile_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Paths for original and corrupted barcode images
        string originalPath = Path.Combine(tempFolder, "original.png");
        string corruptedPath = Path.Combine(tempFolder, "corrupted.png");

        // Generate a correct Code128 barcode
        GenerateBarcode(originalPath, "1234567890");

        // Create a corrupted version by drawing a line over the original image
        CreateCorruptedImage(originalPath, corruptedPath);

        // Number of scan repetitions for each setting
        const int scanIterations = 20;

        // Profile without AllowIncorrectBarcodes (default false)
        long timeWithout = ProfileScanning(corruptedPath, false, scanIterations);
        // Profile with AllowIncorrectBarcodes set to true
        long timeWith = ProfileScanning(corruptedPath, true, scanIterations);

        Console.WriteLine($"Scanning {scanIterations} times without AllowIncorrectBarcodes: {timeWithout} ms");
        Console.WriteLine($"Scanning {scanIterations} times with AllowIncorrectBarcodes:    {timeWith} ms");

        // Clean up temporary files
        try
        {
            if (File.Exists(originalPath)) File.Delete(originalPath);
            if (File.Exists(corruptedPath)) File.Delete(corruptedPath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect the demo
        }
    }

    // Generates a barcode image at the specified path
    static void GenerateBarcode(string path, string codeText)
    {
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Save directly as PNG
            generator.Save(path, BarCodeImageFormat.Png);
        }
    }

    // Creates a simple corrupted image by drawing a black line over the original
    static void CreateCorruptedImage(string sourcePath, string destPath)
    {
        using (var original = (Bitmap)Image.FromFile(sourcePath))
        {
            using (var graphics = Graphics.FromImage(original))
            {
                using (var pen = new Pen(Color.Black, 5f))
                {
                    graphics.DrawLine(pen, 0, 0, original.Width, original.Height);
                }
            }
            original.Save(destPath, Aspose.Drawing.Imaging.ImageFormat.Png);
        }
    }

    // Scans the given image repeatedly and returns total elapsed milliseconds
    static long ProfileScanning(string imagePath, bool allowIncorrect, int iterations)
    {
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return 0;
        }

        Stopwatch sw = new Stopwatch();
        sw.Start();

        for (int i = 0; i < iterations; i++)
        {
            ScanBarcode(imagePath, allowIncorrect);
        }

        sw.Stop();
        return sw.ElapsedMilliseconds;
    }

    // Performs a single barcode read with the specified AllowIncorrectBarcodes setting
    static void ScanBarcode(string imagePath, bool allowIncorrect)
    {
        BaseDecodeType decodeType = DecodeType.Code128;
        using (var reader = new BarCodeReader(imagePath, decodeType))
        {
            // Set the quality setting
            reader.QualitySettings.AllowIncorrectBarcodes = allowIncorrect;

            // Read all barcodes (result may be empty if not recognized)
            BarCodeResult[] results = reader.ReadBarCodes();

            // Optionally process results (here we just iterate to ensure full read)
            foreach (var result in results)
            {
                // Output suppressed to avoid clutter; could log if needed
                // Console.WriteLine($"Detected: {result.CodeText}");
            }
        }
    }
}