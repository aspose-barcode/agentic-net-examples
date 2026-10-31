// Title: Generate Codabar Barcode and Embed in PDF Report
// Description: Creates a Codabar barcode with start symbol A and stop symbol B, saves it as an image, and embeds the image into a PDF document.
// Category-Description: This example demonstrates how to use Aspose.BarCode to generate a Codabar barcode and Aspose.Pdf to embed the generated barcode image into a PDF report. It covers the BarcodeGenerator class for barcode creation, setting Codabar-specific parameters, and using Aspose.Pdf Document, Page, and Image classes to compose a PDF. Developers working on document automation, reporting, or inventory labeling often need to generate barcodes and include them in PDF files.
// Prompt: Generate a Codabar barcode with start symbol A, stop symbol B, and embed it in a PDF report.
// Tags: codabar,barcode,generation,pdf,aspose.barcode,aspose.pdf,report,embedding

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Demonstrates generating a Codabar barcode and embedding it into a PDF report using Aspose libraries.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode, creates a PDF, and saves the result.
    /// </summary>
    static void Main()
    {
        // Define the output PDF file path.
        string outputPdf = Path.Combine(Directory.GetCurrentDirectory(), "CodabarReport.pdf");
        // Text to encode in the barcode (excluding start/stop symbols which are set separately).
        string barcodeText = "1234567890";

        // Initialize the barcode generator for Codabar symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, barcodeText))
        {
            // Set Codabar-specific start and stop symbols.
            generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.A;
            generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.B;
            // Define barcode and background colors.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Render the barcode to a memory stream in PNG format.
            using (var barcodeStream = new MemoryStream())
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                // Reset stream position for reading.
                barcodeStream.Position = 0;

                // Create a new PDF document.
                using (var pdfDoc = new Document())
                {
                    // Add a page to the PDF.
                    var page = pdfDoc.Pages.Add();

                    // Create an image object linked to the barcode stream.
                    var pdfImage = new Aspose.Pdf.Image
                    {
                        ImageStream = barcodeStream,
                        FixWidth = 200,
                        FixHeight = 100,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Margin = new MarginInfo { Top = 20 }
                    };

                    // Add the image to the page's paragraph collection.
                    page.Paragraphs.Add(pdfImage);

                    // Save the PDF document to the specified file.
                    pdfDoc.Save(outputPdf);
                }
            }
        }

        // Inform the user where the PDF was saved.
        Console.WriteLine($"PDF report generated at: {outputPdf}");
    }
}