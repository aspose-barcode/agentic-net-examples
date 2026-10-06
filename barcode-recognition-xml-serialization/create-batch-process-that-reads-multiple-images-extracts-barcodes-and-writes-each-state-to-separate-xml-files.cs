// Title: Batch barcode generation, recognition, and XML export example
// Description: Demonstrates creating multiple barcode images, reading them to extract barcode data, and exporting each recognition state to an XML file.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category, showcasing how to use BarcodeGenerator for image creation, BarCodeReader for multi‑symbology detection, and the ExportToXml method to persist recognition results. Typical use cases include automated scanning pipelines, bulk verification, and archival of barcode data. Developers often need to generate test images, process large sets of files, and store results in a structured format for downstream systems.
// Prompt: Create a batch process that reads multiple images, extracts barcodes, and writes each state to separate XML files.
// Tags: barcode generation, barcode recognition, batch processing, xml export, aspose.barcode, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch generation of barcodes, recognition of those barcodes, and exporting the recognition state to XML files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample barcode images, reads them, and writes recognition results to XML.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BatchBarcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample data for barcode generation (file name, text, and symbology)
        var samples = new List<(string fileName, string codeText, BaseEncodeType encodeType)>
        {
            ("qr1.png", "Sample QR 1", EncodeTypes.QR),
            ("code128_1.png", "ABC12345", EncodeTypes.Code128),
            ("datamatrix_1.png", "DM123", EncodeTypes.DataMatrix)
        };

        // -----------------------------------------------------------------
        // Generate barcode images based on the sample data
        // -----------------------------------------------------------------
        foreach (var sample in samples)
        {
            string imagePath = Path.Combine(tempFolder, sample.fileName);
            using (BarcodeGenerator generator = new BarcodeGenerator(sample.encodeType, sample.codeText))
            {
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }
        }

        // -----------------------------------------------------------------
        // Process each generated image: read barcodes and export recognition state to XML
        // -----------------------------------------------------------------
        foreach (var sample in samples)
        {
            string imagePath = Path.Combine(tempFolder, sample.fileName);
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                continue;
            }

            string xmlPath = Path.Combine(tempFolder, Path.GetFileNameWithoutExtension(sample.fileName) + ".xml");
            try
            {
                using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
                {
                    // Read all barcodes present in the image
                    BarCodeResult[] results = reader.ReadBarCodes();
                    Console.WriteLine($"Processed {sample.fileName}: {results.Length} barcode(s) detected.");

                    // Output each detected barcode's type and text
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                    }

                    // Export the complete reader state (including detected barcodes) to an XML file
                    reader.ExportToXml(xmlPath);
                    Console.WriteLine($"Exported recognition state to: {xmlPath}");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Failed to read {sample.fileName}: {ex.Message}");
            }
        }

        // -----------------------------------------------------------------
        // Cleanup: delete the temporary folder and its contents
        // -----------------------------------------------------------------
        try
        {
            Directory.Delete(tempFolder, true);
            Console.WriteLine("Temporary files cleaned up.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup failed: {ex.Message}");
        }
    }
}