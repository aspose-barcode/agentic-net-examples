// Title: Generate QR Code and embed into PDF form field
// Description: Demonstrates creating a QR Code image and placing it into a PDF button form field, producing an interactive document.
// Category-Description: This example belongs to the Aspose.BarCode PDF integration category, showing how to use BarcodeGenerator (Aspose.BarCode.Generation) to create a QR Code, and Aspose.Pdf (Aspose.Pdf.Document, Forms) to embed the barcode image into a PDF form field. Typical use cases include generating scannable QR codes for contracts, tickets, or marketing materials and embedding them directly into interactive PDF forms for seamless user experience.
// Prompt: Generate QR Code barcode and embed barcode into PDF form field for interactive documents.
// Tags: qr code, barcode generation, pdf form field, aspose.barcode, aspose.pdf, image embedding

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a QR Code and embeds it into a PDF button form field.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR Code image, adds it to a PDF button field, and saves the PDF.
    /// </summary>
    static void Main()
    {
        // Define output file names
        string barcodeFile = "qr.png";
        string pdfFile = "output.pdf";

        // Create a QR Code generator with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set barcode appearance
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Render the barcode to a memory stream in PNG format
            using (var barcodeStream = new MemoryStream())
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0; // Reset stream position for reading

                // Create a new PDF document and add a page
                var pdfDoc = new Aspose.Pdf.Document();
                var page = pdfDoc.Pages.Add();

                // Define the rectangle area for the button field
                var rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);
                var button = new Aspose.Pdf.Forms.ButtonField(page, rect);

                // Embed the barcode image into the button field
                using (var pdfImage = new Aspose.Pdf.Drawing.PdfImage(barcodeStream))
                {
                    button.AddImage(pdfImage);
                }

                // Add the button field to the PDF form and save the document
                pdfDoc.Form.Add(button, 1);
                pdfDoc.Save(pdfFile);
            }
        }

        Console.WriteLine("QR code generated and embedded into PDF successfully.");
    }
}