// Title: Generate PDF with a grid of barcodes (different symbologies & checksum settings)
// Description: This example creates a PDF file containing a 2x2 grid, each cell showing a barcode generated with a distinct symbology and checksum configuration.
// Category-Description: Demonstrates Aspose.BarCode generation combined with Aspose.Pdf to embed barcode images into a PDF document. It covers using BarcodeGenerator, setting checksum options, customizing colors, and positioning images on a PDF page—common tasks for developers automating document creation, labeling, or reporting workflows.
// Prompt: Generate a PDF document with a grid of barcodes, each cell using a different symbology and checksum setting.
// Tags: barcode, symbology, checksum, pdf, aspose.barcode, aspose.pdf, grid, image generation

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Demonstrates creating a PDF with a grid of barcodes using various symbologies and checksum settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the PDF and saves it to disk.
    /// </summary>
    static void Main()
    {
        // Output PDF file name
        string outputPdf = "BarcodesGrid.pdf";

        // Define barcode specifications (symbology and checksum setting) for each cell
        var barcodeSpecs = new List<(BaseEncodeType Symbology, EnableChecksum Checksum)>
        {
            (EncodeTypes.Code128, EnableChecksum.Yes),               // Code128 (checksum always enabled)
            (EncodeTypes.Code39FullASCII, EnableChecksum.No),        // Code39 with checksum disabled
            (EncodeTypes.Codabar, EnableChecksum.Yes),              // Codabar with checksum enabled
            (EncodeTypes.ITF14, EnableChecksum.No)                  // ITF14 with checksum disabled (if supported)
        };

        // Create a new PDF document and add a single page
        var pdfDoc = new Document();
        var page = pdfDoc.Pages.Add();

        // Determine grid layout (2 rows x 2 columns) and cell dimensions
        int rows = 2;
        int cols = 2;
        double pageWidth = page.PageInfo.Width;
        double pageHeight = page.PageInfo.Height;
        double cellWidth = pageWidth / cols;
        double cellHeight = pageHeight / rows;

        // Keep memory streams alive until after the PDF is saved
        var streams = new List<MemoryStream>();

        // Iterate over each barcode specification and place it in the appropriate grid cell
        for (int i = 0; i < barcodeSpecs.Count; i++)
        {
            int row = i / cols; // Current row index
            int col = i % cols; // Current column index

            var spec = barcodeSpecs[i];
            string codeText = $"Sample{i + 1}";

            // Create a barcode generator for the current specification
            using (var generator = new BarcodeGenerator(spec.Symbology, codeText))
            {
                // Apply checksum setting (some symbologies may ignore this)
                try
                {
                    generator.Parameters.Barcode.IsChecksumEnabled = spec.Checksum;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Warning: Unable to set checksum for {spec.Symbology.TypeName}: {ex.Message}");
                }

                // Set barcode foreground and background colors
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Render the barcode to a memory stream as PNG
                var ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;
                streams.Add(ms);

                // Calculate the rectangle that represents the current cell (lower‑left origin)
                double llx = col * cellWidth;
                double lly = pageHeight - (row + 1) * cellHeight;
                double urx = llx + cellWidth;
                double ury = lly + cellHeight;
                var rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                // Add the barcode image to the PDF page within the calculated rectangle
                page.AddImage(ms, rect, (int)cellWidth, (int)cellHeight, true);
            }
        }

        // Save the populated PDF document to disk
        pdfDoc.Save(outputPdf);
        Console.WriteLine($"PDF saved to {Path.GetFullPath(outputPdf)}");

        // Dispose all memory streams
        foreach (var ms in streams)
        {
            ms.Dispose();
        }
    }
}