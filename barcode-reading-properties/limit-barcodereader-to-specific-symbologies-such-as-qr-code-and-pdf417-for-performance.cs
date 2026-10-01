// Title: Limit BarCodeReader to Specific Symbologies (QR and PDF417) for High Performance
// Description: Demonstrates how to generate QR and PDF417 barcodes, then read them using BarCodeReader limited to those symbologies to improve performance.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating the use of BarCodeReader with DecodeType filtering and QualitySettings for performance optimization. It shows generating barcodes with BarcodeGenerator, saving as PNG, and reading them by specifying only required symbologies. Developers often need to limit recognition to certain types to reduce processing time in bulk scanning scenarios.
// Prompt: Limit BarCodeReader to specific symbologies such as QR Code and PDF417 for performance.
// Tags: barcode symbology, recognition, performance, qr, pdf417, aspose.barcode, c#
using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Sample program that generates QR and PDF417 barcodes, then reads them
/// while restricting the BarCodeReader to those specific symbologies for
/// higher performance.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Creates temporary barcode images, reads them
    /// with limited symbology detection, and cleans up the temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for sample barcodes
        string tempFolder = Path.Combine(Path.GetTempPath(), "LimitedReader_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Prepare sample data: each tuple contains the encode type, text, and output file name
        var samples = new List<(BaseEncodeType Encode, string Text, string FileName)>
        {
            (EncodeTypes.QR, "Hello QR", "qr.png"),
            (EncodeTypes.Pdf417, "Hello PDF417", "pdf417.png")
        };

        // Generate barcode images and save them as PNG files
        foreach (var (encode, text, fileName) in samples)
        {
            string filePath = Path.Combine(tempFolder, fileName);
            using (var generator = new BarcodeGenerator(encode, text))
            {
                // Save the generated barcode image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // --------------------------------------------------------------------
        // Read QR Code with BarCodeReader limited to QR symbology
        // --------------------------------------------------------------------
        string qrPath = Path.Combine(tempFolder, "qr.png");
        if (File.Exists(qrPath))
        {
            using (var reader = new BarCodeReader(qrPath, DecodeType.QR))
            {
                // Apply high‑performance preset to speed up recognition
                reader.QualitySettings = QualitySettings.HighPerformance;

                // Iterate over detected barcodes (expected one per image)
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"QR Detected: Text='{result.CodeText}', Type='{result.CodeTypeName}'");
                }
            }
        }

        // --------------------------------------------------------------------
        // Read PDF417 with BarCodeReader limited to PDF417 symbology
        // --------------------------------------------------------------------
        string pdf417Path = Path.Combine(tempFolder, "pdf417.png");
        if (File.Exists(pdf417Path))
        {
            using (var reader = new BarCodeReader(pdf417Path, DecodeType.Pdf417))
            {
                reader.QualitySettings = QualitySettings.HighPerformance;
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"PDF417 Detected: Text='{result.CodeText}', Type='{result.CodeTypeName}'");
                }
            }
        }

        // Clean up temporary files and folder; ignore any errors during deletion
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Deletion failure is non‑critical for this demo
        }
    }
}