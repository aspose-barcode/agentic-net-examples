// Title: Merge Multiple Barcode Recognition XML States into a Summary Document
// Description: Demonstrates generating barcodes, recognizing them, exporting each recognition state to XML, and merging those XML files into a single summary that lists all detected barcodes.
// Category-Description: This example belongs to the Aspose.BarCode operations category covering barcode generation, recognition, and state management. It showcases the use of BarcodeGenerator, BarCodeReader, ExportToXml, and ImportFromXml APIs to create images, read them, persist recognition state, and later re‑import that state for further processing. Developers often need to batch‑process many barcodes, keep a record of detection results, and produce consolidated reports—this snippet provides a clear pattern for those scenarios.
// Prompt: Implement a feature that merges multiple XML state files into a single document summarizing all detected barcodes.
// Tags: barcode symbology, generation, recognition, xml, merge, summary, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates merging multiple barcode recognition XML state files into a single summary document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates sample barcodes, reads them, exports recognition state to XML,
    /// imports the states, and creates a combined summary XML file.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a unique temporary folder for all generated files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeMergeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample barcodes to generate (type and text)
        var samples = new List<(BaseEncodeType encode, string text)>
        {
            (EncodeTypes.Code128, "ABC123"),
            (EncodeTypes.QR, "https://example.com"),
            (EncodeTypes.Pdf417, "Sample PDF417 text")
        };

        var xmlFiles = new List<string>();   // Holds paths to exported XML state files
        var imageFiles = new List<string>(); // Holds paths to generated barcode images

        // Generate each barcode, recognize it, and export the recognition state to XML
        foreach (var (encode, text) in samples)
        {
            // Build unique file names for the image and its corresponding XML state
            string imagePath = Path.Combine(tempFolder, $"{encode}_{Guid.NewGuid().ToString("N")}.png");
            string xmlPath = Path.Combine(tempFolder, $"{encode}_{Guid.NewGuid().ToString("N")}.xml");

            // ---- Barcode generation ----
            using (var generator = new BarcodeGenerator(encode, text))
            {
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }

            // ---- Barcode recognition and state export ----
            using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                // Force a read to ensure detection before exporting state
                var _ = reader.ReadBarCodes();

                // Export the internal recognition state to an XML file
                reader.ExportToXml(xmlPath);
            }

            // Store file paths for later merging
            xmlFiles.Add(xmlPath);
            imageFiles.Add(imagePath);
        }

        // ---- Merge all XML states into a single summary document ----
        var summaryDoc = new XDocument(new XElement("BarcodesSummary"));

        for (int i = 0; i < xmlFiles.Count; i++)
        {
            string xmlPath = xmlFiles[i];
            string imagePath = imageFiles[i];

            // Import the previously saved recognition state
            using (var reader = BarCodeReader.ImportFromXml(xmlPath))
            {
                // Associate the original image with the imported state
                reader.SetBarCodeImage(imagePath);

                // Read barcodes using the imported state
                var results = reader.ReadBarCodes();

                foreach (var result in results)
                {
                    // Add a new entry to the summary XML for each detected barcode
                    summaryDoc.Root.Add(new XElement("Barcode",
                        new XAttribute("SourceFile", Path.GetFileName(imagePath)),
                        new XElement("CodeType", result.CodeTypeName),
                        new XElement("CodeText", result.CodeText)));

                    // Write a brief line to the console for immediate feedback
                    Console.WriteLine($"File: {Path.GetFileName(imagePath)} - {result.CodeTypeName}: {result.CodeText}");
                }
            }
        }

        // Save the combined summary XML to the temporary folder
        string summaryPath = Path.Combine(tempFolder, "CombinedSummary.xml");
        summaryDoc.Save(summaryPath);
        Console.WriteLine($"Combined summary saved to: {summaryPath}");
    }
}