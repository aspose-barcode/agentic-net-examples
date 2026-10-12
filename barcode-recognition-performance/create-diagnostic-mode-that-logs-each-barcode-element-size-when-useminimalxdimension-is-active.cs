// Title: QR Code Generation and Minimal X-Dimension Barcode Recognition
// Description: This example generates a QR barcode, then reads it with the UseMinimalXDimension mode enabled, logging the element size used during detection.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition APIs, focusing on the XDimensionMode settings. It uses BarcodeGenerator for creating barcodes and BarCodeReader with QualitySettings to control minimal element dimensions, a common requirement for high‑density or low‑resolution scanning scenarios. Developers often need to adjust X‑dimension to improve detection accuracy and to diagnose element sizing.
// Prompt: Create a diagnostic mode that logs each barcode element size when UseMinimalXDimension is active.
// Tags: qr, barcode, generation, recognition, minimalxdimension, diagnostics, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR barcode, then reading it with UseMinimalXDimension enabled
/// and logging diagnostic information about the element size used during detection.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, configures minimal X‑dimension
    /// for recognition, and outputs diagnostic details for each detected barcode.
    /// </summary>
    static void Main()
    {
        // Generate a sample QR barcode and store it in a memory stream
        using (var barcodeStream = new MemoryStream())
        {
            // Create a BarcodeGenerator for QR encoding with the text "HelloWorld"
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "HelloWorld"))
            {
                // Disable automatic sizing to keep control over dimensions
                generator.Parameters.AutoSizeMode = AutoSizeMode.None;
                // Set the X‑dimension (module size) to 2 points
                generator.Parameters.Barcode.XDimension.Point = 2f;
                // Save the generated barcode as PNG into the memory stream
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
            }

            // Reset stream position to the beginning for reading
            barcodeStream.Position = 0;

            // Initialize a BarCodeReader for QR decoding using the memory stream
            using (var reader = new BarCodeReader(barcodeStream, DecodeType.QR))
            {
                // Enable UseMinimalXDimension mode and define the minimal element size (2 pixels)
                reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                reader.QualitySettings.MinimalXDimension = 2f; // pixels

                Console.WriteLine("UseMinimalXDimension is active. MinimalXDimension set to 2 pixels.");

                // Perform barcode recognition
                BarCodeResult[] results = reader.ReadBarCodes();

                // Check if any barcodes were detected
                if (results.Length == 0)
                {
                    Console.WriteLine("No barcodes detected.");
                }
                else
                {
                    // Iterate over each detected barcode and log diagnostic information
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"Detected barcode:");
                        Console.WriteLine($"  Code Text: {result.CodeText}");
                        Console.WriteLine($"  Code Type: {result.CodeTypeName}");
                        // Log the minimal X‑dimension used as a proxy for element size
                        Console.WriteLine($"  MinimalXDimension used for detection: {reader.QualitySettings.MinimalXDimension} pixels");
                    }
                }
            }
        }
    }
}