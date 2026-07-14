// Title: Generate Swiss QR Bill PDF with embedded QR code
// Description: Demonstrates creating a Swiss QR‑Bill PDF by generating a QR code using Aspose.BarCode and embedding it into a PDF with Aspose.Pdf.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and Aspose.Pdf document creation category. It shows how to use the SwissQRBill class together with ComplexBarcodeGenerator to produce a Swiss QR‑Code, and then embed the resulting image into a PDF document using Aspose.Pdf. Developers working on invoicing, payment slips, or any financial documents that require Swiss QR‑Bills can follow this pattern to automate PDF generation.
// Prompt: Use SwissQRBill class to generate a PDF QR‑bill document embedding the generated Swiss QR Code image.
// Tags: swissqr, qr-bill, pdf, barcode generation, aspose.barcode, aspose.pdf

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;
using Aspose.Pdf.Text;

/// <summary>
/// Example program that creates a Swiss QR‑Bill PDF by generating a QR code image
/// with Aspose.BarCode and embedding it into a PDF document using Aspose.Pdf.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Prepares QR‑Bill data, generates the QR code,
    /// embeds it into a PDF, adds bill details as text, and saves the document.
    /// </summary>
    static void Main()
    {
        // Prepare Swiss QR‑Bill data
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Optional additional fields
        swissQr.Bill.BillInformation = "Invoice 12345";

        // Generate QR code image into a memory stream
        using (var qrGenerator = new ComplexBarcodeGenerator(swissQr))
        {
            using (var qrStream = new MemoryStream())
            {
                qrGenerator.Save(qrStream, BarCodeImageFormat.Png);
                qrStream.Position = 0; // Reset stream position for reading

                // Create PDF document and embed the QR code image
                using (var pdfDoc = new Document())
                {
                    var page = pdfDoc.Pages.Add();

                    // Add QR code image to the PDF page
                    var pdfImage = new Image
                    {
                        ImageStream = new MemoryStream(qrStream.ToArray())
                    };
                    // Set image size and remove margins
                    pdfImage.Margin = new MarginInfo(0, 0, 0, 0);
                    pdfImage.FixHeight = 150;
                    pdfImage.FixWidth = 150;
                    page.Paragraphs.Add(pdfImage);

                    // Add bill information as text below the QR code
                    var tf = new TextFragment("Swiss QR Bill\nCreditor: John Doe\nAmount: CHF 199.95")
                    {
                        TextState = { FontSize = 12, Font = FontRepository.FindFont("Helvetica") }
                    };
                    tf.Position = new Position(0, 200);
                    page.Paragraphs.Add(tf);

                    // Save the final PDF document to disk
                    pdfDoc.Save("SwissQRBill.pdf");
                }
            }
        }
    }
}