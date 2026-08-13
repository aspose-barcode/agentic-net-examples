// Title: Generate Swiss QR‑Bill PDF with embedded QR code using Aspose.BarCode
// Description: Demonstrates how to create a Swiss QR‑Bill, render its QR code as an image, and embed it into a PDF document.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator with SwissQRCodetext to produce a QR‑Bill QR code, and the Aspose.Pdf library to embed the image into a PDF. Developers working with financial documents, QR‑based payments, or PDF reporting often need to generate QR‑Bill PDFs programmatically.
// Prompt: Use SwissQRBill class to generate a PDF QR‑bill document embedding the generated Swiss QR Code image.
// Tags: barcode symbology, generation, pdf, swissqr, complexbarcode, aspose.pdf

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;
using Aspose.Pdf.Text;

/// <summary>
/// Example program that creates a Swiss QR‑Bill, generates its QR code image,
/// and embeds the image into a PDF document using Aspose.BarCode and Aspose.Pdf.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare Swiss QR‑Bill data
        // ------------------------------------------------------------
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // ------------------------------------------------------------
        // Generate QR code image and store it in a memory stream
        // ------------------------------------------------------------
        using (var qrStream = new MemoryStream())
        {
            // Use ComplexBarcodeGenerator to render the QR code as PNG
            using (var generator = new ComplexBarcodeGenerator(swissQr))
            {
                generator.Save(qrStream, BarCodeImageFormat.Png);
            }

            // Reset the stream position so it can be read from the beginning
            qrStream.Position = 0;

            // ------------------------------------------------------------
            // Create a PDF document and embed the QR code image
            // ------------------------------------------------------------
            var pdfDoc = new Document();
            var page = pdfDoc.Pages.Add();

            var pdfImage = new Aspose.Pdf.Image
            {
                ImageStream = qrStream,
                FixWidth = 150,
                FixHeight = 150,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new MarginInfo { Top = 20 }
            };

            // Add the image to the page's paragraph collection
            page.Paragraphs.Add(pdfImage);

            // ------------------------------------------------------------
            // Save the PDF to a file
            // ------------------------------------------------------------
            string outputPath = "SwissQRBill.pdf";
            pdfDoc.Save(outputPath);
            Console.WriteLine($"PDF with Swiss QR code saved to: {Path.GetFullPath(outputPath)}");
        }
    }
}