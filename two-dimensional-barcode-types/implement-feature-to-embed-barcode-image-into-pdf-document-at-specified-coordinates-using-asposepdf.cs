// Title: Embed a Code128 barcode into a PDF at specific coordinates
// Description: Demonstrates generating a Code128 barcode image and placing it at defined X/Y coordinates within a PDF using Aspose.BarCode and Aspose.PDF.
// Category-Description: This example belongs to the Aspose.BarCode and Aspose.PDF integration category, illustrating how to combine barcode generation with PDF document creation. It showcases key API classes such as BarcodeGenerator, Document, Page, and Image, which developers commonly use to embed barcodes into reports, invoices, or shipping labels. Ideal for scenarios where precise placement of barcode graphics within PDF layouts is required.
// Prompt: Implement feature to embed barcode image into PDF document at specified coordinates using Aspose.PDF
// Tags: barcode, code128, embed, pdf, aspose.barcode, aspose.pdf, image, coordinates

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Pdf;
using Aspose.Pdf.Text;

/// <summary>
/// Example program that generates a Code128 barcode and embeds it into a PDF document at specified coordinates.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode, creates a PDF, and places the barcode image on the page.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the output PDF file name.
        string pdfPath = "BarcodeEmbedded.pdf";

        // Initialize a barcode generator for Code128 with sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the barcode's bar color to black.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

            // Render the barcode to a memory stream in PNG format.
            using (var barcodeStream = new MemoryStream())
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0; // Reset stream position for reading.

                // Create a new PDF document.
                using (var pdfDoc = new Document())
                {
                    // Add a single page to the document.
                    var page = pdfDoc.Pages.Add();

                    // Create an image object from the barcode stream.
                    var image = new Aspose.Pdf.Image
                    {
                        ImageStream = barcodeStream,
                        // Position the image using left and bottom margins (coordinates in points).
                        Margin = new MarginInfo { Left = 100f, Bottom = 200f }
                    };

                    // Add the image to the page's paragraph collection.
                    page.Paragraphs.Add(image);

                    // Save the PDF document to the specified path.
                    pdfDoc.Save(pdfPath);
                }
            }
        }

        // Inform the user where the PDF was saved.
        Console.WriteLine($"PDF with embedded barcode saved to: {Path.GetFullPath(pdfPath)}");
    }
}