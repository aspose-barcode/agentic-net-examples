// Title: Generate and embed a Postnet barcode into a PDF
// Description: This example creates a Postnet postal barcode and places it onto an existing PDF page at a specific location.
// Category-Description: Demonstrates how to use Aspose.BarCode to generate barcode images and Aspose.Pdf to insert those images into PDF documents. Typical scenarios include adding shipping or mailing barcodes to invoices, labels, or reports. Developers often need to generate barcodes on‑the‑fly and embed them without creating intermediate files.
// Prompt: Generate a postal barcode and embed it directly into an existing PDF page at a specified coordinate.
// Tags: postnet, barcode generation, pdf embedding, aspose.barcode, aspose.pdf, image insertion, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Demonstrates generating a Postnet barcode and embedding it into a PDF page.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, creates a placeholder PDF if needed, and embeds the barcode at defined coordinates.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Input and output PDF file paths
        string inputPdfPath = "input.pdf";
        string outputPdfPath = "output.pdf";

        // Ensure the input PDF exists; create a simple one if it does not.
        if (!File.Exists(inputPdfPath))
        {
            using (var doc = new Document())
            {
                doc.Pages.Add(); // add a blank page
                doc.Save(inputPdfPath);
            }
        }

        // Generate a postal barcode (Postnet) and embed it into the PDF.
        // Sample code text "12345678" – adjust as needed.
        using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, "12345678"))
        {
            // Optional: set barcode colors
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save barcode image to a memory stream in PNG format.
            using (var barcodeStream = new MemoryStream())
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0; // reset for reading

                // Load the existing PDF and add the barcode image at specified coordinates.
                using (var pdfDoc = new Document(inputPdfPath))
                {
                    var page = pdfDoc.Pages[1];

                    // Define placement rectangle: lower-left (100,500), upper-right (250,650)
                    var rect = new Aspose.Pdf.Rectangle(100, 500, 250, 650);

                    // Add the image to the page.
                    page.AddImage(barcodeStream, rect);

                    // Save the modified PDF.
                    pdfDoc.Save(outputPdfPath);
                }
            }
        }

        Console.WriteLine($"Barcode embedded successfully. Output saved to '{outputPdfPath}'.");
    }
}