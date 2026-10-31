// Title: Generate Mailmark Barcode and Embed in PDF
// Description: Demonstrates creating a Mailmark 4‑state barcode with default settings using Aspose.BarCode, then embedding the generated PNG image into a PDF document with Aspose.Pdf.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator and MailmarkCodetext to produce Mailmark barcodes, and the Aspose.Pdf Document API to embed images into PDF files. Developers working with postal barcodes, document automation, or PDF report generation often need to create barcodes and place them directly into PDFs for printing or archival.
// Prompt: Generate a Mailmark barcode with default settings and embed the image into a PDF document.
// Tags: mailmark, barcode, pdf, aspose.barcode, aspose.pdf, image-embedding, generation

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;
using Aspose.Pdf.Text;

/// <summary>
/// Example program that creates a Mailmark barcode and embeds it into a PDF document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Mailmark barcode, saves it to a memory stream, and embeds the image into a PDF file.
    /// </summary>
    static void Main()
    {
        // Prepare output directory and PDF file path
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }
        string pdfPath = Path.Combine(outputDir, "MailmarkBarcode.pdf");

        // Create Mailmark 4‑state codetext with default‑like values
        MailmarkCodetext mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Generate barcode image into a memory stream
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Set image resolution (pixels per module)
            generator.Parameters.Barcode.XDimension.Pixels = 4;

            using (var ms = new MemoryStream())
            {
                // Save barcode as PNG to the memory stream
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // Create a new PDF document and add a page
                using (var pdfDoc = new Document())
                {
                    var page = pdfDoc.Pages.Add();

                    // Create an image object that reads from the memory stream
                    var pdfImage = new Aspose.Pdf.Image
                    {
                        ImageStream = ms,
                        FixWidth = 200,
                        FixHeight = 200,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Margin = new MarginInfo { Top = 20 }
                    };

                    // Add the image to the page's paragraph collection
                    page.Paragraphs.Add(pdfImage);

                    // Save the PDF to the specified path
                    pdfDoc.Save(pdfPath);
                }
            }
        }

        Console.WriteLine($"PDF with Mailmark barcode saved to: {pdfPath}");
    }
}