// Title: Switching between HighPerformance and HighQuality presets for barcode reading
// Description: Demonstrates generating a Code128 barcode and reading it using Aspose.BarCode's HighPerformance and HighQuality quality settings.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing how to adjust the QualitySettings preset on BarCodeReader. It highlights the use of BarcodeGenerator for creation, BarCodeReader for decoding, and the QualitySettings enum to toggle between performance‑oriented and quality‑oriented reading modes—common tasks for developers needing fast scans or maximum accuracy.
// Prompt: Write documentation examples demonstrating how to switch between HighPerformance and HighQuality presets.
// Tags: barcode, code128, highperformance, highquality, qualitysettings, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to generate a barcode and read it using different quality presets (HighPerformance vs HighQuality).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, reads it with two different quality settings, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary directory for the demo files
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "sample.png");

        // Generate a simple Code128 barcode and save it as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "AsposeDemo"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was successfully created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to generate barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // Read the barcode using the HighPerformance preset (faster, lower accuracy)
        // ------------------------------------------------------------
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            reader.QualitySettings = QualitySettings.HighPerformance;
            var results = reader.ReadBarCodes();
            Console.WriteLine($"HighPerformance preset - Barcodes read: {results.Length}");
            foreach (var result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // ------------------------------------------------------------
        // Read the same barcode using the HighQuality preset (slower, higher accuracy)
        // ------------------------------------------------------------
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            reader.QualitySettings = QualitySettings.HighQuality;
            var results = reader.ReadBarCodes();
            Console.WriteLine($"HighQuality preset - Barcodes read: {results.Length}");
            foreach (var result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Clean up temporary files and directory
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }
    }
}