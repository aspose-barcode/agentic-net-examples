// Title: Detect Large Barcodes in High‑Resolution Scans Using XDimension Settings
// Description: This example generates a high‑resolution Code128 barcode, then reads it while configuring QualitySettings.XDimension to a minimal value of 6 pixels to improve detection of large barcodes.
// Category-Description: Demonstrates Aspose.BarCode barcode generation and recognition with custom quality settings. It covers the BarcodeGenerator, BarCodeReader, and QualitySettings classes, showing how to adjust XDimension for high‑resolution scans—a common requirement when processing large or dense barcodes in industrial or retail applications.
// Prompt: Set QualitySettings.XDimension to 6 pixels for detecting large barcodes in high‑resolution scans.
// Tags: barcode, code128, xdimension, qualitysettings, highresolution, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Sample program that creates a high‑resolution barcode image and reads it using
/// custom XDimension settings to handle large barcodes in high‑resolution scans.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the sample. Generates a barcode, configures QualitySettings,
    /// reads the barcode, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the sample barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a high‑resolution barcode image (300 DPI) using Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Parameters.Resolution = 300; // high resolution
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify the file was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Set up the reader for Code128 barcodes
        BaseDecodeType decodeType = DecodeType.Code128;
        using (var reader = new BarCodeReader(barcodePath, decodeType))
        {
            // Configure QualitySettings to use a minimal XDimension of 6 pixels
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 6f;

            // Read all barcodes from the image
            BarCodeResult[] results = reader.ReadBarCodes();
            Console.WriteLine($"Barcodes read: {results.Length}");
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                Console.WriteLine($"Reading quality: {result.ReadingQuality}");
            }
        }

        // Clean up temporary files
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}