// Title: Impact of AllowIncorrectBarcodes on CPU Load During Repeated Scanning
// Description: Demonstrates how toggling the AllowIncorrectBarcodes setting affects processing time when reading both valid and corrupted QR codes.
// Category-Description: This example belongs to the Aspose.BarCode scanning and quality settings category. It showcases the BarCodeReader class with its QualitySettings, illustrating typical use cases such as performance profiling and error tolerance configuration for developers working with continuous barcode scanning.
// Prompt: Profile the impact of AllowIncorrectBarcodes on overall CPU load during continuous scanning.
// Tags: qr, barcode, scanning, performance, allowincorrectbarcodes, qualitysettings, aspose.barcode, c#

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates performance measurement of the <c>AllowIncorrectBarcodes</c> quality setting
/// while repeatedly scanning correct and corrupted QR code images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates test images, measures read times with different
    /// <c>AllowIncorrectBarcodes</c> settings, and outputs the results.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary working folder for generated images
        // --------------------------------------------------------------------
        string workFolder = Path.Combine(Path.GetTempPath(), "AllowIncorrectBarcodesDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Paths for the correct and corrupted QR code images
        string correctPath = Path.Combine(workFolder, "correct.png");
        string corruptedPath = Path.Combine(workFolder, "corrupted.png");

        // --------------------------------------------------------------------
        // Generate a correct QR code image
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            generator.Save(correctPath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Corrupt the QR code by drawing a black line across the image
        // --------------------------------------------------------------------
        File.Copy(correctPath, corruptedPath, true);
        using (var bitmap = new Bitmap(corruptedPath))
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                using (var pen = new Pen(Color.Black, 5f))
                {
                    graphics.DrawLine(pen, 0, 0, bitmap.Width, bitmap.Height);
                }
            }
            bitmap.Save(corruptedPath, ImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Verify that both images were created successfully
        // --------------------------------------------------------------------
        if (!File.Exists(correctPath) || !File.Exists(corruptedPath))
        {
            Console.WriteLine("Failed to create test images.");
            return;
        }

        // --------------------------------------------------------------------
        // Measure reading time with AllowIncorrectBarcodes set to false and true
        // --------------------------------------------------------------------
        const int iterations = 20;
        long elapsedFalse = MeasureReading(correctPath, corruptedPath, false, iterations);
        long elapsedTrue = MeasureReading(correctPath, corruptedPath, true, iterations);

        // Output the measured times
        Console.WriteLine($"AllowIncorrectBarcodes = false, total time: {elapsedFalse} ms");
        Console.WriteLine($"AllowIncorrectBarcodes = true,  total time: {elapsedTrue} ms");

        // --------------------------------------------------------------------
        // Clean up temporary files and folder
        // --------------------------------------------------------------------
        try { Directory.Delete(workFolder, true); } catch { }
    }

    /// <summary>
    /// Measures the total time required to read both the correct and corrupted QR code images
    /// for a given number of iterations, using the specified <c>AllowIncorrectBarcodes</c> setting.
    /// </summary>
    /// <param name="correctPath">Path to the valid QR code image.</param>
    /// <param name="corruptedPath">Path to the corrupted QR code image.</param>
    /// <param name="allowIncorrect">Value to assign to <c>QualitySettings.AllowIncorrectBarcodes</c>.</param>
    /// <param name="iterations">Number of read cycles to perform.</param>
    /// <returns>Total elapsed time in milliseconds.</returns>
    static long MeasureReading(string correctPath, string corruptedPath, bool allowIncorrect, int iterations)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        for (int i = 0; i < iterations; i++)
        {
            // ----------------------------------------------------------------
            // Read the correct image
            // ----------------------------------------------------------------
            using (var reader = new BarCodeReader(correctPath, DecodeType.QR))
            {
                reader.QualitySettings.AllowIncorrectBarcodes = allowIncorrect;
                reader.ReadBarCodes();
            }

            // ----------------------------------------------------------------
            // Read the corrupted image
            // ----------------------------------------------------------------
            using (var reader = new BarCodeReader(corruptedPath, DecodeType.QR))
            {
                reader.QualitySettings.AllowIncorrectBarcodes = allowIncorrect;
                reader.ReadBarCodes();
            }
        }

        sw.Stop();
        return sw.ElapsedMilliseconds;
    }
}