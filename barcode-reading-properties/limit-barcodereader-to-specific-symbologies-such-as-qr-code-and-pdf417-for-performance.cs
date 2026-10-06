// Title: Limit BarCodeReader to Specific Symbologies (QR Code and PDF417)
// Description: Demonstrates generating QR and PDF417 barcodes, then reading them while restricting the BarCodeReader to those symbologies for improved performance.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create barcode images and BarCodeReader with a symbology filter to efficiently decode them. Developers commonly need to generate barcodes for various data formats and then recognize only the required types to reduce processing time, especially in high‑throughput scenarios.
// Prompt: Limit BarCodeReader to specific symbologies such as QR Code and PDF417 for performance.
// Tags: barcode symbology, generation, recognition, performance, qrcode, pdf417, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates limiting barcode recognition to specific symbologies (QR Code and PDF417) to improve performance.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample QR and PDF417 barcodes, reads them using a filtered BarCodeReader, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for sample barcodes
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Prepare sample barcode data (encode type, text, output file name)
        var samples = new (BaseEncodeType Encode, string Text, string FileName)[]
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
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // List of generated files to be read
        var filesToRead = new string[]
        {
            Path.Combine(tempFolder, "qr.png"),
            Path.Combine(tempFolder, "pdf417.png")
        };

        // Define target symbologies for recognition (QR and PDF417 only)
        BaseDecodeType[] targetTypes = new BaseDecodeType[] { DecodeType.QR, DecodeType.Pdf417 };

        // Read each file using BarCodeReader limited to the specified symbologies
        foreach (string file in filesToRead)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            using (var reader = new BarCodeReader(file, targetTypes))
            {
                // Apply a high‑performance preset to speed up recognition
                reader.QualitySettings = QualitySettings.HighPerformance;

                BarCodeResult[] results = reader.ReadBarCodes();
                Console.WriteLine($"Reading '{Path.GetFileName(file)}' - found {results.Length} barcode(s):");
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                }
            }
        }

        // Clean up temporary files and folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}