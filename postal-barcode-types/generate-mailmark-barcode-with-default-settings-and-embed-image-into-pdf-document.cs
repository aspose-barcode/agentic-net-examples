// Title: Generate Mailmark barcode and embed in PDF
// Description: Demonstrates creating a Mailmark 4‑State barcode with default settings using Aspose.BarCode, converting it to PNG, and inserting the image into a PDF with Aspose.Pdf.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as Mailmark. It shows how to use ComplexBarcodeGenerator with MailmarkCodetext, adjust barcode parameters, and combine the output with Aspose.Pdf to produce a document. Developers working on shipping, logistics, or postal applications often need to generate Mailmark symbols and embed them into PDFs for printing or electronic distribution.
// Prompt: Generate a Mailmark barcode with default settings and embed the image into a PDF document.
// Tags: mailmark, barcode, generation, pdf, aspose.barcode, aspose.pdf, complexbarcode, image, embed

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Example program that creates a Mailmark barcode and embeds it into a PDF document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, adds it to a PDF, and saves the result.
    /// </summary>
    static void Main()
    {
        // Define output directory and PDF file path
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);
        string pdfPath = Path.Combine(outputDir, "MailmarkBarcode.pdf");

        // Create Mailmark 4‑State codetext with default values
        var mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Generate the barcode image using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Set barcode resolution (pixels per module)
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // Create a new PDF document and add a page
                using (var pdfDoc = new Document())
                {
                    var page = pdfDoc.Pages.Add();

                    // Create an image object from the barcode stream
                    var pdfImage = new Aspose.Pdf.Image
                    {
                        ImageStream = ms,
                        FixWidth = 200.0,
                        FixHeight = 200.0,
                        HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center,
                        VerticalAlignment = Aspose.Pdf.VerticalAlignment.Center
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