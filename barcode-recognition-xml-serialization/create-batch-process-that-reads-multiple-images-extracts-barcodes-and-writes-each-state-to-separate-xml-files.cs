// Title: Batch barcode generation, recognition, and XML export
// Description: Demonstrates how to generate multiple barcode images, read them in a batch, and export each recognition result to an XML file.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category, showcasing the use of BarcodeGenerator for creating barcodes, BarCodeReader for recognizing them, and ExportToXml for persisting recognition state. Typical use cases include automated scanning pipelines, bulk data extraction, and archival of barcode metadata. Developers often need to process many images efficiently and store results in a structured format such as XML.
// Prompt: Create a batch process that reads multiple images, extracts barcodes, and writes each state to separate XML files.
// Tags: barcode generation, barcode recognition, batch processing, xml export, aspose.barcode, code128, qr, datamatrix, pdf417, aztec

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch generation of barcode images, recognition of each image, and exporting the recognition state to XML files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, processes them, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch process
        string batchFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // List to hold generated image file paths
        List<string> imageFiles = new List<string>();

        // Sample data for barcode generation (symbology and corresponding text)
        var samples = new (BaseEncodeType encode, string text)[]
        {
            (EncodeTypes.Code128, "CODE128_SAMPLE"),
            (EncodeTypes.QR, "https://example.com"),
            (EncodeTypes.DataMatrix, "DM123456"),
            (EncodeTypes.Pdf417, "PDF417_SAMPLE_TEXT"),
            (EncodeTypes.Aztec, "AZTEC_SAMPLE")
        };

        // Generate barcode images and store their file paths
        foreach (var (encode, text) in samples)
        {
            string fileName = Path.Combine(batchFolder, $"{encode}_{Guid.NewGuid().ToString("N")}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(encode, text))
            {
                // Save the generated barcode as a PNG image
                generator.Save(fileName, BarCodeImageFormat.Png);
            }
            imageFiles.Add(fileName);
        }

        // Process each image: read barcodes and export recognition state to XML
        foreach (string imagePath in imageFiles)
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                continue;
            }

            // Determine the XML file name based on the image name
            string xmlPath = Path.ChangeExtension(imagePath, ".xml");

            try
            {
                using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
                {
                    // Perform recognition and obtain results
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // Export the reader state (including recognized barcodes) to XML
                    reader.ExportToXml(xmlPath);

                    Console.WriteLine($"Processed '{Path.GetFileName(imagePath)}' - {results.Length} barcode(s) found. XML saved to '{Path.GetFileName(xmlPath)}'.");
                }
            }
            catch (ArgumentException ex)
            {
                // Handle image loading failures gracefully
                Console.WriteLine($"Warning: Could not load image '{imagePath}'. {ex.Message}");
            }
        }

        // Cleanup: optionally delete the temporary folder and its contents
        // Comment out the following block if you want to inspect the generated files.
        try
        {
            Directory.Delete(batchFolder, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup failed: {ex.Message}");
        }
    }
}