// Title: Generate DataBar Expanded Stacked Barcodes and Compile into a PDF
// Description: Demonstrates creating DataBar Expanded Stacked barcodes with varying column counts and assembling them into a single PDF document.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator with EncodeTypes.DatabarExpandedStacked, configure DataBar column settings, and embed the resulting PNG images into an Aspose.Pdf Document. Typical use cases include batch barcode creation for product labeling, inventory management, and generating printable PDF reports. Developers often need to combine multiple barcodes into one document, adjust layout parameters, and export to common formats like PDF.
// Prompt: Generate series of DataBar Expanded Stacked barcodes with varying column counts, compile single PDF document.
// Tags: databar, expandedstacked, barcode, pdf, aspose.barcode, aspose.pdf, generation, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Example program that creates DataBar Expanded Stacked barcodes with different column counts
/// and combines them into a single PDF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcode images, positions them on a PDF page,
    /// and saves the resulting document to a temporary folder.
    /// </summary>
    static void Main()
    {
        // Define output directory and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "DatabarExpandedStackedDemo");
        Directory.CreateDirectory(outputDir);
        string pdfPath = Path.Combine(outputDir, "DatabarExpandedStacked.pdf");

        // Column counts to generate (max 4 per guidelines)
        int[] columnCounts = new int[] { 4, 5, 6, 7 };

        // Create a new PDF document
        using (var pdfDoc = new Document())
        {
            // Add a single page to the document
            var page = pdfDoc.Pages.Add();

            // Layout settings for barcode images
            double leftMargin = 50;
            double imageWidth = 150;
            double imageHeight = 150;
            double verticalSpacing = 20;
            double pageHeight = page.PageInfo.Height;

            // Iterate over each column count and generate corresponding barcode
            for (int i = 0; i < columnCounts.Length; i++)
            {
                int columns = columnCounts[i];

                // Initialize barcode generator for DataBar Expanded Stacked symbology
                using (var generator = new BarcodeGenerator(EncodeTypes.DatabarExpandedStacked, "Databar Expanded Stacked long"))
                {
                    // Set the number of columns for the DataBar barcode
                    generator.Parameters.Barcode.DataBar.Columns = columns;

                    // Render barcode to an in‑memory PNG stream
                    using (var ms = new MemoryStream())
                    {
                        generator.Save(ms, BarCodeImageFormat.Png);
                        ms.Position = 0;

                        // Calculate vertical position for the current image
                        double yPos = pageHeight - ((i + 1) * (imageHeight + verticalSpacing));
                        var rect = new Aspose.Pdf.Rectangle(leftMargin, yPos, leftMargin + imageWidth, yPos + imageHeight);

                        // Add the barcode image to the PDF page
                        page.AddImage(ms, rect, (int)imageWidth, (int)imageHeight, true);
                    }
                }
            }

            // Save the assembled PDF to the specified path
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF with DataBar Expanded Stacked barcodes saved to: {pdfPath}");
    }
}