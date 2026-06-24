using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;
using Aspose.Pdf.Text;

/// <summary>
/// Demonstrates creating a Mailmark barcode and embedding it into a PDF document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define input and output PDF file paths.
        string inputPdfPath = "input.pdf";
        string outputPdfPath = "output.pdf";

        // If the input PDF does not exist, create a blank PDF with a single page.
        if (!File.Exists(inputPdfPath))
        {
            using (var doc = new Document())
            {
                doc.Pages.Add();               // Add an empty page.
                doc.Save(inputPdfPath);        // Save the new PDF to the specified path.
            }
        }

        // Configure the Mailmark barcode data.
        var mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Generate the barcode image and embed it into the PDF.
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        using (var barcodeStream = new MemoryStream())
        {
            // Save the generated barcode as a PNG into the memory stream.
            generator.Save(barcodeStream, BarCodeImageFormat.Png);
            barcodeStream.Position = 0; // Reset stream position for reading.

            // Open the existing PDF document.
            using (var pdfDocument = new Document(inputPdfPath))
            {
                // Access the first page of the PDF.
                var page = pdfDocument.Pages[1];

                // Create an image object from the barcode stream.
                var pdfImage = new Aspose.Pdf.Image
                {
                    ImageStream = barcodeStream,
                    FixWidth = 200.0,   // Set desired width.
                    FixHeight = 100.0,  // Set desired height.
                    Margin = new MarginInfo { Top = 0.0, Left = 0.0 } // No margin.
                };

                // Add the barcode image to the page's paragraphs collection.
                page.Paragraphs.Add(pdfImage);

                // Save the modified PDF to the output path.
                pdfDocument.Save(outputPdfPath);
            }
        }

        // Inform the user that the process completed successfully.
        Console.WriteLine($"Mailmark barcode has been embedded and saved to '{outputPdfPath}'.");
    }
}