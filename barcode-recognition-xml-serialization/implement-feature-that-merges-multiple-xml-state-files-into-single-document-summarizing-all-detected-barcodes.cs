// Title: Merge Multiple XML State Files into a Single Barcode Summary
// Description: Demonstrates merging several XML state files that contain detected barcode information into one consolidated summary document.
// Category-Description: This example belongs to the Aspose.BarCode file handling category, illustrating how to work with barcode state XML files using standard .NET XML APIs. It shows typical use cases such as aggregating results from multiple scans, generating a unified report, and preparing data for further processing. Developers often need to read, combine, and export barcode metadata, leveraging classes like BarCodeReader, BarCodeGenerator, and XDocument.
// Prompt: Implement a feature that merges multiple XML state files into a single document summarizing all detected barcodes.
// Tags: barcode symbology, merge, xml, summary, aspose.barcode, file-io

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

/// <summary>
/// Provides a console application that merges multiple XML state files containing barcode information
/// into a single summary XML document. The example creates sample state files, reads them,
/// aggregates the barcode entries, and writes the combined result to disk.
/// </summary>
class Program
{
    /// <summary>
    /// Simple model representing a detected barcode with its type and text.
    /// </summary>
    class BarcodeInfo
    {
        public string CodeTypeName { get; set; }
        public string CodeText { get; set; }
    }

    /// <summary>
    /// Entry point of the application. Generates sample XML state files, merges them,
    /// and saves a consolidated summary document.
    /// </summary>
    static void Main()
    {
        // Define the folder that will hold the sample XML state files.
        string stateFolder = "states";

        // Ensure the folder exists.
        if (!Directory.Exists(stateFolder))
        {
            Directory.CreateDirectory(stateFolder);
        }

        // --------------------------------------------------------------------
        // Generate a few sample XML state files.
        // In a real scenario these files would already exist on disk.
        // --------------------------------------------------------------------
        GenerateSampleStateFile(Path.Combine(stateFolder, "state1.xml"), new[]
        {
            new BarcodeInfo { CodeTypeName = "Code128", CodeText = "ABC123" },
            new BarcodeInfo { CodeTypeName = "QR", CodeText = "https://example.com" }
        });

        GenerateSampleStateFile(Path.Combine(stateFolder, "state2.xml"), new[]
        {
            new BarcodeInfo { CodeTypeName = "Code39", CodeText = "CODE39VALUE" },
            new BarcodeInfo { CodeTypeName = "Code128", CodeText = "XYZ789" }
        });

        // --------------------------------------------------------------------
        // Collect all barcode entries from every XML file in the folder.
        // --------------------------------------------------------------------
        List<BarcodeInfo> allBarcodes = new List<BarcodeInfo>();
        string[] xmlFiles = Directory.GetFiles(stateFolder, "*.xml");

        foreach (string xmlFile in xmlFiles)
        {
            // Load the XML document safely using a FileStream.
            XDocument doc;
            using (FileStream fs = new FileStream(xmlFile, FileMode.Open, FileAccess.Read))
            {
                doc = XDocument.Load(fs);
            }

            // Expected XML structure:
            // <Barcodes>
            //   <Barcode>
            //     <CodeTypeName>...</CodeTypeName>
            //     <CodeText>...</CodeText>
            //   </Barcode>
            //   ...
            // </Barcodes>
            foreach (XElement barcodeElem in doc.Root.Elements("Barcode"))
            {
                string typeName = barcodeElem.Element("CodeTypeName")?.Value ?? string.Empty;
                string codeText = barcodeElem.Element("CodeText")?.Value ?? string.Empty;

                // Only add entries that have both type and text.
                if (!string.IsNullOrEmpty(typeName) && !string.IsNullOrEmpty(codeText))
                {
                    allBarcodes.Add(new BarcodeInfo { CodeTypeName = typeName, CodeText = codeText });
                }
            }
        }

        // --------------------------------------------------------------------
        // Build the summary XML document that contains all collected barcodes.
        // --------------------------------------------------------------------
        XDocument summaryDoc = new XDocument(
            new XElement("Summary",
                new XElement("Barcodes",
                    // Convert each BarcodeInfo into a <Barcode> element.
                    new List<XElement>(CreateBarcodeElements(allBarcodes))
                )
            )
        );

        // Save the merged summary to a file.
        string summaryPath = "merged_summary.xml";
        using (FileStream outStream = new FileStream(summaryPath, FileMode.Create, FileAccess.Write))
        {
            summaryDoc.Save(outStream);
        }

        Console.WriteLine($"Merged summary saved to '{summaryPath}'. Total barcodes: {allBarcodes.Count}");
    }

    /// <summary>
    /// Generates a sample XML state file containing the specified barcodes.
    /// </summary>
    /// <param name="filePath">Full path where the XML file will be created.</param>
    /// <param name="barcodes">Array of barcode information to include.</param>
    static void GenerateSampleStateFile(string filePath, BarcodeInfo[] barcodes)
    {
        XDocument doc = new XDocument(
            new XElement("Barcodes",
                new List<XElement>(CreateBarcodeElements(barcodes))
            )
        );

        using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        {
            doc.Save(fs);
        }
    }

    /// <summary>
    /// Converts a collection of <see cref="BarcodeInfo"/> objects into a sequence of <see cref="XElement"/>
    /// representing individual <c>&lt;Barcode&gt;</c> elements.
    /// </summary>
    /// <param name="barcodes">Enumerable of barcode information.</param>
    /// <returns>IEnumerable of <see cref="XElement"/> ready for inclusion in an XML document.</returns>
    static IEnumerable<XElement> CreateBarcodeElements(IEnumerable<BarcodeInfo> barcodes)
    {
        foreach (var b in barcodes)
        {
            yield return new XElement("Barcode",
                new XElement("CodeTypeName", b.CodeTypeName),
                new XElement("CodeText", b.CodeText)
            );
        }
    }
}