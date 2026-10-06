// Title: Generate Codabar barcode and embed in PDF report
// Description: Demonstrates creating a Codabar barcode with start symbol A and stop symbol B, then inserting the barcode image into a PDF document.
// Category-Description: This example belongs to the Aspose.BarCode for .NET barcode generation category, illustrating how to configure Codabar parameters, render the barcode as an image, and embed it into a PDF using Aspose.Pdf. Developers commonly use these APIs to automate report creation, invoices, or shipping documents that require machine‑readable barcodes.
// Prompt: Generate a Codabar barcode with start symbol A, stop symbol B, and embed it in a PDF report.
// Tags: codabar, barcode generation, pdf, aspose.barcode, aspose.pdf, image embedding

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Example program that creates a Codabar barcode and embeds it into a PDF report.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode, adds it to a PDF page, and saves the document.
    /// </summary>
    static void Main()
    {
        // Define the output PDF file name.
        string outputPdf = "CodabarReport.pdf";

        // Create a barcode generator for Codabar with the data "12345".
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "12345"))
        {
            // Set start and stop symbols as required (A and B).
            generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.A;
            generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.B;

            // Adjust the X dimension (module width) for better image quality.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Render the barcode to a memory stream in PNG format.
            using (var barcodeStream = new MemoryStream())
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0; // Reset stream position for reading.

                // Create a new PDF document.
                using (var pdfDoc = new Document())
                {
                    // Add a page to the PDF.
                    var page = pdfDoc.Pages.Add();

                    // Create an image object that uses the barcode stream.
                    var pdfImage = new Aspose.Pdf.Image
                    {
                        ImageStream = barcodeStream,
                        FixWidth = 200,
                        FixHeight = 100,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Margin = new MarginInfo { Top = 20 }
                    };

                    // Add the image to the page's paragraph collection.
                    page.Paragraphs.Add(pdfImage);

                    // Save the PDF document to the specified file.
                    pdfDoc.Save(outputPdf);
                }
            }
        }

        // Output the full path of the generated PDF for verification.
        Console.WriteLine($"PDF report generated: {Path.GetFullPath(outputPdf)}");
    }
}