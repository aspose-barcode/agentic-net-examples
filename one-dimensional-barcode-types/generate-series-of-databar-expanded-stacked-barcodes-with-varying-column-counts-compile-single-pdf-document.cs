// Title: Generate DataBar Expanded Stacked barcodes and compile into a PDF
// Description: Demonstrates creating DataBar Expanded Stacked barcodes with different column counts and combining them into a single PDF document.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure DataBar symbology parameters, render barcodes to image streams, and embed them into an Aspose.Pdf document. Developers working with product identification, GS1 DataBar, or multi‑column stacked barcodes can use these APIs to produce printable PDFs for inventory, labeling, or reporting scenarios.
// Prompt: Generate series of DataBar Expanded Stacked barcodes with varying column counts, compile single PDF document.
// Tags: databar, expandedstacked, barcode, pdf, aspnet, aspose.barcode, aspose.pdf, image, generation

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Example program that generates a set of DataBar Expanded Stacked barcodes with varying column counts
/// and assembles them into a single PDF document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Prepare a collection to hold the generated barcode image streams.
        List<MemoryStream> barcodeStreams = new List<MemoryStream>();

        // Generate DataBar Expanded Stacked barcodes for column counts 1 through 4.
        for (int columns = 1; columns <= 4; columns++)
        {
            // Initialize a barcode generator for the DatabarExpandedStacked symbology.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DatabarExpandedStacked, "(01)12345678901231"))
            {
                // Configure visual appearance: black bars on a white background.
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Set the specific number of columns for this barcode instance.
                generator.Parameters.Barcode.DataBar.Columns = columns;

                // Render the barcode to a memory stream in PNG format.
                MemoryStream ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for subsequent reading.
                barcodeStreams.Add(ms);
            }
        }

        // Create a new PDF document to hold the barcode images.
        Document pdfDoc = new Document();

        // Add a separate page for each barcode image (up to four pages).
        for (int i = 0; i < barcodeStreams.Count; i++)
        {
            Page page = pdfDoc.Pages.Add();

            // Determine the full page dimensions.
            double pageWidth = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, pageWidth, pageHeight);

            // Insert the barcode image onto the page.
            // Width and height are set to 300x150 pixels; adjust as needed.
            page.AddImage(barcodeStreams[i], rect, 300, 150, true);
        }

        // Save the assembled PDF to disk.
        string outputPdfPath = "DataBarExpandedStacked.pdf";
        pdfDoc.Save(outputPdfPath);

        // Clean up all memory streams to release resources.
        foreach (var ms in barcodeStreams)
        {
            ms.Dispose();
        }

        Console.WriteLine("PDF generated: " + Path.GetFullPath(outputPdfPath));
    }
}