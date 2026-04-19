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
        const string pdfPath = "BarcodeDocument.pdf";
        const string barcodeText = "1234567890";
        const float barcodeX = 100f;
        const float barcodeY = 500f;

        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, barcodeText))
        {
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                using (var imageStream = new MemoryStream())
                {
                    bitmap.Save(imageStream, ImageFormat.Png);
                    imageStream.Position = 0;

                    using (var pdfDocument = new Document())
                    {
                        var page = pdfDocument.Pages.Add();

                        var pdfImage = new Aspose.Pdf.Image
                        {
                            ImageStream = imageStream,
                            FixWidth = bitmap.Width,
                            FixHeight = bitmap.Height,
                            Margin = new MarginInfo(barcodeX, barcodeY, 0, 0)
                        };

                        page.Paragraphs.Add(pdfImage);
                        pdfDocument.Save(pdfPath);
                    }
                }
            }
        }

        Console.WriteLine($"PDF created successfully at '{Path.GetFullPath(pdfPath)}'.");
    }
}