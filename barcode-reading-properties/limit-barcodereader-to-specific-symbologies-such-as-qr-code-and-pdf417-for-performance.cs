// Title: Limit BarCodeReader to Specific Symbologies (QR Code and PDF417)
// Description: Demonstrates generating QR Code and PDF417 barcodes, then reading them while restricting the BarCodeReader to those symbologies for improved performance.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator to create barcodes and BarCodeReader with DecodeType filters to read only selected symbologies. Developers often need to limit recognition to specific types such as QR Code or PDF417 to reduce processing time and resource usage in high‑throughput scenarios.
// Prompt: Limit BarCodeReader to specific symbologies such as QR Code and PDF417 for performance.
// Tags: barcode, symbology, generation, recognition, qr, pdf417, performance, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates limiting barcode recognition to specific symbologies (QR Code and PDF417) for performance optimization.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample QR and PDF417 barcodes, reads them with a filtered BarCodeReader, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to store generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        var barcodeFiles = new List<string>();

        // -------------------- Generate QR Code --------------------
        string qrPath = Path.Combine(tempFolder, "qr.png");
        using (var qrGenerator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR"))
        {
            // Save QR Code as PNG
            qrGenerator.Save(qrPath, BarCodeImageFormat.Png);
        }
        barcodeFiles.Add(qrPath);

        // -------------------- Generate PDF417 --------------------
        string pdfPath = Path.Combine(tempFolder, "pdf417.png");
        using (var pdfGenerator = new BarcodeGenerator(EncodeTypes.Pdf417, "Sample PDF417"))
        {
            // Save PDF417 as PNG
            pdfGenerator.Save(pdfPath, BarCodeImageFormat.Png);
        }
        barcodeFiles.Add(pdfPath);

        // -------------------- Read barcodes with limited symbologies --------------------
        foreach (string file in barcodeFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            // Limit recognition to QR and PDF417 only
            using (var reader = new BarCodeReader(file, DecodeType.QR, DecodeType.Pdf417))
            {
                BarCodeResult[] results = reader.ReadBarCodes();
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"{Path.GetFileName(file)} - Detected {result.CodeTypeName}: {result.CodeText}");
                }
            }
        }

        // -------------------- Clean up temporary files (optional) --------------------
        try
        {
            foreach (string file in barcodeFiles)
            {
                if (File.Exists(file))
                    File.Delete(file);
            }

            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup error: {ex.Message}");
        }
    }
}