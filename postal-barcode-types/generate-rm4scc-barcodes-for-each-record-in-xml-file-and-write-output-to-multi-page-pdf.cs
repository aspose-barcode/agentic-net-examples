// Title: Generate RM4SCC barcodes from XML and embed into a multi‑page PDF
// Description: Reads an XML file, extracts <Code> values, creates RM4SCC barcode images, and compiles them into a PDF document with one barcode per page.
// Category-Description: Aspose.BarCode PDF generation examples – demonstrates how to use Aspose.BarCode.Generation.BarcodeGenerator with EncodeTypes.RM4SCC to create barcode images and embed them into an Aspose.Pdf.Document. Typical use cases include batch barcode creation from data sources such as XML, CSV, or databases, and producing printable PDF reports. Developers often need to combine barcode generation with PDF manipulation APIs to automate document workflows.
// Prompt: Generate RM4SCC barcodes for each record in an XML file and write output to a multi‑page PDF.
// Tags: rm4scc, barcode, xml, pdf, aspose.barcode, aspose.pdf, generation, batch

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;
using Aspose.Pdf.Text;

/// <summary>
/// Demonstrates generating RM4SCC barcodes from an XML file and writing them to a multi‑page PDF.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Reads input XML, creates barcodes, and saves a PDF.
    /// </summary>
    static void Main()
    {
        // Input and output file paths (fallback to defaults if not provided)
        string inputXmlPath = "input.xml";
        string outputPdfPath = "output.pdf";

        // Validate input XML file existence
        if (!File.Exists(inputXmlPath))
        {
            Console.WriteLine($"Input XML file not found: {Path.GetFullPath(inputXmlPath)}");
            return;
        }

        // Load XML and extract code texts (assumes <Record><Code>value</Code></Record> structure)
        List<string> codeTexts = new List<string>();
        try
        {
            XDocument doc = XDocument.Load(inputXmlPath);
            foreach (XElement record in doc.Descendants("Record"))
            {
                XElement codeElement = record.Element("Code");
                if (codeElement != null && !string.IsNullOrWhiteSpace(codeElement.Value))
                {
                    codeTexts.Add(codeElement.Value.Trim());
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to parse XML: {ex.Message}");
            return;
        }

        if (codeTexts.Count == 0)
        {
            Console.WriteLine("No records with <Code> element found in the XML.");
            return;
        }

        // Limit to 4 items for Aspose.Pdf evaluation mode
        int maxItems = Math.Min(codeTexts.Count, 4);

        // Prepare PDF document
        Document pdfDoc = new Document();

        // Keep streams alive until after PDF is saved
        List<MemoryStream> barcodeStreams = new List<MemoryStream>();

        // Generate barcodes and add them to PDF pages
        for (int i = 0; i < maxItems; i++)
        {
            string code = codeTexts[i];

            // Generate RM4SCC barcode image into a memory stream
            MemoryStream ms = new MemoryStream();
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.RM4SCC, code))
            {
                // Optional visual settings
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
                generator.Parameters.Resolution = 300;

                // Save as PNG to the stream
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset for reading
            }

            barcodeStreams.Add(ms);

            // Add a new page and embed the barcode image
            Page page = pdfDoc.Pages.Add();
            Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image
            {
                ImageStream = ms
                // Adjust size as needed; here we let the image keep its original dimensions
                // FixWidth and FixHeight can be set if a specific size is required
            };
            page.Paragraphs.Add(pdfImage);
        }

        // Save the PDF
        try
        {
            pdfDoc.Save(outputPdfPath);
            Console.WriteLine($"PDF generated successfully: {Path.GetFullPath(outputPdfPath)}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to save PDF: {ex.Message}");
        }
        finally
        {
            // Dispose all memory streams
            foreach (var stream in barcodeStreams)
            {
                stream.Dispose();
            }
        }
    }
}