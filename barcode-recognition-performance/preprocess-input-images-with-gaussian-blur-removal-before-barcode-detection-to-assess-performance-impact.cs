// Title: Barcode detection performance with Gaussian blur removal
// Description: Demonstrates baseline barcode recognition versus recognition with deconvolution (blur removal) to evaluate performance impact.
// Prompt: Preprocess input images with Gaussian blur removal before barcode detection to assess performance impact.
// Tags: barcode symbology, deconvolution, performance, aspose.barcode, aspose.drawing

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Sample program that generates a Code128 barcode, then measures recognition time
/// with and without deconvolution (Gaussian blur removal) to assess performance impact.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, runs baseline recognition, then runs recognition
    /// with deconvolution enabled, printing results and timing information.
    /// </summary>
    static void Main()
    {
        // Generate a sample Code128 barcode and keep it in memory
        const string codeText = "1234567890";
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set a moderate image size for the generated barcode
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Save the barcode to a memory stream in PNG format
            using (MemoryStream barcodeStream = new MemoryStream())
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0;

                // Load the barcode image from the stream into a Bitmap object
                using (Bitmap barcodeImage = new Bitmap(barcodeStream))
                {
                    // -------------------------------------------------------------
                    // 1. Recognition without any preprocessing (baseline)
                    // -------------------------------------------------------------
                    var stopwatch = Stopwatch.StartNew();

                    // Create a reader for the barcode image using Code128 decoding
                    using (var reader = new BarCodeReader(barcodeImage, DecodeType.Code128))
                    {
                        // Use default quality settings
                        foreach (var result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"[Baseline] Detected: {result.CodeTypeName}, Text: {result.CodeText}");
                        }
                    }

                    stopwatch.Stop();
                    Console.WriteLine($"Baseline recognition time: {stopwatch.ElapsedMilliseconds} ms");

                    // -------------------------------------------------------------
                    // 2. Recognition with deconvolution (Gaussian blur removal)
                    // -------------------------------------------------------------
                    // Create a fresh reader instance for the same image
                    using (var reader = new BarCodeReader(barcodeImage, DecodeType.Code128))
                    {
                        // Enable deconvolution to mitigate blur effects
                        reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;

                        stopwatch.Restart();

                        foreach (var result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"[Deconvolution] Detected: {result.CodeTypeName}, Text: {result.CodeText}");
                        }

                        stopwatch.Stop();
                        Console.WriteLine($"Deconvolution recognition time: {stopwatch.ElapsedMilliseconds} ms");
                    }
                }
            }
        }

        // Note: In a real scenario, you could apply an actual Gaussian blur to the image
        // (e.g., using Aspose.Imaging) and then run the same two recognition steps to
        // compare performance on blurred vs. deblurred images.
    }
}