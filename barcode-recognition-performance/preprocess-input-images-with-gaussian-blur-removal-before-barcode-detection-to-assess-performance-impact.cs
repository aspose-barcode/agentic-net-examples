// Title: Gaussian Blur Removal Impact on Barcode Detection
// Description: Demonstrates how applying Gaussian blur removal (deconvolution) affects barcode recognition speed and accuracy.
// Category-Description: This example belongs to the Aspose.BarCode image preprocessing category, showcasing the use of BarCodeReader.QualitySettings.Deconvolution to mitigate blur before detection. Developers often need to compare baseline detection with enhanced preprocessing to optimize performance for blurred images.
// Prompt: Preprocess input images with Gaussian blur removal before barcode detection to assess performance impact.
// Tags: barcode, gaussian blur, deconvolution, performance, preprocessing, aspose.barcode, code128, detection

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode, then reads it twice: once without any preprocessing
/// and once with Gaussian blur removal (deconvolution). The execution times and
/// decoded texts are printed for comparison.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, detection with and
    /// without deconvolution, and outputs the results.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a unique temporary folder to store the generated barcode image.
        // --------------------------------------------------------------------
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "barcode.png");

        // ---------------------------------------------------------------
        // Generate a simple Code128 barcode image and save it as PNG.
        // ---------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // ---------------------------------------------------------------
        // Read the barcode without any deconvolution (baseline measurement).
        // ---------------------------------------------------------------
        long timeWithout;
        string resultWithout;
        var sw = new Stopwatch();
        sw.Start();

        using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            resultWithout = null;
            foreach (var res in reader.ReadBarCodes())
            {
                resultWithout = res.CodeText;
                break; // Stop after the first successful read.
            }
        }

        sw.Stop();
        timeWithout = sw.ElapsedMilliseconds;

        // ---------------------------------------------------------------
        // Read the barcode with Gaussian blur removal (Deconvolution mode set to Normal).
        // ---------------------------------------------------------------
        long timeWith;
        string resultWith;
        sw.Restart();

        using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            // Enable deconvolution to mitigate blur before detection.
            reader.QualitySettings.Deconvolution = DeconvolutionMode.Normal;

            resultWith = null;
            foreach (var res in reader.ReadBarCodes())
            {
                resultWith = res.CodeText;
                break; // Stop after the first successful read.
            }
        }

        sw.Stop();
        timeWith = sw.ElapsedMilliseconds;

        // ---------------------------------------------------------------
        // Output the comparison results to the console.
        // ---------------------------------------------------------------
        Console.WriteLine($"Without deconvolution: Text = '{resultWithout ?? "null"}', Time = {timeWithout} ms");
        Console.WriteLine($"With deconvolution (Normal): Text = '{resultWith ?? "null"}', Time = {timeWith} ms");

        // ---------------------------------------------------------------
        // Clean up temporary files and directory.
        // ---------------------------------------------------------------
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit.
        }
    }
}