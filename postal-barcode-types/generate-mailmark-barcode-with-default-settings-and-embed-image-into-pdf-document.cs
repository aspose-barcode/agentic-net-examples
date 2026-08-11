// Title: Generate Mailmark barcode and embed in PDF
// Description: Demonstrates creating a Mailmark barcode with default settings using Aspose.BarCode, converting it to PNG, and embedding the image into a PDF document with Aspose.Pdf.
// Category-Description: This example belongs to the Aspose.BarCode generation and Aspose.Pdf integration category. It shows how to use ComplexBarcodeGenerator with MailmarkCodetext to produce a barcode image, then use Aspose.Pdf Document to place the image into a PDF. Typical use cases include generating postal Mailmark barcodes for shipping labels and embedding them directly into PDF invoices or documents. Developers often need to combine barcode generation with PDF creation, using classes such as ComplexBarcodeGenerator, MailmarkCodetext, Document, Page, and Image.
// Prompt: Generate a Mailmark barcode with default settings and embed the image into a PDF document.
// Tags: mailmark, barcode, generation, pdf, aspose.barcode, aspose.pdf, image, embedding

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Pdf;
using Aspose.Pdf.Text;

/// <summary>
/// Demonstrates generating a Mailmark barcode and embedding it into a PDF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a Mailmark barcode, saves it as PNG in memory, and inserts it into a PDF.
    /// </summary>
    static void Main()
    {
        // Initialize Mailmark codetext with required default values
        var mailmark = new MailmarkCodetext
        {
            // Mailmark 4‑state format
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            // Destination post code plus DPS must end with a space
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Generate the barcode image and store it in a memory stream
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // Create a new PDF document and add a page
                using (var pdfDoc = new Document())
                {
                    var page = pdfDoc.Pages.Add();

                    // Configure the image object to embed the barcode
                    var pdfImage = new Aspose.Pdf.Image
                    {
                        ImageStream = ms,
                        FixWidth = 200.0,
                        FixHeight = 100.0,
                        HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center,
                        Margin = new MarginInfo { Top = 20 }
                    };

                    // Add the image to the page's paragraph collection
                    page.Paragraphs.Add(pdfImage);

                    // Save the PDF to disk
                    pdfDoc.Save("MailmarkBarcode.pdf");
                }
            }
        }

        Console.WriteLine("Mailmark barcode PDF generated: MailmarkBarcode.pdf");
    }
}