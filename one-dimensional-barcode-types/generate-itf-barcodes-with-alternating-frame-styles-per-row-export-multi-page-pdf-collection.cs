// Title: Generate ITF-14 Barcodes with Alternating Frame Styles and Export to Multi-Page PDF
// Description: Demonstrates creating ITF-14 barcodes with different frame styles per row, embedding each barcode into its own PDF page, and saving the result as a multi-page PDF document.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use BarcodeGenerator with ITF-14 symbology, customize border types, and combine generated images into a PDF using Aspose.Pdf. Typical use cases include batch creation of product barcodes with varied visual frames and compiling them into a single PDF for printing or distribution. Developers often need to generate multiple barcodes, adjust appearance settings, and programmatically assemble them into documents.
// Prompt: Generate ITF barcodes with alternating frame styles per row, export multi‑page PDF collection.
// Tags: itf14, barcode generation, frame style, pdf export, aspose.barcode, aspose.pdf, multi-page pdf, barcode border, c#
using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;
using Aspose.Pdf.Text;

/// <summary>
/// Demonstrates generating ITF‑14 barcodes with alternating frame styles and exporting them to a multi‑page PDF.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates barcode images, embeds them into a PDF, and saves the file.
    /// </summary>
    static void Main()
    {
        // Prepare sample data for ITF barcodes (14‑digit numeric strings)
        var codeTexts = new List<string>
        {
            "12345678901231", // valid ITF14
            "98765432109876",
            "11111111111111",
            "22222222222222"
        };

        // Define alternating frame styles for each barcode
        var borderTypes = new ITF14BorderType[]
        {
            ITF14BorderType.Frame,
            ITF14BorderType.Bar,
            ITF14BorderType.FrameOut,
            ITF14BorderType.BarOut
        };

        // Store generated barcode images in memory streams
        var barcodeStreams = new List<MemoryStream>();

        // Generate up to four barcodes (evaluation mode limit)
        for (int i = 0; i < Math.Min(codeTexts.Count, 4); i++)
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.ITF14, codeTexts[i]))
            {
                // Set common appearance options
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                generator.Parameters.Barcode.XDimension.Point = 2f;

                // Apply the alternating border style for the current barcode
                generator.Parameters.Barcode.ITF.BorderType = borderTypes[i % borderTypes.Length];
                generator.Parameters.Barcode.ITF.BorderThickness.Point = 2f;

                // Render the barcode to a PNG image stored in a memory stream
                var ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for later reading
                barcodeStreams.Add(ms);
            }
        }

        // Create a PDF document and embed each barcode on its own page
        using (var pdfDoc = new Document())
        {
            for (int i = 0; i < barcodeStreams.Count; i++)
            {
                var page = pdfDoc.Pages.Add();

                var pdfImage = new Image
                {
                    ImageStream = barcodeStreams[i],
                    FixWidth = 200,
                    FixHeight = 200,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new MarginInfo { Top = 20 }
                };

                page.Paragraphs.Add(pdfImage);
            }

            // Save the multi‑page PDF to disk
            const string outputPdf = "ITF_Barcodes.pdf";
            pdfDoc.Save(outputPdf);
            Console.WriteLine($"PDF saved to: {Path.GetFullPath(outputPdf)}");
        }

        // Dispose all memory streams after the PDF has been saved
        foreach (var ms in barcodeStreams)
        {
            ms.Dispose();
        }
    }
}