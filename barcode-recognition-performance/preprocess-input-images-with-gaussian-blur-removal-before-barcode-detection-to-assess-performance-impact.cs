// Title: Barcode detection performance comparison with Gaussian blur removal
// Description: Demonstrates generating a QR barcode image and measuring detection time with and without applying deconvolution (Gaussian blur removal) to assess its impact on recognition speed.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and QualitySettings.Deconvolution to improve image quality before detection. Typical scenarios include preprocessing scanned images to enhance barcode readability and evaluating performance trade‑offs in automated scanning systems.
// Prompt: Preprocess input images with Gaussian blur removal before barcode detection to assess performance impact.
// Tags: barcode, qr, detection, deconvolution, gaussian blur, performance, aspose.barcode, generation, recognition

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates a QR barcode, then reads it twice: once without preprocessing and once with Gaussian blur removal (deconvolution),
/// printing detection results and timing information for each approach.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes barcode generation, detection, timing, and cleanup.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "barcode.png");

        // Generate a sample QR barcode image and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // Detection without any preprocessing
        // ------------------------------------------------------------
        Stopwatch swNoPre = new Stopwatch();
        swNoPre.Start();

        using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            // Read all barcodes found in the image
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"[NoPre] Detected: {result.CodeText} Type: {result.CodeTypeName}");
            }
        }

        swNoPre.Stop();
        Console.WriteLine($"Detection time without preprocessing: {swNoPre.ElapsedMilliseconds} ms");

        // ------------------------------------------------------------
        // Detection with Gaussian blur removal (Deconvolution)
        // ------------------------------------------------------------
        Stopwatch swDeconv = new Stopwatch();
        swDeconv.Start();

        using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            // Enable deconvolution (blur removal) before reading
            reader.QualitySettings.Deconvolution = DeconvolutionMode.Normal;

            // Read all barcodes after applying deconvolution
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"[Deconv] Detected: {result.CodeText} Type: {result.CodeTypeName}");
            }
        }

        swDeconv.Stop();
        Console.WriteLine($"Detection time with deconvolution: {swDeconv.ElapsedMilliseconds} ms");

        // ------------------------------------------------------------
        // Cleanup temporary files and directories
        // ------------------------------------------------------------
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored – cleanup failures should not affect program outcome
        }
    }
}