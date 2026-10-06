// Title: Render DataBar Stacked Barcodes to Multi‑Page PDF
// Description: Demonstrates generating DataBar stacked, omnidirectional, and expanded stacked barcodes, customizing column count, and exporting each barcode image to a separate page in a PDF document.
// Category-Description: This example belongs to the Aspose.BarCode generation and Aspose.Pdf export category. It showcases the use of BarcodeGenerator (EncodeTypes.DatabarStacked, DatabarStackedOmniDirectional, DatabarExpandedStacked) to create barcode images, adjust parameters such as XDimension and column count, and then embed those images into a PDF using Aspose.Pdf Document. Developers often need to generate multiple barcode types and combine them into a single PDF report or batch file, making this pattern a common solution for inventory, shipping, and retail applications.
// Prompt: Render DataBar stacked barcodes with custom column counts, export each to separate PDF pages.
// Tags: databar, stacked, barcode, pdf, aspose.barcode, aspose.pdf, image generation, export

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;
using Aspose.Pdf.Text;

/// <summary>
/// Generates several DataBar stacked barcode variants, customizes their appearance,
/// and saves each as an image on a separate page of a PDF document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates barcode images, assembles them into a PDF,
    /// and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for output files
        string outputFolder = Path.Combine(Path.GetTempPath(), "DataBarPdf_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        string pdfPath = Path.Combine(outputFolder, "DataBarStackedBarcodes.pdf");

        // List to hold barcode image streams for later PDF insertion
        List<MemoryStream> barcodeStreams = new List<MemoryStream>();

        // -------------------------------------------------
        // 1. DataBar Stacked
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.DatabarStacked, "(01)12345678901231"))
        {
            // Set module width (X-dimension) to 2 pixels for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            var ms = new MemoryStream();
            generator.Save(ms, BarCodeImageFormat.Png);
            ms.Position = 0; // Reset stream position before reading
            barcodeStreams.Add(ms);
        }

        // -------------------------------------------------
        // 2. DataBar Stacked Omnidirectional
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.DatabarStackedOmniDirectional, "(01)12345678901231"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            var ms = new MemoryStream();
            generator.Save(ms, BarCodeImageFormat.Png);
            ms.Position = 0;
            barcodeStreams.Add(ms);
        }

        // -------------------------------------------------
        // 3. DataBar Expanded Stacked with custom column count (e.g., 4)
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.DatabarExpandedStacked, "(01)12345678901231"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            // Customize the number of columns for the expanded stacked variant
            generator.Parameters.Barcode.DataBar.Columns = 4;
            var ms = new MemoryStream();
            generator.Save(ms, BarCodeImageFormat.Png);
            ms.Position = 0;
            barcodeStreams.Add(ms);
        }

        // -------------------------------------------------
        // Create PDF and add each barcode image on a separate page
        // (Evaluation version limits the document to a maximum of 4 pages)
        // -------------------------------------------------
        using (var pdfDoc = new Document())
        {
            int pageCount = Math.Min(barcodeStreams.Count, 4);
            for (int i = 0; i < pageCount; i++)
            {
                // Add a new page to the PDF
                var page = pdfDoc.Pages.Add();

                // Create an image object linked to the barcode stream
                var img = new Aspose.Pdf.Image
                {
                    ImageStream = barcodeStreams[i],
                    FixWidth = 300,
                    FixHeight = 150,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new MarginInfo { Top = 20 }
                };

                // Insert the image into the page's paragraph collection
                page.Paragraphs.Add(img);
            }

            // Save the assembled PDF to the designated path
            pdfDoc.Save(pdfPath);
        }

        // Dispose all memory streams now that the PDF has been saved
        foreach (var ms in barcodeStreams)
        {
            ms.Dispose();
        }

        // Inform the user where the PDF was generated
        Console.WriteLine("PDF generated at: " + pdfPath);
    }
}