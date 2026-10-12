// Title: Generate QR Code Barcodes and Combine into a PDF Portfolio
// Description: This example creates QR Code barcodes from a set of strings, saves each as a PNG in memory, and assembles them into a multi‑page PDF portfolio.
// Category-Description: Demonstrates Aspose.BarCode barcode generation (BarcodeGenerator, EncodeTypes.QR, QRErrorLevel) together with Aspose.Pdf document creation (Document, Image, Paragraphs). Typical for scenarios where multiple barcodes need to be packaged into a single PDF for distribution, printing, or archival. Developers working with barcode imaging and PDF composition frequently use these APIs to automate report generation or batch processing.
// Prompt: Generate QR Code barcode and create a PDF portfolio containing multiple barcode pages.
// Tags: qr code, barcode generation, pdf, portfolio, aspose.barcode, aspose.pdf, image, memorystream

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;
using Aspose.Pdf.Text;

/// <summary>
/// Demonstrates how to generate QR Code barcodes and embed them into a PDF portfolio.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates QR Code images, adds each to a separate PDF page, and saves the resulting document.
    /// </summary>
    static void Main()
    {
        // Define sample QR code texts (limited to 3 items for quick evaluation)
        string[] qrTexts = new[] { "Hello World", "Aspose.BarCode", "QR Code Sample" };
        int maxItems = Math.Min(qrTexts.Length, 3);

        // Collect generated barcode images in memory streams
        var barcodeStreams = new List<MemoryStream>();

        // Generate a QR Code for each text entry
        for (int i = 0; i < maxItems; i++)
        {
            string text = qrTexts[i];
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, text))
            {
                // Use the highest error correction level for robustness
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                // Save the barcode as a PNG image into a memory stream
                var ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for later reading
                barcodeStreams.Add(ms);
            }
        }

        // Determine output PDF file path
        string outputPdfPath = Path.Combine(Directory.GetCurrentDirectory(), "QrBarcodesPortfolio.pdf");

        // Create a PDF document and add a page for each barcode image
        using (var pdfDoc = new Document())
        {
            foreach (var stream in barcodeStreams)
            {
                // Add a new page to the PDF
                var page = pdfDoc.Pages.Add();

                // Create an image object linked to the barcode stream
                var pdfImage = new Aspose.Pdf.Image
                {
                    ImageStream = stream,
                    FixWidth = 200,
                    FixHeight = 200,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new MarginInfo { Top = 20 }
                };

                // Insert the image into the page's paragraph collection
                page.Paragraphs.Add(pdfImage);
            }

            // Save the assembled PDF portfolio to disk
            pdfDoc.Save(outputPdfPath);
        }

        // Release all memory streams now that the PDF has been saved
        foreach (var ms in barcodeStreams)
        {
            ms.Dispose();
        }

        Console.WriteLine($"PDF portfolio created at: {outputPdfPath}");
    }
}