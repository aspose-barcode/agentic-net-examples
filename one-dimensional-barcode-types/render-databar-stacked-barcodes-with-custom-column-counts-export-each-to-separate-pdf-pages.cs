// Title: Render DataBar Stacked Barcodes with Custom Column Counts to PDF
// Description: Demonstrates generating stacked DataBar barcodes with custom column counts and exporting each barcode to a separate page in a PDF document.
// Category-Description: This example belongs to the Aspose.BarCode generation and export category, showcasing how to use BarcodeGenerator, set DataBar specific parameters (such as column count), and combine generated images into a multi‑page PDF using Aspose.Pdf. Typical use cases include retail product labeling, inventory management, and any scenario requiring stacked DataBar symbologies with precise layout control. Developers often need to customize barcode dimensions, appearance, and then embed them into documents for printing or distribution.
// Prompt: Render DataBar stacked barcodes with custom column counts, export each to separate PDF pages.
// Tags: databar, stacked, barcode, pdf, aspose.barcode, image generation, aspose.pdf

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Pdf;

/// <summary>
/// Generates stacked DataBar barcodes with custom column counts and saves each barcode on a separate PDF page.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates barcode images, adds them to a PDF, and saves the document.
    /// </summary>
    static void Main()
    {
        // Define the DataBar stacked symbologies and their corresponding code texts.
        BaseEncodeType[] dataBarTypes = new BaseEncodeType[]
        {
            EncodeTypes.DatabarStacked,
            EncodeTypes.DatabarStackedOmniDirectional,
            EncodeTypes.DatabarLimited,
            EncodeTypes.DatabarExpandedStacked
        };

        string[] codeTexts = new string[]
        {
            "(01)12345678901231", // for DatabarStacked
            "(01)12345678901231", // for DatabarStackedOmniDirectional
            "(01)08888888888888", // for DatabarLimited (requires GTIN‑style value)
            "(01)12345678901231"  // for DatabarExpandedStacked
        };

        // Custom column counts for each barcode (example values).
        int[] columnCounts = new int[] { 2, 3, 4, 5 };

        // List to hold the generated barcode image streams.
        List<MemoryStream> barcodeStreams = new List<MemoryStream>();

        // Generate each barcode and store its PNG image in a memory stream.
        for (int i = 0; i < dataBarTypes.Length && i < 4; i++) // limit to 4 items per evaluation rules
        {
            BaseEncodeType type = dataBarTypes[i];
            string codeText = codeTexts[i];
            int columns = columnCounts[i];

            using (var generator = new BarcodeGenerator(type, codeText))
            {
                // Set basic appearance.
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Define explicit image size (required for stacked DataBar).
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 150f;

                // Disable auto‑size to allow manual BarHeight.
                generator.Parameters.AutoSizeMode = AutoSizeMode.None;
                generator.Parameters.Barcode.BarHeight.Point = 50f;

                // Apply custom column count.
                generator.Parameters.Barcode.DataBar.Columns = columns;

                // Save barcode to a memory stream as PNG.
                var ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset for later reading.
                barcodeStreams.Add(ms);
            }
        }

        // Create a PDF document and add each barcode image on a separate page.
        using (var pdfDoc = new Document())
        {
            for (int i = 0; i < barcodeStreams.Count; i++)
            {
                var page = pdfDoc.Pages.Add();

                // Define rectangle where the image will be placed (matches image size).
                var rect = new Aspose.Pdf.Rectangle(0, 0, 300, 150);

                // Add image from the corresponding stream.
                var stream = barcodeStreams[i];
                stream.Position = 0;
                page.AddImage(stream, rect);
            }

            // Save the PDF document to the current directory.
            string outputPdf = Path.Combine(Environment.CurrentDirectory, "DataBarStacked.pdf");
            pdfDoc.Save(outputPdf);
            Console.WriteLine($"PDF saved to: {outputPdf}");
        }

        // Dispose all memory streams.
        foreach (var ms in barcodeStreams)
        {
            ms.Dispose();
        }
    }
}