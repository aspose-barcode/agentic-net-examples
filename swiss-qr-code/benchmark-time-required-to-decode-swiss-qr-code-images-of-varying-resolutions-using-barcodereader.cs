// Title: Benchmark decoding time for Swiss QR Code images at various resolutions
// Description: Demonstrates how to generate Swiss QR Code barcodes of different sizes and measure the time required to decode them using Aspose.BarCode's BarCodeReader.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on complex barcode types such as Swiss QR Code. It showcases the use of ComplexBarcodeGenerator for creating QR bills and BarCodeReader for decoding, a common task for developers building payment processing or QR‑code scanning solutions. The snippet helps compare performance across image resolutions.
// Prompt: Benchmark the time required to decode Swiss QR Code images of varying resolutions using BarCodeReader.
// Tags: swiss qr code, barcode generation, barcode decoding, performance benchmark, aspnet.barcode, complexbarcodegenerator, barcodereader

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates Swiss QR Code images at different resolutions and benchmarks the decoding time using BarCodeReader.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Iterates over predefined resolutions, creates a Swiss QR Code for each,
    /// decodes it, and prints the elapsed time.
    /// </summary>
    static void Main()
    {
        // Define a set of image resolutions to test (width x height in points)
        var resolutions = new (int width, int height)[]
        {
            (100, 100),
            (200, 200),
            (400, 400)
        };

        // Process each resolution
        foreach (var res in resolutions)
        {
            // Generate a Swiss QR Code image at the specified resolution
            byte[] imageData = GenerateSwissQrImage(res.width, res.height);

            // Decode the image and measure the time taken
            double elapsedMs = DecodeImageAndMeasure(imageData);

            // Output the benchmark result
            Console.WriteLine($"Resolution: {res.width}x{res.height} points - Decode time: {elapsedMs:F2} ms");
        }
    }

    /// <summary>
    /// Generates a Swiss QR Code image with the given width and height (points) and returns the PNG bytes.
    /// </summary>
    /// <param name="width">Image width in points.</param>
    /// <param name="height">Image height in points.</param>
    /// <returns>Byte array containing the PNG image.</returns>
    static byte[] GenerateSwissQrImage(int width, int height)
    {
        // Prepare Swiss QR Code codetext with required fields
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Create the generator for the complex barcode
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Set image size in points
            generator.Parameters.ImageWidth.Point = (float)width;
            generator.Parameters.ImageHeight.Point = (float)height;

            // Save to a memory stream in PNG format and return the byte array
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                return ms.ToArray();
            }
        }
    }

    /// <summary>
    /// Decodes the provided image bytes and returns the elapsed time in milliseconds.
    /// </summary>
    /// <param name="imageBytes">Byte array containing the barcode image.</param>
    /// <returns>Decoding duration in milliseconds.</returns>
    static double DecodeImageAndMeasure(byte[] imageBytes)
    {
        using (var ms = new MemoryStream(imageBytes))
        {
            // Initialize the reader for all supported barcode types
            using (var reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
            {
                // Start timing
                var stopwatch = Stopwatch.StartNew();

                // Perform the decoding operation
                var results = reader.ReadBarCodes();

                // Stop timing
                stopwatch.Stop();

                // Optionally output decoded text (if any)
                foreach (var result in results)
                {
                    Console.WriteLine($"Decoded Text: {result.CodeText}");
                }

                // Return elapsed time in milliseconds
                return stopwatch.Elapsed.TotalMilliseconds;
            }
        }
    }
}