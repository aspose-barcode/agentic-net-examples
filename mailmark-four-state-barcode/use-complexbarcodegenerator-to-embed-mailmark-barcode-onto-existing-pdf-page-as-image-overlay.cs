// Title: Embed Mailmark Barcode onto PDF using ComplexBarcodeGenerator
// Description: Demonstrates generating a Mailmark barcode with Aspose.BarCode and overlaying it onto an existing PDF page as an image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation and PDF manipulation category. It showcases the use of ComplexBarcodeGenerator, MailmarkCodetext, and Aspose.Pdf Document classes to create a Mailmark barcode image and embed it into a PDF. Developers working with postal barcodes, document automation, or custom PDF overlays commonly need such patterns to integrate barcode data into existing documents.
// Prompt: Use ComplexBarcodeGenerator to embed a Mailmark barcode onto an existing PDF page as an image overlay.
// Tags: mailmark, barcode, complexbarcode, pdf, overlay, image, generation, aspose.barcode, aspose.pdf

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Example program that generates a Mailmark barcode and embeds it as an image overlay
/// onto the first page of an existing PDF document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Paths for input and output PDF files
        string inputPdfPath = "input.pdf";
        string outputPdfPath = "output.pdf";

        // Verify that the input PDF exists before proceeding
        if (!File.Exists(inputPdfPath))
        {
            Console.WriteLine($"Input PDF file not found: {inputPdfPath}");
            return;
        }

        // Prepare Mailmark codetext with required values
        var mailmark = new MailmarkCodetext
        {
            Format = 4,                     // 4‑state Mailmark format
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T " // trailing space is mandatory
        };

        // Generate the Mailmark barcode image into a memory stream
        using (var barcodeStream = new MemoryStream())
        {
            // Create a ComplexBarcodeGenerator for the Mailmark codetext
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                // Save the barcode as PNG; BarCodeImageFormat is from Aspose.BarCode.Generation
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
            }

            // Reset stream position before using it in PDF
            barcodeStream.Position = 0;

            // Load the existing PDF document
            using (var pdfDocument = new Document(inputPdfPath))
            {
                // Get the first page (pages are 1‑based)
                var page = pdfDocument.Pages[1];

                // Define the rectangle where the barcode will be placed (coordinates in points)
                var rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700); // llx, lly, urx, ury

                // Add the barcode image as an overlay on the page
                page.AddImage(barcodeStream, rect);

                // Save the modified PDF to the output path
                pdfDocument.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Mailmark barcode embedded successfully into '{outputPdfPath}'.");
    }
}