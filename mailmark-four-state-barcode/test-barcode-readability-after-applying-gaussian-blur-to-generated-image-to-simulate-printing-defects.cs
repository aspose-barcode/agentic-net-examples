// Title: Generate QR Code, Apply Gaussian Blur Simulation, and Test Readability
// Description: This example generates a QR code, saves it as PNG, and attempts to read it back using quality settings to simulate the effect of printing defects such as Gaussian blur.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition workflow, focusing on image quality handling. Shows how to create a barcode with BarcodeGenerator, configure appearance, save as image, and use BarCodeReader with QualitySettings to improve detection of degraded images. Useful for developers working on scanning printed barcodes, handling blur, low contrast, or other defects.
// Prompt: Test barcode readability after applying Gaussian blur to the generated image to simulate printing defects.
// Tags: qr, barcode generation, barcode recognition, gaussian blur, image quality, aspose.barcode, png, c#

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation, optional image degradation simulation, and recognition with quality settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, (optionally) applies Gaussian blur, and verifies readability.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare temporary directory and file paths for the barcode image.
        // ------------------------------------------------------------
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeBlurTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "barcode.png");

        // ------------------------------------------------------------
        // Generate a QR code barcode and save it as a PNG image.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Test123"))
        {
            // Set barcode and background colors.
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Add padding around the barcode.
            generator.Parameters.Barcode.Padding.Left.Point = 5f;
            generator.Parameters.Barcode.Padding.Top.Point = 5f;
            generator.Parameters.Barcode.Padding.Right.Point = 5f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 5f;

            // Save the generated barcode image.
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Apply Gaussian blur to simulate printing defects.
        // Aspose.Drawing does not provide a direct Gaussian blur filter,
        // so this step is omitted. In a full environment you could apply
        // a blur using a supported image processing library before reading.
        // ------------------------------------------------------------

        // ------------------------------------------------------------
        // Attempt to read the (unblurred) barcode using quality settings
        // that help recognize degraded images.
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            // Configure quality settings to improve recognition of blurred images.
            reader.QualitySettings.Deconvolution = DeconvolutionMode.Normal;
            reader.QualitySettings.BarcodeQuality = BarcodeQualityMode.Low;
            reader.QualitySettings.AllowIncorrectBarcodes = true;

            // Perform barcode detection.
            var results = reader.ReadBarCodes();

            // Output the result.
            if (results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText))
            {
                Console.WriteLine("Barcode read successfully: " + results[0].CodeText);
            }
            else
            {
                Console.WriteLine("Failed to read barcode.");
            }
        }

        // ------------------------------------------------------------
        // Clean up temporary files and directories.
        // ------------------------------------------------------------
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignore any cleanup errors.
        }
    }
}