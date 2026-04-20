using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Prepare primary data for HIBC LIC barcode
        var primaryCodetext = new HIBCLICPrimaryDataCodetext();
        primaryCodetext.BarcodeType = EncodeTypes.HIBCCode128LIC;
        primaryCodetext.Data = new PrimaryData();
        primaryCodetext.Data.ProductOrCatalogNumber = "12345";
        primaryCodetext.Data.LabelerIdentificationCode = "A999";
        primaryCodetext.Data.UnitOfMeasureID = 1;

        // Generate barcode image and store it in a memory stream
        using (var generator = new ComplexBarcodeGenerator(primaryCodetext))
        {
            using (var bitmap = generator.GenerateBarCodeImage())
            {
                using (var imageStream = new MemoryStream())
                {
                    bitmap.Save(imageStream, ImageFormat.Png);
                    imageStream.Position = 0;

                    // Create a PDF document and embed the barcode image
                    using (var pdfDoc = new Document())
                    {
                        var page = pdfDoc.Pages.Add();
                        var pdfImage = new Aspose.Pdf.Image
                        {
                            ImageStream = imageStream,
                            // Adjust size as needed
                            FixWidth = 200f,
                            FixHeight = 100f
                        };
                        page.Paragraphs.Add(pdfImage);
                        pdfDoc.Save("HIBCLICPrimary.pdf");
                    }
                }
            }
        }
    }
}