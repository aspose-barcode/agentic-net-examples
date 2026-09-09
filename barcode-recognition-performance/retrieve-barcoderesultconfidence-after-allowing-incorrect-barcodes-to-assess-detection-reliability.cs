// Title: Retrieve barcode confidence after allowing incorrect barcodes
// Description: Demonstrates how to generate a QR code, read it with Aspose.BarCode while permitting incorrect barcodes, and obtain the detection confidence value.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing the use of BarCodeReader, QualitySettings, and BarCodeResult to evaluate detection reliability. Developers often need to assess confidence scores when scanning imperfect or partially damaged barcodes, especially in quality‑control or inventory systems.
// Prompt: Retrieve BarCodeResult.Confidence after allowing incorrect barcodes to assess detection reliability.
// Tags: qr, barcode, confidence, allowincorrectbarcodes, aspnet, aspnetcore, aspose.barcode, barcode-recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a QR barcode, reading it with incorrect barcode allowance, and retrieving confidence scores.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a temporary QR barcode image, reads it with confidence values, and cleans up resources.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for demo files
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define the full path for the barcode image
        string barcodePath = Path.Combine(tempDir, "barcode.png");

        // Generate a QR barcode image
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample123"))
        {
            // Save the generated barcode as a PNG file
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Read the barcode while allowing incorrect barcodes and retrieve confidence values
        using (var reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            // Enable the setting that permits detection of potentially incorrect barcodes
            reader.QualitySettings.AllowIncorrectBarcodes = true;

            // Perform the read operation and obtain all results
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output the number of barcodes detected
            Console.WriteLine($"Barcodes read: {results.Length}");

            // Iterate through each result and display its details
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"Confidence: {result.Confidence}");
            }
        }

        // Cleanup temporary files and directories
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);

            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }
    }
}