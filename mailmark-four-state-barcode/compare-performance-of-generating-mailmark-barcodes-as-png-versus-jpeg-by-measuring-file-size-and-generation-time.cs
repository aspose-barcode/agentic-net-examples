// Title: Performance comparison of Mailmark barcode generation in PNG vs JPEG
// Description: Demonstrates generating a Mailmark 2‑D barcode as PNG and JPEG, measuring generation time and resulting file size.
// Category-Description: Shows Aspose.BarCode barcode generation using ComplexBarcodeGenerator for Mailmark 2D symbology. Typical use cases include creating postal barcodes and evaluating image format trade‑offs. Developers often need to benchmark generation speed and output size when choosing PNG or JPEG formats.
// Prompt: Compare performance of generating Mailmark barcodes as PNG versus JPEG by measuring file size and generation time.
// Tags: mailmark, barcode, performance, png, jpeg, generation, aspose.barcode, complexbarcodegenerator

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that generates a Mailmark 2‑D barcode in PNG and JPEG formats,
/// then reports the time taken and file size for each format.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Prepares the Mailmark codetext, generates PNG and JPEG images,
    /// and writes the performance results to the console.
    /// </summary>
    static void Main()
    {
        // Prepare Mailmark 2D codetext with sample data
        var mailmark = new Mailmark2DCodetext
        {
            UPUCountryID = "JGB ",
            InformationTypeID = "0",
            VersionID = "1",
            Class = "1",
            SupplyChainID = 384224,
            ItemID = 16563762,
            DestinationPostCodeAndDPS = "EF61AH8T ",
            DataMatrixType = Mailmark2DType.Type_7,
            CustomerContent = "CUSTOM"
        };

        // Generate PNG image and capture performance data
        var pngPath = Path.Combine(Path.GetTempPath(), "Mailmark2D.png");
        var pngResult = GenerateBarcode(mailmark, pngPath, BarCodeImageFormat.Png);

        // Generate JPEG image and capture performance data
        var jpegPath = Path.Combine(Path.GetTempPath(), "Mailmark2D.jpg");
        var jpegResult = GenerateBarcode(mailmark, jpegPath, BarCodeImageFormat.Jpeg);

        // Output the measured time (ms) and file size (bytes) for each format
        Console.WriteLine($"PNG: Time = {pngResult.TimeMs} ms, Size = {pngResult.FileSize} bytes");
        Console.WriteLine($"JPEG: Time = {jpegResult.TimeMs} ms, Size = {jpegResult.FileSize} bytes");
    }

    /// <summary>
    /// Generates a barcode image using the specified codetext, output path, and image format.
    /// Returns the elapsed time in milliseconds and the resulting file size in bytes.
    /// </summary>
    /// <param name="codetext">Mailmark 2D codetext to encode.</param>
    /// <param name="outputPath">Full file path where the image will be saved.</param>
    /// <param name="format">Desired image format (PNG or JPEG).</param>
    /// <returns>Tuple containing generation time (ms) and file size (bytes).</returns>
    private static (long TimeMs, long FileSize) GenerateBarcode(Mailmark2DCodetext codetext, string outputPath, BarCodeImageFormat format)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        // Use ComplexBarcodeGenerator to create the barcode image
        using (var generator = new ComplexBarcodeGenerator(codetext))
        {
            // Set X-dimension (module size) to 4 pixels for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Save(outputPath, format);
        }

        stopwatch.Stop();

        // Determine the size of the generated file, if it exists
        long size = 0;
        if (File.Exists(outputPath))
        {
            size = new FileInfo(outputPath).Length;
        }

        return (stopwatch.ElapsedMilliseconds, size);
    }
}