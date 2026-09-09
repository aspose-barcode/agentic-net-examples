// Title: Demonstrate QualitySettings.AllowIncorrectBarcodes with QR Code Generation and Recognition
// Description: Shows how to generate a QR barcode, then read it twice—once with AllowIncorrectBarcodes disabled and once enabled—to illustrate capturing potentially unreadable barcodes during debugging.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It demonstrates using BarcodeGenerator to create a QR code and BarCodeReader with QualitySettings to control the AllowIncorrectBarcodes flag. Developers working with barcode validation, debugging, or error‑tolerant scanning can use these APIs to decide whether to accept imperfect barcodes.
// Prompt: Enable QualitySettings.AllowIncorrectBarcodes to capture potentially unreadable barcodes during debugging sessions.
// Tags: qr,barcode,allowincorrectbarcodes,debugging,qualitysettings,aspnet,aspose.barcode,generation,recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates enabling and disabling QualitySettings.AllowIncorrectBarcodes while reading a QR barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates a QR code, reads it with different AllowIncorrectBarcodes settings,
    /// and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the demo
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string imagePath = Path.Combine(tempDir, "qr.png");

        // Generate a simple QR barcode image
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "SampleText"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Read the barcode with AllowIncorrectBarcodes disabled
        Console.WriteLine("AllowIncorrectBarcodes: false");
        using (var reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            // Disable acceptance of potentially incorrect barcodes
            reader.QualitySettings.AllowIncorrectBarcodes = false;
            BarCodeResult[] results = reader.ReadBarCodes();
            Console.WriteLine($"Barcodes read: {results.Length}");
            foreach (BarCodeResult result in reader.FoundBarCodes)
            {
                Console.WriteLine($"{result.CodeTypeName}:{result.CodeText}");
            }
        }

        // Read the barcode with AllowIncorrectBarcodes enabled
        Console.WriteLine("AllowIncorrectBarcodes: true");
        using (var reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            // Enable acceptance of potentially incorrect barcodes
            reader.QualitySettings.AllowIncorrectBarcodes = true;
            BarCodeResult[] results = reader.ReadBarCodes();
            Console.WriteLine($"Barcodes read: {results.Length}");
            foreach (BarCodeResult result in reader.FoundBarCodes)
            {
                Console.WriteLine($"{result.CodeTypeName}:{result.CodeText}");
            }
        }

        // Cleanup temporary files
        try
        {
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect demo execution
        }
    }
}