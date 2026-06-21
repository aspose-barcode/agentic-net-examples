using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generation and recognition of 1D (Code128) and 2D (QR) barcodes,
/// measuring and comparing their recognition times.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates temporary barcode images, reads them back, measures recognition time,
    /// outputs results, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Define temporary file paths for the generated barcode images.
        // --------------------------------------------------------------------
        string code128Path = Path.Combine(Path.GetTempPath(), "code128.png");
        string qrPath = Path.Combine(Path.GetTempPath(), "qr.png");

        // --------------------------------------------------------------------
        // Generate a 1D barcode (Code128) and save it to the temporary path.
        // --------------------------------------------------------------------
        using (var generator1D = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator1D.Save(code128Path);
        }

        // --------------------------------------------------------------------
        // Generate a 2D barcode (QR) and save it to the temporary path.
        // --------------------------------------------------------------------
        using (var generator2D = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            generator2D.Save(qrPath);
        }

        // --------------------------------------------------------------------
        // Prepare a common QualitySettings instance for both readers.
        // --------------------------------------------------------------------
        var quality = QualitySettings.NormalQuality;

        // --------------------------------------------------------------------
        // Measure recognition time for the 1D barcode.
        // --------------------------------------------------------------------
        double time1D;
        using (var reader1D = new BarCodeReader(code128Path, DecodeType.Code128))
        {
            // Apply the same quality settings.
            reader1D.QualitySettings = quality;

            // Start timing.
            var sw = Stopwatch.StartNew();

            // Read all barcodes in the image.
            foreach (var result in reader1D.ReadBarCodes())
            {
                // Output detection details to ensure processing occurs.
                Console.WriteLine($"1D Detected: {result.CodeTypeName} - {result.CodeText}");
            }

            // Stop timing and record elapsed milliseconds.
            sw.Stop();
            time1D = sw.Elapsed.TotalMilliseconds;
        }

        // --------------------------------------------------------------------
        // Measure recognition time for the 2D barcode.
        // --------------------------------------------------------------------
        double time2D;
        using (var reader2D = new BarCodeReader(qrPath, DecodeType.QR))
        {
            // Apply the same quality settings.
            reader2D.QualitySettings = quality;

            // Start timing.
            var sw = Stopwatch.StartNew();

            // Read all barcodes in the image.
            foreach (var result in reader2D.ReadBarCodes())
            {
                // Output detection details to ensure processing occurs.
                Console.WriteLine($"2D Detected: {result.CodeTypeName} - {result.CodeText}");
            }

            // Stop timing and record elapsed milliseconds.
            sw.Stop();
            time2D = sw.Elapsed.TotalMilliseconds;
        }

        // --------------------------------------------------------------------
        // Output a comparison of the recognition times.
        // --------------------------------------------------------------------
        Console.WriteLine();
        Console.WriteLine($"Recognition time (1D Code128): {time1D:F2} ms");
        Console.WriteLine($"Recognition time (2D QR): {time2D:F2} ms");
        Console.WriteLine(time1D < time2D
            ? "1D barcode recognized faster."
            : "2D barcode recognized faster.");

        // --------------------------------------------------------------------
        // Clean up temporary files, ignoring any errors.
        // --------------------------------------------------------------------
        try { if (File.Exists(code128Path)) File.Delete(code128Path); } catch { }
        try { if (File.Exists(qrPath)) File.Delete(qrPath); } catch { }
    }
}