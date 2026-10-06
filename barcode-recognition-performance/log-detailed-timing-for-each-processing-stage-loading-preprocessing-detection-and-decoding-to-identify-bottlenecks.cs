// Title: Barcode processing timing demo with Aspose.BarCode
// Description: Demonstrates generating a QR code, loading it, configuring the reader, and measuring the time taken for each processing stage.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader with QualitySettings for fast, high‑performance detection. Typical use cases include performance profiling of barcode workflows, benchmarking different quality settings, and identifying bottlenecks in loading, preprocessing, detection, and decoding stages. Developers often need to log detailed timing to optimize barcode processing pipelines.
// Prompt: Log detailed timing for each processing stage—loading, preprocessing, detection, and decoding—to identify bottlenecks.
// Tags: qr, barcode, timing, performance, generation, recognition, aspose.barcode

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation, loading, preprocessing, detection, and decoding while logging detailed timing for each stage.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates a QR barcode, measures loading, preprocessing, detection, and decoding times, and outputs results.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary folder and define the path for the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeTimingDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "sample.png");

        // Generate a sample QR barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // -------------------------------------------------
        // Stage 1: Loading the image into memory
        // -------------------------------------------------
        Stopwatch swLoad = Stopwatch.StartNew();
        byte[] imageData;
        using (FileStream fs = File.OpenRead(imagePath))
        {
            imageData = new byte[fs.Length];
            fs.Read(imageData, 0, imageData.Length);
        }
        MemoryStream imageStream = new MemoryStream(imageData);
        swLoad.Stop();
        Console.WriteLine($"Loading time: {swLoad.ElapsedMilliseconds} ms");

        // -------------------------------------------------
        // Stage 2: Preprocessing – configure the barcode reader
        // -------------------------------------------------
        Stopwatch swPrep = Stopwatch.StartNew();
        using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
        {
            // Apply high‑performance quality settings to speed up detection
            reader.QualitySettings = QualitySettings.HighPerformance;
            reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 1f;
            swPrep.Stop();
            Console.WriteLine($"Preprocessing time: {swPrep.ElapsedMilliseconds} ms");

            // -------------------------------------------------
            // Stage 3: Detection & Decoding
            // -------------------------------------------------
            Stopwatch swDetect = Stopwatch.StartNew();
            BarCodeResult[] results = reader.ReadBarCodes();
            swDetect.Stop();
            Console.WriteLine($"Detection/Decoding time: {swDetect.ElapsedMilliseconds} ms");

            // Output detection results
            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes detected.");
            }
            else
            {
                foreach (var result in results)
                {
                    Console.WriteLine($"Code Text: {result.CodeText}");
                    Console.WriteLine($"Symbology: {result.CodeTypeName}");
                    Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                    var rect = result.Region.Rectangle;
                    Console.WriteLine($"Region - X:{rect.X}, Y:{rect.Y}, Width:{rect.Width}, Height:{rect.Height}");
                    Console.WriteLine($"Orientation Angle: {result.Region.Angle}");
                    Console.WriteLine(new string('-', 40));
                }
            }
        }

        // -------------------------------------------------
        // Cleanup temporary files and resources
        // -------------------------------------------------
        try
        {
            imageStream.Dispose();
            File.Delete(imagePath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored – cleanup failures should not affect program outcome
        }
    }
}