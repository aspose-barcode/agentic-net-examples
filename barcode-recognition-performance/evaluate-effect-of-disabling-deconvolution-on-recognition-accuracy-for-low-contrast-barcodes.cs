// Title: Effect of Deconvolution Mode on Low‑Contrast Barcode Recognition
// Description: Demonstrates how disabling deconvolution impacts the detection accuracy of a low‑contrast Code128 barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating the use of BarCodeReader with different DeconvolutionMode settings. It shows how to generate a low‑contrast barcode, configure quality settings, and compare recognition results. Developers working with image preprocessing, barcode quality tuning, and low‑contrast scanning can use similar patterns.
// Prompt: Evaluate the effect of disabling deconvolution on recognition accuracy for low‑contrast barcodes.
// Tags: code128, low-contrast, deconvolution, barcode recognition, quality settings, aspose.barcode, image preprocessing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates the impact of disabling deconvolution on recognition accuracy for low‑contrast barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a low‑contrast barcode, reads it with normal and fast deconvolution modes, and outputs detection statistics.
    /// </summary>
    static void Main()
    {
        // Create a temporary directory for generated images
        string tempDir = Path.Combine(Path.GetTempPath(), "DeconvTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define the path for the barcode image and the text to encode
        string barcodePath = Path.Combine(tempDir, "low_contrast.png");
        string codeText = "LowContrastTest";

        // Generate a low‑contrast Code128 barcode
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set low contrast colors (gray on light gray)
            generator.Parameters.Barcode.BarColor = Color.Gray;
            generator.Parameters.BackColor = Color.LightGray;

            // Use a small XDimension for a clearer image
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the barcode as a PNG file
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to generate barcode image.");
            return;
        }

        // Local function to read a barcode with a specified deconvolution mode
        int ReadBarcode(string path, DeconvolutionMode deconvMode, out double avgQuality)
        {
            avgQuality = 0.0;
            using (var reader = new BarCodeReader(path, DecodeType.AllSupportedTypes))
            {
                // Configure low quality settings for low‑contrast images
                reader.QualitySettings.BarcodeQuality = BarcodeQualityMode.Low;
                reader.QualitySettings.Deconvolution = deconvMode;

                // Perform the recognition
                BarCodeResult[] results = reader.ReadBarCodes();
                if (results.Length == 0)
                    return 0;

                // Calculate the average reading quality across all detected barcodes
                double total = 0.0;
                foreach (var result in results)
                {
                    total += result.ReadingQuality;
                }
                avgQuality = total / results.Length;
                return results.Length;
            }
        }

        // Read the barcode using normal deconvolution (more processing)
        int countNormal = ReadBarcode(barcodePath, DeconvolutionMode.Normal, out double avgQualityNormal);
        // Read the barcode using fast deconvolution (minimal processing, effectively "disabled")
        int countFast = ReadBarcode(barcodePath, DeconvolutionMode.Fast, out double avgQualityFast);

        // Output the detection results for both modes
        Console.WriteLine($"Deconvolution = Normal: Detected {countNormal} barcode(s), Avg. Quality = {avgQualityNormal:F2}");
        Console.WriteLine($"Deconvolution = Fast  : Detected {countFast} barcode(s), Avg. Quality = {avgQualityFast:F2}");

        // Clean up temporary files and directory
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}