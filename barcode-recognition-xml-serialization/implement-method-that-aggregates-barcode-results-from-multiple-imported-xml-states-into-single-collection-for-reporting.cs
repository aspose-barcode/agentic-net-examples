// Title: Aggregate Barcode Results from Multiple XML States
// Description: Demonstrates how to import barcode reader configurations from XML and combine the resulting BarCodeResult objects into a single collection for reporting.
// Category-Description: This example belongs to the Aspose.BarCode XML import/export category, showcasing the use of BarCodeReader.ImportFromXml, ExportToXml, and BarcodeGenerator. Typical use cases include persisting barcode reading sessions, batch processing, and consolidating results across multiple images. Developers often need to aggregate results for analytics, reporting, or further processing.
// Prompt: Implement a method that aggregates barcode results from multiple imported XML states into a single collection for reporting.
// Tags: barcode, aggregation, xml, import, export, aspose.barcode, barcodereader, barcodegenerator, code128, png, resultcollection

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides methods to generate barcodes, read them, export/import reader state as XML, and aggregate results.
/// </summary>
class Program
{
    /// <summary>
    /// Aggregates barcode results from multiple XML states.
    /// </summary>
    /// <param name="xmlContents">List of XML strings exported from <c>BarCodeReader.ExportToXml()</c>.</param>
    /// <param name="imagePaths">Corresponding image file paths used for each reader.</param>
    /// <returns>A combined list of <c>BarCodeResult</c> objects.</returns>
    static List<BarCodeResult> AggregateBarcodeResults(List<string> xmlContents, List<string> imagePaths)
    {
        if (xmlContents == null) throw new ArgumentException(nameof(xmlContents));
        if (imagePaths == null) throw new ArgumentException(nameof(imagePaths));
        if (xmlContents.Count != imagePaths.Count) throw new ArgumentException("XML and image collections must have the same count.");

        var aggregated = new List<BarCodeResult>();

        // Process each XML state together with its image
        for (int i = 0; i < xmlContents.Count; i++)
        {
            string xml = xmlContents[i];
            string imagePath = imagePaths[i];

            // Load the XML into a memory stream
            using (var xmlStream = new MemoryStream())
            {
                using (var writer = new StreamWriter(xmlStream, System.Text.Encoding.UTF8, 1024, leaveOpen: true))
                {
                    writer.Write(xml);
                }
                xmlStream.Position = 0;

                // Import the reader configuration from the XML stream
                using (var importedReader = BarCodeReader.ImportFromXml(xmlStream))
                {
                    // Assign the image source for this reader instance
                    importedReader.SetBarCodeImage(imagePath);

                    // Read barcodes and add each result to the aggregated list
                    foreach (var result in importedReader.ReadBarCodes())
                    {
                        aggregated.Add(result);
                    }
                }
            }
        }

        return aggregated;
    }

    /// <summary>
    /// Entry point that creates sample barcodes, exports reader state to XML, aggregates results, and displays them.
    /// </summary>
    static void Main()
    {
        // Create a temporary working folder for generated images
        string workFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        var imagePaths = new List<string>();
        var xmlContents = new List<string>();

        // Generate sample barcodes, read them, and export each reader state to XML
        for (int i = 1; i <= 3; i++)
        {
            string codeText = $"Sample{i}";
            string imagePath = Path.Combine(workFolder, $"barcode{i}.png");

            // Generate a barcode image and save as PNG
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }

            imagePaths.Add(imagePath);

            // Read the generated barcode and export the reader state to XML
            using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                // Force reading to populate results
                foreach (var _ in reader.ReadBarCodes()) { }

                // Export results and settings to an XML string
                using (var xmlStream = new MemoryStream())
                {
                    reader.ExportToXml(xmlStream);
                    xmlStream.Position = 0;
                    using (var sr = new StreamReader(xmlStream, leaveOpen: true))
                    {
                        string xml = sr.ReadToEnd();
                        xmlContents.Add(xml);
                    }
                }
            }
        }

        // Aggregate results from the exported XML states
        List<BarCodeResult> aggregatedResults = AggregateBarcodeResults(xmlContents, imagePaths);

        // Output aggregated results to the console
        Console.WriteLine("Aggregated Barcode Results:");
        foreach (var result in aggregatedResults)
        {
            Console.WriteLine($"CodeText: {result.CodeText}, Type: {result.CodeTypeName}, Quality: {result.ReadingQuality}");
        }

        // Clean up temporary files and folder
        foreach (var path in imagePaths)
        {
            if (File.Exists(path))
                File.Delete(path);
        }
        Directory.Delete(workFolder, true);
    }
}