// Title: Generate Codabar Barcode and Embed in PDF
// Description: This example creates a Codabar barcode with start symbol A and stop symbol D, saves it as a PNG in memory, and embeds the image into a PDF report.
// Category-Description: Demonstrates Aspose.BarCode generation and Aspose.Pdf embedding. The example uses BarcodeGenerator (EncodeTypes.Codabar) to produce a barcode image, then creates a PDF document with Aspose.Pdf.Document, adds the image to a page, and saves the file. Typical for developers who need to include barcodes in generated reports, invoices, or shipping labels.
// Prompt: Generate a Codabar barcode with start symbol A, stop symbol D, and embed the PNG in a PDF report.
// Tags: codabar, barcode, pdf, image, aspose.barcode, aspose.pdf, generation, embedding

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;
using Aspose.Pdf.Text;

/// <summary>
/// Demonstrates generating a Codabar barcode and embedding it into a PDF document using Aspose.BarCode and Aspose.Pdf.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, creates a PDF, and saves the result.
    /// </summary>
    static void Main()
    {
        // Prepare output directory and PDF file path
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodePdfDemo");
        Directory.CreateDirectory(outputDir);
        string pdfPath = Path.Combine(outputDir, "CodabarReport.pdf");

        // Generate Codabar barcode with start symbol A and stop symbol D
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "12345"))
        {
            generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.A;
            generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.D;

            // Save barcode to a memory stream as PNG
            using (var barcodeStream = new MemoryStream())
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0; // Reset stream position for reading

                // Create a new PDF document and embed the barcode image
                using (var pdfDoc = new Document())
                {
                    var page = pdfDoc.Pages.Add();

                    var pdfImage = new Aspose.Pdf.Image
                    {
                        ImageStream = barcodeStream,
                        FixWidth = 200.0,
                        FixHeight = 200.0,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Margin = new MarginInfo { Top = 20 }
                    };

                    // Add the image to the page's paragraphs collection
                    page.Paragraphs.Add(pdfImage);

                    // Save the PDF document to the specified path
                    pdfDoc.Save(pdfPath);
                }
            }
        }

        Console.WriteLine($"PDF report generated at: {pdfPath}");
    }
}