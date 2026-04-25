using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Output PDF file path
        const string outputPdfPath = "qr_form.pdf";

        // Generate QR code and save to a memory stream as PNG
        using (var barcodeStream = new MemoryStream())
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                generator.CodeText = "https://example.com";
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
            }

            // Reset stream position before reading
            barcodeStream.Position = 0;

            // Create PDF document and add the barcode image
            using (var pdfDoc = new Document())
            {
                var page = pdfDoc.Pages.Add();
                // Place the image at specified rectangle (llx, lly, urx, ury)
                page.AddImage(barcodeStream, new Aspose.Pdf.Rectangle(100, 500, 300, 700));
                pdfDoc.Save(outputPdfPath);
            }
        }

        Console.WriteLine("QR code added to PDF as a static image. Interactive ButtonField image embedding requires System.Drawing support not available in this runner.");
    }
}