// Title: Generate ITF barcodes with alternating frame styles and export as multi‑page PDF
// Description: Demonstrates creating ITF‑14 barcodes with different border styles, placing each on its own PDF page, and saving the collection as a multi‑page PDF file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to customize ITF‑14 border types using the BarcodeGenerator class, capture barcode images, and embed them into an Aspose.Pdf Document. Typical use cases include batch barcode creation for product packaging, where varying frame styles are required per item, and exporting the results as a single PDF for printing or distribution.
// Prompt: Generate ITF barcodes with alternating frame styles per row, export multi‑page PDF collection.
// Tags: itf14, barcode, border, frame, pdf, aspose.barcode, aspose.pdf, image generation, multi-page

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;
using Aspose.Pdf.Text;

/// <summary>
/// Demonstrates generating ITF‑14 barcodes with alternating border styles and exporting them as a multi‑page PDF.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcode images, embeds each into a separate PDF page, and saves the document.
    /// </summary>
    static void Main()
    {
        // Prepare the output PDF file path in the temporary folder.
        string outputPdfPath = Path.Combine(Path.GetTempPath(), "ITF_Barcodes.pdf");

        // Define the set of border styles to apply (limited to four for this example).
        ITF14BorderType[] borderStyles = new ITF14BorderType[]
        {
            ITF14BorderType.None,
            ITF14BorderType.Frame,
            ITF14BorderType.Bar,
            ITF14BorderType.FrameOut
        };

        // Collection that will hold the generated barcode image streams.
        List<MemoryStream> barcodeStreams = new List<MemoryStream>();

        // Generate a barcode for each border style and store its image in memory.
        foreach (ITF14BorderType style in borderStyles)
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.ITF14, "12345678901231"))
            {
                // Set barcode visual parameters.
                generator.Parameters.Barcode.XDimension.Pixels = 2;
                generator.Parameters.Barcode.ITF.BorderType = style;

                // Save the barcode as a PNG image into a memory stream.
                var ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for later reading.
                barcodeStreams.Add(ms);
            }
        }

        // Create a new PDF document and add each barcode image to a separate page.
        using (var pdfDoc = new Document())
        {
            foreach (MemoryStream stream in barcodeStreams)
            {
                // Add a new page to the PDF.
                var page = pdfDoc.Pages.Add();

                // Configure the image to be placed on the page.
                var pdfImage = new Image
                {
                    ImageStream = stream,
                    FixWidth = 200.0,
                    FixHeight = 100.0,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new MarginInfo { Top = 20 }
                };

                // Add the image to the page's paragraph collection.
                page.Paragraphs.Add(pdfImage);
            }

            // Save the assembled PDF to the specified path.
            pdfDoc.Save(outputPdfPath);
        }

        // Release all memory streams now that the PDF has been saved.
        foreach (MemoryStream stream in barcodeStreams)
        {
            stream.Dispose();
        }

        // Inform the user where the PDF was saved.
        Console.WriteLine($"PDF with ITF barcodes saved to: {outputPdfPath}");
    }
}