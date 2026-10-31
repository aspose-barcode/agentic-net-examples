// Title: Render DataBar Stacked Barcodes with Variable Column Counts to PDF
// Description: Demonstrates how to generate DataBar Expanded Stacked barcodes with different column counts and embed each barcode image on separate pages of a PDF document.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator with EncodeTypes.DatabarExpandedStacked, configuring XDimension and column count, and exporting images via Aspose.Pdf. Typical use cases include creating multi-page PDF catalogs of product barcodes where each page shows a distinct barcode variant. Developers often need to adjust DataBar column settings and combine barcode images into PDF reports.
// Prompt: Render DataBar stacked barcodes with custom column counts, export each to separate PDF pages.
// Tags: databar, stacked, barcode, pdf, aspose.barcode, aspose.pdf, image generation, column count

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates DataBar Expanded Stacked barcodes with varying column counts and saves them as separate pages in a PDF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates barcode images, adds them to a PDF document, and saves the result.
    /// </summary>
    static void Main()
    {
        // Define the output PDF file path in the temporary directory.
        string outputPdf = Path.Combine(Path.GetTempPath(), "DataBarStacked.pdf");

        // Column counts to generate for the stacked DataBar barcodes.
        int[] columnCounts = new int[] { 4, 6, 8 };

        // List to hold the generated barcode image streams.
        var streams = new List<MemoryStream>();

        // Generate a barcode image for each column count.
        foreach (int cols in columnCounts)
        {
            // Create a barcode generator for the DataBar Expanded Stacked symbology.
            using (var generator = new BarcodeGenerator(EncodeTypes.DatabarExpandedStacked, "(01)12345678901231"))
            {
                // Set the X-dimension (module width) in pixels.
                generator.Parameters.Barcode.XDimension.Pixels = 2f;

                // Apply the custom column count for the DataBar.
                generator.Parameters.Barcode.DataBar.Columns = cols;

                // Save the barcode as a PNG image into a memory stream.
                var ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);

                // Reset the stream position to the beginning for later reading.
                ms.Position = 0;

                // Store the stream for later PDF insertion.
                streams.Add(ms);
            }
        }

        // Create a new PDF document.
        using (var pdfDoc = new Document())
        {
            // Add each barcode image to a separate PDF page.
            foreach (var ms in streams)
            {
                // Add a new page to the PDF.
                var page = pdfDoc.Pages.Add();

                // Create an Aspose.Pdf.Image object linked to the barcode stream.
                var pdfImage = new Aspose.Pdf.Image
                {
                    ImageStream = ms,
                    FixWidth = 200.0,
                    FixHeight = 200.0,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new MarginInfo { Top = 20 }
                };

                // Insert the image into the page's paragraph collection.
                page.Paragraphs.Add(pdfImage);
            }

            // Save the assembled PDF to the specified file.
            pdfDoc.Save(outputPdf);
        }

        // Dispose all memory streams to release resources.
        foreach (var ms in streams)
        {
            ms.Dispose();
        }

        // Inform the user where the PDF was saved.
        Console.WriteLine($"PDF saved to: {outputPdf}");
    }
}