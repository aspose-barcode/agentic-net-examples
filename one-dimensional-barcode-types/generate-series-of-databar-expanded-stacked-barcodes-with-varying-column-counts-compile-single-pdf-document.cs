// Title: Generate DataBar Expanded Stacked Barcodes and Combine into PDF
// Description: Creates DataBar Expanded Stacked barcodes with different column counts and compiles them into a single PDF file.
// Category-Description: This example demonstrates how to use Aspose.BarCode to generate DataBar Expanded Stacked symbology and Aspose.Pdf to embed the generated images into a PDF document. It covers barcode parameter configuration, image stream handling, and PDF page composition—common tasks for developers integrating barcode generation into reporting or document automation workflows.
// Prompt: Generate series of DataBar Expanded Stacked barcodes with varying column counts, compile single PDF document.
// Tags: databar, expandedstacked, barcode, pdf, aspose.barcode, aspose.pdf, generation, image, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Demonstrates generating multiple DataBar Expanded Stacked barcodes with varying column counts
/// and assembling them into a single PDF document using Aspose.BarCode and Aspose.Pdf.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcode images, adds them to a PDF, and saves the result.
    /// </summary>
    static void Main()
    {
        // Define the output PDF file name.
        string outputPdf = "DataBarExpandedStacked.pdf";

        // Column counts to be applied to each generated barcode.
        int[] columnCounts = new int[] { 4, 6, 8 };

        // List to hold in‑memory streams of generated barcode images.
        var barcodeStreams = new List<MemoryStream>();

        // Generate a barcode for each column count.
        foreach (int cols in columnCounts)
        {
            // Create a barcode generator for the DataBar Expanded Stacked symbology.
            using (var generator = new BarcodeGenerator(EncodeTypes.DatabarExpandedStacked, "(01)12345678901231"))
            {
                // Set the number of columns for the DataBar barcode.
                generator.Parameters.Barcode.DataBar.Columns = cols;

                // Save the barcode image to a memory stream in PNG format.
                var ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for later reading.

                // Store the stream for later PDF insertion.
                barcodeStreams.Add(ms);
            }
        }

        // Create a new PDF document.
        using (var pdfDoc = new Document())
        {
            // Add a page for each barcode image.
            foreach (var stream in barcodeStreams)
            {
                var page = pdfDoc.Pages.Add();

                // Create an image object linked to the barcode stream.
                var img = new Image
                {
                    ImageStream = stream,
                    FixWidth = 300,
                    FixHeight = 150,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                // Add the image to the page's paragraph collection.
                page.Paragraphs.Add(img);
            }

            // Save the assembled PDF to disk.
            pdfDoc.Save(outputPdf);
        }

        // Dispose all memory streams now that the PDF has been saved.
        foreach (var stream in barcodeStreams)
        {
            stream.Dispose();
        }

        // Inform the user where the PDF was saved.
        Console.WriteLine($"PDF saved to {Path.GetFullPath(outputPdf)}");
    }
}