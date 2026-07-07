// Title: Generate ITF14 barcodes with alternating frame styles and export as multi‑page PDF
// Description: Demonstrates creating ITF14 barcodes with different border styles per row and compiling them into a single PDF document.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use BarcodeGenerator with ITF14 symbology, customize border styles via ITF parameters, and combine generated images into a multi‑page PDF using Aspose.Pdf. Developers often need to produce batch barcode PDFs with varied visual styles for packaging or inventory labeling.
// Prompt: Generate ITF barcodes with alternating frame styles per row, export multi‑page PDF collection.
// Tags: itf14, barcode, generation, pdf, aspose.barcode, aspose.pdf, borderstyle, image

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;
using Aspose.Pdf.Text;

/// <summary>
/// Demonstrates generating ITF14 barcodes with alternating frame styles and exporting them as a multi‑page PDF.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcode images, adds them to a PDF, and saves the document.
    /// </summary>
    static void Main()
    {
        // Define border styles to alternate per row (max 4 rows for evaluation mode)
        ITF14BorderType[] borderStyles = new ITF14BorderType[]
        {
            ITF14BorderType.Frame,
            ITF14BorderType.Bar,
            ITF14BorderType.FrameOut,
            ITF14BorderType.BarOut
        };

        // Sample 14‑digit ITF code (ITF14 requires exactly 14 digits)
        const string itfCode = "12345678901231";

        // List to hold barcode image streams until PDF is saved
        List<MemoryStream> barcodeStreams = new List<MemoryStream>();

        // Generate barcode images with alternating border styles
        for (int i = 0; i < borderStyles.Length; i++)
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.ITF14, itfCode))
            {
                // Set barcode colors
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Apply the specific ITF border style and a modest thickness
                generator.Parameters.Barcode.ITF.BorderType = borderStyles[i];
                generator.Parameters.Barcode.ITF.BorderThickness.Point = 2f;

                // Save barcode to a memory stream (PNG format)
                var ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset for reading by Aspose.Pdf
                barcodeStreams.Add(ms);
            }
        }

        // Create a PDF document and add one page per barcode row
        using (var pdfDoc = new Document())
        {
            for (int i = 0; i < barcodeStreams.Count; i++)
            {
                var page = pdfDoc.Pages.Add();

                // Add the barcode image to the page, centered
                var pdfImage = new Aspose.Pdf.Image
                {
                    ImageStream = barcodeStreams[i],
                    FixWidth = 200,
                    FixHeight = 100,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                page.Paragraphs.Add(pdfImage);
            }

            // Save the multi‑page PDF
            const string outputPdf = "ITF_Barcodes.pdf";
            pdfDoc.Save(outputPdf);
            Console.WriteLine($"PDF saved to {Path.GetFullPath(outputPdf)}");
        }

        // Dispose all memory streams after PDF is saved
        foreach (var stream in barcodeStreams)
        {
            stream.Dispose();
        }
    }
}