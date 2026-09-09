// Title: Batch barcode recognition and XML state export
// Description: Demonstrates how to generate sample barcode images, read them in a batch, recognize multiple symbologies, and export each recognition state to an XML file.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category, showcasing the use of BarcodeGenerator for creating barcodes, BarCodeReader for multi‑symbology recognition, and ExportToXml for persisting recognition results. Developers often need to process large sets of images, extract barcode data, and store detailed scan information for auditing or downstream systems.
// Prompt: Create a batch process that reads multiple images, extracts barcodes, and writes each state to separate XML files.
// Tags: barcode, batch, recognition, xml, export, code128, qr, datamatrix, aztec, pdf417, aspose.barcode, generation, reader

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates batch generation, recognition, and XML export of barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates sample barcode images, processes them, and writes recognition state to XML files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch process
        string batchFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Prepare a list to hold paths of generated barcode images
        var imageFiles = new List<string>();

        // ---------- Generate sample Code128 barcode ----------
        string code128Path = Path.Combine(batchFolder, "code128.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(code128Path, BarCodeImageFormat.Png);
        }
        imageFiles.Add(code128Path);

        // ---------- Generate sample QR barcode ----------
        string qrPath = Path.Combine(batchFolder, "qr.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            generator.Save(qrPath, BarCodeImageFormat.Png);
        }
        imageFiles.Add(qrPath);

        // ---------- Generate sample DataMatrix barcode ----------
        string dmPath = Path.Combine(batchFolder, "datamatrix.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "DM123"))
        {
            generator.Save(dmPath, BarCodeImageFormat.Png);
        }
        imageFiles.Add(dmPath);

        // ---------- Process each image: recognize barcodes and export state to XML ----------
        foreach (string imagePath in imageFiles)
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                continue;
            }

            try
            {
                // Initialize reader with the desired symbologies
                using (var reader = new BarCodeReader(
                    imagePath,
                    DecodeType.Code128,
                    DecodeType.QR,
                    DecodeType.DataMatrix,
                    DecodeType.Aztec,
                    DecodeType.Pdf417))
                {
                    // Perform recognition
                    BarCodeResult[] results = reader.ReadBarCodes();

                    Console.WriteLine($"Processed '{Path.GetFileName(imagePath)}' - Barcodes found: {results.Length}");
                    foreach (BarCodeResult result in reader.FoundBarCodes)
                    {
                        Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                    }

                    // Export the full recognition state to an XML file
                    string xmlPath = Path.ChangeExtension(imagePath, ".xml");
                    reader.ExportToXml(xmlPath);
                    Console.WriteLine($"  Exported state to: {xmlPath}");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Failed to process '{imagePath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}