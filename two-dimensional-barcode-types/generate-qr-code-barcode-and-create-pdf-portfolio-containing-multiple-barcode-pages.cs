using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating QR code images with Aspose.BarCode,
/// storing them in memory, and assembling them into a PDF portfolio
/// using Aspose.Pdf.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates up to four QR code images, adds each to a separate PDF page,
    /// and saves the resulting PDF to a temporary directory.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare output directory and PDF file path
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodes");
        Directory.CreateDirectory(outputDir);
        string pdfPath = Path.Combine(outputDir, "QrBarcodesPortfolio.pdf");

        // --------------------------------------------------------------------
        // Generate QR code images and keep them in memory streams
        // --------------------------------------------------------------------
        List<MemoryStream> barcodeStreams = new List<MemoryStream>();

        // Evaluation version of Aspose.BarCode allows up to 4 barcodes
        for (int i = 1; i <= 4; i++)
        {
            string codeText = $"Sample QR {i}";
            var stream = new MemoryStream();

            // Create a QR code generator for the current text
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                // Use the highest error correction level (Level H)
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                // Set foreground (barcode) and background colors
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Save the generated QR code as PNG directly into the memory stream
                generator.Save(stream, BarCodeImageFormat.Png);
            }

            // Reset stream position so it can be read later
            stream.Position = 0;
            barcodeStreams.Add(stream);
        }

        // --------------------------------------------------------------------
        // Create a PDF document and add each QR code image as a separate page
        // --------------------------------------------------------------------
        using (var pdfDoc = new Document())
        {
            foreach (var imgStream in barcodeStreams)
            {
                // Add a new page to the PDF
                var page = pdfDoc.Pages.Add();

                // Create an Aspose.Pdf.Image object that reads from the memory stream
                var pdfImage = new Aspose.Pdf.Image
                {
                    ImageStream = imgStream,
                    // Define a fixed size for the QR code image
                    FixWidth = 200.0,
                    FixHeight = 200.0,
                    // Center the image horizontally and vertically on the page
                    HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center,
                    VerticalAlignment = Aspose.Pdf.VerticalAlignment.Center
                };

                // Add the image to the page's paragraph collection
                page.Paragraphs.Add(pdfImage);
            }

            // Save the assembled PDF portfolio to the specified path
            pdfDoc.Save(pdfPath);
        }

        // --------------------------------------------------------------------
        // Clean up: dispose all memory streams now that the PDF is saved
        // --------------------------------------------------------------------
        foreach (var s in barcodeStreams)
        {
            s.Dispose();
        }

        // Inform the user where the PDF was created
        Console.WriteLine($"QR code PDF portfolio created at: {pdfPath}");
    }
}