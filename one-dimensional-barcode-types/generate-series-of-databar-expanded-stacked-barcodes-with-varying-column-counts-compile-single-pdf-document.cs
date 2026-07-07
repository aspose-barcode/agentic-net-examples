// Title: Generate DataBar Expanded Stacked barcodes and compile into a PDF
// Description: Demonstrates creating DataBar Expanded Stacked barcodes with different column counts and placing them into a single PDF document.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure DataBar symbology, adjust column counts, render barcodes to images, and embed them into an Aspose.Pdf document. Developers often need to produce multiple barcodes in one PDF for reports, labels, or batch processing, using BarcodeGenerator, BarcodeParameters, and Pdf Document classes.
// Prompt: Generate series of DataBar Expanded Stacked barcodes with varying column counts, compile single PDF document.
// Tags: databar, expandedstacked, barcode, pdf, aspnet, aspose.barcode, aspose.pdf, generation, image, columns

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Example program that generates a series of DataBar Expanded Stacked barcodes with varying column counts
/// and compiles them into a single PDF document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output PDF file name
        string outputPdf = "DataBarExpandedStacked.pdf";

        // List of column counts to apply to each barcode instance
        List<int> columnCounts = new List<int> { 2, 3, 4, 5 };

        // Create a new PDF document using Aspose.Pdf
        using (var pdfDoc = new Document())
        {
            // Add a single page (evaluation mode permits up to 4 images)
            var page = pdfDoc.Pages.Add();

            // Determine cell dimensions for a 2x2 grid layout on the page
            double pageWidth = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;
            double cellWidth = pageWidth / 2;
            double cellHeight = pageHeight / 2;

            int index = 0;
            foreach (int cols in columnCounts)
            {
                // Respect evaluation restriction: maximum of 4 barcodes per document
                if (index >= 4) break;

                // Initialize barcode generator for DataBar Expanded Stacked symbology
                using (var generator = new BarcodeGenerator(EncodeTypes.DatabarExpandedStacked, "(01)01234567890123"))
                {
                    // Set the specific column count for the DataBar barcode
                    generator.Parameters.Barcode.DataBar.Columns = cols;

                    // Optional visual customizations
                    generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                    generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                    generator.Parameters.Barcode.XDimension.Point = 2f;

                    // Render the barcode to a memory stream in PNG format
                    using (var ms = new MemoryStream())
                    {
                        generator.Save(ms, BarCodeImageFormat.Png);
                        ms.Position = 0;

                        // Calculate the placement rectangle for the current grid cell
                        int row = index / 2;
                        int col = index % 2;
                        double llx = col * cellWidth;
                        double lly = pageHeight - ((row + 1) * cellHeight);
                        double urx = llx + cellWidth;
                        double ury = lly + cellHeight;
                        var rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                        // Add the barcode image to the PDF page within the calculated rectangle
                        page.AddImage(ms, rect, (int)cellWidth, (int)cellHeight, true);
                    }
                }

                index++;
            }

            // Save the assembled PDF document to disk
            pdfDoc.Save(outputPdf);
        }

        // Output the full path of the generated PDF for user reference
        Console.WriteLine($"PDF generated: {Path.GetFullPath(outputPdf)}");
    }
}