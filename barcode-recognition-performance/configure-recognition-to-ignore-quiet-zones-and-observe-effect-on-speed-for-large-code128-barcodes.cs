using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a large Code128 barcode and measuring
/// recognition performance using default and optimized settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, reads it twice with different settings,
    /// and outputs detection results and timing information.
    /// </summary>
    static void Main()
    {
        // Create a 200‑character string consisting of 'A' for the barcode data.
        string longCode = new string('A', 200);

        // Initialize the barcode generator for Code128 with the long data string.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, longCode))
        {
            // Remove all padding to keep the generated image as small as possible.
            generator.Parameters.Barcode.Padding.Left.Point   = 0f;
            generator.Parameters.Barcode.Padding.Top.Point    = 0f;
            generator.Parameters.Barcode.Padding.Right.Point  = 0f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 0f;

            // Save the generated barcode image to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.

                // ------------------------------------------------------------
                // First read: use default reader settings.
                // ------------------------------------------------------------
                double defaultTime;
                using (var reader = new BarCodeReader(ms, DecodeType.Code128))
                {
                    var sw = Stopwatch.StartNew(); // Start timing.

                    // Iterate through all detected barcodes and output their text.
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"[Default] Detected: {result.CodeText}");
                    }

                    sw.Stop(); // Stop timing.
                    defaultTime = sw.Elapsed.TotalMilliseconds;
                }

                // Reset stream position before the second read.
                ms.Position = 0;

                // ------------------------------------------------------------
                // Second read: apply settings intended to speed up recognition.
                // ------------------------------------------------------------
                double optimizedTime;
                using (var reader = new BarCodeReader(ms, DecodeType.Code128))
                {
                    // Use fast deconvolution and allow incorrect barcodes to improve speed.
                    reader.QualitySettings.Deconvolution          = DeconvolutionMode.Fast;
                    reader.QualitySettings.AllowIncorrectBarcodes = true;

                    var sw = Stopwatch.StartNew(); // Start timing.

                    // Iterate through all detected barcodes and output their text.
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"[Optimized] Detected: {result.CodeText}");
                    }

                    sw.Stop(); // Stop timing.
                    optimizedTime = sw.Elapsed.TotalMilliseconds;
                }

                // Output the elapsed times for both reading approaches.
                Console.WriteLine($"Default recognition time: {defaultTime} ms");
                Console.WriteLine($"Optimized recognition time: {optimizedTime} ms");
            }
        }
    }
}