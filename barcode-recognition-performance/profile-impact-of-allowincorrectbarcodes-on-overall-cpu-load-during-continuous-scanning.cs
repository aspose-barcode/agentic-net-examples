// Title: Profiling CPU impact of AllowIncorrectBarcodes during barcode scanning
// Description: Demonstrates how toggling AllowIncorrectBarcodes affects processing time when repeatedly scanning a Code128 barcode image.
// Category-Description: This example belongs to the Aspose.BarCode scanning performance category, illustrating the use of BarCodeReader, QualitySettings, and DecodeType classes. Developers often need to benchmark barcode recognition settings to optimize CPU usage in high‑throughput applications such as inventory systems or point‑of‑sale terminals.
// Prompt: Profile the impact of AllowIncorrectBarcodes on overall CPU load during continuous scanning.
// Tags: barcode, scanning, performance, allowincorrectbarcodes, code128, aspose.barcode, csharp

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates profiling the CPU impact of the AllowIncorrectBarcodes setting during continuous barcode scanning.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a sample barcode if needed, runs scans with both settings, and outputs timing comparison.
    /// </summary>
    static void Main()
    {
        // Path for the sample barcode image
        const string imagePath = "sample_barcode.png";

        // Generate a sample barcode image if it does not exist
        if (!File.Exists(imagePath))
        {
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }
        }

        // Number of scans per configuration (small fixed number for CI safety)
        const int scanCount = 5;

        // Measure with AllowIncorrectBarcodes = false
        TimeSpan timeWithoutAllowIncorrect = ScanAndMeasure(imagePath, false, scanCount);
        Console.WriteLine($"AllowIncorrectBarcodes = false : Total time for {scanCount} scans = {timeWithoutAllowIncorrect.TotalMilliseconds} ms");

        // Measure with AllowIncorrectBarcodes = true
        TimeSpan timeWithAllowIncorrect = ScanAndMeasure(imagePath, true, scanCount);
        Console.WriteLine($"AllowIncorrectBarcodes = true  : Total time for {scanCount} scans = {timeWithAllowIncorrect.TotalMilliseconds} ms");

        // Simple comparison output
        double percentChange = (timeWithAllowIncorrect.TotalMilliseconds - timeWithoutAllowIncorrect.TotalMilliseconds) /
                               timeWithoutAllowIncorrect.TotalMilliseconds * 100.0;
        Console.WriteLine($"CPU load impact (approximate): {percentChange:F2}% change when AllowIncorrectBarcodes is enabled.");
    }

    // Performs a number of scans on the given image with the specified AllowIncorrectBarcodes setting
    static TimeSpan ScanAndMeasure(string imagePath, bool allowIncorrect, int iterations)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        for (int i = 0; i < iterations; i++)
        {
            // Create a new reader for each scan to simulate independent processing
            using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code128))
            {
                // Apply the quality setting
                reader.QualitySettings.AllowIncorrectBarcodes = allowIncorrect;

                // Perform the recognition (results are ignored for this profiling)
                reader.ReadBarCodes();
            }
        }

        sw.Stop();
        return sw.Elapsed;
    }
}