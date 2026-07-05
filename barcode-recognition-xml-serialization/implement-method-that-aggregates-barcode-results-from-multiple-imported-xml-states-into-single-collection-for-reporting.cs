// Title: Aggregate barcode results from multiple XML states
// Description: Demonstrates importing barcode generator XML, generating images, recognizing barcodes, and aggregating results for reporting.
// Prompt: Implement a method that aggregates barcode results from multiple imported XML states into a single collection for reporting.
// Tags: barcode symbology, aggregation, xml import, aspose.barcode, console output

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Sample program that creates barcode generators, exports them to XML,
/// imports the XML back, reads the generated barcodes, and aggregates the results.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Prepares sample XML states, aggregates barcode results,
    /// and writes a simple report to the console.
    /// </summary>
    static void Main()
    {
        // Prepare a list to hold the XML streams representing exported barcode generators.
        var xmlStreams = new List<MemoryStream>();

        // ----- First barcode: Code128 with text "ABC123" -----
        using (var gen1 = new BarcodeGenerator(EncodeTypes.Code128, "ABC123"))
        {
            // Export the generator configuration to a memory stream as XML.
            var ms1 = new MemoryStream();
            gen1.ExportToXml(ms1);
            ms1.Position = 0; // Reset stream position for later reading.
            xmlStreams.Add(ms1);
        }

        // ----- Second barcode: QR with text "Hello World" -----
        using (var gen2 = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Export the generator configuration to a memory stream as XML.
            var ms2 = new MemoryStream();
            gen2.ExportToXml(ms2);
            ms2.Position = 0; // Reset stream position for later reading.
            xmlStreams.Add(ms2);
        }

        // Aggregate barcode results from the imported XML states.
        List<BarCodeResult> aggregatedResults = AggregateBarcodeResults(xmlStreams);

        // Report the aggregated results to the console.
        Console.WriteLine("Aggregated Barcode Results:");
        foreach (var result in aggregatedResults)
        {
            Console.WriteLine($"Type: {result.CodeTypeName}, CodeText: {result.CodeText}");
        }

        // Clean up streams to release resources.
        foreach (var stream in xmlStreams)
        {
            stream.Dispose();
        }
    }

    /// <summary>
    /// Imports barcode generators from XML streams, generates barcode images,
    /// recognizes the barcodes, and aggregates all <see cref="BarCodeResult"/> objects into a single collection.
    /// </summary>
    /// <param name="xmlStreams">Collection of streams containing exported barcode generator XML.</param>
    /// <returns>List of <see cref="BarCodeResult"/> objects from all imported states.</returns>
    static List<BarCodeResult> AggregateBarcodeResults(IEnumerable<MemoryStream> xmlStreams)
    {
        var allResults = new List<BarCodeResult>();

        // Process each XML stream individually.
        foreach (var xmlStream in xmlStreams)
        {
            // Import the BarcodeGenerator from its XML representation.
            using (var generator = BarcodeGenerator.ImportFromXml(xmlStream))
            {
                // Generate the barcode image in memory.
                using (var image = generator.GenerateBarCodeImage())
                {
                    // Recognize the barcode(s) from the generated image.
                    using (var reader = new BarCodeReader(image, DecodeType.AllSupportedTypes))
                    {
                        // Read all barcodes found in the image.
                        BarCodeResult[] results = reader.ReadBarCodes();

                        // Add the results to the aggregated collection, if any were found.
                        if (results != null)
                        {
                            allResults.AddRange(results);
                        }
                    }
                }
            }
        }

        return allResults;
    }
}