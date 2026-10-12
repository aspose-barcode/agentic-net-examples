// Title: Embedding a Mailmark Barcode onto a PDF using ComplexBarcodeGenerator
// Description: Demonstrates how to generate a Mailmark 4‑state barcode with Aspose.BarCode and overlay it as an image on a PDF page using Aspose.Pdf.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator together with MailmarkCodetext to create a Mailmark barcode, and then uses Aspose.Pdf to place the generated PNG image onto a PDF document. Developers working with advanced barcode symbologies and needing to combine barcode graphics with PDF output will find this pattern useful for creating shipping labels, invoices, or any document that requires barcode overlays.
// Prompt: Use ComplexBarcodeGenerator to embed a Mailmark barcode onto an existing PDF page as an image overlay.
// Tags: mailmark, barcode, complexbarcode, pdf, image overlay, aspose.barcode, aspose.pdf, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Generates a Mailmark barcode and embeds it as an image overlay on a PDF page.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary folder, generates a Mailmark barcode,
    /// adds it to a new PDF document, and saves the result.
    /// </summary>
    static void Main()
    {
        // -----------------------------------------------------------------
        // Prepare output directory and target PDF file path
        // -----------------------------------------------------------------
        string outputDir = Path.Combine(Path.GetTempPath(), "MailmarkPdfDemo");
        Directory.CreateDirectory(outputDir);
        string pdfPath = Path.Combine(outputDir, "MailmarkOverlay.pdf");

        // -----------------------------------------------------------------
        // Define Mailmark 4‑state codetext (the data encoded in the barcode)
        // -----------------------------------------------------------------
        var mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // -----------------------------------------------------------------
        // Generate the barcode image into a memory stream using ComplexBarcodeGenerator
        // -----------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Adjust the X‑dimension (module size) of the barcode
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            using (var barcodeStream = new MemoryStream())
            {
                // Save the barcode as PNG into the stream
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0; // Reset stream position for reading

                // -----------------------------------------------------------------
                // Create a new PDF document and embed the barcode image
                // -----------------------------------------------------------------
                using (var pdfDoc = new Document())
                {
                    // Add a single page to the document
                    var page = pdfDoc.Pages.Add();

                    // Configure the image that will hold the barcode
                    var pdfImage = new Aspose.Pdf.Image
                    {
                        ImageStream = barcodeStream,
                        FixWidth = 200,
                        FixHeight = 200,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Margin = new MarginInfo { Top = 20 }
                    };

                    // Add the image to the page's paragraph collection
                    page.Paragraphs.Add(pdfImage);

                    // Save the PDF to the specified path
                    pdfDoc.Save(pdfPath);
                }
            }
        }

        // Inform the user where the PDF was saved
        Console.WriteLine($"PDF with Mailmark barcode saved to: {pdfPath}");
    }
}