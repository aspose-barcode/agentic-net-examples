// Title: Generate RM4SCC Barcodes from XML and Export to Multi‑Page PDF
// Description: The example reads an XML file, extracts code values, creates RM4SCC barcodes for each, and compiles them into a multi‑page PDF document.
// Category-Description: This sample belongs to the Aspose.BarCode for .NET barcode generation category, demonstrating how to use BarcodeGenerator with EncodeTypes.RM4SCC, configure barcode dimensions, and combine generated images into a PDF using Aspose.Pdf. Developers often need to batch‑process data sources such as XML or databases to produce printable barcode PDFs for inventory, shipping, or labeling workflows.
// Prompt: Generate RM4SCC barcodes for each record in an XML file and write output to a multi‑page PDF.
// Tags: rm4scc, barcode generation, xml parsing, pdf output, aspose.barcode, aspose.pdf, c# example

using System;
using System.IO;
using System.Xml.Linq;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Demonstrates reading barcode data from an XML file, generating RM4SCC barcodes,
/// and assembling them into a multi‑page PDF using Aspose.BarCode and Aspose.Pdf.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the XML parsing, barcode generation,
    /// PDF creation, and cleanup steps.
    /// </summary>
    static void Main()
    {
        // Define the input XML file path.
        string xmlPath = "input.xml";

        // Verify that the XML file exists before proceeding.
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML document, handling any parsing errors.
        XDocument doc;
        try
        {
            doc = XDocument.Load(xmlPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load XML: {ex.Message}");
            return;
        }

        // Extract barcode values from <Record><Code> elements.
        var codes = new List<string>();
        foreach (var record in doc.Descendants("Record"))
        {
            var codeElement = record.Element("Code");
            if (codeElement != null)
            {
                string code = codeElement.Value?.Trim();
                if (!string.IsNullOrEmpty(code))
                    codes.Add(code);
            }
        }

        // Ensure that at least one barcode value was found.
        if (codes.Count == 0)
        {
            Console.WriteLine("No barcode data found in XML.");
            return;
        }

        // Limit the number of pages for evaluation mode (max 4 barcodes).
        int maxPages = Math.Min(codes.Count, 4);
        var streams = new List<MemoryStream>();

        // Generate a PNG image for each barcode and store it in memory.
        for (int i = 0; i < maxPages; i++)
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.RM4SCC, codes[i]))
            {
                // Configure barcode appearance.
                generator.Parameters.Barcode.XDimension.Pixels = 4;
                generator.Parameters.Barcode.BarHeight.Pixels = 50;

                // Save the barcode image to a memory stream.
                var ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;
                streams.Add(ms);
            }
        }

        // Define the output PDF file name.
        string outputPdf = "RM4SCC_Barcodes.pdf";

        // Create a PDF document and add each barcode image as a separate page.
        using (var pdfDoc = new Document())
        {
            foreach (var ms in streams)
            {
                var page = pdfDoc.Pages.Add();
                var pdfImage = new Image
                {
                    ImageStream = ms,
                    FixWidth = 200.0,
                    FixHeight = 100.0,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                page.Paragraphs.Add(pdfImage);
            }

            // Save the assembled PDF to disk.
            pdfDoc.Save(outputPdf);
        }

        // Release all memory streams.
        foreach (var ms in streams)
        {
            ms.Dispose();
        }

        Console.WriteLine($"PDF generated: {outputPdf}");
    }
}