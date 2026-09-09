// Title: Benchmark decoding time for Swiss QR Code images at different XDimensions
// Description: Demonstrates how to generate Swiss QR Code barcodes with varying XDimension values, decode them using BarCodeReader, and measure the elapsed time for each resolution.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, showcasing the ComplexBarcodeGenerator for Swiss QR Bill creation and the BarCodeReader for QR decoding. Developers often need to benchmark performance across different barcode sizes or resolutions, and this snippet illustrates typical API usage for such performance testing scenarios.
// Prompt: Benchmark the time required to decode Swiss QR Code images of varying resolutions using BarCodeReader.
// Tags: swiss qr, barcode generation, barcode decoding, performance benchmark, aspnet.barcode, complexbarcodegenerator, barcodereader

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates benchmarking of Swiss QR Code decoding across different XDimension settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates Swiss QR Code images with varying XDimensions, decodes them, and reports timing results.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for generated images
        string tempDir = Path.Combine(Path.GetTempPath(), "SwissQRBenchmark_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define XDimension values to simulate different image resolutions
        float[] xDimensions = new float[] { 1f, 2f, 3f };
        var imageFiles = new List<string>();

        // Generate Swiss QR Code images for each XDimension
        foreach (float xDim in xDimensions)
        {
            string filePath = Path.Combine(tempDir, $"SwissQR_{xDim}.png");

            var swiss = new SwissQRCodetext();
            swiss.Bill.Creditor.Name = "John Doe";
            swiss.Bill.Creditor.CountryCode = "CH";
            swiss.Bill.Account = "CH9300762011623852957";
            swiss.Bill.Amount = 199.95m;
            swiss.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

            // Use ComplexBarcodeGenerator to create the barcode image
            using (var generator = new ComplexBarcodeGenerator(swiss))
            {
                generator.Parameters.Barcode.XDimension.Point = xDim;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            imageFiles.Add(filePath);
        }

        // Benchmark decoding time for each generated image
        var results = new List<(float XDim, long Milliseconds)>();
        foreach (string file in imageFiles)
        {
            var stopwatch = Stopwatch.StartNew();

            // Decode the QR code using BarCodeReader
            using (var reader = new BarCodeReader(file, DecodeType.QR))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    // Attempt to parse Swiss QR content (result not used further)
                    var swissResult = ComplexCodetextReader.TryDecodeSwissQR(result.CodeText);
                }
            }

            stopwatch.Stop();

            // Extract XDimension from the filename for reporting
            string name = Path.GetFileNameWithoutExtension(file);
            float xDim = float.Parse(name.Split('_')[1]);
            results.Add((xDim, stopwatch.ElapsedMilliseconds));
        }

        // Output benchmark results to the console
        Console.WriteLine("Swiss QR Code decoding benchmark (XDimension → time ms):");
        foreach (var r in results)
        {
            Console.WriteLine($"XDimension {r.XDim}: {r.Milliseconds} ms");
        }

        // Clean up temporary files and folder
        foreach (var f in imageFiles)
        {
            if (File.Exists(f))
                File.Delete(f);
        }
        if (Directory.Exists(tempDir))
            Directory.Delete(tempDir);
    }
}