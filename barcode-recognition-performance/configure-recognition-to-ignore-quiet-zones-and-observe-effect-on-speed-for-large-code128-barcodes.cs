// Title: Code128 Barcode Recognition Speed with Quiet Zone Variations
// Description: Demonstrates how disabling quiet zones affects recognition speed for large Code128 barcodes.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating Code128 barcodes, BarCodeReader for decoding them, and QualitySettings to control recognition performance. Developers often need to tune quiet zone padding and quality settings to balance accuracy and speed in high‑volume scanning scenarios.
// Prompt: Configure recognition to ignore quiet zones and observe effect on speed for large Code128 barcodes.
// Tags: code128, quiet zone, recognition speed, performance, generation, Aspose.BarCode, barcode, qualitysettings

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates two large Code128 barcodes—one with the default quiet zone and one without—and
/// measures the recognition time using different quality settings to illustrate the impact of quiet zones on performance.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates barcode images, runs recognition benchmarks, and prints the results.
    /// </summary>
    static void Main()
    {
        // Prepare a long Code128 text (200 characters) to simulate a large barcode.
        string longText = new string('A', 200);

        // Define file names for the generated images.
        string imageWithQuietZone = "code128_with_quietzone.png";
        string imageNoQuietZone = "code128_no_quietzone.png";

        // --------------------------------------------------------------------
        // Generate a barcode with the default quiet zone (non‑zero padding).
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, longText))
        {
            // The default padding is retained; no changes required.
            generator.Save(imageWithQuietZone, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Generate a barcode with all padding set to zero (effectively no quiet zone).
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, longText))
        {
            generator.Parameters.Barcode.Padding.Left.Point = 0f;
            generator.Parameters.Barcode.Padding.Top.Point = 0f;
            generator.Parameters.Barcode.Padding.Right.Point = 0f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 0f;
            generator.Save(imageNoQuietZone, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Local function that measures the time required to read all barcodes
        // from an image using the specified quality settings.
        // --------------------------------------------------------------------
        double MeasureRecognition(string imagePath, QualitySettings settings)
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                return -1;
            }

            using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
            {
                // Apply the requested quality configuration.
                reader.QualitySettings = settings;

                var stopwatch = Stopwatch.StartNew();

                // Iterate through all detected barcodes to ensure full processing.
                foreach (var result in reader.ReadBarCodes())
                {
                    // Output the decoded text (optional, but forces full decode).
                    Console.WriteLine($"Detected: {result.CodeText}");
                }

                stopwatch.Stop();
                return stopwatch.Elapsed.TotalMilliseconds;
            }
        }

        // --------------------------------------------------------------------
        // Run recognition benchmarks with different quality settings and quiet zone configurations.
        // --------------------------------------------------------------------
        double timeWithQuietZone = MeasureRecognition(imageWithQuietZone, QualitySettings.NormalQuality);
        double timeWithQuietZoneFast = MeasureRecognition(imageWithQuietZone, QualitySettings.HighPerformance);
        double timeNoQuietZone = MeasureRecognition(imageNoQuietZone, QualitySettings.NormalQuality);
        double timeNoQuietZoneFast = MeasureRecognition(imageNoQuietZone, QualitySettings.HighPerformance);

        // --------------------------------------------------------------------
        // Display the timing results.
        // --------------------------------------------------------------------
        Console.WriteLine();
        Console.WriteLine("Recognition timing (ms):");
        Console.WriteLine($"With quiet zone (NormalQuality): {timeWithQuietZone}");
        Console.WriteLine($"With quiet zone (HighPerformance): {timeWithQuietZoneFast}");
        Console.WriteLine($"Without quiet zone (NormalQuality): {timeNoQuietZone}");
        Console.WriteLine($"Without quiet zone (HighPerformance): {timeNoQuietZoneFast}");
    }
}