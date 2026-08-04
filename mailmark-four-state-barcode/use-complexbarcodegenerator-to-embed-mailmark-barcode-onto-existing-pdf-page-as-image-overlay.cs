// Title: Overlay Mailmark barcode on a PDF page using ComplexBarcodeGenerator
// Description: Demonstrates how to generate a Mailmark barcode image with Aspose.BarCode and overlay it onto an existing PDF document as an image.
// Category-Description: This example belongs to the Aspose.BarCode PDF integration category, showcasing the use of ComplexBarcodeGenerator to create complex symbologies (Mailmark) and Aspose.Pdf to embed the generated image into a PDF. Developers often need to add tracking or postal barcodes to documents; the key API classes involved are ComplexBarcodeGenerator, MailmarkCodetext, BarCodeImageFormat, and Aspose.Pdf.Document/Image.
// Prompt: Use ComplexBarcodeGenerator to embed a Mailmark barcode onto an existing PDF page as an image overlay.
// Tags: mailmark, barcode, pdf, overlay, complexbarcodegenerator, aspose.barcode, aspose.pdf

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Generates a Mailmark barcode and overlays it onto a PDF page as an image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a Mailmark barcode, embeds it into a PDF, and saves the result.
    /// </summary>
    static void Main()
    {
        // Define file paths for the source and resulting PDF documents.
        string inputPdfPath = "input.pdf";
        string outputPdfPath = "output.pdf";

        // Ensure the input PDF exists; if not, create a simple one‑page PDF.
        if (!File.Exists(inputPdfPath))
        {
            using (var doc = new Document())
            {
                doc.Pages.Add();
                doc.Save(inputPdfPath);
            }
        }

        // Prepare Mailmark codetext with valid values.
        var mailmark = new MailmarkCodetext
        {
            Format = 4,                     // 4‑state Mailmark
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T " // trailing space is required
        };

        // Generate the Mailmark barcode image into a memory stream.
        using (var barcodeStream = new MemoryStream())
        {
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                // Optional: customize barcode and background colors.
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Save the barcode as PNG into the stream.
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
            }

            // Reset the stream position before reading it for PDF insertion.
            barcodeStream.Position = 0;

            // Load the existing PDF and overlay the barcode image.
            using (var pdfDoc = new Document(inputPdfPath))
            {
                // Use the first page of the PDF (adjust index as needed).
                var page = pdfDoc.Pages[1];

                // Create an Aspose.Pdf.Image element backed by the barcode stream.
                var pdfImage = new Aspose.Pdf.Image
                {
                    ImageStream = barcodeStream,
                    FixWidth = 200.0,   // Width in points.
                    FixHeight = 200.0,  // Height in points.
                    HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center,
                    VerticalAlignment = Aspose.Pdf.VerticalAlignment.Center,
                    Margin = new Aspose.Pdf.MarginInfo { Top = 10 }
                };

                // Add the image to the page's paragraph collection.
                page.Paragraphs.Add(pdfImage);

                // Save the modified PDF to the output path.
                pdfDoc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Barcode overlay completed. Output saved to '{outputPdfPath}'.");
    }
}