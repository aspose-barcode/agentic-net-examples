// Title: Generate QR Code Barcodes and Assemble Them into a PDF Portfolio
// Description: This example creates four QR Code images, embeds each on a separate PDF page, and saves the collection as a PDF portfolio.
// Category-Description: Demonstrates how to use Aspose.BarCode to generate QR Code barcodes and Aspose.Pdf to compose a multi‑page PDF document. Typical scenarios include batch barcode creation for reports, catalogs, or document bundles where each barcode appears on its own page. Developers working with barcode generation and PDF composition frequently need to combine these APIs to produce printable or distributable documents.
// Prompt: Generate QR Code barcode and create a PDF portfolio containing multiple barcode pages.
// Tags: qr code, barcode generation, pdf, portfolio, aspose.barcode, aspose.pdf, c#

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Demonstrates QR Code generation and PDF portfolio creation using Aspose.BarCode and Aspose.Pdf.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates QR Code images, adds them to a PDF document, and saves the result.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory for the generated files.
        string outputDir = Path.Combine(Path.GetTempPath(), "QrPortfolioDemo");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Generate QR Code images and keep their streams open for later use.
        List<MemoryStream> barcodeStreams = new List<MemoryStream>();
        for (int i = 1; i <= 4; i++)
        {
            var ms = new MemoryStream();
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, $"Sample QR {i}"))
            {
                // Set error correction level to Medium (Level M).
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
                // Save the barcode as a PNG image into the memory stream.
                generator.Save(ms, BarCodeImageFormat.Png);
            }
            ms.Position = 0; // Reset stream position before reading.
            barcodeStreams.Add(ms);
        }

        // Create a new PDF document and add each QR Code image as a separate page.
        string pdfPath = Path.Combine(outputDir, "QrPortfolio.pdf");
        using (var pdfDoc = new Document())
        {
            foreach (var stream in barcodeStreams)
            {
                // Add a new page to the PDF.
                var page = pdfDoc.Pages.Add();

                // Configure the image to be placed on the page.
                var pdfImage = new Aspose.Pdf.Image
                {
                    ImageStream = stream,
                    FixWidth = 200,
                    FixHeight = 200,
                    HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center,
                    VerticalAlignment = Aspose.Pdf.VerticalAlignment.Center,
                    Margin = new MarginInfo { Top = 20 }
                };

                // Add the image to the page's paragraph collection.
                page.Paragraphs.Add(pdfImage);
            }

            // Save the assembled PDF portfolio to disk.
            pdfDoc.Save(pdfPath);
        }

        // Dispose all memory streams now that the PDF has been saved.
        foreach (var stream in barcodeStreams)
        {
            stream.Dispose();
        }

        Console.WriteLine($"PDF portfolio created at: {pdfPath}");
    }
}