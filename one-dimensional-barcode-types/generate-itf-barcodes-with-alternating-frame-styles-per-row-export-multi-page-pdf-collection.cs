// Title: Generate ITF-14 Barcodes with Alternating Frame Styles and Export to Multi‑Page PDF
// Description: This example creates ITF‑14 barcodes using different border styles, places each barcode on its own PDF page, and saves the collection as a multi‑page PDF file.
// Category-Description: Demonstrates how to use Aspose.BarCode to generate barcodes with varying visual properties and Aspose.Pdf to compose a multi‑page document. Key classes include BarcodeGenerator, ITF14BorderType, Document, and Image. Typical scenarios involve batch barcode creation for packaging, inventory, or shipping labels where each page represents a distinct style or product variant.
// Prompt: Generate ITF barcodes with alternating frame styles per row, export multi‑page PDF collection.
// Tags: itf, itf14, barcode, pdf, aspose.barcode, aspose.pdf, image-generation

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Demonstrates generation of ITF‑14 barcodes with different border styles
/// and combines them into a multi‑page PDF document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcode images, adds them to a PDF,
    /// and writes the resulting file to the temporary folder.
    /// </summary>
    static void Main()
    {
        // Define the output PDF file path in the system temporary directory.
        string outputPdfPath = Path.Combine(Path.GetTempPath(), "ITFBarcodes.pdf");

        // Define the set of border styles to apply to each barcode.
        // The evaluation version of Aspose.Pdf limits the number of distinct styles to four.
        ITF14BorderType[] borderStyles = new ITF14BorderType[]
        {
            ITF14BorderType.None,
            ITF14BorderType.Frame,
            ITF14BorderType.Bar,
            ITF14BorderType.FrameOut
        };

        // Store each generated barcode image in a memory stream until the PDF is built.
        List<MemoryStream> barcodeStreams = new List<MemoryStream>();

        // --------------------------------------------------------------------
        // Generate barcodes with alternating frame styles
        // --------------------------------------------------------------------
        foreach (ITF14BorderType style in borderStyles)
        {
            // Create a barcode generator for the ITF‑14 symbology with a sample data string.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.ITF14, "12345678901231"))
            {
                // Set basic visual parameters.
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Apply the current border style and a modest border thickness.
                generator.Parameters.Barcode.ITF.BorderType = style;
                generator.Parameters.Barcode.ITF.BorderThickness.Pixels = 5f;

                // Save the barcode image to a memory stream in PNG format.
                MemoryStream ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for later reading.
                barcodeStreams.Add(ms);
            }
        }

        // --------------------------------------------------------------------
        // Create a PDF document and add each barcode image on a separate page
        // --------------------------------------------------------------------
        using (Document pdfDoc = new Document())
        {
            foreach (MemoryStream barcodeStream in barcodeStreams)
            {
                // Add a new blank page to the PDF.
                Page page = pdfDoc.Pages.Add();

                // Create an Aspose.Pdf.Image object from the barcode stream.
                Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image
                {
                    ImageStream = barcodeStream,
                    // Define a reasonable display size for the barcode.
                    FixWidth = 200.0,
                    FixHeight = 100.0,
                    // Center the image horizontally and vertically on the page.
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                // Insert the image into the page's paragraph collection.
                page.Paragraphs.Add(pdfImage);
            }

            // Save the assembled PDF to the specified file path.
            pdfDoc.Save(outputPdfPath);
        }

        // Release all memory streams now that the PDF has been saved.
        foreach (MemoryStream ms in barcodeStreams)
        {
            ms.Dispose();
        }

        // Inform the user where the PDF was written.
        Console.WriteLine($"PDF with ITF barcodes saved to: {outputPdfPath}");
    }
}