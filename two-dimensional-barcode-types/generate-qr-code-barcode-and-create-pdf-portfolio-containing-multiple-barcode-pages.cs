// Title: Generate QR Code Images and Assemble PDF Portfolio
// Description: Demonstrates generating QR Code barcodes with Aspose.BarCode, embedding them into separate pages of a PDF document using Aspose.Pdf, and saving the resulting PDF portfolio.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and Aspose.Pdf document creation category. It showcases the use of BarcodeGenerator (for QR code creation) and Document/Image classes (for PDF assembly). Typical scenarios include producing printable barcode sheets, batch‑processing barcodes into PDFs, and creating multi‑page barcode portfolios for distribution. Developers often need to generate barcode images in memory and combine them into a single PDF without intermediate files.
// Prompt: Generate QR Code barcode and create a PDF portfolio containing multiple barcode pages.
// Tags: qr code, barcode generation, pdf creation, aspose.barcode, aspose.pdf, image, portfolio

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;

/// <summary>
/// Example program that creates QR Code barcodes and compiles them into a multi‑page PDF portfolio.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates QR Code images, embeds each on a separate PDF page, and saves the document.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare a temporary output directory for the generated PDF.
        // ------------------------------------------------------------
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodePdfDemo");
        Directory.CreateDirectory(outputDir);

        // ------------------------------------------------------------
        // Define QR code texts (limited to 4 pages for evaluation mode).
        // ------------------------------------------------------------
        string[] qrTexts = new[] { "Hello", "Aspose", "QR Code", "Sample 4" };
        int pageCount = Math.Min(qrTexts.Length, 4);

        // ------------------------------------------------------------
        // Generate QR code images and store them in memory streams.
        // ------------------------------------------------------------
        var imageStreams = new List<MemoryStream>();
        for (int i = 0; i < pageCount; i++)
        {
            string text = qrTexts[i];
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, text))
            {
                // Configure barcode appearance.
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                // Save the barcode as PNG into a memory stream.
                var ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for later reading.
                imageStreams.Add(ms);
            }
        }

        // ------------------------------------------------------------
        // Create a PDF document and embed each barcode image on its own page.
        // ------------------------------------------------------------
        string pdfPath = Path.Combine(outputDir, "Barcodes.pdf");
        using (var pdfDoc = new Document())
        {
            foreach (var stream in imageStreams)
            {
                // Add a new page to the PDF.
                var page = pdfDoc.Pages.Add();

                // Create an image object linked to the barcode stream.
                var pdfImage = new Aspose.Pdf.Image
                {
                    ImageStream = stream,
                    FixWidth = 200,
                    FixHeight = 200,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new MarginInfo { Top = 20 }
                };

                // Add the image to the page's paragraph collection.
                page.Paragraphs.Add(pdfImage);
            }

            // Save the assembled PDF to disk.
            pdfDoc.Save(pdfPath);
        }

        // ------------------------------------------------------------
        // Release all memory streams now that the PDF has been saved.
        // ------------------------------------------------------------
        foreach (var stream in imageStreams)
        {
            stream.Dispose();
        }

        Console.WriteLine($"PDF portfolio created at: {pdfPath}");
    }
}