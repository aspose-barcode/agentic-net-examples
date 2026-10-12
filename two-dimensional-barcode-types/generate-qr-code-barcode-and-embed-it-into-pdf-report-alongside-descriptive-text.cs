// Title: Generate QR Code and embed into PDF report
// Description: Demonstrates creating a QR Code barcode, converting it to an image, and inserting it into a PDF document with title and description text.
// Category-Description: This example belongs to the Aspose.BarCode and Aspose.Pdf integration category, showcasing how to generate barcodes (QR, DataMatrix, etc.) using the BarcodeGenerator class and embed them as images in PDF files via the Document class. Typical use cases include adding scannable codes to invoices, tickets, or reports where developers need to combine barcode creation with PDF document generation.
// Prompt: Generate QR Code barcode and embed it into a PDF report alongside descriptive text.
// Tags: qr code, barcode generation, pdf creation, aspose.barcode, aspose.pdf, image embedding, report generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;
using Aspose.Pdf.Text;

/// <summary>
/// Example program that creates a QR Code barcode and embeds it into a PDF report with accompanying text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR Code, adds it to a PDF document, and saves the result.
    /// </summary>
    static void Main()
    {
        // Define the output PDF file path.
        string outputPdf = Path.Combine(Directory.GetCurrentDirectory(), "QrReport.pdf");

        // Text to encode in the QR Code.
        string qrText = "https://example.com";

        // Generate the QR Code image and store it in a memory stream.
        MemoryStream barcodeStream = new MemoryStream();
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
        {
            // Set high error correction level for better readability.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
            // Define barcode and background colors.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;
            // Set the size of each QR module (pixel dimension).
            generator.Parameters.Barcode.XDimension.Pixels = 8f;
            // Save the generated barcode as a PNG image into the stream.
            generator.Save(barcodeStream, BarCodeImageFormat.Png);
        }
        // Reset stream position for reading.
        barcodeStream.Position = 0;

        // Create a new PDF document and embed the barcode image with descriptive text.
        using (var pdfDoc = new Document())
        {
            // Add a new page to the PDF.
            var page = pdfDoc.Pages.Add();

            // Create an image object from the barcode stream.
            var pdfImage = new Aspose.Pdf.Image
            {
                ImageStream = barcodeStream,
                FixWidth = 200.0,
                FixHeight = 200.0,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            // Add the image to the page.
            page.Paragraphs.Add(pdfImage);

            // Add a title text fragment.
            var title = new TextFragment("QR Code Report")
            {
                Position = new Position(50, 750),
                TextState = { FontSize = 20 }
            };
            page.Paragraphs.Add(title);

            // Add a description text fragment.
            var description = new TextFragment("This QR code links to example.com")
            {
                Position = new Position(50, 500),
                TextState = { FontSize = 12 }
            };
            page.Paragraphs.Add(description);

            // Save the PDF document to the specified file.
            pdfDoc.Save(outputPdf);
        }

        // Release the memory stream resources.
        barcodeStream.Dispose();

        // Inform the user where the PDF was saved.
        Console.WriteLine($"PDF report generated at: {outputPdf}");
    }
}