using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating multiple barcodes, storing them in memory,
/// and embedding them into a PDF document using Aspose libraries.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates barcodes, creates a PDF, and saves the result.
    /// </summary>
    static void Main()
    {
        // Define barcode specifications: symbology, text, and checksum setting.
        var barcodes = new List<(BaseEncodeType type, string text, EnableChecksum checksum)>
        {
            (EncodeTypes.Code128, "ABC123456", EnableChecksum.Yes),               // Code128 requires checksum
            (EncodeTypes.Code39FullASCII, "CODE39*TEST", EnableChecksum.No),    // Code39 optional checksum
            (EncodeTypes.EAN13, "5901234123457", EnableChecksum.Yes),          // EAN13 with checksum
            (EncodeTypes.Pdf417, "PDF417 Sample Text", EnableChecksum.Yes)    // PDF417 (checksum ignored)
        };

        // Collection to hold generated barcode images as memory streams.
        var barcodeImages = new List<MemoryStream>();

        // Generate each barcode and store its PNG image in a memory stream.
        foreach (var (type, text, checksum) in barcodes)
        {
            using (var generator = new BarcodeGenerator(type, text))
            {
                // Apply the specified checksum setting.
                generator.Parameters.Barcode.IsChecksumEnabled = checksum;

                // Save the barcode image to a memory stream in PNG format.
                var ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for subsequent reading.
                barcodeImages.Add(ms);
            }
        }

        // Create a new PDF document and add a single page.
        var pdfDoc = new Document();
        var page = pdfDoc.Pages.Add();

        // Embed each barcode image into the PDF page.
        // (Maximum of 4 images per evaluation limits.)
        foreach (var imgStream in barcodeImages)
        {
            var pdfImage = new Aspose.Pdf.Image
            {
                ImageStream = imgStream,
                FixWidth = 200.0,
                FixHeight = 100.0
            };
            page.Paragraphs.Add(pdfImage);
        }

        // Save the assembled PDF to disk.
        const string outputPdf = "BarcodesGrid.pdf";
        pdfDoc.Save(outputPdf);
        Console.WriteLine($"PDF with barcodes saved to: {outputPdf}");
    }
}