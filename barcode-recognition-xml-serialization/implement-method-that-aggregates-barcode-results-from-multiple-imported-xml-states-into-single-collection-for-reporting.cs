// Title: Aggregate Barcode Results from Multiple XML Reader States
// Description: Demonstrates generating barcodes, exporting reader state to XML, importing those states, and aggregating the results into a single collection for reporting.
// Category-Description: This example belongs to the Aspose.BarCode reading and state management category. It showcases the use of BarcodeGenerator, BarCodeReader, ExportToXml, and ImportFromXml APIs to handle barcode data across multiple files. Developers often need to persist reader configurations, import them later, and combine results for batch processing or reporting scenarios.
// Prompt: Implement a method that aggregates barcode results from multiple imported XML states into a single collection for reporting.
// Tags: barcode, aggregation, xml, import, export, aspose.barcode, code128, reader, generator

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates aggregation of barcode results from multiple imported XML reader states.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates sample barcodes, exports reader states, aggregates results, and displays them.
    /// </summary>
    static void Main()
    {
        // Create a temporary working directory for generated images and XML files
        string workDir = Path.Combine(Path.GetTempPath(), "BarcodeAgg_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        // Sample data: generate barcodes, read them, and export reader state to XML
        int sampleCount = 3;
        var imagePaths = new List<string>();
        var xmlPaths = new List<string>();

        for (int i = 0; i < sampleCount; i++)
        {
            string codeText = $"CODE{i + 1}";
            string imagePath = Path.Combine(workDir, $"barcode_{i}.png");
            string xmlPath = Path.Combine(workDir, $"reader_{i}.xml");

            // Generate barcode image using Code128 symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }

            // Read barcode and export the reader state to XML
            using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                reader.ReadBarCodes(); // Populate FoundBarCodes
                reader.ExportToXml(xmlPath);
            }

            imagePaths.Add(imagePath);
            xmlPaths.Add(xmlPath);
        }

        // Aggregate results from imported XML states
        List<BarCodeResult> aggregatedResults = AggregateBarcodeResults(xmlPaths, imagePaths);

        // Reporting: output aggregated barcode results to console
        Console.WriteLine("Aggregated Barcode Results:");
        foreach (var result in aggregatedResults)
        {
            Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
        }

        // Cleanup (optional)
        // Directory.Delete(workDir, true);
    }

    /// <summary>
    /// Imports barcode reader states from XML files, reassigns the corresponding images, reads barcodes, and aggregates all results.
    /// </summary>
    /// <param name="xmlFiles">List of XML file paths containing exported reader states.</param>
    /// <param name="imageFiles">List of image file paths corresponding to each XML state.</param>
    /// <returns>A combined list of <see cref="BarCodeResult"/> objects from all imported states.</returns>
    static List<BarCodeResult> AggregateBarcodeResults(List<string> xmlFiles, List<string> imageFiles)
    {
        var allResults = new List<BarCodeResult>();

        for (int i = 0; i < xmlFiles.Count; i++)
        {
            string xmlPath = xmlFiles[i];
            string imagePath = imageFiles[i];

            // Skip iteration if either the XML or image file is missing
            if (!File.Exists(xmlPath) || !File.Exists(imagePath))
                continue;

            // Import reader settings from XML
            using (var reader = BarCodeReader.ImportFromXml(xmlPath))
            {
                // Set the image source (image path is not stored in the XML)
                reader.SetBarCodeImage(imagePath);

                // Read barcodes using the imported settings
                BarCodeResult[] results = reader.ReadBarCodes();

                if (results != null)
                {
                    allResults.AddRange(results);
                }
            }
        }

        return allResults;
    }
}