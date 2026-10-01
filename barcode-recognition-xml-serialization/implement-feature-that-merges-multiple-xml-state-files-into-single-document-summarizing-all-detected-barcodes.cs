// Title: Merge Multiple Barcode XML State Files into a Summary Document
// Description: Demonstrates how to combine several XML files containing barcode detection results into a single summary XML document.
// Category-Description: This example belongs to the Aspose.BarCode XML handling category, showcasing how to work with barcode result state files using System.Xml.Linq. It illustrates creating temporary files, reading <BarCodeResult> elements, and aggregating them into a <BarcodesSummary> root. Developers often need to merge detection outputs for reporting or further processing, and this pattern uses Aspose.BarCode together with standard .NET I/O and LINQ to XML APIs.
// Prompt: Implement a feature that merges multiple XML state files into a single document summarizing all detected barcodes.
// Tags: barcode, xml, merge, summary, aspose.barcode, xdocument, file-io

using System;
using System.IO;
using System.Xml.Linq;
using System.Collections.Generic;
using Aspose.BarCode; // Required namespace for Aspose.BarCode usage

/// <summary>
/// Provides a demo that merges multiple barcode result XML files into a single summary document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates sample XML state files, merges them, and displays the result.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for the demo
        string tempFolder = Path.Combine(Path.GetTempPath(), "MergeXmlDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Prepare sample XML state files (simulating barcode detection results)
        var sampleData = new List<(string CodeText, string CodeType)>
        {
            ("12345", "Code128"),
            ("ABCDEF", "QR"),
            ("9876543210", "DataMatrix")
        };

        var inputFiles = new List<string>();
        int index = 1;
        foreach (var (codeText, codeType) in sampleData)
        {
            // Build file path for the current sample
            string filePath = Path.Combine(tempFolder, $"Result{index}.xml");

            // Create XML document representing a single barcode result
            var doc = new XDocument(
                new XElement("BarCodeResult",
                    new XElement("CodeText", codeText),
                    new XElement("CodeTypeName", codeType)
                )
            );

            // Write XML to file
            using (var writer = new StreamWriter(filePath, false))
            {
                doc.Save(writer);
            }

            inputFiles.Add(filePath);
            index++;
        }

        // Define output summary file
        string summaryFile = Path.Combine(tempFolder, "Summary.xml");

        // Merge the XML state files into a single summary document
        MergeXmlFiles(inputFiles.ToArray(), summaryFile);

        // Output the merged summary to console
        Console.WriteLine("Merged summary XML:");
        Console.WriteLine(File.ReadAllText(summaryFile));

        // Clean up temporary files (optional)
        // Comment out the following block if you want to inspect the files after execution
        try
        {
            foreach (var file in inputFiles)
                File.Delete(file);
            File.Delete(summaryFile);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failures are non‑critical for the demo
        }
    }

    /// <summary>
    /// Merges multiple barcode result XML files into a single summary XML.
    /// Each input file is expected to contain a &lt;BarCodeResult&gt; element with
    /// &lt;CodeText&gt; and &lt;CodeTypeName&gt; child elements.
    /// The output file will contain a root &lt;BarcodesSummary&gt; element with
    /// a &lt;BarCode&gt; entry for each detected barcode.
    /// </summary>
    /// <param name="inputFiles">Array of input XML file paths.</param>
    /// <param name="outputFile">Path of the merged summary XML file.</param>
    static void MergeXmlFiles(string[] inputFiles, string outputFile)
    {
        var summaryRoot = new XElement("BarcodesSummary");

        foreach (string file in inputFiles)
        {
            // Verify that the file exists before attempting to load it
            if (!File.Exists(file))
            {
                Console.WriteLine($"Warning: File not found – {file}");
                continue;
            }

            XDocument doc;
            try
            {
                // Load the XML document from the file
                doc = XDocument.Load(file);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Warning: Failed to load XML from {file}: {ex.Message}");
                continue;
            }

            // Expect the root element to be <BarCodeResult>
            XElement resultElem = doc.Root;
            if (resultElem == null || resultElem.Name != "BarCodeResult")
            {
                Console.WriteLine($"Warning: Unexpected XML structure in {file}");
                continue;
            }

            // Extract barcode data
            string codeText = resultElem.Element("CodeText")?.Value ?? string.Empty;
            string codeType = resultElem.Element("CodeTypeName")?.Value ?? string.Empty;

            // Create a <BarCode> element for the summary
            var barcodeElem = new XElement("BarCode",
                new XElement("CodeText", codeText),
                new XElement("CodeType", codeType)
            );

            // Add the barcode entry to the summary root
            summaryRoot.Add(barcodeElem);
        }

        // Build the final summary document with XML declaration
        var summaryDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), summaryRoot);

        // Write the merged summary XML to the output file
        using (var writer = new StreamWriter(outputFile, false))
        {
            summaryDoc.Save(writer);
        }
    }
}