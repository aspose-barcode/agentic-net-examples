using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Prepare barcode generator for Codabar
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Codabar))
        {
            // Set the data to encode (without start/stop symbols, they are set separately)
            generator.CodeText = "123456";

            // Configure start and stop symbols: A and B
            generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.A;
            generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.B;

            // Save barcode image to a memory stream (PNG format)
            using (MemoryStream barcodeStream = new MemoryStream())
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0; // Reset for reading

                // Create PDF document and add a page
                using (Document pdfDoc = new Document())
                {
                    var page = pdfDoc.Pages.Add();

                    // Define rectangle where the barcode will be placed (llx, lly, urx, ury)
                    var rect = new Aspose.Pdf.Rectangle(100, 500, 400, 600);

                    // Add the barcode image to the PDF page
                    page.AddImage(barcodeStream, rect);

                    // Save the PDF report
                    pdfDoc.Save("CodabarReport.pdf");
                }
            }
        }

        Console.WriteLine("PDF report with Codabar barcode has been created.");
    }
}