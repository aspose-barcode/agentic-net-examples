// Title: Generate Swiss QR‑Bill PDF with embedded QR Code using Aspose.BarCode
// Description: Demonstrates creating a Swiss QR‑Bill, generating its QR Code barcode, and embedding the image into a PDF document.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as Swiss QR‑Bill. It showcases the use of ComplexBarcodeGenerator, SwissQRCodetext, and Aspose.Pdf to produce a PDF containing a QR‑Code image. Developers needing to automate invoice or payment slip creation can follow this pattern for similar QR‑based billing solutions.
// Prompt: Use SwissQRBill class to generate a PDF QR‑bill document embedding the generated Swiss QR Code image.
// Tags: swissqr, qr-bill, pdf, barcode generation, aspose.barcode, aspose.pdf

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;
using Aspose.Pdf.Text;

/// <summary>
/// Demonstrates generating a Swiss QR‑Bill PDF with an embedded QR Code using Aspose.BarCode and Aspose.Pdf.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the QR‑Bill data, generates the QR Code image, embeds it into a PDF, and saves the file.
    /// </summary>
    static void Main()
    {
        // Define the output PDF file name
        string outputPdf = "SwissQRBill.pdf";

        // --------------------------------------------------------------------
        // Create Swiss QR Code codetext and populate bill details
        // --------------------------------------------------------------------
        var swissQRCode = new SwissQRCodetext();
        swissQRCode.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissQRCode.Bill.Account = "CH4431999123000889012";
        swissQRCode.Bill.Amount = 1000.25m;
        swissQRCode.Bill.Currency = "CHF";
        swissQRCode.Bill.Reference = "210000000003139471430009017";

        // Set creditor (payee) address information
        swissQRCode.Bill.Creditor = new Address
        {
            Name = "Muster & Söhne",
            Street = "Musterstrasse",
            HouseNo = "12b",
            PostalCode = "8200",
            Town = "Zürich",
            CountryCode = "CH"
        };

        // Set debtor (payer) address information
        swissQRCode.Bill.Debtor = new Address
        {
            Name = "Muster AG",
            Street = "Musterstrasse",
            HouseNo = "1",
            PostalCode = "3030",
            Town = "Bern",
            CountryCode = "CH"
        };

        // --------------------------------------------------------------------
        // Generate the Swiss QR Code barcode image
        // --------------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(swissQRCode))
        {
            // Configure barcode appearance
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;

            // Save the barcode image to a memory stream (PNG format)
            using (var barcodeStream = new MemoryStream())
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0; // Reset stream position for reading

                // ----------------------------------------------------------------
                // Create a PDF document and embed the barcode image
                // ----------------------------------------------------------------
                using (var pdfDoc = new Document())
                {
                    // Add a new page to the PDF
                    var page = pdfDoc.Pages.Add();

                    // Create an Aspose.Pdf.Image object from the barcode stream
                    var pdfImage = new Aspose.Pdf.Image
                    {
                        ImageStream = barcodeStream,
                        FixWidth = 200.0,
                        FixHeight = 200.0,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Margin = new MarginInfo { Top = 20 }
                    };

                    // Add the image to the page's paragraph collection
                    page.Paragraphs.Add(pdfImage);

                    // Save the PDF document to the specified file
                    pdfDoc.Save(outputPdf);
                }
            }
        }

        // Inform the user where the PDF was saved
        Console.WriteLine($"PDF QR‑bill generated: {Path.GetFullPath(outputPdf)}");
    }
}