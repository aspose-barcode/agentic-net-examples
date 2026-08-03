// Title: Aggregate barcode results from multiple XML state files
// Description: Demonstrates exporting barcode generators to XML, importing them, and aggregating recognition results for reporting.
// Category-Description: This example belongs to the Aspose.BarCode XML state management category, showcasing how to use BarcodeGenerator.ExportToXml and BarcodeGenerator.ImportFromXml together with BarCodeReader. Developers often need to persist barcode generation settings, share them across services, and later batch‑process the generated barcodes for reporting or analytics. The snippet highlights key classes such as BarcodeGenerator, BarCodeReader, and DecodeType, useful for batch barcode processing scenarios.
// Prompt: Implement a method that aggregates barcode results from multiple imported XML states into a single collection for reporting.
// Tags: barcode symbology, generation, recognition, xml, aspose.barcode, batch processing

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates exporting barcode generators to XML, importing them, and aggregating
/// recognition results from the generated images for reporting purposes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates sample barcode generators, saves their state
    /// to XML files, imports each state, generates barcode images, reads the barcodes,
    /// and aggregates the results into a single collection.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define a working directory for temporary XML state files.
        string workDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(workDir))
        {
            Directory.CreateDirectory(workDir);
        }

        // Prepare sample barcode definitions to be exported as XML states.
        var samples = new List<(BaseEncodeType type, string text, string fileName)>
        {
            (EncodeTypes.Code128, "ABC123", "code128.xml"),
            (EncodeTypes.QR, "Hello World", "qr.xml"),
            (EncodeTypes.DataMatrix, "DM123", "datamatrix.xml")
        };

        // Export each barcode generator's configuration to an individual XML file.
        foreach (var sample in samples)
        {
            string xmlPath = Path.Combine(workDir, sample.fileName);
            using (var generator = new BarcodeGenerator(sample.type, sample.text))
            {
                generator.ExportToXml(xmlPath);
            }
        }

        // Aggregate barcode results from all imported XML states.
        var aggregatedResults = new List<BarCodeResult>();
        string[] xmlFiles = Directory.GetFiles(workDir, "*.xml");
        foreach (string xmlFile in xmlFiles)
        {
            // Import the generator configuration from the XML file.
            using (var generator = BarcodeGenerator.ImportFromXml(xmlFile))
            {
                // Generate the barcode image based on the imported configuration.
                using (var image = generator.GenerateBarCodeImage())
                {
                    // Initialize a reader that can decode all supported barcode types.
                    using (var reader = new BarCodeReader(image, DecodeType.AllSupportedTypes))
                    {
                        // Read all barcodes found in the image and add them to the collection.
                        foreach (var result in reader.ReadBarCodes())
                        {
                            aggregatedResults.Add(result);
                        }
                    }
                }
            }
        }

        // Simple console reporting of the aggregated results.
        Console.WriteLine($"Aggregated {aggregatedResults.Count} barcode result(s) from {xmlFiles.Length} XML state file(s).");
        int index = 1;
        foreach (var result in aggregatedResults)
        {
            Console.WriteLine($"{index++}: Type = {result.CodeTypeName}, Text = {result.CodeText}");
        }
    }
}