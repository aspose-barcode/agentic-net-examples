// Title: Barcode generation, loading, detection, and decoding timing example
// Description: Demonstrates how to generate a QR barcode, load it, and measure the time taken for each processing stage (loading, preprocessing, detection, decoding) using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode performance profiling category, illustrating the use of BarcodeGenerator, BarCodeReader, and QualitySettings to evaluate processing speed. Developers often need to benchmark barcode operations to optimize applications that generate or read barcodes in high‑throughput scenarios.
// Prompt: Log detailed timing for each processing stage—loading, preprocessing, detection, and decoding—to identify bottlenecks.
// Tags: qr, barcode, generation, recognition, timing, performance, aspose.barcode, barcodegenerator, barcodereader, qualitysettings

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Contains the program that measures barcode processing times.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point for the barcode timing demonstration.
    /// </summary>
    static void Main()
    {
        // Prepare temporary directory and file paths
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeTiming_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "sample.png");

        // Stage 1: Generate a sample QR barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Aspose Timing Test"))
        {
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Stage 2: Loading – read the image file into a byte array
        Stopwatch swLoad = Stopwatch.StartNew();
        byte[] imageBytes;
        using (var fs = new FileStream(barcodePath, FileMode.Open, FileAccess.Read))
        using (var ms = new MemoryStream())
        {
            fs.CopyTo(ms);
            imageBytes = ms.ToArray();
        }
        swLoad.Stop();
        Console.WriteLine($"Loading time: {swLoad.ElapsedMilliseconds} ms");

        // Stage 3: Preprocessing – create a reader, configure quality settings, and load the image
        Stopwatch swPre = Stopwatch.StartNew();
        BaseDecodeType decodeType = DecodeType.AllSupportedTypes;
        using (var reader = new BarCodeReader(new MemoryStream(imageBytes), decodeType))
        {
            // Apply high‑performance quality settings to speed up detection
            reader.QualitySettings = QualitySettings.HighPerformance;
            reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 2f;
            swPre.Stop();
            Console.WriteLine($"Preprocessing time: {swPre.ElapsedMilliseconds} ms");

            // Stage 4: Detection – read all barcodes from the image
            Stopwatch swDetect = Stopwatch.StartNew();
            BarCodeResult[] results = reader.ReadBarCodes();
            swDetect.Stop();
            Console.WriteLine($"Detection time: {swDetect.ElapsedMilliseconds} ms");

            // Stage 5: Decoding – extract and display data from each detection result
            Stopwatch swDecode = Stopwatch.StartNew();
            foreach (var result in results)
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");
                Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                var bounds = result.Region.Rectangle;
                Console.WriteLine($"Region - X:{bounds.X} Y:{bounds.Y} W:{bounds.Width} H:{bounds.Height} Angle:{result.Region.Angle}");
            }
            swDecode.Stop();
            Console.WriteLine($"Decoding time: {swDecode.ElapsedMilliseconds} ms");
        }

        // Cleanup temporary files and directory
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Suppress any cleanup exceptions
        }
    }
}