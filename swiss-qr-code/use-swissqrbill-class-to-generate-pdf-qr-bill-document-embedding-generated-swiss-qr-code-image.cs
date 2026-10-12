// Title: Generate Swiss QR‑Bill PDF with embedded QR Code using Aspose.BarCode
// Description: Demonstrates creating a Swiss QR‑Bill data object, rendering it as a QR Code image, and embedding that image into a PDF document.
// Category-Description: This example belongs to the Aspose.BarCode PDF integration category, showcasing how to use ComplexBarcodeGenerator with SwissQRCodetext to produce QR‑Code images and embed them into Aspose.Pdf documents. Typical use cases include generating payment QR‑bills for Swiss banking standards. Developers often need to combine barcode generation with PDF creation for invoicing and financial documents.
// Prompt: Use SwissQRBill class to generate a PDF QR‑bill document embedding the generated Swiss QR Code image.
// Tags: swissqr, qr-bill, pdf, barcode generation, aspose.barcode, aspose.pdf

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Demonstrates generating a Swiss QR‑Bill PDF with an embedded QR Code using Aspose.BarCode and Aspose.Pdf.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the QR‑Bill data, generates a QR Code image, embeds it into a PDF, and saves the file.
    /// </summary>
    static void Main()
    {
        // Define output PDF file name
        string outputPdf = "SwissQRBill.pdf";

        // ------------------------------------------------------------
        // Create Swiss QR Code data (bill information)
        // ------------------------------------------------------------
        var swissQRCode = new SwissQRCodetext();
        swissQRCode.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissQRCode.Bill.Account = "CH4431999123000889012";
        swissQRCode.Bill.Amount = 1000.25m;
        swissQRCode.Bill.Currency = "CHF";
        swissQRCode.Bill.Reference = "210000000003139471430009017";
        swissQRCode.Bill.Creditor = new Address
        {
            Name = "Muster & Söhne",
            Street = "Musterstrasse",
            HouseNo = "12b",
            PostalCode = "8200",
            Town = "Zürich",
            CountryCode = "CH"
        };
        swissQRCode.Bill.Debtor = new Address
        {
            Name = "Muster AG",
            Street = "Musterstrasse",
            HouseNo = "1",
            PostalCode = "3030",
            Town = "Bern",
            CountryCode = "CH"
        };

        // ------------------------------------------------------------
        // Generate QR code image into a memory stream
        // ------------------------------------------------------------
        using (var barcodeStream = new MemoryStream())
        {
            using (var generator = new ComplexBarcodeGenerator(swissQRCode))
            {
                // Set image resolution and QR encoding options
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
                generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;

                // Save QR code as PNG into the stream
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
            }

            // Reset stream position for reading
            barcodeStream.Position = 0;

            // ------------------------------------------------------------
            // Create PDF document and embed the QR code image
            // ------------------------------------------------------------
            using (var pdfDoc = new Document())
            {
                // Add a new page to the PDF
                var page = pdfDoc.Pages.Add();

                // Configure image properties
                var pdfImage = new Aspose.Pdf.Image
                {
                    ImageStream = barcodeStream,
                    FixWidth = 200.0,
                    FixHeight = 200.0,
                    HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center,
                    VerticalAlignment = Aspose.Pdf.VerticalAlignment.Center,
                    Margin = new Aspose.Pdf.MarginInfo { Top = 20, Bottom = 20 }
                };

                // Add the image to the page
                page.Paragraphs.Add(pdfImage);

                // Save the PDF to disk
                pdfDoc.Save(outputPdf);
            }
        }

        // Inform the user where the PDF was saved
        Console.WriteLine($"PDF generated: {Path.GetFullPath(outputPdf)}");
    }
}