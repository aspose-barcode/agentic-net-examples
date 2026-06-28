using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR code, verifying it, and embedding it into a PDF using Aspose libraries.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code image, reads it back for verification, and creates a PDF containing the QR code.
    /// </summary>
    static void Main()
    {
        // QR code content to encode
        string qrContent = "https://example.com";

        // File paths for the generated QR image and the resulting PDF
        string qrImagePath = "qr.png";
        string pdfPath = "qr_document.pdf";

        // -------------------------------------------------
        // 1. Generate QR code image and save as PNG file
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrContent))
        {
            // Use the highest error correction level to improve readability
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the generated QR code directly to a PNG file
            generator.Save(qrImagePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"QR code image saved to: {qrImagePath}");

        // -------------------------------------------------
        // 2. Read the generated QR code to verify it works
        // -------------------------------------------------
        using (var reader = new BarCodeReader(qrImagePath, DecodeType.QR))
        {
            // Iterate through all detected barcodes (should be only one)
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected QR code text: {result.CodeText}");
            }
        }

        // -------------------------------------------------
        // 3. Create a PDF document and embed the QR code image
        // -------------------------------------------------
        // Open the PNG image file as a stream; keep it open until the PDF is saved
        using (var imageStream = new FileStream(qrImagePath, FileMode.Open, FileAccess.Read))
        {
            // Aspose.Pdf.Document does not implement IDisposable, so we instantiate it without using
            var pdfDocument = new Document();

            // Add a new page to the PDF
            var page = pdfDocument.Pages.Add();

            // Create an image object for the PDF and assign the image stream
            var pdfImage = new Aspose.Pdf.Image
            {
                ImageStream = imageStream,
                // Set the displayed size of the image in points (1 point = 1/72 inch)
                FixWidth = 200.0,
                FixHeight = 200.0
            };

            // Insert the image into the page's paragraph collection
            page.Paragraphs.Add(pdfImage);

            // Save the PDF document to the specified path
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"PDF document with embedded QR code saved to: {pdfPath}");
    }
}