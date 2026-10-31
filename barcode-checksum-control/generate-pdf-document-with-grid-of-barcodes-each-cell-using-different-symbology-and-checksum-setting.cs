// Title: Generate PDF with barcode grid
// Description: Demonstrates creating a PDF containing a grid of barcodes, each using a different symbology and checksum setting.
// Category-Description: This example belongs to the Aspose.BarCode PDF generation category. It shows how to use BarcodeGenerator to create barcode images, configure checksum options, and embed them into an Aspose.Pdf Document. Typical use cases include batch printing of multiple barcode types, creating catalog sheets, or generating reports that require a variety of symbologies. Developers often need to combine barcode generation with PDF layout APIs to produce printable documents.
// Prompt: Generate a PDF document with a grid of barcodes, each cell using a different symbology and checksum setting.
// Tags: barcode symbology, pdf generation, checksum, aspose.barcode, aspose.pdf, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Demonstrates generating a PDF file that contains a grid of barcodes,
/// each using a distinct symbology and checksum configuration.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcode images, arranges them in a grid,
    /// embeds them into a PDF document, and saves the result to disk.
    /// </summary>
    static void Main()
    {
        // Define the output PDF file path in the current working directory.
        string outputPdf = Path.Combine(Directory.GetCurrentDirectory(), "BarcodesGrid.pdf");

        // List of barcode specifications (limited to 4 types in evaluation mode).
        var specs = new List<(BaseEncodeType Encode, string Text, EnableChecksum Checksum)>
        {
            (EncodeTypes.Code39FullASCII, "ABC-123", EnableChecksum.Yes),
            (EncodeTypes.Codabar, "A12345B", EnableChecksum.Yes),
            (EncodeTypes.Code11, "1234-5678", EnableChecksum.No),
            (EncodeTypes.Interleaved2of5, "1234567890", EnableChecksum.No)
        };

        // Generate barcode images and store them in memory streams.
        var barcodeStreams = new List<MemoryStream>();
        foreach (var spec in specs)
        {
            using (var generator = new BarcodeGenerator(spec.Encode, spec.Text))
            {
                // Apply the checksum setting for the current barcode.
                generator.Parameters.Barcode.IsChecksumEnabled = spec.Checksum;

                // Save the barcode as a PNG image into a memory stream.
                var ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for later reading.
                barcodeStreams.Add(ms);
            }
        }

        // Create a new PDF document and add a single page.
        using (var pdfDoc = new Document())
        {
            var page = pdfDoc.Pages.Add();

            // Determine grid dimensions based on the number of barcodes.
            int cols = 2;
            int rows = (int)Math.Ceiling((double)barcodeStreams.Count / cols);
            double pageWidth = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;
            double cellWidth = pageWidth / cols;
            double cellHeight = pageHeight / rows;

            // Place each barcode image into its corresponding cell.
            for (int i = 0; i < barcodeStreams.Count; i++)
            {
                int col = i % cols;          // Column index (0‑based).
                int row = i / cols;          // Row index (0‑based).

                // Calculate rectangle coordinates for the current cell.
                double llx = col * cellWidth;
                double lly = pageHeight - (row + 1) * cellHeight;
                double urx = llx + cellWidth;
                double ury = lly + cellHeight;

                var rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                // Add the barcode image to the PDF page within the calculated rectangle.
                page.AddImage(barcodeStreams[i], rect, (int)cellWidth, (int)cellHeight, true);
            }

            // Save the assembled PDF document to disk.
            pdfDoc.Save(outputPdf);
        }

        // Release all memory streams used for barcode images.
        foreach (var ms in barcodeStreams)
        {
            ms.Dispose();
        }

        Console.WriteLine($"PDF with barcodes saved to: {outputPdf}");
    }
}