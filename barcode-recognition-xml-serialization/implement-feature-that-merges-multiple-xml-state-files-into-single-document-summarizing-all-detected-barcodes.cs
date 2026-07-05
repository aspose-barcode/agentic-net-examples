// Title: Merge multiple barcode state XML files into a single summary document
// Description: Demonstrates importing Aspose.BarCode.BarCodeReader state from several XML files, extracting detected barcodes, and writing a consolidated XML summary.
// Prompt: Implement a feature that merges multiple XML state files into a single document summarizing all detected barcodes.
// Tags: barcode symbology, import, xml, summary, aspose.barcode, csharp

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.Generation;

/// <summary>
/// Program that merges barcode detection results from multiple Aspose.BarCode state XML files into a single summary XML document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Reads each XML state file, extracts barcode type and text, and writes a consolidated summary.
    /// </summary>
    static void Main()
    {
        // Define the paths to the XML state files (replace with actual file locations as needed)
        string[] xmlFiles = { "state1.xml", "state2.xml", "state3.xml" };

        // List that will hold the combined barcode type and text pairs from all files
        var mergedBarcodes = new List<(string Type, string CodeText)>();

        // Iterate over each XML file to import its BarCodeReader state
        foreach (string xmlPath in xmlFiles)
        {
            // Verify that the file exists before attempting to import
            if (!File.Exists(xmlPath))
            {
                Console.WriteLine($"Warning: File not found - {xmlPath}");
                continue;
            }

            // Import the BarCodeReader state from the XML file
            using (BarCodeReader reader = BarCodeReader.ImportFromXml(xmlPath))
            {
                // If the import fails, report and skip to the next file
                if (reader == null)
                {
                    Console.WriteLine($"Warning: Failed to import {xmlPath}");
                    continue;
                }

                // Read barcodes from the imported state; this populates FoundBarCodes if needed
                foreach (var result in reader.ReadBarCodes())
                {
                    // Add only valid results (non‑null and with a non‑empty CodeText) to the merged list
                    if (result != null && !string.IsNullOrEmpty(result.CodeText))
                    {
                        mergedBarcodes.Add((result.CodeTypeName, result.CodeText));
                    }
                }
            }
        }

        // Build a summary XML document containing all collected barcode information
        var summaryDoc = new XDocument(
            new XElement("Barcodes",
                // Attribute indicating the total number of barcodes found across all files
                new XAttribute("TotalCount", mergedBarcodes.Count),
                // Timestamp of when the summary was generated (ISO 8601 format)
                new XElement("GeneratedOn", DateTime.UtcNow.ToString("o")),
                // Container for individual barcode entries
                new XElement("Items",
                    new List<XElement>(mergedBarcodes.ConvertAll(bc =>
                        new XElement("BarCode",
                            new XAttribute("Type", bc.Type),
                            new XAttribute("CodeText", bc.CodeText)))))));

        // Define the output path for the merged summary XML
        string outputPath = "merged_summary.xml";

        // Save the summary document to disk
        summaryDoc.Save(outputPath);
        Console.WriteLine($"Merged summary saved to {outputPath}");
    }
}