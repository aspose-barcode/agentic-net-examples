// Title: Generate QR Code and embed into PDF
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, saving it as a PNG image, and embedding the image into a PDF document using Aspose.Pdf.
// Category-Description: This example belongs to the Aspose.BarCode and Aspose.Pdf integration category, showcasing how to generate QR Code barcodes (BarcodeGenerator, EncodeTypes.QR) and combine them with PDF creation (Document, Image). Typical use cases include adding scannable QR codes to reports, invoices, or marketing materials. Developers often need to generate barcodes, customize appearance, and embed them directly into documents without intermediate file handling.
// Prompt: Generate QR Code barcode and document generation workflow in README with code snippets.
// Tags: qr code, barcode generation, pdf, aspose.barcode, aspose.pdf, image embedding

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;
using Aspose.Pdf.Text;

/// <summary>
/// Example program that creates a QR Code barcode, saves it as an image,
/// and embeds the image into a PDF document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR Code, writes it to a PNG file, and creates a PDF containing the QR Code.
    /// </summary>
    static void Main()
    {
        // Define output file paths
        string qrImagePath = "qr_code.png";
        string pdfPath = "qr_document.pdf";

        // Generate QR Code barcode
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set high error correction level for better readability
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Define barcode and background colors
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the barcode as a PNG image file
            generator.Save(qrImagePath, BarCodeImageFormat.Png);

            // Also keep the image in memory for embedding into the PDF
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position before reading

                // Create a new PDF document
                using (var pdfDoc = new Document())
                {
                    // Add a page to the PDF
                    var page = pdfDoc.Pages.Add();

                    // Add a title to the page
                    var title = new TextFragment("QR Code Document")
                    {
                        TextState = {
                            FontSize = 18,
                            FontStyle = FontStyles.Bold,
                            Font = FontRepository.FindFont("Helvetica")
                        }
                    };
                    title.Position = new Position(50, 750);
                    page.Paragraphs.Add(title);

                    // Embed the QR Code image into the PDF
                    var pdfImage = new Aspose.Pdf.Image
                    {
                        ImageStream = ms,
                        FixWidth = 150,
                        FixHeight = 150,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Margin = new MarginInfo { Top = 20 }
                    };
                    page.Paragraphs.Add(pdfImage);

                    // Save the PDF document to disk
                    pdfDoc.Save(pdfPath);
                }
            }
        }

        // Output the locations of the generated files
        Console.WriteLine($"QR code image saved to: {Path.GetFullPath(qrImagePath)}");
        Console.WriteLine($"PDF document saved to: {Path.GetFullPath(pdfPath)}");
    }
}