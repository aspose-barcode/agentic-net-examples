// Title: Generate QR Code and embed into PDF using Aspose.BarCode and Aspose.Pdf
// Description: This example creates a QR Code barcode, saves it as a PNG image in memory, and embeds the image into a PDF document.
// Category-Description: Demonstrates Aspose.BarCode barcode generation (QR Code) and Aspose.Pdf document creation. Key API classes include BarcodeGenerator, EncodeTypes, QRErrorLevel, BarCodeImageFormat, Document, and Image. Typical use cases involve generating barcodes for marketing, authentication, or inventory, then incorporating them into printable PDFs. Developers often need to customize barcode parameters, render images in-memory, and place them precisely within PDF pages.
// Prompt: Generate QR Code barcode and document generation workflow in README with code snippets.
// Tags: qr code, barcode generation, pdf creation, aspose.barcode, aspose.pdf, image embedding, c#

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Pdf;

/// <summary>
/// Demonstrates generating a QR Code barcode and embedding it into a PDF document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates QR code, creates PDF, and saves it to a temporary folder.
    /// </summary>
    static void Main()
    {
        // Define a unique temporary folder for output files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Full path for the resulting PDF document
        string pdfPath = Path.Combine(tempFolder, "QrCodeDocument.pdf");

        // Text to encode in the QR Code
        string qrText = "https://example.com";

        // Initialize the barcode generator for QR Code
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
        {
            // Configure QR Code error correction level (high)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Optional: set the size of a single QR module (point units)
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Render the QR Code to a memory stream in PNG format
            using (var qrStream = new MemoryStream())
            {
                generator.Save(qrStream, BarCodeImageFormat.Png);
                qrStream.Position = 0; // Reset stream position for reading

                // Create a new PDF document and add a page
                using (var pdfDoc = new Document())
                {
                    var page = pdfDoc.Pages.Add();

                    // Create an image object that reads from the QR code stream
                    var pdfImage = new Aspose.Pdf.Image
                    {
                        ImageStream = qrStream,
                        // Set image dimensions (points)
                        FixWidth = 150.0,
                        FixHeight = 150.0,
                        // Center the image on the page
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        // Add a small top margin
                        Margin = new MarginInfo { Top = 20 }
                    };

                    // Add the image to the page's paragraph collection
                    page.Paragraphs.Add(pdfImage);

                    // Save the PDF document to the specified path
                    pdfDoc.Save(pdfPath);
                }

                // QR code stream is disposed here; PDF has already been saved
            }
        }

        // Inform the user where the PDF was generated
        Console.WriteLine("QR code PDF generated at:");
        Console.WriteLine(pdfPath);
    }
}