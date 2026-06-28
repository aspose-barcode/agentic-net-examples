using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Drawing;

/// <summary>
/// Generates a PDF report containing a QR code image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a QR code, embeds it in a PDF, and saves the file.
    /// </summary>
    static void Main()
    {
        // Define the output PDF file name.
        const string pdfPath = "Report.pdf";

        // Create a memory stream to hold the generated QR code image.
        using (MemoryStream barcodeStream = new MemoryStream())
        {
            // Generate a QR code for the specified URL.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
            {
                // Set the QR code error correction level to high.
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                // Save the QR code as a PNG image into the memory stream.
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning before reading.
            barcodeStream.Position = 0;

            // Create a new PDF document.
            using (Document pdfDoc = new Document())
            {
                // Add a new page to the PDF.
                Page page = pdfDoc.Pages.Add();

                // Create a text fragment to label the QR code.
                TextFragment text = new TextFragment("QR Code for Example")
                {
                    // Position the text near the top-left corner of the page.
                    Position = new Position(50, 750)
                };
                page.Paragraphs.Add(text);

                // Create an image object that uses the QR code stream.
                Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image
                {
                    ImageStream = barcodeStream,
                    // Set the displayed size of the QR code image.
                    FixWidth = 200,
                    FixHeight = 200
                };
                page.Paragraphs.Add(pdfImage);

                // Save the PDF document to the specified file path.
                pdfDoc.Save(pdfPath);
            }
        }

        // Output the full path of the generated PDF file.
        Console.WriteLine($"PDF report generated: {Path.GetFullPath(pdfPath)}");
    }
}