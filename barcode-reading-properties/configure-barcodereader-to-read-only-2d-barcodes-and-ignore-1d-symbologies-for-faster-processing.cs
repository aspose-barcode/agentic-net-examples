// Title: Read Only 2D Barcodes with BarCodeReader
// Description: Demonstrates configuring Aspose.BarCode's BarCodeReader to recognize only 2D symbologies, improving processing speed by ignoring 1D barcodes.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, focusing on selective symbology decoding. It shows how to use BarCodeReader with a custom BaseDecodeType array and QualitySettings to target 2D barcodes such as QR, DataMatrix, PDF417, Aztec, MicroQR, and MaxiCode. Developers often need to limit scanning to specific symbologies for performance optimization in high‑throughput or resource‑constrained applications.
// Prompt: Configure BarCodeReader to read only 2D barcodes and ignore 1D symbologies for faster processing.
// Tags: barcode, 2d, symbology, recognition, aspose.barcode, qualitysettings, highperformance, qr, datamatrix, pdf417, aztec, microqr, maxicode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Sample program that generates a QR code and reads it back using BarCodeReader
/// configured to process only 2D symbologies for faster performance.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR code image, reads it using a 2D‑only reader,
    /// outputs the decoded information, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "qr.png");

        // Generate a QR code image (sample 2D barcode)
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Ensure the image was created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Define the set of 2D symbologies to recognize
        BaseDecodeType[] decodeTypes = new BaseDecodeType[]
        {
            DecodeType.QR,
            DecodeType.DataMatrix,
            DecodeType.Pdf417,
            DecodeType.Aztec,
            DecodeType.MicroQR,
            DecodeType.MaxiCode
        };

        // Initialize the reader to process only the specified 2D types
        using (var reader = new BarCodeReader(barcodePath, decodeTypes))
        {
            // Use a high‑performance preset for faster scanning
            reader.QualitySettings = QualitySettings.HighPerformance;

            // Perform the recognition
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
            }
            else
            {
                // Output each detected barcode's text and symbology name
                foreach (var result in results)
                {
                    Console.WriteLine($"CodeText: {result.CodeText}");
                    Console.WriteLine($"Symbology: {result.CodeTypeName}");
                }
            }
        }

        // Clean up temporary files
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored – cleanup failure should not affect demo outcome
        }
    }
}