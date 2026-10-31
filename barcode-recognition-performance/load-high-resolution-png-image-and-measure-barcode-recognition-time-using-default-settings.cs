// Title: Measure barcode recognition time for a high‑resolution PNG image
// Description: Generates a high‑resolution QR code PNG and measures the time Aspose.BarCode needs to recognize it using default settings.
// Category-Description: Demonstrates combined use of Aspose.BarCode generation and recognition APIs. The example showcases BarcodeGenerator for creating barcodes (QR, Code128, etc.) and BarCodeReader for decoding them. Typical scenarios include performance testing, batch processing, and validation of barcode quality in high‑DPI images. Developers often need to benchmark recognition speed or verify that generated barcodes are readable across different resolutions.
// Prompt: Load a high‑resolution PNG image and measure barcode recognition time using default settings.
// Tags: qr code, barcode generation, barcode recognition, performance measurement, png, high resolution, aspose.barcode, c#

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how to generate a high‑resolution QR code image, then measure the time required
/// to recognize the barcode using Aspose.BarCode's default settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary PNG, runs recognition, reports timing,
    /// and cleans up temporary resources.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Prepare a temporary folder to store the generated barcode image.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "highres.png");

        // ---------------------------------------------------------------
        // 2. Generate a high‑resolution QR code and save it as a PNG file.
        // ---------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            generator.Parameters.Resolution = 300; // Set DPI to 300 for high quality.
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // -------------------------------------------------
        // 3. Verify that the image file was successfully created.
        // -------------------------------------------------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // -------------------------------------------------
        // 4. Measure barcode recognition time using default settings.
        // -------------------------------------------------
        Stopwatch watch = Stopwatch.StartNew();
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            var results = reader.ReadBarCodes();
            watch.Stop();

            Console.WriteLine($"Recognition time: {watch.ElapsedMilliseconds} ms");
            Console.WriteLine($"Barcodes found: {results.Length}");
            foreach (var result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // -------------------------------------------------
        // 5. Clean up temporary files and folder.
        // -------------------------------------------------
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failures should not affect program outcome.
        }
    }
}