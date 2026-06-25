using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Generates a PDF containing up to four RM4SCC barcodes extracted from an XML source.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Reads XML from a file argument or uses a default string, extracts barcode values,
    /// generates PNG images, embeds them into a PDF, and saves the result.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument may be a path to an XML file.</param>
    static void Main(string[] args)
    {
        // ------------------------------------------------------------
        // Load XML content: from file if provided and exists, otherwise use default.
        // ------------------------------------------------------------
        string xmlContent;
        if (args.Length > 0 && File.Exists(args[0]))
        {
            xmlContent = File.ReadAllText(args[0]);
        }
        else
        {
            xmlContent = @"<Records>
    <Record><Code>AB12</Code></Record>
    <Record><Code>CD34</Code></Record>
    <Record><Code>EF56</Code></Record>
    <Record><Code>GH78</Code></Record>
</Records>";
        }

        // ------------------------------------------------------------
        // Parse XML and retrieve the collection of <Record> elements.
        // ------------------------------------------------------------
        XDocument doc = XDocument.Parse(xmlContent);
        var records = doc.Root?.Elements("Record");

        // ------------------------------------------------------------
        // Create a new PDF document that will hold the barcode images.
        // ------------------------------------------------------------
        var pdfDoc = new Document();

        int pageIndex = 0; // Tracks how many pages (barcodes) have been added.

        if (records != null)
        {
            // ------------------------------------------------------------
            // Iterate through each record, generate a barcode, and add it to the PDF.
            // Stop after four records to limit the number of pages.
            // ------------------------------------------------------------
            foreach (var record in records)
            {
                if (pageIndex >= 4) break; // Limit to four barcodes.

                // Extract the barcode text from the <Code> element; use empty string if missing.
                string codeText = record.Element("Code")?.Value ?? string.Empty;

                // --------------------------------------------------------
                // Generate the barcode image in memory.
                // --------------------------------------------------------
                using (var ms = new MemoryStream())
                {
                    using (var generator = new BarcodeGenerator(EncodeTypes.RM4SCC, codeText))
                    {
                        // Set image dimensions (points).
                        generator.Parameters.ImageWidth.Point = 300;
                        generator.Parameters.ImageHeight.Point = 150;

                        // Save the barcode as PNG into the memory stream.
                        generator.Save(ms, BarCodeImageFormat.Png);
                    }

                    // Create a separate stream for the PDF image to avoid disposal issues.
                    var imageStream = new MemoryStream(ms.ToArray());

                    // ----------------------------------------------------
                    // Add a new page to the PDF and place the barcode image.
                    // ----------------------------------------------------
                    var page = pdfDoc.Pages.Add();

                    var pdfImage = new Aspose.Pdf.Image
                    {
                        ImageStream = imageStream,
                        FixWidth = 0,   // Use original image width.
                        FixHeight = 0   // Use original image height.
                    };

                    // Add the image to the page's paragraph collection.
                    page.Paragraphs.Add(pdfImage);
                }

                pageIndex++; // Increment page counter.
            }
        }

        // ------------------------------------------------------------
        // Save the assembled PDF to disk and report the location.
        // ------------------------------------------------------------
        string outputPdfPath = "RM4SCC_Barcodes.pdf";
        pdfDoc.Save(outputPdfPath);
        Console.WriteLine($"PDF saved to {Path.GetFullPath(outputPdfPath)}");
    }
}