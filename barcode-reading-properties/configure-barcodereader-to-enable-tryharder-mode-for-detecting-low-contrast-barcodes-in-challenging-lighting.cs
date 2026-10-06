// Title: Enable TryHarder Mode for Low‑Contrast Barcode Detection
// Description: Demonstrates configuring BarCodeReader to improve detection of low‑contrast barcodes by adjusting quality settings.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category. It shows how to use the BarCodeReader class together with QualitySettings (ComplexBackgroundMode, BarcodeQualityMode, DeconvolutionMode) to handle challenging images such as low‑contrast or poorly lit barcodes. Developers working on scanning solutions often need to enable “try‑harder” behavior to increase detection robustness in real‑world lighting conditions.
/// Prompt: Configure BarCodeReader to enable tryHarder mode for detecting low‑contrast barcodes in challenging lighting.
// Tags: low-contrast, tryharder, barcode, recognition, qualitysettings, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that configures BarCodeReader for low‑contrast barcode detection.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Reads a barcode image, enables try‑harder settings, and prints detected barcodes.
    /// </summary>
    static void Main()
    {
        // Define the path to the image that contains low‑contrast barcodes.
        string imagePath = Path.Combine(Path.GetTempPath(), "low_contrast_barcode.png");

        // Verify that the image file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            Console.WriteLine("Place a barcode image at the above path and rerun the program.");
            return;
        }

        // Create a BarCodeReader that attempts to decode all supported barcode types.
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Enable more thorough detection (try‑harder equivalent) by adjusting quality settings:
            // - ComplexBackground helps when the background is not uniform.
            // - Low barcode quality mode improves detection on poor‑quality images.
            // - Fast deconvolution assists with low‑contrast scenarios.
            reader.QualitySettings.ComplexBackground = ComplexBackgroundMode.Enabled;
            reader.QualitySettings.BarcodeQuality = BarcodeQualityMode.Low;
            reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;

            // Perform the barcode recognition.
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output the number of barcodes found and their details.
            Console.WriteLine($"Barcodes read: {results.Length}");
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }
    }
}