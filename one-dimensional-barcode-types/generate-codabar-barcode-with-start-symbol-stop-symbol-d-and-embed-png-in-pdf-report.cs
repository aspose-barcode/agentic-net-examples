using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create Codabar barcode generator with start symbol A and stop symbol D
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar))
        {
            // Set start and stop symbols
            generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.A;
            generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.D;

            // Set the data to encode (without start/stop symbols)
            generator.CodeText = "123456";

            // Optional: let the generator size the image automatically
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Save barcode image to a memory stream in PNG format
            using (var barcodeStream = new MemoryStream())
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0; // Reset for reading

                // Create PDF document and embed the barcode image
                using (var pdfDoc = new Aspose.Pdf.Document())
                {
                    var page = pdfDoc.Pages.Add();

                    var pdfImage = new Aspose.Pdf.Image
                    {
                        ImageStream = barcodeStream
                    };

                    page.Paragraphs.Add(pdfImage);

                    // Save the PDF report
                    pdfDoc.Save("CodabarReport.pdf");
                }
            }
        }
    }
}