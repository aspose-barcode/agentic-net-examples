// Title: Generate QR Code and embed into PDF report
// Description: This example creates a QR Code barcode, saves it as an image, and embeds it into a PDF document with a title and descriptive text.
// Category-Description: Demonstrates Aspose.BarCode and Aspose.Pdf integration for generating QR Code barcodes and inserting them into PDF reports. It showcases the BarcodeGenerator class for QR encoding, setting error correction, and customizing colors, as well as the Aspose.Pdf Document, Page, TextFragment, and Image classes for PDF creation. Ideal for developers building automated document generation with embedded barcodes.
// Prompt: Generate QR Code barcode and embed it into a PDF report alongside descriptive text.
// Tags: qr code, barcode generation, pdf creation, aspose.barcode, aspose.pdf, image embedding

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;
using Aspose.Pdf.Text;

/// <summary>
/// Demonstrates generating a QR Code barcode and embedding it into a PDF report.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR Code, creates a PDF, and saves the result.
    /// </summary>
    static void Main()
    {
        // Define the output PDF file path
        string pdfPath = "BarcodeReport.pdf";

        // Initialize QR code generator with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Configure QR error correction level to high (Level H)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Set barcode foreground and background colors
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the generated barcode image to a memory stream in PNG format
            using (var barcodeStream = new MemoryStream())
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0; // Reset stream position for reading

                // Create a new PDF document
                using (var pdfDoc = new Document())
                {
                    // Add a new page to the PDF
                    var page = pdfDoc.Pages.Add();

                    // Add a title text fragment to the page
                    var title = new TextFragment("QR Code Barcode Report")
                    {
                        Position = new Position(50, 750),
                        TextState = { FontSize = 14 }
                    };
                    page.Paragraphs.Add(title);

                    // Embed the barcode image onto the page
                    var pdfImage = new Aspose.Pdf.Image
                    {
                        ImageStream = barcodeStream,
                        FixWidth = 200,
                        FixHeight = 200,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Margin = new MarginInfo { Top = 20 }
                    };
                    page.Paragraphs.Add(pdfImage);

                    // Save the PDF document to the specified file path
                    pdfDoc.Save(pdfPath);
                }

                // Dispose the barcode stream after PDF is saved (handled by using)
            }
        }

        // Output the full path of the generated PDF report
        Console.WriteLine($"PDF report generated: {Path.GetFullPath(pdfPath)}");
    }
}