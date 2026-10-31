// Title: Generate ITF14 barcodes with alternating frame styles and export as multi‑page PDF
// Description: Demonstrates creating ITF14 barcodes with different border styles per row and compiling them into a multi‑page PDF document.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of BarcodeGenerator, ITF14 settings, and Aspose.Pdf for document creation. Developers often need to generate barcodes with varied visual styles and combine them into PDFs for reporting, labeling, or batch printing scenarios. The code illustrates key API classes such as BarcodeGenerator, ITF14BorderType, and Aspose.Pdf.Document, providing a reusable pattern for similar tasks.
// Prompt: Generate ITF barcodes with alternating frame styles per row, export multi‑page PDF collection.
// Tags: itf14, barcode, generation, pdf, aspose.barcode, aspose.pdf, borderstyle, multi-page

using System;
using System.IO;
using System.Collections.Generic;
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
    /// Entry point. Creates barcode images with different border styles, assembles them into a PDF, and saves the result.
    /// </summary>
    static void Main()
    {
        // Define output directory and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "ItfPdfDemo");
        Directory.CreateDirectory(outputDir);
        string pdfPath = Path.Combine(outputDir, "ITF_Barcodes.pdf");

        // Sample 14‑digit code (valid ITF14)
        string codeText = "12345678901231";

        // Define frame styles to alternate across pages
        ITF14BorderType[] styles = new ITF14BorderType[]
        {
            ITF14BorderType.None,
            ITF14BorderType.Frame,
            ITF14BorderType.Bar,
            ITF14BorderType.FrameOut
        };

        // Limit to maximum 4 pages (evaluation mode restriction)
        int maxPages = Math.Min(styles.Length, 4);

        // Store generated barcode images in memory streams
        List<MemoryStream> barcodeStreams = new List<MemoryStream>();

        // Generate a barcode for each style
        for (int i = 0; i < maxPages; i++)
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.ITF14, codeText))
            {
                // Set basic appearance parameters
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Parameters.Barcode.ITF.BorderType = styles[i];
                generator.Parameters.Barcode.ITF.BorderThickness.Pixels = 5f;

                // Save barcode as PNG into a memory stream
                var ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for later reading
                barcodeStreams.Add(ms);
            }
        }

        // Create a PDF document and add each barcode image as a separate page
        using (var pdfDoc = new Document())
        {
            for (int i = 0; i < barcodeStreams.Count; i++)
            {
                var page = pdfDoc.Pages.Add();

                var img = new Aspose.Pdf.Image
                {
                    ImageStream = barcodeStreams[i],
                    FixWidth = 200.0,
                    FixHeight = 200.0,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new MarginInfo { Top = 20 }
                };

                page.Paragraphs.Add(img);
            }

            // Save the assembled PDF to disk
            pdfDoc.Save(pdfPath);
        }

        // Release memory streams after PDF creation
        foreach (var ms in barcodeStreams)
        {
            ms.Dispose();
        }

        Console.WriteLine($"PDF with {maxPages} ITF barcodes saved to: {pdfPath}");
    }
}