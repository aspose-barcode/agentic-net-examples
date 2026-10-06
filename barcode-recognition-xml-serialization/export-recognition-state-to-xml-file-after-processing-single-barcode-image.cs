// Title: Export barcode recognition state to XML
// Description: Demonstrates reading a barcode from an image and exporting the reader's internal recognition state to an XML file. Useful for debugging or persisting scan results.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, showing how to use BarCodeReader to decode barcodes and retrieve the recognition state via ExportToXml. Typical use cases include logging scan details, troubleshooting decoding issues, and integrating barcode data with XML‑based workflows. Developers often work with BarCodeReader, DecodeType, and BarcodeSettings classes to customize reading behavior and export results.
// Prompt: Export the recognition state to an XML file after processing a single barcode image.
// Tags: barcode, recognition, xml, export, aspose.barcode, barcodereader, decode, qrcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Sample program that generates a QR code, reads it, and exports the recognition state to an XML file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for demo files
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define paths for the barcode image and the exported XML
        string imagePath = Path.Combine(tempDir, "sample.png");
        string xmlPath = Path.Combine(tempDir, "readerState.xml");

        // Generate a sample QR code image if it does not already exist
        if (!File.Exists(imagePath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample123"))
            {
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }
        }

        // Initialize the barcode reader for all supported symbologies
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Example setting: ignore FNC characters during decoding
            reader.BarcodeSettings.StripFNC = true;

            // Perform barcode detection and decoding
            var results = reader.ReadBarCodes();
            Console.WriteLine($"Barcodes found: {results.Length}");
            foreach (var result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }

            // Export the internal recognition state to an XML file
            reader.ExportToXml(xmlPath);
            Console.WriteLine($"Recognition state exported to: {xmlPath}");
        }
    }
}