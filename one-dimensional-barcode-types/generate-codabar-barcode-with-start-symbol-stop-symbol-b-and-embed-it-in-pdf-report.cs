// Title: Generate Codabar barcode and embed in PDF
// Description: Demonstrates creating a Codabar barcode with start symbol A and stop symbol B, rendering it as an image, and inserting it into a PDF report.
// Category-Description: This example belongs to the Aspose.BarCode for .NET barcode generation category, showing how to use BarcodeGenerator with Codabar symbology, configure start/stop symbols, and combine the output with Aspose.Pdf to produce a PDF document. Developers often need to generate barcodes for inventory, shipping, or point‑of‑sale systems and embed them in reports or invoices; the key classes are BarcodeGenerator, EncodeTypes, CodabarSymbol, and Aspose.Pdf.Document.
// Prompt: Generate a Codabar barcode with start symbol A, stop symbol B, and embed it in a PDF report.
// Tags: codabar, barcode generation, pdf, aspose.barcode, aspose.pdf, csharp, example

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Demonstrates generating a Codabar barcode and embedding it into a PDF document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a Codabar barcode with start/stop symbols,
    /// saves it to a memory stream, and inserts the image into a PDF file.
    /// </summary>
    static void Main()
    {
        // Define the output PDF file path
        string pdfPath = "CodabarReport.pdf";

        // Initialize a Codabar barcode generator
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar))
        {
            // Set the barcode data (excluding start/stop symbols)
            generator.CodeText = "123456";

            // Configure start and stop symbols: A (start) and B (stop)
            generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.A;
            generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.B;

            // Optional visual settings: black bars on white background
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Render the barcode to a memory stream in PNG format
            var barcodeStream = new MemoryStream();
            generator.Save(barcodeStream, BarCodeImageFormat.Png);
            barcodeStream.Position = 0; // Reset stream position for reading

            // Create a new PDF document and add a page
            using (var pdfDoc = new Document())
            {
                var page = pdfDoc.Pages.Add();

                // Create an Aspose.Pdf.Image from the barcode stream
                var pdfImage = new Image
                {
                    ImageStream = barcodeStream,
                    FixWidth = 200.0,
                    FixHeight = 100.0,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new MarginInfo { Top = 20 }
                };

                // Insert the barcode image into the PDF page
                page.Paragraphs.Add(pdfImage);

                // Save the PDF document to the specified path
                pdfDoc.Save(pdfPath);
            }

            // Release the memory stream resources
            barcodeStream.Dispose();
        }

        // Inform the user where the PDF was saved
        Console.WriteLine($"PDF report generated: {Path.GetFullPath(pdfPath)}");
    }
}