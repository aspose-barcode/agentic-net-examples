// Title: Generate QR Code and embed into PDF report
// Description: Demonstrates creating a QR Code barcode, converting it to an image, and inserting it into a PDF document with descriptive text.
// Category-Description: This example belongs to the Aspose.BarCode and Aspose.Pdf integration category. It shows how to use BarcodeGenerator (Aspose.BarCode.Generation) to produce a QR Code, customize its appearance, and then embed the resulting image into a PDF using Aspose.Pdf classes. Typical scenarios include adding scannable barcodes to reports, invoices, or marketing materials where developers need to combine barcode generation with document creation.
// Prompt: Generate QR Code barcode and embed it into a PDF report alongside descriptive text.
// Tags: qr code, barcode generation, pdf creation, aspose.barcode, aspose.pdf, image embedding, report generation

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Pdf;
using Aspose.Pdf.Text;

/// <summary>
/// Provides an example that generates a QR Code barcode and embeds it into a PDF report with descriptive text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a PDF containing a QR Code and a description.
    /// </summary>
    static void Main()
    {
        // Define the output PDF file name
        string pdfPath = "QRCodeReport.pdf";

        // Generate the PDF with the QR Code and description
        GeneratePdfWithQr("https://example.com", "QR Code Report", pdfPath);

        // Inform the user where the PDF was saved
        Console.WriteLine($"PDF report generated: {Path.GetFullPath(pdfPath)}");
    }

    /// <summary>
    /// Generates a PDF document that contains a QR Code image and a descriptive text fragment.
    /// </summary>
    /// <param name="qrText">The text to encode in the QR Code.</param>
    /// <param name="description">The descriptive text to place above the QR Code.</param>
    /// <param name="outputPdfPath">The file path where the PDF will be saved.</param>
    static void GeneratePdfWithQr(string qrText, string description, string outputPdfPath)
    {
        // Use a memory stream to hold the generated barcode image
        using (var barcodeStream = new MemoryStream())
        {
            // ---------- Generate QR Code ----------
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                // Set the data to encode
                generator.CodeText = qrText;

                // Configure barcode colors
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Set a high error correction level for better resilience
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                // Save the QR Code image to the memory stream in PNG format
                generator.Save(barcodeStream, BarCodeImageFormat.Png);

                // Reset the stream position so it can be read from the beginning
                barcodeStream.Position = 0;
            }

            // ---------- Create PDF and embed content ----------
            using (var pdfDoc = new Document())
            {
                // Add a new page to the PDF
                var page = pdfDoc.Pages.Add();

                // Add descriptive text at the top of the page
                var textFragment = new TextFragment(description)
                {
                    TextState = { FontSize = 14 }
                };
                textFragment.Position = new Position(50, 750);
                page.Paragraphs.Add(textFragment);

                // Create an image object from the barcode stream
                var pdfImage = new Aspose.Pdf.Image
                {
                    ImageStream = barcodeStream,
                    FixWidth = 150,
                    FixHeight = 150,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new MarginInfo { Top = 20 }
                };

                // Add the QR Code image below the descriptive text
                page.Paragraphs.Add(pdfImage);

                // Save the assembled PDF document to the specified path
                pdfDoc.Save(outputPdfPath);
            }
        }
    }
}