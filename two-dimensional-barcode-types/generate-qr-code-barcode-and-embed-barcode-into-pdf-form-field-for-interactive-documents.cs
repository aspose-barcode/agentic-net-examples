// Title: Generate QR Code and embed into PDF form field
// Description: Demonstrates creating a QR Code barcode, saving it to a memory stream, and embedding it as an interactive button field in a PDF document.
// Category-Description: This example belongs to the Aspose.BarCode for .NET PDF integration category. It shows how to use BarcodeGenerator (Aspose.BarCode.Generation) to create QR Code images and Aspose.Pdf (Document, ButtonField, PdfImage) to place the barcode into a PDF form field. Typical use cases include generating dynamic QR codes for invoices, tickets, or interactive PDFs where users can click the barcode. Developers often need to combine barcode generation with PDF form manipulation to produce self‑contained documents.
// Prompt: Generate QR Code barcode and embed barcode into PDF form field for interactive documents.
// Tags: qr code, barcode generation, pdf form, interactive pdf, aspose.barcode, aspose.pdf, image embedding

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

/// <summary>
/// Example program that creates a QR Code barcode and embeds it into a PDF button form field.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR Code, adds it to a PDF form button, and saves the document.
    /// </summary>
    static void Main()
    {
        // Define the output PDF file path
        string pdfPath = "QrBarcodeForm.pdf";

        // Create a memory stream to hold the generated QR Code image
        var barcodeStream = new MemoryStream();

        // Generate QR Code barcode and write it as PNG into the memory stream
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set high error correction level for better readability
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Define barcode and background colors
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the barcode image to the stream in PNG format
            generator.Save(barcodeStream, BarCodeImageFormat.Png);
        }

        // Reset the stream position to the beginning for subsequent reading
        barcodeStream.Position = 0;

        // Create a new PDF document and add a single page
        using (var pdfDoc = new Document())
        {
            var page = pdfDoc.Pages.Add();

            // Define the rectangle area for the button field (lower-left x, lower-left y, upper-right x, upper-right y)
            var rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Instantiate a button field on the page using the defined rectangle
            var button = new ButtonField(page, rect);

            // Add the QR Code image to the button field
            using (var pdfImage = new PdfImage(barcodeStream))
            {
                button.AddImage(pdfImage);
            }

            // Add the button field to the PDF form (page index is 1‑based)
            pdfDoc.Form.Add(button, 1);

            // Save the PDF document to the specified file path
            pdfDoc.Save(pdfPath);
        }

        // Release the memory stream resources after the PDF has been saved
        barcodeStream.Dispose();

        // Inform the user about the successful operation
        Console.WriteLine($"PDF with QR Code button field saved to: {pdfPath}");
    }
}