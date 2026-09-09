// Title: Render DataBar Expanded Stacked Barcodes with Custom Column Counts to PDF
// Description: Demonstrates generating DataBar Expanded Stacked barcodes with varying column counts, saving each as a PNG image, and placing them on separate pages of a PDF document.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to configure barcode parameters (such as column count) and combine the output with Aspose.Pdf to create multi‑page PDF reports. Developers working with product labeling, inventory systems, or retail applications often need to produce stacked DataBar barcodes and embed them in documents for printing or archival.
// Prompt: Render DataBar stacked barcodes with custom column counts, export each to separate PDF pages.
// Tags: databar, stacked, barcode, generation, pdf, aspose.barcode, aspose.pdf, image, columns

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;
using Aspose.Pdf.Text;

/// <summary>
/// Generates DataBar Expanded Stacked barcodes with different column counts,
/// embeds each barcode image on a separate PDF page, and saves the PDF to a temporary folder.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates barcode images, builds a PDF document, and writes the result to disk.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare output directory and PDF file path
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Path.GetTempPath(), "DataBarStackedPdf_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        string pdfPath = Path.Combine(outputDir, "DataBarStackedBarcodes.pdf");

        // --------------------------------------------------------------------
        // Define the column counts to be used for the stacked DataBar barcode
        // --------------------------------------------------------------------
        List<int> columnCounts = new List<int> { 4, 6, 8 };

        // --------------------------------------------------------------------
        // Generate barcode images and store them in memory streams
        // --------------------------------------------------------------------
        List<MemoryStream> barcodeStreams = new List<MemoryStream>();

        foreach (int columns in columnCounts)
        {
            // Create a barcode generator for the DataBar Expanded Stacked symbology
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DatabarExpandedStacked, "Databar Expanded Stacked Example"))
            {
                // Apply the custom column count
                generator.Parameters.Barcode.DataBar.Columns = columns;

                // Optional: increase X‑dimension for better visual clarity
                generator.Parameters.Barcode.XDimension.Pixels = 2f;

                // Save the generated barcode to a memory stream in PNG format
                MemoryStream ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for later reading
                barcodeStreams.Add(ms);
            }
        }

        // --------------------------------------------------------------------
        // Create a PDF document and add each barcode image to its own page
        // --------------------------------------------------------------------
        using (Document pdfDoc = new Document())
        {
            for (int i = 0; i < barcodeStreams.Count; i++)
            {
                // Add a new page to the PDF
                Page page = pdfDoc.Pages.Add();

                // Insert a title that indicates the column count used
                TextFragment title = new TextFragment($"DataBar Expanded Stacked - Columns: {columnCounts[i]}");
                title.Position = new Position(50, 750);
                title.TextState.FontSize = 14;
                page.Paragraphs.Add(title);

                // Configure the barcode image for placement on the page
                MemoryStream barcodeStream = barcodeStreams[i];
                Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image
                {
                    ImageStream = barcodeStream,
                    FixWidth = 300,
                    FixHeight = 150,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new MarginInfo { Top = 20 }
                };
                page.Paragraphs.Add(pdfImage);
            }

            // Save the assembled PDF to the previously defined path
            pdfDoc.Save(pdfPath);
        }

        // --------------------------------------------------------------------
        // Clean up memory streams used for barcode images
        // --------------------------------------------------------------------
        foreach (var stream in barcodeStreams)
        {
            stream.Dispose();
        }

        // Inform the user where the PDF was saved
        Console.WriteLine($"PDF with DataBar stacked barcodes saved to: {pdfPath}");
    }
}