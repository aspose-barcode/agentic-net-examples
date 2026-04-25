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
        // Output PDF file path
        string pdfPath = "BarcodeDocument.pdf";

        // Generate barcode image into a memory stream
        using (var barcodeStream = new MemoryStream())
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Optional visual settings
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Save barcode as PNG to the stream
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0;
            }

            // Create a new PDF document and embed the barcode image
            using (var pdfDoc = new Aspose.Pdf.Document())
            {
                // Add a page to the document
                var page = pdfDoc.Pages.Add();

                // Define placement rectangle (points)
                float llx = 100f;   // lower-left X
                float lly = 500f;   // lower-left Y
                float width = 200f; // desired width
                float height = 100f; // desired height
                float urx = llx + width; // upper-right X
                float ury = lly + height; // upper-right Y

                // Add the barcode image at the specified coordinates
                page.AddImage(barcodeStream, new Aspose.Pdf.Rectangle(llx, lly, urx, ury));

                // Save the PDF document
                pdfDoc.Save(pdfPath);
            }
        }

        Console.WriteLine("PDF created at: " + Path.GetFullPath(pdfPath));
    }
}