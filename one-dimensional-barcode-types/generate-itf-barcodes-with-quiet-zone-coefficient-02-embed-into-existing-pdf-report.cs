// Title: Generate ITF14 barcode with custom quiet zone and embed into PDF
// Description: Demonstrates creating an ITF14 barcode with a quiet‑zone coefficient of 0.2, rendering it to PNG, and inserting the image into an existing PDF document.
// Category-Description: This example belongs to the Aspose.BarCode for .NET barcode generation category, illustrating how to configure barcode parameters such as size, colors, and quiet zone, and how to combine the generated image with Aspose.Pdf to produce a combined report. Typical use cases include adding product barcodes to invoices, shipping labels, or other PDF reports where precise barcode rendering is required. Developers often need to adjust quiet‑zone settings and embed barcodes programmatically, using BarcodeGenerator, BarcodeParameters, and Aspose.Pdf Document classes.
// Prompt: Generate ITF barcodes with quiet zone coefficient 0.2, embed into existing PDF report.
// Tags: itf14, barcode, quietzone, pdf, aspose.barcode, aspose.pdf, image-embedding, generation

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Demonstrates generating an ITF14 barcode with a custom quiet zone and embedding it into a PDF.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a source PDF if missing, generates the barcode, and saves the result.
    /// </summary>
    static void Main()
    {
        // Define file paths for the source PDF and the output PDF
        string inputPdfPath = "input.pdf";
        string outputPdfPath = "output.pdf";

        // If the source PDF does not exist, create a simple one-page document
        if (!File.Exists(inputPdfPath))
        {
            var emptyDoc = new Document();
            emptyDoc.Pages.Add();
            emptyDoc.Save(inputPdfPath);
        }

        // Initialize an ITF14 barcode generator with a 14‑digit value
        using (var generator = new BarcodeGenerator(EncodeTypes.ITF14, "12345678901231"))
        {
            // Configure basic appearance settings
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;
            generator.Parameters.Barcode.BarHeight.Point = 50f;
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Calculate quiet zone based on the X‑dimension (0.2 * XDimension)
            float quietZone = generator.Parameters.Barcode.XDimension.Point * 0.2f;
            generator.Parameters.Barcode.Padding.Left.Point = quietZone;
            generator.Parameters.Barcode.Padding.Right.Point = quietZone;
            generator.Parameters.Barcode.Padding.Top.Point = quietZone;
            generator.Parameters.Barcode.Padding.Bottom.Point = quietZone;

            // Render the barcode to a memory stream in PNG format
            using (var barcodeStream = new MemoryStream())
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0; // Reset stream position for reading

                // Load the existing PDF and embed the barcode image on the first page
                using (var pdfDoc = new Document(inputPdfPath))
                {
                    var page = pdfDoc.Pages[1];
                    var pdfImage = new Aspose.Pdf.Image
                    {
                        ImageStream = barcodeStream,
                        FixWidth = 150,
                        FixHeight = 50,
                        HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center,
                        VerticalAlignment = Aspose.Pdf.VerticalAlignment.Center,
                        Margin = new Aspose.Pdf.MarginInfo { Top = 10 }
                    };
                    page.Paragraphs.Add(pdfImage);
                    pdfDoc.Save(outputPdfPath);
                }
            }
        }
    }
}