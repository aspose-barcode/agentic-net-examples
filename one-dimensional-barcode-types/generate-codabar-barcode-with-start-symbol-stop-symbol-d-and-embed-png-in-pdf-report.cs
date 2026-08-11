// Title: Generate Codabar barcode and embed it in a PDF report
// Description: This example creates a Codabar barcode with start symbol A and stop symbol D, saves the barcode as a PNG image, and embeds the image into a PDF document.
// Category-Description: Demonstrates Aspose.BarCode barcode generation (EncodeTypes.Codabar, BarcodeGenerator) combined with Aspose.Pdf PDF creation. Typical for reports, invoices, or labels where a barcode image must be included in a PDF. Developers often need to configure barcode parameters, render to an image stream, and place the image on a PDF page using Aspose.Pdf.Image.
// Prompt: Generate a Codabar barcode with start symbol A, stop symbol D, and embed the PNG in a PDF report.
// Tags: codabar, barcode generation, png, pdf, aspose.barcode, aspose.pdf

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;
using Aspose.Pdf.Text;

/// <summary>
/// Demonstrates how to generate a Codabar barcode, save it as PNG, and embed it into a PDF report.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, creates a PDF, and saves the result.
    /// </summary>
    static void Main()
    {
        // Define the output PDF file path
        const string pdfPath = "CodabarReport.pdf";

        // Initialize a Codabar barcode generator with start symbol A and stop symbol D
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar))
        {
            // Set the data to encode in the barcode
            generator.CodeText = "123456";

            // Configure start and stop symbols for Codabar
            generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.A;
            generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.D;

            // Optional visual settings: black bars on white background
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Render the barcode to a memory stream in PNG format
            using (var barcodeStream = new MemoryStream())
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0; // Reset stream position for reading

                // Create a new PDF document and add a page
                var pdfDoc = new Document();
                var page = pdfDoc.Pages.Add();

                // Create an image object that reads from the barcode stream
                var pdfImage = new Aspose.Pdf.Image
                {
                    ImageStream = barcodeStream,
                    FixWidth = 200f,
                    FixHeight = 200f,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new MarginInfo { Top = 20 }
                };

                // Add the image to the PDF page
                page.Paragraphs.Add(pdfImage);

                // Save the PDF document to the specified path
                pdfDoc.Save(pdfPath);
            }
        }

        // Output the location of the generated PDF
        Console.WriteLine($"PDF report generated: {Path.GetFullPath(pdfPath)}");
    }
}