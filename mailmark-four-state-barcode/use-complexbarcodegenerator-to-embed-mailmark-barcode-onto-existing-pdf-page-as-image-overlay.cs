// Title: Embed Mailmark barcode onto a PDF using ComplexBarcodeGenerator
// Description: This example generates a Mailmark 4‑State barcode and overlays it as a PNG image on the first page of an existing PDF document.
// Category-Description: Demonstrates Aspose.BarCode complex barcode generation and Aspose.Pdf image overlay techniques. It showcases the use of ComplexBarcodeGenerator, MailmarkCodetext, and BarCodeImageFormat to create a barcode, then uses Aspose.Pdf Document and Image classes to place the barcode onto a PDF page. Ideal for developers needing to add tracking or postal barcodes to PDFs in automated workflows.
// Prompt: Use ComplexBarcodeGenerator to embed a Mailmark barcode onto an existing PDF page as an image overlay.
// Tags: mailmark, barcode, complexbarcode, pdf, overlay, image, aspose.barcode, aspose.pdf

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;
using Aspose.Pdf.Text;

/// <summary>
/// Demonstrates embedding a Mailmark barcode onto a PDF using Aspose.BarCode and Aspose.Pdf.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Mailmark barcode, creates a sample PDF if needed, and overlays the barcode image onto the PDF.
    /// </summary>
    static void Main()
    {
        // Define output directory and file paths
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);
        string pdfPath = Path.Combine(outputDir, "sample.pdf");
        string resultPdfPath = Path.Combine(outputDir, "sample_with_mailmark.pdf");

        // Create a sample PDF if it does not already exist
        if (!File.Exists(pdfPath))
        {
            using (var doc = new Document())
            {
                var page = doc.Pages.Add();
                var text = new TextFragment("Sample PDF for Mailmark barcode overlay");
                page.Paragraphs.Add(text);
                doc.Save(pdfPath);
            }
        }

        // Generate Mailmark 4‑State barcode into a memory stream
        using (var barcodeStream = new MemoryStream())
        {
            var mailmark = new MailmarkCodetext
            {
                Format = 4,
                VersionID = 1,
                Class = "0",
                SupplychainID = 384224,
                ItemID = 16563762,
                DestinationPostCodePlusDPS = "EF61AH8T "
            };

            // Use ComplexBarcodeGenerator to render the barcode as PNG
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
            }

            // Reset stream position for reading
            barcodeStream.Position = 0;

            // Open the existing PDF and overlay the barcode image
            using (var pdfDoc = new Document(pdfPath))
            {
                var page = pdfDoc.Pages[1];
                var pdfImage = new Aspose.Pdf.Image
                {
                    ImageStream = barcodeStream,
                    FixWidth = 150.0,
                    FixHeight = 150.0,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new MarginInfo { Top = 20 }
                };
                page.Paragraphs.Add(pdfImage);
                pdfDoc.Save(resultPdfPath);
            }
        }

        Console.WriteLine($"Barcode‑embedded PDF saved to: {resultPdfPath}");
    }
}