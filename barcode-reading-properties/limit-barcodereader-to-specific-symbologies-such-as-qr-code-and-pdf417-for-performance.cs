// Title: Limit BarCodeReader to specific symbologies (QR Code and PDF417)
// Description: Demonstrates generating QR Code and PDF417 barcodes, then reading them while restricting the BarCodeReader to those symbologies for improved performance.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showing how to use BarCodeGenerator to create barcodes and BarCodeReader with DecodeType filters to limit scanning to selected symbologies. Typical use cases include batch processing where only certain barcode types are expected, reducing processing time and resource usage. Developers often need to generate barcodes, read them from images, and optimize recognition by specifying DecodeType values.
/// Prompt: Limit BarCodeReader to specific symbologies such as QR Code and PDF417 for performance.
/// Tags: barcode symbology, recognition, performance, qr, pdf417, aspnet, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating QR Code and PDF417 barcodes and reading them with BarCodeReader limited to those symbologies.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes, reads them with filtered symbologies, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the QR Code and PDF417 images
        string qrPath = Path.Combine(tempFolder, "qr.png");
        string pdf417Path = Path.Combine(tempFolder, "pdf417.png");

        // Generate a QR Code image
        using (var qrGenerator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR"))
        {
            qrGenerator.Save(qrPath, BarCodeImageFormat.Png);
        }

        // Generate a PDF417 image
        using (var pdfGenerator = new BarcodeGenerator(EncodeTypes.Pdf417, "Sample PDF417"))
        {
            pdfGenerator.Save(pdf417Path, BarCodeImageFormat.Png);
        }

        // Collection of generated files to be processed
        var files = new[] { qrPath, pdf417Path };

        // Iterate over each file and attempt to read barcodes, limiting to QR and PDF417 types
        foreach (string file in files)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            // Initialize BarCodeReader with specific DecodeType filters for performance
            using (var reader = new BarCodeReader(file, DecodeType.QR, DecodeType.Pdf417))
            {
                try
                {
                    BarCodeResult[] results = reader.ReadBarCodes();
                    Console.WriteLine($"Reading '{Path.GetFileName(file)}' - Detected {results.Length} barcode(s):");
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                    }
                }
                catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
                {
                    Console.WriteLine($"Unable to load image '{file}': {ex.Message}");
                }
            }
        }

        // Clean up temporary files and directory
        try
        {
            foreach (string file in files)
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            }
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }
    }
}