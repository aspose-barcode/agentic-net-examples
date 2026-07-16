// Title: Generate QR Code barcodes and compile them into a PDF portfolio
// Description: This example creates up to four QR Code images, embeds each on a separate PDF page, and saves the collection as a PDF portfolio.
// Category-Description: Demonstrates Aspose.BarCode generation of QR Code symbology and Aspose.Pdf composition of a multi‑page PDF portfolio. It showcases the BarcodeGenerator class for QR encoding, setting error correction, and exporting to PNG, then uses Aspose.Pdf Document, Page, and Image classes to embed images. Ideal for developers needing to batch‑create barcodes and package them into a single PDF document for distribution or printing.
// Prompt: Generate QR Code barcode and create a PDF portfolio containing multiple barcode pages.
// Tags: qr code, barcode generation, pdf portfolio, aspose.barcode, aspose.pdf, image embedding

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Pdf;
using Aspose.Pdf.Text;

/// <summary>
/// Demonstrates generating QR Code barcodes and assembling them into a PDF portfolio.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates QR Code images, adds each to a PDF page, and saves the portfolio.
    /// </summary>
    static void Main()
    {
        // Define the output PDF file name
        string outputPdfPath = "QrBarcodesPortfolio.pdf";

        // List to hold in‑memory barcode images
        var barcodeStreams = new List<MemoryStream>();

        // Number of QR codes to generate
        int barcodeCount = 4;

        // Generate QR code images and store them in memory streams
        for (int i = 1; i <= barcodeCount; i++)
        {
            // Unique text for each QR code
            string qrText = $"Sample QR {i} - {Guid.NewGuid()}";

            // Initialize the QR code generator with the text
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
            {
                // Use the highest error correction level for robustness
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                // Adjust the size of each QR module (optional)
                generator.Parameters.Barcode.XDimension.Point = 2f;

                // Save the generated barcode to a memory stream in PNG format
                var ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for later reading
                barcodeStreams.Add(ms);
            }
        }

        // Create a new PDF document and add one page per barcode image
        using (var pdfDoc = new Document())
        {
            foreach (var stream in barcodeStreams)
            {
                // Add a fresh page to the PDF
                var page = pdfDoc.Pages.Add();

                // Create an Aspose.Pdf.Image object from the barcode stream
                var pdfImage = new Aspose.Pdf.Image
                {
                    ImageStream = stream,
                    // Define a fixed size for the QR code on the page
                    FixWidth = 200.0,
                    FixHeight = 200.0,
                    // Center the image horizontally and vertically
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                // Insert the image into the page's paragraph collection
                page.Paragraphs.Add(pdfImage);
            }

            // Save the assembled PDF portfolio to disk
            pdfDoc.Save(outputPdfPath);
        }

        // Release all memory streams holding barcode images
        foreach (var ms in barcodeStreams)
        {
            ms.Dispose();
        }

        // Inform the user that the PDF has been created
        Console.WriteLine($"PDF portfolio with {barcodeCount} QR code pages saved to '{outputPdfPath}'.");
    }
}