using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Define output files in the temporary directory
        string outputPdfPath = Path.Combine(Path.GetTempPath(), "QrCodeReport.pdf");

        // Generate QR Code barcode and store it in a memory stream
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set QR code specific parameters
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            using (var barcodeStream = new MemoryStream())
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0;

                // Create PDF document and embed the barcode image with descriptive text
                using (var pdfDoc = new Document())
                {
                    var page = pdfDoc.Pages.Add();

                    // Add title text
                    var title = new TextFragment("QR Code Report");
                    title.TextState.FontSize = 18;
                    title.TextState.FontStyle = FontStyles.Bold;
                    title.Position = new Position(50, 750);
                    page.Paragraphs.Add(title);

                    // Add the barcode image
                    var pdfImage = new Aspose.Pdf.Image
                    {
                        ImageStream = barcodeStream,
                        FixWidth = 200.0,
                        FixHeight = 200.0,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Margin = new MarginInfo { Top = 20 }
                    };
                    page.Paragraphs.Add(pdfImage);

                    // Add descriptive text below the image
                    var description = new TextFragment("This QR code encodes the URL: https://example.com");
                    description.TextState.FontSize = 12;
                    description.Position = new Position(50, 500);
                    page.Paragraphs.Add(description);

                    // Save the PDF
                    pdfDoc.Save(outputPdfPath);
                }
            }
        }

        Console.WriteLine($"PDF report generated at: {outputPdfPath}");
    }
}