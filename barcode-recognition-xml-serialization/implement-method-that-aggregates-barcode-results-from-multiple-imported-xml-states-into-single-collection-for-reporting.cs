// Title: Aggregate Barcode Results from Multiple XML States
// Description: Demonstrates generating barcodes, exporting their reader state to XML, and then aggregating results from those XML files into a single collection for reporting.
// Category-Description: This example belongs to the Aspose.BarCode processing category, showcasing how to use BarcodeGenerator, BarCodeReader, and XML import/export APIs. Typical use cases include batch barcode processing, state persistence, and consolidated reporting across multiple scans. Developers often need to generate barcodes, read them, store reader states, and later re-import for analysis or reporting.
// Prompt: Implement a method that aggregates barcode results from multiple imported XML states into a single collection for reporting.
// Tags: barcode,qr,code128,aggregation,xml,barcode-generation,barcode-recognition

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates aggregation of barcode results from multiple imported XML states.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, exports reader states to XML, re-imports them, and aggregates results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated files
        string tempFolder = Path.Combine(Path.GetTempPath(), "Agg_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample data for barcode generation (type and text)
        var samples = new List<(BaseEncodeType encode, string text)>
        {
            (EncodeTypes.QR, "SampleQR1"),
            (EncodeTypes.Code128, "Sample128")
        };

        var imagePaths = new List<string>();
        var xmlPaths = new List<string>();

        // Generate barcodes, read them, and export each reader's state to XML
        for (int i = 0; i < samples.Count; i++)
        {
            string imagePath = Path.Combine(tempFolder, $"barcode{i}.png");
            string xmlPath = Path.Combine(tempFolder, $"reader{i}.xml");

            // Generate barcode image and save as PNG
            using (var generator = new BarcodeGenerator(samples[i].encode, samples[i].text))
            {
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }

            // Choose appropriate decode type based on the generated symbology
            BaseDecodeType decodeType = samples[i].encode == EncodeTypes.QR ? DecodeType.QR :
                                        samples[i].encode == EncodeTypes.Code128 ? DecodeType.Code128 :
                                        DecodeType.AllSupportedTypes;

            // Read the barcode and export the reader's internal state to XML
            using (var reader = new BarCodeReader(imagePath, decodeType))
            {
                reader.ReadBarCodes(); // Perform initial read to populate results
                reader.ExportToXml(xmlPath);
            }

            imagePaths.Add(imagePath);
            xmlPaths.Add(xmlPath);
        }

        // Aggregate results from the imported XML states
        var aggregatedResults = new List<BarCodeResult>();

        for (int i = 0; i < xmlPaths.Count; i++)
        {
            string xmlPath = xmlPaths[i];
            string imagePath = imagePaths[i];

            // Import reader state from XML and associate the original image
            using (var importedReader = BarCodeReader.ImportFromXml(xmlPath))
            {
                importedReader.SetBarCodeImage(imagePath);
                BarCodeResult[] results = importedReader.ReadBarCodes();

                // Add any found results to the aggregated collection
                if (results != null)
                {
                    aggregatedResults.AddRange(results);
                }
            }
        }

        // Output aggregated barcode results
        Console.WriteLine("Aggregated Barcode Results:");
        foreach (var result in aggregatedResults)
        {
            Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
        }

        // Cleanup temporary files (optional)
        try
        {
            foreach (var file in Directory.GetFiles(tempFolder))
            {
                File.Delete(file);
            }
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failures should not affect program outcome
        }
    }
}