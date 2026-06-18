using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates benchmarking of Code39 barcode generation with checksum enabled and disabled using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates sample Code39 barcodes and measures the time taken with checksum enabled and disabled.
    /// </summary>
    static void Main()
    {
        // Define sample texts to encode as Code39 barcodes
        string[] samples = new string[]
        {
            "ABC123",
            "CODE39",
            "1234567890",
            "HELLO",
            "TEST"
        };

        // Use the full ASCII Code39 symbology for encoding
        BaseEncodeType encodeType = EncodeTypes.Code39FullASCII;

        // Benchmark generation with checksum enabled
        Stopwatch swEnabled = Stopwatch.StartNew();
        foreach (string text in samples)
        {
            // Create a barcode generator for the current text
            using (var generator = new BarcodeGenerator(encodeType, text))
            {
                // Enable checksum calculation
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

                // Save the generated barcode to a memory stream (PNG format)
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                }
            }
        }
        swEnabled.Stop();

        // Benchmark generation with checksum disabled
        Stopwatch swDisabled = Stopwatch.StartNew();
        foreach (string text in samples)
        {
            // Create a barcode generator for the current text
            using (var generator = new BarcodeGenerator(encodeType, text))
            {
                // Disable checksum calculation
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;

                // Save the generated barcode to a memory stream (PNG format)
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                }
            }
        }
        swDisabled.Stop();

        // Output the elapsed time for both scenarios
        Console.WriteLine($"Code39 generation with checksum ENABLED:  {swEnabled.Elapsed.TotalMilliseconds} ms");
        Console.WriteLine($"Code39 generation with checksum DISABLED: {swDisabled.Elapsed.TotalMilliseconds} ms");
    }
}