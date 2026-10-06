// Title: Barcode XML aggregation example
// Description: Demonstrates generating barcodes, exporting their recognition state to XML, and aggregating results from multiple XML files into a single collection for reporting.
// Category-Description: This example belongs to the Aspose.BarCode operations category covering barcode generation, recognition, and state persistence. It showcases key API classes such as BarcodeGenerator, BarCodeReader, and methods like ExportToXml and ImportFromXml, which developers use to serialize and later reprocess barcode recognition data across sessions or systems.
// Prompt: Implement a method that aggregates barcode results from multiple imported XML states into a single collection for reporting.
// Tags: barcode, xml, aggregation, generation, recognition, aspose.barcode, exporttoxml, importfromxml

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation, XML state export, import, and aggregation of results.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, exports reader state to XML, imports the states, aggregates results, and displays them.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated images and XML files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeXmlAgg_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample barcodes to generate (text and symbology)
        var samples = new (string CodeText, BaseEncodeType Encode)[]
        {
            ("HelloWorld", EncodeTypes.QR),
            ("123456789012", EncodeTypes.Code128),
            ("ABCD1234", EncodeTypes.DataMatrix)
        };

        // Generate barcode images, read them, and export the reader state to XML files
        for (int i = 0; i < samples.Length; i++)
        {
            string imagePath = Path.Combine(tempFolder, $"barcode_{i}.png");
            string xmlPath = Path.Combine(tempFolder, $"reader_{i}.xml");

            // Generate barcode image using the specified symbology and text
            using (var generator = new BarcodeGenerator(samples[i].Encode, samples[i].CodeText))
            {
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }

            // Read the generated barcode and export its recognition state to XML
            using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                // Perform reading (results may be used later)
                reader.ReadBarCodes();

                // Export the recognition state to an XML file
                reader.ExportToXml(xmlPath);
            }
        }

        // Aggregate results from the imported XML states into a single collection
        List<BarCodeResult> aggregatedResults = new List<BarCodeResult>();

        for (int i = 0; i < samples.Length; i++)
        {
            string xmlPath = Path.Combine(tempFolder, $"reader_{i}.xml");
            string imagePath = Path.Combine(tempFolder, $"barcode_{i}.png");

            // Ensure both the XML and image files exist before attempting import
            if (!File.Exists(xmlPath) || !File.Exists(imagePath))
                continue; // Skip missing files gracefully

            // Import the previously exported reader state from XML
            using (var importedReader = BarCodeReader.ImportFromXml(xmlPath))
            {
                // Associate the original image with the imported reader instance
                importedReader.SetBarCodeImage(imagePath);

                // Read barcodes using the imported state
                BarCodeResult[] results = importedReader.ReadBarCodes();

                // Add each result to the aggregated collection
                foreach (var result in results)
                {
                    aggregatedResults.Add(result);
                }
            }
        }

        // Report the aggregated barcode results to the console
        Console.WriteLine("Aggregated Barcode Results:");
        foreach (var result in aggregatedResults)
        {
            Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
        }

        // Clean up the temporary folder (optional). Errors are ignored to avoid breaking the flow.
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}