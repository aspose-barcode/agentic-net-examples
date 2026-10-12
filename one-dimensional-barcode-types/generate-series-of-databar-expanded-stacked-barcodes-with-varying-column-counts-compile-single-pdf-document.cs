// Title: Generate DataBar Expanded Stacked barcodes and combine into a PDF
// Description: Demonstrates creating multiple DataBar Expanded Stacked barcodes with different column counts and merging them into a single PDF document.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure DataBar symbology parameters, render barcodes as images, and embed them into an Aspose.Pdf document. Developers often need to produce batch barcode images and compile them into reports or printable PDFs; the key classes used are BarcodeGenerator, BarCodeImageFormat, and Aspose.Pdf.Document.
// Prompt: Generate series of DataBar Expanded Stacked barcodes with varying column counts, compile single PDF document.
// Tags: databar, expandedstacked, barcode, pdf, aspnet, aspose.barcode, aspose.pdf, image generation, batch processing

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Example program that creates DataBar Expanded Stacked barcodes with varying column counts
/// and combines them into a single PDF document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates barcode images, adds them to a PDF, and saves the result to a temporary folder.
    /// </summary>
    static void Main()
    {
        // Define output directory in the system temporary folder and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "DataBarExpandedStackedPdf");
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting PDF file
        string pdfPath = Path.Combine(outputDir, "DataBarExpandedStacked.pdf");

        // Column counts to be used for each barcode instance
        int[] columnCounts = new int[] { 4, 6, 8 };
        var streams = new List<MemoryStream>();

        // Generate a PNG image for each column count
        foreach (int cols in columnCounts)
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.DatabarExpandedStacked, "(01)12345678901231"))
            {
                // Set the number of columns for the DataBar barcode
                generator.Parameters.Barcode.DataBar.Columns = cols;

                // Save the barcode image to a memory stream
                var ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for later reading
                streams.Add(ms);
            }
        }

        // Create a new PDF document and add each barcode image as a separate page
        using (var pdfDoc = new Document())
        {
            foreach (var ms in streams)
            {
                var page = pdfDoc.Pages.Add();
                var img = new Aspose.Pdf.Image { ImageStream = ms };
                page.Paragraphs.Add(img);
            }

            // Save the compiled PDF to the specified path
            pdfDoc.Save(pdfPath);
        }

        // Dispose all memory streams to free resources
        foreach (var ms in streams)
        {
            ms.Dispose();
        }

        // Inform the user where the PDF was saved
        Console.WriteLine($"PDF saved to: {pdfPath}");
    }
}