// Title: Validate MinimalXDimension > XDimension effect on barcode recognition
// Description: Demonstrates generating a Code128 barcode with a small XDimension and then reading it using normal and minimal XDimension modes to show that setting MinimalXDimension higher than the barcode's XDimension can unintentionally filter out valid barcodes.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader with QualitySettings to control XDimension handling. Developers often need to adjust XDimension settings to improve scan reliability across different devices; this snippet illustrates typical usage and pitfalls when configuring MinimalXDimension.
// Prompt: Validate that setting MinimalXDimension higher than XDimension filters out valid barcodes unintentionally.
// Tags: code128, xdimension, minimalxdimension, barcode generation, barcode recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates the impact of MinimalXDimension setting on barcode recognition.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, reads it with different XDimension settings, and outputs the results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the barcode image
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "code128.png");

        // Generate a Code128 barcode with a specific (small) XDimension
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ASPOSE123"))
        {
            generator.Parameters.Barcode.XDimension.Point = 2f; // small module size
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Local function to read a barcode with custom quality settings and return the number of recognized barcodes
        int ReadBarcodeCount(BaseDecodeType decodeType, Action<BarCodeReader> configureReader)
        {
            if (!File.Exists(barcodePath))
            {
                Console.WriteLine("Barcode image not found.");
                return 0;
            }

            using (var reader = new BarCodeReader(barcodePath, decodeType))
            {
                // Apply caller‑provided quality settings
                configureReader(reader);
                BarCodeResult[] results = reader.ReadBarCodes();
                return results?.Length ?? 0;
            }
        }

        // Read using normal XDimension mode
        int normalCount = ReadBarcodeCount(DecodeType.Code128, reader =>
        {
            reader.QualitySettings.XDimension = XDimensionMode.Normal;
        });

        // Read using UseMinimalXDimension mode with a MinimalXDimension higher than the generated XDimension
        int minimalCount = ReadBarcodeCount(DecodeType.Code128, reader =>
        {
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 5f; // higher than generator's 2 points
        });

        // Output the recognition results
        Console.WriteLine($"Barcodes recognized with Normal mode: {normalCount}");
        Console.WriteLine($"Barcodes recognized with UseMinimalXDimension (MinimalXDimension=5): {minimalCount}");

        // Cleanup temporary files and folder
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}