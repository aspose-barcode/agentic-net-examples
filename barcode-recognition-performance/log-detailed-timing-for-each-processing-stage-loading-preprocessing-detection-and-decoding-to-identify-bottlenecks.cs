// Title: Barcode processing timing example with Aspose.BarCode
// Description: Demonstrates generating a Code128 barcode, loading it, configuring preprocessing, detecting and decoding while logging elapsed time for each stage.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for recognizing them, and QualitySettings for fine‑tuning preprocessing. Typical use cases include performance profiling of barcode workflows, automated quality checks, and bottleneck identification in high‑throughput scanning applications. Developers often need to measure loading, preprocessing, detection, and decoding times to optimize their solutions.
// Prompt: Log detailed timing for each processing stage—loading, preprocessing, detection, and decoding—to identify bottlenecks.
// Tags: barcode, code128, timing, performance, generation, recognition, aspose.barcode, qualitysettings

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation, loading, preprocessing, detection, and decoding while measuring execution time for each stage.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode image, measures processing times, and outputs detection results.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for the sample image
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeTiming_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "sample.png");

        // Stage 0: Generate a sample Code128 barcode image
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Stage 1: Loading the image from disk
        Stopwatch swLoad = Stopwatch.StartNew();
        using (Bitmap bitmap = new Bitmap(imagePath))
        {
            swLoad.Stop();
            Console.WriteLine($"Loading time: {swLoad.ElapsedMilliseconds} ms");

            // Stage 2: Preprocessing – configure reader and quality settings
            Stopwatch swPreprocess = Stopwatch.StartNew();
            BaseDecodeType decodeType = DecodeType.AllSupportedTypes;
            using (BarCodeReader reader = new BarCodeReader(bitmap, decodeType))
            {
                // Apply a high‑performance preset and fast deconvolution
                reader.QualitySettings = QualitySettings.HighPerformance;
                reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;

                // Use minimal XDimension mode as an example of preprocessing
                reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                reader.QualitySettings.MinimalXDimension = 2f;
                swPreprocess.Stop();
                Console.WriteLine($"Preprocessing time: {swPreprocess.ElapsedMilliseconds} ms");

                // Stage 3: Detection & Decoding
                Stopwatch swDetectDecode = Stopwatch.StartNew();
                BarCodeResult[] results = null;
                try
                {
                    results = reader.ReadBarCodes();
                }
                catch (RecognitionAbortedException ex)
                {
                    Console.WriteLine($"Recognition aborted: {ex.Message}");
                }
                swDetectDecode.Stop();
                Console.WriteLine($"Detection+Decoding time: {swDetectDecode.ElapsedMilliseconds} ms");

                // Output detection results
                if (results != null && results.Length > 0)
                {
                    foreach (var result in results)
                    {
                        Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                        Console.WriteLine($"Decoded Text: {result.CodeText}");
                        var rect = result.Region.Rectangle;
                        Console.WriteLine($"Region - X:{rect.X}, Y:{rect.Y}, W:{rect.Width}, H:{rect.Height}");
                        Console.WriteLine($"Orientation Angle: {result.Region.Angle}");
                    }
                }
                else
                {
                    Console.WriteLine("No barcode detected.");
                }
            }
        }

        // Clean up temporary files
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failure should not affect program outcome
        }
    }
}