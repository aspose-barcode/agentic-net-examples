// Title: Generate Swiss Post Parcel barcodes and compile into multi-page PDF
// Description: Demonstrates creating Swiss Post Parcel domestic barcodes and embedding them into a single PDF document, one barcode per page.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showing how to use BarcodeGenerator with EncodeTypes.SwissPostParcel, configure barcode dimensions, and combine generated images into a PDF using Aspose.Pdf. Typical use cases include batch creation of shipping labels or parcel identifiers for Swiss Post services, where developers need to produce multiple barcodes and consolidate them into a printable document. The example highlights key classes such as BarcodeGenerator, BarCodeImageFormat, Document, Page, and Image.
// Prompt: Generate a batch of Swiss Post Parcel domestic barcodes and create a single multi‑page PDF containing all.
// Tags: barcode, swisspostparcel, pdf, aspnet, aspose.barcode, aspose.pdf, generation, batch, multi-page

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Example program that generates a set of Swiss Post Parcel domestic barcodes
/// and assembles them into a single multi‑page PDF document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates barcode images, adds each to a PDF page, and saves the PDF.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the output PDF file path in the current working directory.
        string outputPdf = Path.Combine(Directory.GetCurrentDirectory(), "SwissPostParcelBarcodes.pdf");

        // List of sample Swiss Post Parcel domestic barcode strings.
        var domesticCodes = new List<string>
        {
            "98.34.123456.12345678",
            "99.12.654321.87654321",
            "98.56.111111.22222222"
        };

        // Limit the number of barcodes to generate (max 4) to avoid excessive processing.
        int count = Math.Min(domesticCodes.Count, 4);
        var barcodeStreams = new List<MemoryStream>();

        // Generate barcode images and store them in memory streams.
        for (int i = 0; i < count; i++)
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, domesticCodes[i]))
            {
                // Configure barcode appearance.
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Parameters.Barcode.BarHeight.Pixels = 40f;

                // Save the barcode as PNG into a memory stream.
                var ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for later reading.
                barcodeStreams.Add(ms);
            }
        }

        // Create a new PDF document and add each barcode image to a separate page.
        using (var pdfDoc = new Document())
        {
            foreach (var stream in barcodeStreams)
            {
                // Add a new page to the PDF.
                var page = pdfDoc.Pages.Add();

                // Create an Image object linked to the barcode stream.
                var pdfImage = new Image
                {
                    ImageStream = stream,
                    FixWidth = 200.0,
                    FixHeight = 100.0,
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                // Insert the image into the page's paragraph collection.
                page.Paragraphs.Add(pdfImage);
            }

            // Save the assembled PDF to the specified file.
            pdfDoc.Save(outputPdf);
        }

        // Release all memory streams used for barcode images.
        foreach (var stream in barcodeStreams)
        {
            stream.Dispose();
        }

        // Inform the user where the PDF was saved.
        Console.WriteLine($"PDF saved to: {outputPdf}");
    }
}