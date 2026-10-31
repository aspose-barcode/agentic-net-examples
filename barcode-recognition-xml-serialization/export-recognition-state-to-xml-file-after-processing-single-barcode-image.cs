// Title: Export Barcode Recognition State to XML
// Description: Demonstrates generating a QR barcode, reading it, and exporting the reader's internal state to an XML file.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases key API classes such as BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and the ExportToXml method for persisting the recognition state. Typical use cases include debugging barcode processing pipelines, auditing read operations, or integrating with systems that require a detailed XML report of barcode recognition results. Developers working with barcode generation, multi‑symbology reading, or state export will find this pattern useful.
// Prompt: Export the recognition state to an XML file after processing a single barcode image.
// Tags: barcode, qr, generation, recognition, export, xml, aspose.barcode, aspose.barcode.generation, aspose.barcode.recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR barcode, reads it, and exports the recognition state to an XML file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a temporary folder, generates a barcode image, reads it, exports the reader state, and prints detected barcodes.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for all generated files
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define paths for the barcode image and the exported XML state
        string barcodePath = Path.Combine(tempDir, "barcode.png");
        string xmlPath = Path.Combine(tempDir, "readerState.xml");

        // Generate a sample QR barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample123"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2f; // Set module size
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Initialize a reader that supports all barcode types
        BaseDecodeType decodeType = DecodeType.AllSupportedTypes;

        // Read the barcode and export the internal recognition state to an XML file
        using (var reader = new BarCodeReader(barcodePath, decodeType))
        {
            reader.BarcodeSettings.StripFNC = true; // Optional setting to ignore FNC characters
            reader.ExportToXml(xmlPath);            // Persist the reader's state
            Console.WriteLine($"Recognition state exported to: {xmlPath}");
        }

        // Optional: read and display detected barcodes for verification
        using (var reader = new BarCodeReader(barcodePath, decodeType))
        {
            var results = reader.ReadBarCodes();
            foreach (var result in results)
            {
                Console.WriteLine($"Detected {result.CodeTypeName}: {result.CodeText}");
            }
        }
    }
}