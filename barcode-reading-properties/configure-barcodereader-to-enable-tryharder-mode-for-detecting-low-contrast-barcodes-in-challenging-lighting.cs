// Title: Enable tryHarder mode for low‑contrast QR barcode detection using BarCodeReader
// Description: Demonstrates configuring BarCodeReader's quality settings to improve detection of low‑contrast QR codes in challenging lighting conditions.
// Category-Description: This example belongs to the Aspose.BarCode reading category, showcasing how to adjust BarCodeReader's QualitySettings (BarcodeQuality, Deconvolution, ComplexBackground) for robust barcode recognition. Developers often need to read barcodes from poor‑quality images, low‑contrast prints, or complex backgrounds; this pattern provides a practical approach using the Aspose.BarCode API.
// Prompt: Configure BarCodeReader to enable tryHarder mode for detecting low‑contrast barcodes in challenging lighting.
// Tags: qr, low-contrast, tryharder, barcode reading, quality settings, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates configuring BarCodeReader to improve detection of low‑contrast QR barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates a QR barcode, reads it with enhanced quality settings, and outputs the result.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary folder and file path for the generated barcode image
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "barcode.png");

        // --------------------------------------------------------------
        // Generate a simple QR barcode image and save it to the temp file
        // --------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "LowContrastTest"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------
        // Verify that the barcode image was created successfully
        // --------------------------------------------------------------
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // --------------------------------------------------------------
        // Read the barcode using BarCodeReader with enhanced settings
        // (equivalent to a 'tryHarder' mode for low‑contrast detection)
        // --------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            // Aspose.BarCode does not expose a direct 'TryHarder' flag.
            // Instead, configure quality settings to improve detection of low‑contrast barcodes.
            reader.QualitySettings.BarcodeQuality = BarcodeQualityMode.Low;               // Thorough analysis mode
            reader.QualitySettings.Deconvolution = DeconvolutionMode.Slow;               // Heavy deconvolution preprocessing
            reader.QualitySettings.ComplexBackground = ComplexBackgroundMode.Enabled;   // Handle colored/complex backgrounds

            // Perform the barcode reading operation
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output the number of barcodes detected and their details
            Console.WriteLine($"Barcodes read: {results.Length}");
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // --------------------------------------------------------------
        // Clean up temporary files and directories
        // --------------------------------------------------------------
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failure should not affect program outcome
        }
    }
}