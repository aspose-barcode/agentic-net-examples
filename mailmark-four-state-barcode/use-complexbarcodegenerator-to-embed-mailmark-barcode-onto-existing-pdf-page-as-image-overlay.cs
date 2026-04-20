using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        string inputPdfPath = "input.pdf";
        string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            using (var doc = new Document())
            {
                doc.Pages.Add();
                doc.Save(inputPdfPath);
            }
        }

        var mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        MemoryStream barcodeStream;
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // BarHeight must be positive; set using Inches to avoid zero size
            generator.Parameters.Barcode.BarHeight.Inches = 0.02f;
            generator.Parameters.Resolution = 300;

            barcodeStream = new MemoryStream();
            generator.Save(barcodeStream, BarCodeImageFormat.Png);
            barcodeStream.Position = 0;
        }

        using (var pdfDoc = new Document(inputPdfPath))
        {
            var page = pdfDoc.Pages[1];
            var rect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);
            page.AddImage(barcodeStream, rect);
            pdfDoc.Save(outputPdfPath);
        }

        barcodeStream.Dispose();
    }
}