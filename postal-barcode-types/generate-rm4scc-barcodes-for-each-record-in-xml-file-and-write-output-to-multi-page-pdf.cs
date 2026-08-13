// Title: Generate RM4SCC Barcodes from XML and Export to Multi‑Page PDF
// Description: Demonstrates reading code values from an XML file, creating RM4SCC barcodes for each record, and compiling them into a multi‑page PDF document.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator (EncodeTypes.RM4SCC) together with Aspose.Pdf to produce printable barcode documents. Typical use cases include batch barcode creation for inventory, shipping, or labeling systems where data originates from XML sources. Developers often need to combine barcode rendering with PDF pagination, and this snippet illustrates the common workflow using Aspose.BarCode and Aspose.Pdf APIs.
// Prompt: Generate RM4SCC barcodes for each record in an XML file and write output to a multi‑page PDF.
// Tags: rm4scc, barcode, generation, pdf, aspose.barcode, aspose.pdf, xml, csharp

using System;
using System.IO;
using System.Xml.Linq;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Example program that reads record codes from an XML file, generates RM4SCC barcodes,
/// and writes them to a multi‑page PDF.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    /// <param name="args">
    /// Optional command‑line arguments:
    /// args[0] – path to the input XML file (default: "records.xml").
    /// args[1] – path to the output PDF file (default: "output.pdf").
    /// </param>
    static void Main(string[] args)
    {
        // Determine input XML file path (first argument or default)
        string xmlPath = args.Length > 0 ? args[0] : "records.xml";

        // Determine output PDF file path (second argument or default)
        string pdfPath = args.Length > 1 ? args[1] : "output.pdf";

        // Ensure a sample XML file exists when none is provided
        if (!File.Exists(xmlPath))
        {
            CreateSampleXml(xmlPath);
        }

        // Load barcode text values from the XML file
        List<string> codeTexts = LoadCodeTexts(xmlPath);

        // Limit the number of records to four as required by the example rule
        if (codeTexts.Count > 4)
        {
            codeTexts = codeTexts.GetRange(0, 4);
        }

        // Create a new PDF document that will hold the barcode pages
        var pdfDoc = new Document();

        // Keep references to memory streams until the PDF is saved
        var streams = new List<MemoryStream>();

        // Iterate over each code value and generate a corresponding barcode page
        foreach (string code in codeTexts)
        {
            // Create a memory stream to hold the barcode image
            var barcodeStream = new MemoryStream();

            // Generate the RM4SCC barcode and write it as PNG into the stream
            using (var generator = new BarcodeGenerator(EncodeTypes.RM4SCC, code))
            {
                // Optional visual customizations
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
                generator.Parameters.Barcode.XDimension.Point = 2f;

                // Save the barcode image to the memory stream
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
            }

            // Reset stream position so it can be read by Aspose.Pdf
            barcodeStream.Position = 0;
            streams.Add(barcodeStream);

            // Add a new page to the PDF and place the barcode image on it
            var page = pdfDoc.Pages.Add();
            var pdfImage = new Aspose.Pdf.Image
            {
                ImageStream = barcodeStream,
                FixWidth = 200,
                FixHeight = 200,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new MarginInfo { Top = 20 }
            };
            page.Paragraphs.Add(pdfImage);
        }

        // Persist the assembled PDF document to the specified file path
        pdfDoc.Save(pdfPath);

        // Release all memory streams now that the PDF has been saved
        foreach (var ms in streams)
        {
            ms.Dispose();
        }

        Console.WriteLine($"PDF generated at: {Path.GetFullPath(pdfPath)}");
    }

    /// <summary>
    /// Loads the values of the &lt;Code&gt; elements from each &lt;Record&gt; node in the XML file.
    /// </summary>
    /// <param name="xmlFile">Path to the XML file containing records.</param>
    /// <returns>List of code strings extracted from the XML.</returns>
    static List<string> LoadCodeTexts(string xmlFile)
    {
        var list = new List<string>();
        try
        {
            XDocument doc = XDocument.Load(xmlFile);
            foreach (var elem in doc.Descendants("Record"))
            {
                var codeElem = elem.Element("Code");
                if (codeElem != null)
                {
                    list.Add(codeElem.Value.Trim());
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading XML: {ex.Message}");
        }
        return list;
    }

    /// <summary>
    /// Creates a simple sample XML file with a few <Record> entries for demonstration purposes.
    /// </summary>
    /// <param name="path">File path where the sample XML will be saved.</param>
    static void CreateSampleXml(string path)
    {
        var doc = new XDocument(
            new XElement("Records",
                new XElement("Record", new XElement("Code", "AB12C3")),
                new XElement("Record", new XElement("Code", "D4E5F6")),
                new XElement("Record", new XElement("Code", "G7H8I9"))
            )
        );
        doc.Save(path);
        Console.WriteLine($"Sample XML created at: {Path.GetFullPath(path)}");
    }
}