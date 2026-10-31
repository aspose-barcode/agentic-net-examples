// Title: Generate Codabar Barcode and Embed in PDF
// Description: Creates a Codabar barcode with start symbol A and stop symbol D, saves it as a PNG image, and embeds the image into a PDF report.
// Category-Description: This example demonstrates how to use Aspose.BarCode to generate barcodes and Aspose.Pdf to embed images into PDF documents. It covers barcode configuration (symbology, start/stop symbols, dimensions), image export to a memory stream, and PDF creation with image placement. Developers working on reporting, invoicing, or document automation frequently need to generate barcodes and include them in PDF outputs.
// Prompt: Generate a Codabar barcode with start symbol A, stop symbol D, and embed the PNG in a PDF report.
// Tags: codabar, barcode, generation, pdf, aspose.barcode, aspose.pdf, image, png, embed

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Demonstrates generating a Codabar barcode and embedding it into a PDF document using Aspose libraries.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Codabar barcode, saves it as PNG in memory,
    /// creates a PDF, embeds the barcode image, and saves the PDF to disk.
    /// </summary>
    static void Main()
    {
        // Define output PDF file name and barcode data (including start/stop symbols)
        string pdfPath = "CodabarReport.pdf";
        string barcodeData = "A12345D";

        // Initialize the barcode generator for Codabar symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, barcodeData))
        {
            // Configure start and stop symbols as required
            generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.A;
            generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.D;

            // Set the X-dimension (module width) in pixels for better image quality
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Save the generated barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // Create a new PDF document and add a page
                using (var pdfDoc = new Document())
                {
                    var page = pdfDoc.Pages.Add();

                    // Create an Aspose.Pdf.Image object from the memory stream
                    var pdfImage = new Aspose.Pdf.Image
                    {
                        ImageStream = ms,
                        FixWidth = 200.0,   // Desired image width in points
                        FixHeight = 100.0   // Desired image height in points
                    };

                    // Add the image to the page's paragraph collection
                    page.Paragraphs.Add(pdfImage);

                    // Save the PDF document to the specified file path
                    pdfDoc.Save(pdfPath);
                }
            }
        }

        // Inform the user that the PDF report has been generated
        Console.WriteLine($"PDF report generated: {pdfPath}");
    }
}