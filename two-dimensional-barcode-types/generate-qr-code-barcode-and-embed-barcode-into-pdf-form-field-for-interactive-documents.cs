// Title: Generate QR Code and embed into PDF form button field
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, converting it to an image, and placing it into a button form field of a PDF using Aspose.Pdf for interactive documents.
// Category-Description: This example belongs to the Aspose.BarCode and Aspose.Pdf integration category, showcasing how to generate barcodes (specifically QR codes) and embed them into PDF form fields. Key API classes include BarcodeGenerator, Document, ButtonField, and PdfImage. Typical use cases involve creating interactive PDFs where users can click on barcode images to trigger actions or simply view embedded barcodes. Developers often need to combine barcode generation with PDF form manipulation to produce dynamic, data‑rich documents.
// Prompt: Generate QR Code barcode and embed barcode into PDF form field for interactive documents.
// Tags: qr code, barcode generation, pdf, form field, aspose.barcode, aspose.pdf, image embedding

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
        // Define the output PDF path in the temporary folder
        string pdfPath = Path.Combine(Path.GetTempPath(), "QrInPdf.pdf");

        // Generate QR Code barcode into a memory stream
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Configure barcode appearance
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Set high error correction level for the QR Code (optional)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Stream to hold the generated barcode image
            using (var barcodeStream = new MemoryStream())
            {
                // Save the barcode as a PNG image into the stream
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0; // Reset stream position for reading

                // Create a new PDF document
                using (var pdfDoc = new Document())
                {
                    // Add a single page to the document
                    var page = pdfDoc.Pages.Add();

                    // Define the rectangle area for the button field (lower-left X/Y, upper-right X/Y)
                    var rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

                    // Create a button form field on the page using the defined rectangle
                    var button = new ButtonField(page, rect);

                    // Embed the barcode image into the button field
                    using (var pdfImage = new Aspose.Pdf.Drawing.PdfImage(barcodeStream))
                    {
                        button.AddImage(pdfImage);
                    }

                    // Add the button field to the PDF form (page index 1)
                    pdfDoc.Form.Add(button, 1);

                    // Save the PDF document to the specified path
                    pdfDoc.Save(pdfPath);
                }
            }
        }

        // Inform the user where the PDF was saved
        Console.WriteLine($"PDF with embedded QR Code saved to: {pdfPath}");
    }
}