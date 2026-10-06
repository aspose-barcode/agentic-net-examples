// Title: Detect Large Barcodes in High‑Resolution Scans Using QualitySettings.XDimension
// Description: Demonstrates how to generate a high‑resolution barcode image and configure the BarCodeReader's QualitySettings to detect large barcodes by setting XDimension mode to Large and MinimalXDimension to 6 pixels.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating the use of BarCodeReader and its QualitySettings for fine‑tuning detection of barcodes in high‑resolution images. It showcases key API classes such as BarcodeGenerator, BarCodeReader, and QualitySettings, which developers commonly use when processing scanned documents, invoices, or product labels that contain large or high‑density barcodes. The snippet helps developers understand how to adjust XDimension settings to improve recognition accuracy for large barcodes.
// Prompt: Set QualitySettings.XDimension to 6 pixels for detecting large barcodes in high‑resolution scans.
// Tags: barcode, recognition, high-resolution, xdimension, large barcode, aspose.barcode, code128, image processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a high‑resolution Code128 barcode and reading it with
/// QualitySettings configured for large barcode detection.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the sample. Generates a barcode, reads it with custom QualitySettings,
    /// and outputs the recognized values.
    /// </summary>
    static void Main()
    {
        // Create a temporary directory for the sample files
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Path for the generated barcode image
        string barcodePath = Path.Combine(tempDir, "sample.png");

        // Generate a high‑resolution barcode image
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "HIGHRES123"))
        {
            // Set high resolution (e.g., 300 dpi) to ensure clear bar definition
            generator.Parameters.Resolution = 300;

            // Optionally set XDimension for clearer bars (2 points ≈ 0.28 mm)
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the barcode as a PNG file
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify the image was created before attempting recognition
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode with QualitySettings configured for large barcodes
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            // Set XDimension mode to Large to enable detection of large barcodes
            reader.QualitySettings.XDimension = XDimensionMode.Large;

            // Define minimal element size in pixels (6 pixels) as required
            reader.QualitySettings.MinimalXDimension = 6f;

            // Perform recognition
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output the number of barcodes read and their details
            Console.WriteLine($"Barcodes read: {results.Length}");
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Clean up temporary files
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }
}