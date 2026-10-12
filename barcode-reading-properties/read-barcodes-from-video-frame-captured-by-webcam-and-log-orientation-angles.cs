// Title: Read barcode from a generated image and log its orientation angle
// Description: Generates a QR code image, reads it back using Aspose.BarCode, and logs the barcode type, text, and detected angle, simulating a video frame capture.
// Category-Description: This example demonstrates the core Aspose.BarCode workflow of barcode generation and recognition. It uses BarcodeGenerator to create a barcode image and BarCodeReader to decode it, exposing properties such as CodeTypeName, CodeText, and Region.Angle. Developers working with image or video streams often need to generate test barcodes and extract orientation information for alignment or quality‑control purposes. The snippet showcases typical API classes (BarcodeGenerator, BarCodeReader, BarCodeResult) and common use cases like QR code handling and angle detection, making it a useful reference for quick prototyping or CI‑based validation of barcode processing pipelines.
// Prompt: Read barcodes from a video frame captured by a webcam and log orientation angles.
// Tags: barcode, qr, generation, recognition, orientation, angle, aspose.barcode, c#, console

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation, recognition, and angle extraction using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, reads it, and prints detection details.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodeFile = Path.Combine(tempFolder, "sample.png");

        // Generate a sample QR barcode image
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            generator.Save(barcodeFile, BarCodeImageFormat.Png);
        }

        // Verify the generated file exists
        if (!File.Exists(barcodeFile))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read barcodes from the generated image (simulating a captured video frame)
        using (var reader = new BarCodeReader(barcodeFile, DecodeType.AllSupportedTypes))
        {
            BarCodeResult[] results = reader.ReadBarCodes();
            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes detected.");
            }
            else
            {
                foreach (BarCodeResult result in results)
                {
                    // Output barcode type, decoded text, and detected orientation angle
                    Console.WriteLine($"CodeType: {result.CodeTypeName}");
                    Console.WriteLine($"CodeText: {result.CodeText}");
                    Console.WriteLine($"Angle: {result.Region.Angle}");
                }
            }
        }

        // Clean up temporary files
        try
        {
            File.Delete(barcodeFile);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }
}