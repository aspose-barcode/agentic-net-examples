using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Define output file names
        const string barcodeImagePath = "codabar.png";
        const string pdfReportPath = "CodabarReport.pdf";

        // Create Codabar barcode with start symbol A and stop symbol B
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "123456"))
        {
            // Set start and stop symbols
            generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.A;
            generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.B;

            // Optional: set colors
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save barcode image to a file (also keep it in memory for PDF embedding)
            generator.Save(barcodeImagePath);
            using (var barcodeStream = new MemoryStream())
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0;

                // Create PDF document and embed the barcode image
                using (var pdfDoc = new Document())
                {
                    var page = pdfDoc.Pages.Add();

                    // Define rectangle where the image will be placed (llx, lly, urx, ury)
                    var rect = new Aspose.Pdf.Rectangle(100, 500, 400, 700);
                    page.AddImage(barcodeStream, rect);

                    // Save the PDF report
                    pdfDoc.Save(pdfReportPath);
                }
            }
        }

        Console.WriteLine($"Barcode image saved to: {Path.GetFullPath(barcodeImagePath)}");
        Console.WriteLine($"PDF report saved to: {Path.GetFullPath(pdfReportPath)}");
    }
}