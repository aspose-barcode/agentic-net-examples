// Title: Compare recognition speed of 1D vs 2D barcodes
// Description: Demonstrates generating a Code128 and QR barcode, then measures and compares their recognition times using identical QualitySettings.
// Prompt: Compare recognition speed of 1D barcodes versus 2D barcodes under identical QualitySettings.
// Tags: barcode symbology, recognition speed, qualitysettings, code128, qr, aspnet.barcode, csharp

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation and recognition speed comparison between 1D (Code128) and 2D (QR) symbologies.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates temporary barcode images, measures recognition times, outputs results, and cleans up files.
    /// </summary>
    static void Main()
    {
        // Define file paths for temporary barcode images
        string code128Path = "code128.png";
        string qrPath = "qr.png";

        // -------------------------------------------------
        // Generate a 1D Code128 barcode and save as PNG
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(code128Path, BarCodeImageFormat.Png);
        }

        // -------------------------------------------------
        // Generate a 2D QR barcode and save as PNG
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            generator.Save(qrPath, BarCodeImageFormat.Png);
        }

        // Prepare identical quality settings for both recognitions
        var quality = QualitySettings.NormalQuality;

        // -------------------------------------------------
        // Measure recognition time for the 1D barcode
        // -------------------------------------------------
        long time1D;
        using (var reader = new BarCodeReader(code128Path, DecodeType.Code128))
        {
            // Apply the same quality settings
            reader.QualitySettings = quality;

            // Start timing
            var sw = Stopwatch.StartNew();

            // Perform recognition
            var results = reader.ReadBarCodes();

            // Stop timing
            sw.Stop();
            time1D = sw.ElapsedMilliseconds;

            // Output detection results
            Console.WriteLine($"1D Code128 detected: {results.Length} barcode(s).");
            foreach (var result in results)
            {
                Console.WriteLine($"  Text: {result.CodeText}");
            }
        }

        // -------------------------------------------------
        // Measure recognition time for the 2D barcode
        // -------------------------------------------------
        long time2D;
        using (var reader = new BarCodeReader(qrPath, DecodeType.QR))
        {
            // Apply the same quality settings
            reader.QualitySettings = quality;

            // Start timing
            var sw = Stopwatch.StartNew();

            // Perform recognition
            var results = reader.ReadBarCodes();

            // Stop timing
            sw.Stop();
            time2D = sw.ElapsedMilliseconds;

            // Output detection results
            Console.WriteLine($"2D QR detected: {results.Length} barcode(s).");
            foreach (var result in results)
            {
                Console.WriteLine($"  Text: {result.CodeText}");
            }
        }

        // -------------------------------------------------
        // Output comparison of recognition times
        // -------------------------------------------------
        Console.WriteLine();
        Console.WriteLine("Recognition time comparison (identical QualitySettings):");
        Console.WriteLine($"  1D (Code128): {time1D} ms");
        Console.WriteLine($"  2D (QR)     : {time2D} ms");

        // -------------------------------------------------
        // Clean up temporary files
        // -------------------------------------------------
        try
        {
            if (File.Exists(code128Path)) File.Delete(code128Path);
            if (File.Exists(qrPath)) File.Delete(qrPath);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }
    }
}