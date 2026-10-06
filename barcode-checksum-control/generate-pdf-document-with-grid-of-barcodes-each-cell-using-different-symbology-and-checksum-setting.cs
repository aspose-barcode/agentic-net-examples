// Title: Generate PDF with a grid of different barcodes
// Description: Creates a PDF document containing a 2x2 grid where each cell displays a barcode of a distinct symbology and checksum configuration.
// Category-Description: This example belongs to the Aspose.BarCode generation category, demonstrating how to create barcode images and embed them into PDF files using Aspose.Pdf. It showcases the use of BarcodeGenerator, setting barcode parameters such as symbology and checksum, and placing the generated images onto a PDF page. Developers often need to generate multiple barcodes and combine them into documents for reporting, labeling, or batch printing.
// Prompt: Generate a PDF document with a grid of barcodes, each cell using a different symbology and checksum setting.
// Tags: barcode symbology, generation, pdf, aspose.barcode, aspose.pdf

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Demonstrates how to generate several barcodes with different symbologies,
/// place them into a 2x2 grid, and save the result as a PDF document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcode images, arranges them in a grid,
    /// and writes the final PDF to a temporary location.
    /// </summary>
    static void Main()
    {
        // Define barcode specifications (maximum of 4 for evaluation mode)
        var specs = new List<(BaseEncodeType type, string text, EnableChecksum? checksum)>
        {
            (EncodeTypes.Code39FullASCII, "ABC-123", EnableChecksum.Yes), // optional checksum enabled
            (EncodeTypes.Code128, "1234567890", null),                  // obligatory checksum
            (EncodeTypes.QR, "https://example.com", null),             // QR code
            (EncodeTypes.DataMatrix, "DataMatrixTest", null)           // DataMatrix
        };

        // Generate barcode images and store them in memory streams
        var barcodeStreams = new List<MemoryStream>();
        foreach (var spec in specs)
        {
            using (var generator = new BarcodeGenerator(spec.type, spec.text))
            {
                // Apply checksum setting if provided
                if (spec.checksum.HasValue)
                {
                    generator.Parameters.Barcode.IsChecksumEnabled = spec.checksum.Value;
                }

                // Set simple foreground and background colors
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Save the barcode as PNG into a memory stream
                var ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;
                barcodeStreams.Add(ms);
            }
        }

        // Create a new PDF document and add a page for the barcode grid
        using (var pdfDoc = new Document())
        {
            var page = pdfDoc.Pages.Add();

            // Determine page dimensions and calculate cell size for a 2x2 grid
            double pageWidth = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;
            int cols = 2;
            int rows = 2;
            double cellWidth = pageWidth / cols;
            double cellHeight = pageHeight / rows;

            // Place each barcode image into its corresponding cell
            int index = 0;
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (index >= barcodeStreams.Count)
                        break;

                    var stream = barcodeStreams[index];
                    double llx = c * cellWidth;
                    double lly = pageHeight - (r + 1) * cellHeight;
                    double urx = llx + cellWidth;
                    double ury = lly + cellHeight;
                    var rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                    // Add the barcode image to the PDF page within the calculated rectangle
                    page.AddImage(stream, rect, (int)cellWidth, (int)cellHeight, true);
                    index++;
                }
            }

            // Save the PDF to a temporary file and report the location
            string outputPath = Path.Combine(Path.GetTempPath(), "BarcodesGrid.pdf");
            pdfDoc.Save(outputPath);
            Console.WriteLine($"PDF saved to: {outputPath}");
        }

        // Clean up memory streams after the PDF has been saved
        foreach (var ms in barcodeStreams)
        {
            ms.Dispose();
        }
    }
}