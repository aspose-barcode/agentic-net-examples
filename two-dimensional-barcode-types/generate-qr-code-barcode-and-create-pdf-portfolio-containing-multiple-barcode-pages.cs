// Title: Generate QR Code PDF Portfolio
// Description: Creates QR code images for a set of URLs, embeds each on a separate PDF page, and saves the result as a PDF portfolio.
// Category-Description: This example demonstrates how to use Aspose.BarCode to generate QR Code barcodes and Aspose.Pdf to compose a multi‑page PDF document. It covers barcode generation, image handling via memory streams, and PDF page creation—common tasks for developers building printable or shareable barcode documents.
// Prompt: Generate QR Code barcode and create a PDF portfolio containing multiple barcode pages.
// Tags: qr code, barcode generation, pdf, portfolio, aspose.barcode, aspose.pdf, image, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Text;

/// <summary>
/// Demonstrates generating QR Code barcodes and assembling them into a PDF portfolio.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates QR codes, adds them to a PDF, and saves the file.
    /// </summary>
    static void Main()
    {
        // Define QR code texts (each will become a separate PDF page)
        List<string> qrTexts = new List<string>
        {
            "https://example.com/page1",
            "https://example.com/page2",
            "https://example.com/page3"
        };

        // Create a new PDF document
        using (var pdfDoc = new Document())
        {
            // Store barcode image streams for later disposal
            List<MemoryStream> barcodeStreams = new List<MemoryStream>();

            // Generate a QR code image for each text and add it to the PDF
            foreach (string text in qrTexts)
            {
                // Generate QR code into a memory stream
                var barcodeStream = new MemoryStream();
                using (var generator = new BarcodeGenerator(EncodeTypes.QR))
                {
                    generator.CodeText = text;
                    // Use high error correction level for robustness
                    generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
                    // Set module size (optional)
                    generator.Parameters.Barcode.XDimension.Point = 2f;
                    generator.Save(barcodeStream, BarCodeImageFormat.Png);
                }

                // Reset stream position before reading
                barcodeStream.Position = 0;
                barcodeStreams.Add(barcodeStream);

                // Add a new page to the PDF and place the barcode image
                var page = pdfDoc.Pages.Add();
                var pdfImage = new Aspose.Pdf.Image
                {
                    ImageStream = barcodeStream,
                    FixWidth = 200,
                    FixHeight = 200,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new MarginInfo { Top = 20 }
                };
                page.Paragraphs.Add(pdfImage);
            }

            // Save the assembled PDF portfolio to a temporary location
            string outputPath = Path.Combine(Path.GetTempPath(), "QrCodePortfolio.pdf");
            pdfDoc.Save(outputPath);
            Console.WriteLine($"PDF portfolio created at: {outputPath}");

            // Clean up all memory streams used for barcode images
            foreach (var ms in barcodeStreams)
            {
                ms.Dispose();
            }
        }
    }
}