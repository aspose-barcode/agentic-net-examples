using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates importing barcode generator settings from XML files,
/// generating barcode images, reading them, and aggregating the results.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define sample XML state files (replace with real paths as needed)
        var xmlFiles = new List<string>
        {
            "state1.xml",
            "state2.xml",
            "state3.xml"
        };

        // Process each XML file and collect all detected barcode results
        var aggregatedResults = AggregateBarcodeResults(xmlFiles);

        // Output the total number of barcodes detected
        Console.WriteLine($"Total barcodes detected: {aggregatedResults.Count}");

        // List each detected barcode's type and text
        foreach (var result in aggregatedResults)
        {
            Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
        }
    }

    /// <summary>
    /// Imports barcode generators from the given XML files, generates images,
    /// reads barcodes from those images and aggregates all results.
    /// </summary>
    /// <param name="xmlPaths">Paths to XML files containing barcode generator settings.</param>
    /// <returns>List of all detected <see cref="BarCodeResult"/> objects.</returns>
    static List<BarCodeResult> AggregateBarcodeResults(IEnumerable<string> xmlPaths)
    {
        // Collection to hold all barcode results from all files
        var allResults = new List<BarCodeResult>();

        // Iterate over each provided XML path
        foreach (var path in xmlPaths)
        {
            // Verify the XML file exists before attempting to import
            if (!File.Exists(path))
            {
                Console.WriteLine($"Warning: XML file not found – skipping '{path}'.");
                continue;
            }

            // Import generator settings from the XML file
            using (var generator = BarcodeGenerator.ImportFromXml(path))
            {
                // Create a memory stream to hold the generated barcode image
                using (var imageStream = new MemoryStream())
                {
                    // Save the barcode image as PNG into the memory stream
                    generator.Save(imageStream, BarCodeImageFormat.Png);
                    // Reset stream position to the beginning for reading
                    imageStream.Position = 0;

                    // Initialize a barcode reader to decode all supported types
                    using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
                    {
                        // Read all barcodes present in the image
                        var results = reader.ReadBarCodes();

                        // If any barcodes were detected, add them to the aggregate list
                        if (results != null)
                        {
                            allResults.AddRange(results);
                        }
                    }
                }
            }
        }

        // Return the complete list of detected barcode results
        return allResults;
    }
}