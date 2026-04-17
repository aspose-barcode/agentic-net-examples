using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        const string outputPdf = "GS1Code128.pdf";
        const string gs1Data = "(01)12345678901231";

        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1Code128, gs1Data))
        {
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Parameters.Barcode.BarHeight.Point = 50f;

            using (MemoryStream barcodeStream = new MemoryStream())
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0;

                using (Document pdfDoc = new Document())
                {
                    Page page = pdfDoc.Pages.Add();

                    Aspose.Pdf.Image image = new Aspose.Pdf.Image
                    {
                        ImageStream = barcodeStream
                    };
                    page.Paragraphs.Add(image);

                    pdfDoc.Save(outputPdf);
                }
            }
        }

        Console.WriteLine($"PDF with GS1 Code 128 barcode saved to '{outputPdf}'.");
    }
}