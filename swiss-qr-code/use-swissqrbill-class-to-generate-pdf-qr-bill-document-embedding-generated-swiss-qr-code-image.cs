using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Demonstrates generating a Swiss QR bill QR code and embedding it into a PDF using Aspose libraries.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Prepares QR bill data, creates a QR code image, and embeds it into a PDF.
    /// </summary>
    static void Main()
    {
        // Prepare Swiss QR bill data
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Generate QR code image into a memory stream
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            using (var qrStream = new MemoryStream())
            {
                // Save QR code as PNG into the stream
                generator.Save(qrStream, BarCodeImageFormat.Png);
                // Reset stream position for reading
                qrStream.Position = 0;

                // Create a new PDF document
                using (var pdfDoc = new Document())
                {
                    // Add a page to the PDF
                    var page = pdfDoc.Pages.Add();

                    // Create an image object that reads from the QR code stream
                    var pdfImage = new Aspose.Pdf.Image
                    {
                        ImageStream = qrStream,
                        // Set desired dimensions for the QR code image
                        FixWidth = 200.0,
                        FixHeight = 200.0
                    };

                    // Add the image to the page's paragraph collection
                    page.Paragraphs.Add(pdfImage);

                    // Define output file name
                    string outputPdf = "SwissQRBill.pdf";
                    // Save the PDF to disk
                    pdfDoc.Save(outputPdf);
                    // Inform the user where the file was saved
                    Console.WriteLine($"PDF QR‑bill generated: {Path.GetFullPath(outputPdf)}");
                }
            }
        }
    }
}