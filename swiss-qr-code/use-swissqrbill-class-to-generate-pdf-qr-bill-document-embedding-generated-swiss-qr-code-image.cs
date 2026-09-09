// Title: Generate Swiss QR‑Bill PDF with embedded QR code
// Description: Demonstrates creating a Swiss QR‑Bill using Aspose.BarCode, rendering it as a PNG, and embedding the image into a PDF document.
// Category-Description: This example belongs to the Aspose.BarCode PDF generation and complex barcode category. It showcases the SwissQRCodetext and ComplexBarcodeGenerator classes to produce a Swiss QR‑Bill, then uses Aspose.Pdf to embed the generated QR code image into a PDF. Developers needing to create QR‑bill PDFs for Swiss payments can follow this pattern for similar use‑cases.
// Prompt: Use SwissQRBill class to generate a PDF QR‑bill document embedding the generated Swiss QR Code image.
// Tags: swissqr, qr-bill, pdf, barcode generation, aspose.barcode, aspose.pdf

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Swiss QR‑Bill, converts it to a PNG image,
/// and embeds the image into a PDF document using Aspose libraries.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the QR‑Bill, saves the PDF, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the output PDF file path in the temporary folder.
        string outputPdf = Path.Combine(Path.GetTempPath(), "SwissQRBill.pdf");

        // --------------------------------------------------------------------
        // Build the Swiss QR Code payload (codetext) with required bill data.
        // --------------------------------------------------------------------
        var swissQRCode = new SwissQRCodetext();
        swissQRCode.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissQRCode.Bill.Account = "CH4431999123000889012";
        swissQRCode.Bill.Amount = 1000.25m;
        swissQRCode.Bill.Currency = "CHF";
        swissQRCode.Bill.Reference = "210000000003139471430009017";

        // Set creditor address.
        swissQRCode.Bill.Creditor = new Address
        {
            Name = "Muster & Söhne",
            Street = "Musterstrasse",
            HouseNo = "12b",
            PostalCode = "8200",
            Town = "Zürich",
            CountryCode = "CH"
        };

        // Set debtor address.
        swissQRCode.Bill.Debtor = new Address
        {
            Name = "Muster AG",
            Street = "Musterstrasse",
            HouseNo = "1",
            PostalCode = "3030",
            Town = "Bern",
            CountryCode = "CH"
        };

        // --------------------------------------------------------------
        // Generate the QR code image using ComplexBarcodeGenerator.
        // --------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(swissQRCode))
        {
            // Configure image quality and encoding.
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;

            // Render the QR code to a memory stream as PNG.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.

                // --------------------------------------------------------------
                // Create a PDF document and embed the QR code image.
                // --------------------------------------------------------------
                var pdfDoc = new Aspose.Pdf.Document();
                var page = pdfDoc.Pages.Add();

                var pdfImage = new Aspose.Pdf.Image
                {
                    ImageStream = ms,
                    FixWidth = 200.0,
                    FixHeight = 200.0,
                    HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center,
                    VerticalAlignment = Aspose.Pdf.VerticalAlignment.Center,
                    Margin = new Aspose.Pdf.MarginInfo { Top = 20 }
                };

                // Add the image to the page and save the PDF.
                page.Paragraphs.Add(pdfImage);
                pdfDoc.Save(outputPdf);
            }
        }

        // Inform the user where the PDF was saved.
        Console.WriteLine($"PDF saved to: {outputPdf}");
    }
}