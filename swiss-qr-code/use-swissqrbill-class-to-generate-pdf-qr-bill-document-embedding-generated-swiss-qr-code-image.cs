using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Prepare Swiss QR bill data
        var qrCodeText = new SwissQRCodetext();
        qrCodeText.Bill.Account = "CH9300762011623852957";
        qrCodeText.Bill.Amount = 199.95m;
        qrCodeText.Bill.Currency = "CHF";
        qrCodeText.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Creditor address (minimal required fields)
        qrCodeText.Bill.Creditor.Name = "Creditor Name";
        qrCodeText.Bill.Creditor.Street = "Main Street";
        qrCodeText.Bill.Creditor.HouseNo = "1";
        qrCodeText.Bill.Creditor.PostalCode = "8000";
        qrCodeText.Bill.Creditor.Town = "Zurich";
        qrCodeText.Bill.Creditor.CountryCode = "CH";

        // Optional debtor address
        qrCodeText.Bill.Debtor.Name = "Debtor Name";
        qrCodeText.Bill.Debtor.Street = "Second Street";
        qrCodeText.Bill.Debtor.HouseNo = "2";
        qrCodeText.Bill.Debtor.PostalCode = "3000";
        qrCodeText.Bill.Debtor.Town = "Bern";
        qrCodeText.Bill.Debtor.CountryCode = "CH";

        // Generate Swiss QR code image into a memory stream
        using (var qrStream = new MemoryStream())
        {
            using (var generator = new ComplexBarcodeGenerator(qrCodeText))
            {
                // Save as PNG; keep the stream open for PDF embedding
                generator.Save(qrStream, BarCodeImageFormat.Png);
                qrStream.Position = 0;
            }

            // Create PDF document and embed the QR code image
            using (var pdfDoc = new Document())
            {
                var page = pdfDoc.Pages.Add();

                // Add image using the same stream; keep the stream alive until after Save
                var image = new Aspose.Pdf.Image
                {
                    ImageStream = qrStream,
                    // Adjust size as needed (e.g., 150 points width, maintain aspect ratio)
                    FixWidth = 150,
                    FixHeight = 150
                };
                page.Paragraphs.Add(image);

                // Save PDF to file
                pdfDoc.Save("SwissQRBill.pdf");
            }
        }
    }
}