// Title: Embed Mailmark Barcode into PDF using ComplexBarcodeGenerator
// Description: Demonstrates generating a Mailmark 4‑State barcode with Aspose.BarCode and overlaying it onto an existing PDF page as an image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation and Aspose.Pdf manipulation category. It shows how to use ComplexBarcodeGenerator with MailmarkCodetext to create a barcode image, then embed that image into a PDF using Aspose.Pdf's Image class. Developers working with postal services, logistics, or document automation often need to generate Mailmark barcodes and place them on existing documents for tracking and compliance.
// Prompt: Use ComplexBarcodeGenerator to embed a Mailmark barcode onto an existing PDF page as an image overlay.
// Tags: mailmark, barcode, complexbarcode, pdf, image overlay, aspose.barcode, aspose.pdf

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;
using Aspose.Pdf.Text;

/// <summary>
/// Example program that creates a Mailmark barcode and embeds it into a PDF.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Mailmark 4‑State barcode, saves it to a memory stream, and overlays it onto the first page of an existing PDF.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Define input and output PDF file paths
        // --------------------------------------------------------------------
        string inputPdfPath = Path.Combine(Directory.GetCurrentDirectory(), "input.pdf");
        string outputPdfPath = Path.Combine(Directory.GetCurrentDirectory(), "output.pdf");

        // --------------------------------------------------------------------
        // Ensure the input PDF exists; create a simple placeholder if it does not
        // --------------------------------------------------------------------
        if (!File.Exists(inputPdfPath))
        {
            using (var doc = new Document())
            {
                doc.Pages.Add(); // Add a single blank page
                doc.Save(inputPdfPath);
            }
        }

        // --------------------------------------------------------------------
        // Configure Mailmark 4‑State codetext parameters
        // --------------------------------------------------------------------
        var mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // --------------------------------------------------------------------
        // Generate the barcode image and store it in a memory stream
        // --------------------------------------------------------------------
        var barcodeStream = new MemoryStream();
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Set image resolution (pixel size of a single module)
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Save(barcodeStream, BarCodeImageFormat.Png);
        }
        barcodeStream.Position = 0; // Reset stream position for reading

        // --------------------------------------------------------------------
        // Load the existing PDF and add the barcode image to the first page
        // --------------------------------------------------------------------
        using (var pdfDoc = new Document(inputPdfPath))
        {
            var page = pdfDoc.Pages[1]; // Pages collection is 1‑based

            var pdfImage = new Aspose.Pdf.Image
            {
                ImageStream = barcodeStream,
                FixWidth = 150.0,
                FixHeight = 150.0,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new MarginInfo { Top = 20 }
            };

            // Add the image as a paragraph element to the page
            page.Paragraphs.Add(pdfImage);
            pdfDoc.Save(outputPdfPath);
        }

        // --------------------------------------------------------------------
        // Release resources
        // --------------------------------------------------------------------
        barcodeStream.Dispose();

        Console.WriteLine($"Barcode embedded and PDF saved to: {outputPdfPath}");
    }
}