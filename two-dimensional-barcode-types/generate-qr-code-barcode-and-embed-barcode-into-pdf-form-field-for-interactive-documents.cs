// Title: Generate QR Code and embed into PDF form button
// Description: Demonstrates creating a QR Code barcode image and placing it into a PDF button form field, producing an interactive PDF document.
// Category-Description: This example belongs to the Aspose.BarCode and Aspose.Pdf integration category, showing how to generate barcodes (using BarcodeGenerator, EncodeTypes) and embed them into PDF forms (using Document, ButtonField, PdfImage). Typical use cases include adding scannable QR codes to interactive PDFs for marketing, tickets, or data capture. Developers often need to combine barcode generation with PDF form manipulation to create dynamic, user‑friendly documents.
// Prompt: Generate QR Code barcode and embed barcode into PDF form field for interactive documents.
// Tags: qr code, barcode generation, pdf form, aspose.barcode, aspose.pdf, interactive document

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
    /// Entry point. Generates a QR Code, adds it to a PDF button field, and saves the document.
    /// </summary>
    static void Main()
    {
        // Generate QR code image into a memory stream
        using (var barcodeStream = new MemoryStream())
        {
            // Configure and create the QR Code barcode
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                generator.CodeText = "https://example.com";

                // Set high error correction level for better readability
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                // Define barcode and background colors
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Save the barcode as a PNG image into the memory stream
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
            }

            // Reset stream position so it can be read from the beginning
            barcodeStream.Position = 0;

            // Create a new PDF document and embed the barcode into a button form field
            using (var pdfDoc = new Document())
            {
                // Add a page to the PDF
                var page = pdfDoc.Pages.Add();

                // Define the rectangle area for the button field (lower-left X, lower-left Y, upper-right X, upper-right Y)
                var rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

                // Create a button field on the page using the defined rectangle
                var button = new ButtonField(page, rect);

                // Add the barcode image to the button field
                using (var pdfImage = new PdfImage(barcodeStream))
                {
                    button.AddImage(pdfImage);
                }

                // Register the button field with the PDF form (page index is 1‑based)
                pdfDoc.Form.Add(button, 1);

                // Save the resulting PDF to disk
                string outputPath = "QrBarcodeForm.pdf";
                pdfDoc.Save(outputPath);
                Console.WriteLine($"PDF saved to {outputPath}");
            }
        }
    }
}