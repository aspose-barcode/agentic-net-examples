using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation of DataBar Expanded Stacked barcodes and embedding them into a PDF.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcode images for different column counts,
    /// adds each image to a separate PDF page, saves the PDF, and cleans up resources.
    /// </summary>
    static void Main()
    {
        // Define column counts for the DataBar Expanded Stacked barcodes (evaluation limit: max 4)
        int[] columnCounts = { 1, 2, 3, 4 };

        // Sample GTIN code text suitable for DataBar symbologies
        const string codeText = "(01)12345678901231";

        // Keep generated image streams until the PDF is saved
        List<MemoryStream> imageStreams = new List<MemoryStream>();

        // Create a PDF document (Aspose.Pdf Document is not IDisposable)
        Document pdfDoc = new Document();

        // Iterate over each column count to generate corresponding barcode
        foreach (int columns in columnCounts)
        {
            // Generate the barcode image in memory using a using block for the generator
            using (var generator = new BarcodeGenerator(EncodeTypes.DatabarExpandedStacked, codeText))
            {
                // Set the desired number of columns for this barcode
                generator.Parameters.Barcode.DataBar.Columns = columns;

                // Save the barcode to a memory stream in PNG format
                var ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // Store the stream for later disposal after PDF is saved
                imageStreams.Add(ms);

                // Add a new page to the PDF and place the barcode image on it
                Page page = pdfDoc.Pages.Add();
                var pdfImage = new Aspose.Pdf.Image
                {
                    ImageStream = ms,
                    // Adjust size as needed; using fixed dimensions for simplicity
                    FixWidth = 200.0,
                    FixHeight = 100.0
                };
                page.Paragraphs.Add(pdfImage);
            }
        }

        // Save the compiled PDF containing all barcode pages
        const string pdfPath = "DataBarExpandedStacked.pdf";
        pdfDoc.Save(pdfPath);
        Console.WriteLine($"PDF document created: {pdfPath}");

        // Dispose all memory streams after the PDF has been saved
        foreach (var stream in imageStreams)
        {
            stream.Dispose();
        }
    }
}