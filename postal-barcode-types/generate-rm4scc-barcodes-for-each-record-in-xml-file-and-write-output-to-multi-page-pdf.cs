// Title: Generate RM4SCC barcodes from XML and export to multi‑page PDF
// Description: Demonstrates reading codes from an XML file, creating RM4SCC barcodes, and compiling them into a PDF document with one barcode per page.
// Category-Description: This example belongs to the Aspose.BarCode for .NET barcode generation category, illustrating how to use BarcodeGenerator with EncodeTypes.RM4SCC, configure barcode dimensions, and embed generated images into an Aspose.Pdf Document. Typical use cases include batch barcode creation from data sources and producing printable PDF reports. Developers often need to combine barcode generation with PDF composition for inventory, shipping, or labeling solutions.
// Prompt: Generate RM4SCC barcodes for each record in an XML file and write output to a multi‑page PDF.
// Tags: rm4scc, barcode generation, xml, pdf, aspose.barcode, aspose.pdf, batch processing

using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Program that reads codes from an XML file, generates RM4SCC barcodes,
/// and writes them to a multi‑page PDF document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates sample XML if missing, extracts up to four codes,
    /// generates PNG barcodes, embeds them into a PDF, and saves the result.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define input XML and output PDF file paths.
        string inputPath = "input.xml";
        string outputPath = "output.pdf";

        // If the input XML does not exist, create a sample file with a few records.
        if (!File.Exists(inputPath))
        {
            var sample = new XDocument(
                new XElement("Records",
                    new XElement("Record", new XElement("Code", "123456ASPOSE")),
                    new XElement("Record", new XElement("Code", "ABCDEF")),
                    new XElement("Record", new XElement("Code", "987654"))
                )
            );
            sample.Save(inputPath);
        }

        // Load the XML document and extract up to four non‑empty code values.
        XDocument doc = XDocument.Load(inputPath);
        var codes = doc.Root.Elements("Record")
            .Select(r => (string)r.Element("Code"))
            .Where(c => !string.IsNullOrEmpty(c))
            .Take(4)
            .ToList();

        // Prepare a new PDF document and a list to hold barcode image streams.
        var pdfDoc = new Document();
        var streams = new List<MemoryStream>();

        // Iterate over each code, generate a barcode image, and add it to a new PDF page.
        foreach (var code in codes)
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.RM4SCC, code))
            {
                // Configure barcode appearance.
                generator.Parameters.Barcode.XDimension.Pixels = 4;
                generator.Parameters.Barcode.BarHeight.Pixels = 50;

                // Save the barcode as a PNG into a memory stream.
                var ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;
                streams.Add(ms);

                // Add a new page to the PDF and place the barcode image at the center.
                var page = pdfDoc.Pages.Add();
                var pdfImage = new Image
                {
                    ImageStream = ms,
                    FixWidth = 200,
                    FixHeight = 200,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                page.Paragraphs.Add(pdfImage);
            }
        }

        // Save the assembled PDF to the specified output path.
        pdfDoc.Save(outputPath);

        // Dispose all memory streams to release resources.
        foreach (var s in streams)
        {
            s.Dispose();
        }

        // Inform the user about the successful generation.
        Console.WriteLine($"Generated PDF with {codes.Count} barcodes at '{outputPath}'.");
    }
}