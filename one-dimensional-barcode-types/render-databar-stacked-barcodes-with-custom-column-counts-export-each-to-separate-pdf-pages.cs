// Title: Render DataBar Stacked Barcodes to PDF with Custom Column Counts
// Description: Demonstrates generating DataBar stacked barcodes with varying column counts and exporting each barcode to a separate page in a PDF document.
// Category-Description: Shows how to use Aspose.BarCode to create DataBar stacked symbology, customize its column count, and embed the generated images into an Aspose.Pdf document. This example belongs to the barcode generation and PDF export category, where developers commonly need to produce multiple barcodes and combine them into a single PDF for reporting or printing.
// Prompt: Render DataBar stacked barcodes with custom column counts, export each to separate PDF pages.
// Tags: databar, stacked, barcode generation, pdf export, aspose.barcode, aspose.pdf

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;
using Aspose.Pdf.Text;

/// <summary>
/// Generates DataBar stacked barcodes with custom column counts and saves them to a multi‑page PDF.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates barcode images, adds each to a separate PDF page, and saves the document.
    /// </summary>
    static void Main()
    {
        // Define the output PDF file name.
        string pdfPath = "DataBarStacked.pdf";

        // Column counts to apply to each generated DataBar stacked barcode.
        int[] columnCounts = { 2, 3, 4, 5 };
        // Limit the number of barcodes for evaluation mode (max 4).
        int maxCount = Math.Min(columnCounts.Length, 4);

        // Collect generated barcode images in memory streams.
        List<MemoryStream> barcodeStreams = new List<MemoryStream>();

        // Generate a barcode for each column count.
        for (int i = 0; i < maxCount; i++)
        {
            // Initialize a DataBar stacked barcode generator with a sample GTIN.
            using (var generator = new BarcodeGenerator(EncodeTypes.DatabarStacked, "(01)01234567890123"))
            {
                // Apply the custom column count.
                generator.Parameters.Barcode.DataBar.Columns = columnCounts[i];

                // Optional visual settings: black bars on white background.
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Save the barcode image to a memory stream in PNG format.
                var ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for later reading.
                barcodeStreams.Add(ms);
            }
        }

        // Create a new PDF document and add each barcode image to its own page.
        using (var pdfDoc = new Document())
        {
            foreach (var stream in barcodeStreams)
            {
                // Add a new page to the PDF.
                var page = pdfDoc.Pages.Add();

                // Configure the image to be placed on the page.
                var pdfImage = new Image
                {
                    ImageStream = stream,
                    FixWidth = 200,
                    FixHeight = 200,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new MarginInfo { Top = 20 }
                };

                // Add the image to the page's paragraph collection.
                page.Paragraphs.Add(pdfImage);
            }

            // Save the assembled PDF to disk.
            pdfDoc.Save(pdfPath);
        }

        // Release all memory streams used for barcode images.
        foreach (var ms in barcodeStreams)
        {
            ms.Dispose();
        }

        // Inform the user where the PDF was saved.
        Console.WriteLine($"PDF with DataBar stacked barcodes saved to: {Path.GetFullPath(pdfPath)}");
    }
}