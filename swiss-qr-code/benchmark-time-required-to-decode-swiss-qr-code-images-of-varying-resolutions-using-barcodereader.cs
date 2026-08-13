// Title: Benchmark decoding Swiss QR Code images at multiple DPI levels
// Description: Demonstrates generating Swiss QR Code barcodes at different resolutions and measuring the time required to decode them using Aspose.BarCodeReader.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to create Swiss QR (QR‑Bill) barcodes with the ComplexBarcodeGenerator, adjust image resolution, and use BarCodeReader with DecodeType.QR to read the code. Developers often need to benchmark performance across DPI settings for high‑resolution scanning scenarios, such as payment processing or document verification.
// Prompt: Benchmark the time required to decode Swiss QR Code images of varying resolutions using BarCodeReader.
// Tags: swiss qr, qr code, barcode generation, barcode recognition, performance benchmark, decode, aspose.barcode, complexbarcodegenerator, barcodereader

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Generates Swiss QR Code images at various DPI settings and benchmarks the decoding time using BarCodeReader.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates barcode images, then measures and reports decoding performance.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Prepare output directory for generated barcode images
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "SwissQRImages");
        Directory.CreateDirectory(outputDir);

        // Define the DPI resolutions to test
        int[] resolutions = new int[] { 72, 150, 300 };

        // --------------------------------------------------------------------
        // Generate Swiss QR Code images for each DPI setting
        // --------------------------------------------------------------------
        foreach (int dpi in resolutions)
        {
            string filePath = Path.Combine(outputDir, $"SwissQR_{dpi}dpi.png");
            GenerateSwissQRImage(filePath, dpi);
        }

        // --------------------------------------------------------------------
        // Benchmark decoding each generated image
        // --------------------------------------------------------------------
        foreach (int dpi in resolutions)
        {
            string filePath = Path.Combine(outputDir, $"SwissQR_{dpi}dpi.png");

            // Verify that the image file exists before attempting to read it
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Start timing the decode operation
            var stopwatch = Stopwatch.StartNew();

            // Use BarCodeReader to decode the QR code from the image file
            using (var reader = new BarCodeReader(filePath, DecodeType.QR))
            {
                var results = reader.ReadBarCodes();

                // Output each decoded result
                foreach (var result in results)
                {
                    Console.WriteLine($"Decoded ({dpi} DPI): {result.CodeText}");
                }
            }

            // Stop timing and report elapsed milliseconds
            stopwatch.Stop();
            Console.WriteLine($"Decoding time for {dpi} DPI: {stopwatch.ElapsedMilliseconds} ms");
        }
    }

    /// <summary>
    /// Generates a Swiss QR Code image with the specified DPI resolution.
    /// </summary>
    /// <param name="path">Full file path where the PNG image will be saved.</param>
    /// <param name="dpi">Resolution (dots per inch) for the generated image.</param>
    private static void GenerateSwissQRImage(string path, int dpi)
    {
        // Create Swiss QR code data (QR‑Bill) with sample creditor information
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Generate the barcode image using the specified DPI
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            generator.Parameters.Resolution = dpi;
            generator.Save(path, BarCodeImageFormat.Png);
        }
    }
}