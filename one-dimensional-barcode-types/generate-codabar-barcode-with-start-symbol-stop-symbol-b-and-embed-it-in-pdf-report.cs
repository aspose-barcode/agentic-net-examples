// Title: Generate Codabar Barcode and Embed in PDF
// Description: Creates a Codabar barcode with start symbol A and stop symbol B, saves it as a PNG image, and embeds the image into a PDF report.
// Category-Description: This example demonstrates how to use Aspose.BarCode to generate a barcode (EncodeTypes.Codabar) and Aspose.Pdf to insert the generated image into a PDF document. It covers common tasks such as configuring barcode parameters, handling image streams, and creating simple PDF pages—typical operations for developers building automated reporting or document generation solutions that require barcodes.
// Prompt: Generate a Codabar barcode with start symbol A, stop symbol B, and embed it in a PDF report.
// Tags: codabar, barcode generation, pdf, aspose.barcode, aspose.pdf

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;
using Aspose.Pdf.Text;

/// <summary>
/// Demonstrates generating a Codabar barcode with specific start/stop symbols
/// and embedding the resulting image into a PDF document using Aspose libraries.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, creates a PDF, and saves the result.
    /// </summary>
    static void Main()
    {
        // Define the output PDF file path in the current working directory.
        string outputPdf = Path.Combine(Directory.GetCurrentDirectory(), "CodabarAB.pdf");

        // Initialize the barcode generator for Codabar with the data "12345".
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "12345"))
        {
            // Set the start and stop symbols to A and B respectively.
            generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.A;
            generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.B;

            // Adjust the X-dimension (module width) for better visual quality.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Render the barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.

                // Create a new PDF document.
                using (var pdfDoc = new Document())
                {
                    // Add a page to the PDF.
                    var page = pdfDoc.Pages.Add();

                    // Create an image object from the barcode stream.
                    var pdfImage = new Image
                    {
                        ImageStream = ms,
                        FixWidth = 200,
                        FixHeight = 200,
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

        // Inform the user that the PDF has been generated.
        Console.WriteLine("PDF report generated: " + outputPdf);
    }
}