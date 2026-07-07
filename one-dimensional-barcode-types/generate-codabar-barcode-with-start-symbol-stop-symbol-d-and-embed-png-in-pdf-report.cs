// Title: Generate Codabar barcode and embed in PDF report
// Description: Demonstrates creating a Codabar barcode with specific start/stop symbols, saving it as PNG, and embedding the image into a PDF document.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and Aspose.Pdf document creation category. It showcases the use of BarcodeGenerator, Codabar settings, BarCodeImageFormat, and Aspose.Pdf Document/Image classes to produce printable reports. Developers often need to generate barcodes and combine them with PDF reports for invoices, shipping labels, or inventory documents.
// Prompt: Generate a Codabar barcode with start symbol A, stop symbol D, and embed the PNG in a PDF report.
// Tags: codabar, barcode generation, pdf embedding, aspose.barcode, aspose.pdf, png, pdf

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Program entry point demonstrating Codabar barcode generation and PDF embedding.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Codabar barcode PNG with start symbol A and stop symbol D, then embeds it into a PDF report.
    /// </summary>
    static void Main()
    {
        // Define output file names
        const string pngPath = "codabar.png";
        const string pdfPath = "CodabarReport.pdf";

        // Create a Codabar barcode with start symbol A and stop symbol D
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "123456"))
        {
            // Explicitly set the start and stop symbols
            generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.A;
            generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.D;

            // Save the generated barcode as a PNG image
            generator.Save(pngPath, BarCodeImageFormat.Png);
        }

        // Create a new PDF document and embed the generated PNG image
        using (var pdfDoc = new Document())
        {
            // Add a new page to the PDF
            var page = pdfDoc.Pages.Add();

            // Create an image object that references the PNG file
            var image = new Aspose.Pdf.Image
            {
                File = pngPath
                // Image dimensions can be set here if needed (in points, 1 point = 1/72 inch)
                // Width = 200,
                // Height = 100
            };

            // Add the image to the page's content
            page.Paragraphs.Add(image);

            // Save the PDF report to disk
            pdfDoc.Save(pdfPath);
        }

        // Inform the user where the files have been saved
        Console.WriteLine($"Barcode PNG saved to: {pngPath}");
        Console.WriteLine($"PDF report saved to: {pdfPath}");
    }
}