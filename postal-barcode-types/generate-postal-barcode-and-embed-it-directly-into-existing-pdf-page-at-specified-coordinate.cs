using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Paths for input and output PDF files
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // Load existing PDF or create a new one if it does not exist
        Document pdfDocument;
        if (File.Exists(inputPdfPath))
        {
            pdfDocument = new Document(inputPdfPath);
        }
        else
        {
            pdfDocument = new Document();
            pdfDocument.Pages.Add(); // add a blank page
        }

        // Generate a postal barcode (Postnet) with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, "12345678"))
        {
            // Example of setting a postal-specific parameter (short bar height)
            generator.Parameters.Barcode.Postal.ShortBarHeight.Point = 10f;

            // Save barcode image to a memory stream in PNG format
            using (var barcodeStream = new MemoryStream())
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0; // reset stream position for reading

                // Define placement rectangle on the first page (coordinates in points)
                var page = pdfDocument.Pages[1];
                var placement = new Aspose.Pdf.Rectangle(100, 100, 200, 150); // llx, lly, urx, ury

                // Embed the barcode image into the PDF page
                page.AddImage(barcodeStream, placement);

                // Save the modified PDF
                pdfDocument.Save(outputPdfPath);
            }
        }
    }
}