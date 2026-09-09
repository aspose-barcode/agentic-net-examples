// Title: Generate Mailmark Barcode and Embed in PDF
// Description: Demonstrates creating a Mailmark barcode with default settings using Aspose.BarCode and embedding the generated PNG into a PDF document with Aspose.Pdf.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator and MailmarkCodetext to produce a Mailmark symbology, then embeds the resulting image into a PDF using Aspose.Pdf's Document and Image classes. Typical scenarios include generating postal barcodes for mailing labels and integrating them into printable documents. Developers often need to combine barcode creation with PDF rendering, making this pattern a common reference point.
/// Prompt: Generate a Mailmark barcode with default settings and embed the image into a PDF document.
/// Tags: mailmark, barcode, pdf, aspose.barcode, aspose.pdf, image-generation, complex-barcode

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;
using Aspose.Pdf.Text;

/// <summary>
/// Example program that creates a Mailmark barcode and embeds it into a PDF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define output directory and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting PDF file
        string pdfPath = Path.Combine(outputDir, "Mailmark.pdf");

        // Configure Mailmark barcode data with default settings
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
            using (var ms = new MemoryStream())
            {
                // Save barcode as PNG into memory stream
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // Create a new PDF document
                using (var pdfDoc = new Document())
                {
                    // Add a page to the PDF
                    var page = pdfDoc.Pages.Add();

                    // Create an image object from the barcode stream
                    var pdfImage = new Aspose.Pdf.Image
                    {
                        ImageStream = ms,
                        FixWidth = 200.0,
                        FixHeight = 200.0,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Margin = new MarginInfo { Top = 20 }
                    };

                    // Add the image to the page's paragraphs collection
                    page.Paragraphs.Add(pdfImage);

                    // Save the PDF to the specified path
                    pdfDoc.Save(pdfPath);
                }
            }
        }

        // Inform the user where the PDF was saved
        Console.WriteLine($"PDF saved to {pdfPath}");
    }
}