using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Demonstrates generation of a Mailmark barcode and embedding it into a PDF using Aspose libraries.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Mailmark barcode, inserts it into a PDF, and saves the file.
    /// </summary>
    static void Main()
    {
        // Prepare Mailmark codetext with required fields
        var mailmark = new MailmarkCodetext
        {
            Format = 4,               // 4‑state format
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Generate barcode image into a memory stream
        using (var barcodeStream = new MemoryStream())
        {
            // Create a barcode generator for the Mailmark codetext
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                // Save the generated barcode as PNG into the memory stream
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
            }

            // Reset stream position before reading the image data
            barcodeStream.Position = 0;

            // Create a new PDF document and add a page
            var pdfDoc = new Document();
            var page = pdfDoc.Pages.Add();

            // Create an Aspose.Pdf.Image object from the barcode stream
            var pdfImage = new Aspose.Pdf.Image
            {
                ImageStream = barcodeStream,
                // Set desired size (points). Adjust as needed.
                FixWidth = 200.0,
                FixHeight = 100.0
            };

            // Add the image to the page's paragraph collection
            page.Paragraphs.Add(pdfImage);

            // Define output PDF file name
            const string pdfPath = "MailmarkBarcode.pdf";

            // Save the PDF document to disk
            pdfDoc.Save(pdfPath);

            // Output the full path of the saved PDF
            Console.WriteLine($"PDF with Mailmark barcode saved to: {Path.GetFullPath(pdfPath)}");
        }
    }
}