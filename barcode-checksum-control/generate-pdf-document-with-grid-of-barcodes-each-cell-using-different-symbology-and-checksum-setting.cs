// Title: PDF Barcode Grid Example
// Description: Generates a PDF containing a 2x2 grid where each cell displays a barcode of a different symbology and checksum configuration.
// Prompt: Generate a PDF document with a grid of barcodes, each cell using a different symbology and checksum setting.
// Tags: barcode, symbology, pdf, aspose.barcode, aspose.pdf, grid, checksum

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using Aspose.Pdf;

/// <summary>
/// Demonstrates how to create a PDF document that contains a grid of barcodes,
/// each using a distinct symbology and checksum setting.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the PDF and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Output PDF file name
        string pdfPath = "BarcodesGrid.pdf";

        // Define a small set of barcode specifications (max 4 for evaluation mode)
        var specs = new List<(BaseEncodeType Type, string CodeText, EnableChecksum Checksum, string Caption)>
        {
            // Code128 - checksum always required
            (EncodeTypes.Code128, "ABC123", EnableChecksum.Yes, "Code128"),
            // Code39FullASCII - checksum optional, enable it
            (EncodeTypes.Code39FullASCII, "CODE39", EnableChecksum.Yes, "Code39FullASCII"),
            // Codabar - checksum not applicable, set to No
            (EncodeTypes.Codabar, "A123B", EnableChecksum.No, "Codabar"),
            // DataMatrix - 2D barcode, checksum not applicable
            (EncodeTypes.DataMatrix, "DMATRIX", EnableChecksum.Default, "DataMatrix")
        };

        // Create a new PDF document
        using (var pdfDoc = new Document())
        {
            // Add a single page to the document
            var page = pdfDoc.Pages.Add();

            // Determine grid layout (2 columns x 2 rows)
            int cols = 2;
            int rows = 2;
            double pageWidth = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;
            double cellWidth = pageWidth / cols;
            double cellHeight = pageHeight / rows;

            // Iterate over each barcode specification and place it in the grid
            for (int i = 0; i < specs.Count; i++)
            {
                var spec = specs[i];

                // Create a barcode generator for the current specification
                using (var generator = new BarcodeGenerator(spec.Type, spec.CodeText))
                {
                    // Apply checksum setting where applicable
                    generator.Parameters.Barcode.IsChecksumEnabled = spec.Checksum;

                    // For Code128, show checksum in the human‑readable text
                    if (spec.Type == EncodeTypes.Code128)
                    {
                        generator.Parameters.Barcode.ChecksumAlwaysShow = true;
                    }

                    // Render the barcode to a memory stream as PNG
                    using (var ms = new MemoryStream())
                    {
                        generator.Save(ms, BarCodeImageFormat.Png);
                        ms.Position = 0;

                        // Calculate the rectangle for the current cell
                        int col = i % cols;
                        int row = i / cols;
                        double llx = col * cellWidth;
                        double lly = pageHeight - ((row + 1) * cellHeight);
                        double urx = llx + cellWidth;
                        double ury = lly + cellHeight;

                        // Add the barcode image to the PDF page within the calculated rectangle
                        page.AddImage(
                            ms,
                            new Aspose.Pdf.Rectangle(llx, lly, urx, ury),
                            (int)cellWidth,
                            (int)cellHeight,
                            true);
                    }
                }
            }

            // Save the generated PDF to disk
            pdfDoc.Save(pdfPath);
        }

        // Inform the user where the PDF was saved
        Console.WriteLine($"PDF with barcode grid saved to: {Path.GetFullPath(pdfPath)}");
    }
}