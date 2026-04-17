using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Text to encode in the QR code
        const string qrText = "https://example.com";

        // Create QR code generator
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
        {
            // Set high error correction level
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Define image size using unit members
            generator.Parameters.ImageWidth.Point = 200f;
            generator.Parameters.ImageHeight.Point = 200f;

            // Generate barcode image
            using (Bitmap barcodeBitmap = generator.GenerateBarCodeImage())
            {
                // Save barcode to a memory stream in PNG format
                using (var imageStream = new MemoryStream())
                {
                    barcodeBitmap.Save(imageStream, ImageFormat.Png);
                    imageStream.Position = 0;

                    // Create PDF document
                    using (var pdfDoc = new Document())
                    {
                        // Add a page
                        Page page = pdfDoc.Pages.Add();

                        // Insert QR code image
                        var pdfImage = new Aspose.Pdf.Image
                        {
                            ImageStream = imageStream,
                            FixWidth = 200,
                            FixHeight = 200,
                            Margin = new MarginInfo(0, 0, 0, 0)
                        };
                        page.Paragraphs.Add(pdfImage);

                        // Add descriptive text below the image
                        var text = new TextFragment("This QR code links to example.com")
                        {
                            // Position the text a bit lower on the page
                            Position = new Position(0, 250),
                            Margin = new MarginInfo(0, 0, 10, 0)
                        };
                        page.Paragraphs.Add(text);

                        // Save the PDF report
                        pdfDoc.Save("Report.pdf");
                    }
                }
            }
        }
    }
}