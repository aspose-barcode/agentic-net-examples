// Title: Generate Codabar barcode and embed in PDF report
// Description: Demonstrates creating a Codabar barcode with start symbol A and stop symbol D, saving it as a PNG, and inserting the image into a PDF document.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use BarcodeGenerator with Codabar symbology, customize start/stop symbols, and combine the output with Aspose.Pdf to produce a PDF report. Developers often need to generate barcodes for inventory, shipping, or point‑of‑sale systems and embed them directly into documents for printing or electronic distribution.
// Prompt: Generate a Codabar barcode with start symbol A, stop symbol D, and embed the PNG in a PDF report.
// Tags: codabar, barcode generation, pdf embedding, aspose.barcode, aspose.pdf, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Demonstrates generating a Codabar barcode and embedding it into a PDF report.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, creates a PDF, and saves it to disk.
    /// </summary>
    static void Main()
    {
        // Define the output PDF file path in the current working directory.
        string pdfPath = Path.Combine(Directory.GetCurrentDirectory(), "CodabarReport.pdf");

        // Initialize the barcode generator for Codabar with the data string "123456".
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "123456"))
        {
            // Configure start and stop symbols as required (A and D).
            generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.A;
            generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.D;

            // Optional: set the barcode color to black.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

            // Create a memory stream to hold the generated PNG image.
            using (var imageStream = new MemoryStream())
            {
                // Save the barcode image as PNG into the memory stream.
                generator.Save(imageStream, BarCodeImageFormat.Png);
                // Reset stream position to the beginning for reading.
                imageStream.Position = 0;

                // Create a new PDF document.
                using (var pdfDoc = new Document())
                {
                    // Add a page to the PDF.
                    var page = pdfDoc.Pages.Add();

                    // Create an Image object that uses the barcode PNG stream.
                    var pdfImage = new Image
                    {
                        ImageStream = imageStream,
                        FixWidth = 200f,
                        FixHeight = 100f,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Margin = new MarginInfo { Top = 20f }
                    };

                    // Add the image to the page's paragraph collection.
                    page.Paragraphs.Add(pdfImage);

                    // Save the PDF document to the specified path.
                    pdfDoc.Save(pdfPath);
                }
            }
        }

        // Inform the user that the PDF report has been generated.
        Console.WriteLine($"PDF report generated: {pdfPath}");
    }
}