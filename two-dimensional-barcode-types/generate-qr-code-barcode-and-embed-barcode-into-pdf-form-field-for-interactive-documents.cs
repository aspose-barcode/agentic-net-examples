// Title: Generate QR Code and embed into PDF form button field
// Description: Demonstrates creating a QR Code barcode, converting it to PNG, and embedding the image into an interactive PDF button form field.
// Category-Description: This example belongs to the Aspose.BarCode and Aspose.Pdf integration category, showcasing how to generate barcodes (using BarcodeGenerator) and incorporate them into PDF documents (using Document, Form, and ButtonField). Typical use cases include adding scannable QR codes to interactive PDFs for marketing, authentication, or data capture. Developers often need to combine barcode generation with PDF form manipulation to create dynamic, user‑friendly documents.
// Prompt: Generate QR Code barcode and embed barcode into PDF form field for interactive documents.
// Tags: qr code, barcode generation, pdf, form field, embed image, aspose.barcode, aspose.pdf

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

/// <summary>
/// Example program that creates a QR Code barcode and embeds it into a PDF button form field.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR Code, adds it to a PDF button field, and saves the document.
    /// </summary>
    static void Main()
    {
        // Define the output PDF file path in the current working directory.
        string outputPdfPath = Path.Combine(Directory.GetCurrentDirectory(), "QrBarcodeForm.pdf");

        // ------------------------------------------------------------
        // Generate QR Code barcode and store it in a memory stream.
        // ------------------------------------------------------------
        MemoryStream barcodeStream = new MemoryStream();
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Configure barcode appearance.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the barcode image as PNG into the memory stream.
            generator.Save(barcodeStream, BarCodeImageFormat.Png);
        }

        // Reset the stream position so it can be read from the beginning.
        barcodeStream.Position = 0;

        // ------------------------------------------------------------
        // Create a new PDF document and embed the barcode image into a button field.
        // ------------------------------------------------------------
        using (var pdfDoc = new Document())
        {
            // Add a single page to the document.
            var page = pdfDoc.Pages.Add();

            // Define the rectangle that bounds the button field (left, bottom, right, top).
            var rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);
            var button = new ButtonField(page, rect);

            // Add the barcode image to the button field.
            using (var pdfImage = new Aspose.Pdf.Drawing.PdfImage(barcodeStream))
            {
                button.AddImage(pdfImage);
            }

            // Register the button field with the PDF form on page 1.
            pdfDoc.Form.Add(button, 1);

            // Save the resulting PDF to the specified path.
            pdfDoc.Save(outputPdfPath);
        }

        // Release the memory stream now that the PDF has been saved.
        barcodeStream.Dispose();

        // Inform the user where the PDF was saved.
        Console.WriteLine($"PDF with embedded QR Code saved to: {outputPdfPath}");
    }
}