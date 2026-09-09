// Title: Generate Swiss Post Parcel barcodes and compile into multi-page PDF
// Description: Demonstrates how to generate Swiss Post Parcel domestic barcodes using Aspose.BarCode and embed them into a multi‑page PDF with Aspose.Pdf.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and Aspose.Pdf document creation category. It shows how to use BarcodeGenerator with EncodeTypes.SwissPostParcel, configure barcode appearance, and combine multiple barcode images into a single PDF document. Developers working on shipping, logistics, or document automation often need to batch‑create barcodes and produce printable PDFs.
// Prompt: Generate a batch of Swiss Post Parcel domestic barcodes and create a single multi‑page PDF containing all.
// Tags: swisspostparcel, barcode generation, pdf creation, aspnet, aspose.barcode, aspose.pdf, multi-page, batch

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;
using Aspose.Pdf.Text;

/// <summary>
/// Example program that generates a set of Swiss Post Parcel domestic barcodes
/// and assembles them into a single multi‑page PDF document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcode images, adds each to a new PDF page,
    /// saves the PDF, and cleans up resources.
    /// </summary>
    static void Main()
    {
        // Sample Swiss Post Parcel Domestic identifiers (original format)
        var identifiers = new List<string>
        {
            "98.34.123456.12345678",
            "99.12.654321.87654321",
            "98.56.111111.22222222",
            "99.99.999999.99999999"
        };

        // Limit to 4 items as per evaluation mode restriction
        int count = Math.Min(identifiers.Count, 4);

        // Determine output PDF path in the current directory
        var pdfPath = Path.Combine(Directory.GetCurrentDirectory(), "SwissPostParcelBarcodes.pdf");

        // List to hold open streams until PDF is saved (prevents premature disposal)
        var barcodeStreams = new List<MemoryStream>();

        // Create a new PDF document that will contain one page per barcode
        using (var pdfDoc = new Document())
        {
            // Iterate over each identifier, generate its barcode, and add to PDF
            for (int i = 0; i < count; i++)
            {
                // Generate barcode image into a memory stream
                var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, identifiers[i]);
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Parameters.Barcode.BarHeight.Pixels = 40f;
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                var ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading
                barcodeStreams.Add(ms);

                // Add a new page to the PDF document
                var page = pdfDoc.Pages.Add();

                // Create a PDF image object from the barcode stream
                var pdfImage = new Aspose.Pdf.Image
                {
                    ImageStream = ms,
                    FixWidth = 200.0,
                    FixHeight = 80.0,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new MarginInfo { Top = 20 }
                };

                // Place the image on the page
                page.Paragraphs.Add(pdfImage);
            }

            // Save the assembled PDF to disk
            pdfDoc.Save(pdfPath);
        }

        // Dispose all memory streams now that the PDF has been saved
        foreach (var stream in barcodeStreams)
        {
            stream.Dispose();
        }

        Console.WriteLine($"PDF with {count} Swiss Post Parcel barcodes saved to: {pdfPath}");
    }
}