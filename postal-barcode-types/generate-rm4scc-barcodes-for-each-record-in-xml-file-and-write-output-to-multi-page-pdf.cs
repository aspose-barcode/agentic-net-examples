// Title: Generate RM4SCC barcodes from XML records and save to a multi‑page PDF
// Description: This example reads up to four record values from an XML file, creates RM4SCC barcode images for each, and compiles them into a PDF where each barcode occupies a separate page.
// Category-Description: Demonstrates Aspose.BarCode and Aspose.Pdf integration for barcode generation and PDF creation. The sample uses BarcodeGenerator (EncodeTypes.RM4SCC) to produce PNG images, then adds them to a Document via Aspose.Pdf. Typical use cases include batch barcode printing, report generation, and automated document workflows where developers need to convert data records into printable barcodes within PDF files.
// Prompt: Generate RM4SCC barcodes for each record in an XML file and write output to a multi‑page PDF.
// Tags: rm4scc, barcode, pdf, xml, aspose.barcode, aspose.pdf, generation, image, batch

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Demonstrates generating RM4SCC barcodes from XML records and exporting them to a multi‑page PDF using Aspose.BarCode and Aspose.Pdf.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Reads record values from an XML file, creates barcode images, and writes them to a PDF.
    /// </summary>
    /// <param name="args">Optional command‑line argument specifying the path to the XML file.</param>
    static void Main(string[] args)
    {
        // Determine XML input path (use first argument or default to "records.xml")
        string xmlPath = args.Length > 0 ? args[0] : "records.xml";

        // If the XML file does not exist, create a sample file with up to five records
        if (!File.Exists(xmlPath))
        {
            var sampleXml = new XDocument(
                new XElement("Records",
                    new XElement("Record", "123456"),
                    new XElement("Record", "ABCDEF"),
                    new XElement("Record", "7890GH"),
                    new XElement("Record", "LMNOPQ"),
                    new XElement("Record", "RSTUVW")
                )
            );
            sampleXml.Save(xmlPath);
            Console.WriteLine($"Sample XML created at '{xmlPath}'.");
        }

        // Load records from the XML file
        XDocument doc = XDocument.Load(xmlPath);
        List<string> codes = doc.Root?
            .Elements("Record")
            .Select(e => e.Value.Trim())
            .Where(v => !string.IsNullOrEmpty(v))
            .ToList() ?? new List<string>();

        if (codes.Count == 0)
        {
            Console.WriteLine("No records found to process.");
            return;
        }

        // Limit processing to a maximum of 4 items (evaluation mode restriction)
        int maxItems = Math.Min(codes.Count, 4);
        var selectedCodes = codes.Take(maxItems).ToList();

        // Prepare a list to hold barcode image streams in memory
        var barcodeStreams = new List<MemoryStream>();

        // Generate a PNG barcode image for each selected code
        foreach (string code in selectedCodes)
        {
            var generator = new BarcodeGenerator(EncodeTypes.RM4SCC, code);
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Parameters.Barcode.BarHeight.Pixels = 50;

            var ms = new MemoryStream();
            generator.Save(ms, BarCodeImageFormat.Png);
            ms.Position = 0; // Reset stream position for later reading
            barcodeStreams.Add(ms);
        }

        // Create a PDF document and add each barcode image as a separate page
        string pdfPath = "RM4SCC_Barcodes.pdf";
        using (var pdfDoc = new Document())
        {
            for (int i = 0; i < barcodeStreams.Count; i++)
            {
                var page = pdfDoc.Pages.Add();
                var stream = barcodeStreams[i];

                // Define a rectangle that covers the full page
                var rect = new Aspose.Pdf.Rectangle(0, 0, page.PageInfo.Width, page.PageInfo.Height);

                // Add the barcode image to the page, scaling it to fill the page
                page.AddImage(stream, rect, (int)page.PageInfo.Width, (int)page.PageInfo.Height, true);
            }

            // Save the assembled PDF to disk
            pdfDoc.Save(pdfPath);
        }

        // Dispose all memory streams to release resources
        foreach (var ms in barcodeStreams)
        {
            ms.Dispose();
        }

        Console.WriteLine($"PDF with {maxItems} barcode page(s) saved to '{pdfPath}'.");
    }
}