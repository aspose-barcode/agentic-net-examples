// Title: Generate Mailmark barcode and embed in PDF
// Description: Demonstrates creating a Mailmark barcode with default settings and inserting it into a PDF document.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, showcasing the use of ComplexBarcodeGenerator and MailmarkCodetext to produce Mailmark symbology. Typical use cases include embedding postal barcodes into documents such as PDFs for mailing automation. Developers often need to generate barcode images and combine them with other file formats using Aspose.Pdf.
// Prompt: Generate a Mailmark barcode with default settings and embed the image into a PDF document.
// Tags: mailmark, barcode, pdf, aspose.barcode, aspose.pdf, complexbarcodegenerator, image-embedding

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Example program that creates a Mailmark barcode and embeds it into a PDF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Mailmark barcode image and saves it inside a PDF document.
    /// </summary>
    static void Main()
    {
        // Initialize MailmarkCodetext with required default values.
        var mailmark = new MailmarkCodetext
        {
            Format = 4,                         // 4‑state format
            VersionID = 1,                      // version
            Class = "0",                        // class (null/test)
            SupplychainID = 384224,             // supply chain identifier
            ItemID = 16563762,                  // item identifier
            DestinationPostCodePlusDPS = "EF61AH8T " // known valid postcode+DP
        };

        // Generate the Mailmark barcode using ComplexBarcodeGenerator.
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Produce the barcode image (Aspose.Drawing.Bitmap).
            using (var bitmap = generator.GenerateBarCodeImage())
            {
                // Save the bitmap to a memory stream in PNG format.
                using (var imageStream = new MemoryStream())
                {
                    bitmap.Save(imageStream, Aspose.Drawing.Imaging.ImageFormat.Png);
                    imageStream.Position = 0; // Reset stream position for reading.

                    // Create a new PDF document and add a page.
                    var pdfDoc = new Document();
                    var page = pdfDoc.Pages.Add();

                    // Create an Aspose.Pdf.Image from the barcode stream.
                    var pdfImage = new Aspose.Pdf.Image
                    {
                        ImageStream = imageStream
                    };

                    // Insert the image into the PDF page.
                    page.Paragraphs.Add(pdfImage);

                    // Save the resulting PDF to disk.
                    pdfDoc.Save("Mailmark.pdf");
                }
            }
        }
    }
}