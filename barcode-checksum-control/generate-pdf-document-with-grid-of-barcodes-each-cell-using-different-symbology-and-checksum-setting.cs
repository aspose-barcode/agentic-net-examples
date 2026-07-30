// Title: Generate PDF with Grid of Barcodes
// Description: Creates a PDF document containing a grid where each cell displays a barcode of a different symbology and checksum setting.
// Category-Description: This example demonstrates how to use Aspose.BarCode and Aspose.Pdf to generate a PDF document with multiple barcodes arranged in a grid. It covers creating BarcodeGenerator objects, configuring checksum options, rendering barcodes to images, and placing them onto PDF pages using Aspose.Pdf's image handling. Developers working with barcode generation for reports, invoices, or packaging can use these APIs to produce printable barcode layouts.
// Prompt: Generate a PDF document with a grid of barcodes, each cell using a different symbology and checksum setting.
// Tags: barcode, symbology, generation, pdf, grid, aspose.barcode, aspose.pdf

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Demonstrates generating a PDF document that contains a grid of barcodes,
/// each using a different symbology and checksum configuration.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Builds the barcode specifications,
    /// creates a PDF, renders each barcode into a cell, and saves the result.
    /// </summary>
    static void Main()
    {
        // Output PDF file name
        string pdfPath = "BarcodesGrid.pdf";

        // Define a list of barcode specifications:
        // each tuple contains the symbology type, the text to encode, and the checksum setting.
        var specs = new List<(BaseEncodeType Type, string Text, EnableChecksum Checksum)>
        {
            (EncodeTypes.Code128, "ABC123", EnableChecksum.Yes),               // Code128 (checksum required)
            (EncodeTypes.Code39FullASCII, "CODE39*", EnableChecksum.Yes),     // Code39 Full ASCII
            (EncodeTypes.EAN13, "1234567890128", EnableChecksum.Yes),        // EAN13 (valid checksum)
            (EncodeTypes.QR, "https://example.com", EnableChecksum.Yes),     // QR (checksum not applicable, set Yes)
            (EncodeTypes.DataMatrix, "DM123", EnableChecksum.Yes),           // DataMatrix
            (EncodeTypes.Pdf417, "PDF417", EnableChecksum.Yes),              // PDF417
            (EncodeTypes.Aztec, "AZTEC", EnableChecksum.Yes),                // Aztec
            (EncodeTypes.Codabar, "A123456A", EnableChecksum.No),            // Codabar (checksum not used)
            (EncodeTypes.ITF14, "12345678901231", EnableChecksum.Yes),       // ITF14
            (EncodeTypes.UPCA, "012345678905", EnableChecksum.Yes)          // UPCA
        };

        // Create a new PDF document and add a single page.
        var pdfDoc = new Document();
        var page = pdfDoc.Pages.Add();

        // Determine grid dimensions (2 columns) and calculate cell size.
        int cols = 2;
        int rows = (int)Math.Ceiling(specs.Count / (double)cols);
        double pageWidth = page.PageInfo.Width;
        double pageHeight = page.PageInfo.Height;
        double cellWidth = pageWidth / cols;
        double cellHeight = pageHeight / rows;

        // Loop through each barcode specification and place it in the appropriate cell.
        for (int i = 0; i < specs.Count; i++)
        {
            var spec = specs[i];
            int row = i / cols;
            int col = i % cols;

            // Initialize the barcode generator with the specified type and text.
            using (var generator = new BarcodeGenerator(spec.Type, spec.Text))
            {
                // Apply the checksum setting.
                generator.Parameters.Barcode.IsChecksumEnabled = spec.Checksum;

                // Optional: set foreground and background colors.
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Render the barcode to a PNG image stored in a memory stream.
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    ms.Position = 0;

                    // Compute the rectangle that represents the current cell on the PDF page.
                    double llx = col * cellWidth;                                 // lower-left X
                    double lly = pageHeight - (row + 1) * cellHeight;            // lower-left Y
                    double urx = (col + 1) * cellWidth;                          // upper-right X
                    double ury = pageHeight - row * cellHeight;                  // upper-right Y

                    // Add the barcode image to the PDF page within the calculated rectangle.
                    page.AddImage(
                        ms,
                        new Aspose.Pdf.Rectangle(llx, lly, urx, ury),
                        (int)cellWidth,
                        (int)cellHeight,
                        true);
                }
            }
        }

        // Save the populated PDF document to disk.
        pdfDoc.Save(pdfPath);
        Console.WriteLine($"PDF with barcode grid saved to: {Path.GetFullPath(pdfPath)}");
    }
}