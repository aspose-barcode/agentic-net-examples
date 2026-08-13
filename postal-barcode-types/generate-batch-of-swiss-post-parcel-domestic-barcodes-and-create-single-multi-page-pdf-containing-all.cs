// Title: Generate Swiss Post Parcel Barcodes and Combine into Multi‑Page PDF
// Description: Demonstrates how to generate Swiss Post Parcel domestic barcodes using Aspose.BarCode and embed them into a multi‑page PDF with Aspose.Pdf.
// Category-Description: This example belongs to the barcode generation and PDF composition category of Aspose.BarCode. It showcases the use of BarcodeGenerator (EncodeTypes.SwissPostParcel), BarCodeImageFormat, and Aspose.Pdf Document and Image classes to create barcode images and place them on separate PDF pages. Typical use cases include batch printing of shipping labels, parcel tracking documents, and bulk barcode reports where developers need to programmatically generate multiple barcodes and consolidate them into a single PDF file.
// Prompt: Generate a batch of Swiss Post Parcel domestic barcodes and create a single multi‑page PDF containing all.
// Tags: swisspostparcel, barcode, pdf, aspose.barcode, aspose.pdf, generation, image

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Demonstrates generating Swiss Post Parcel domestic barcodes and assembling them into a multi‑page PDF.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that triggers PDF generation with barcode images.
    /// </summary>
    static void Main()
    {
        // Generate a PDF with a batch of Swiss Post Parcel domestic barcodes.
        GenerateSwissPostParcelPdf();
    }

    static void GenerateSwissPostParcelPdf()
    {
        // Sample code texts for Swiss Post Parcel domestic barcodes.
        // In a real scenario these would be valid parcel identifiers.
        var codeTexts = new List<string>
        {
            "1234567890",
            "9876543210",
            "1122334455",
            "5566778899"
        };

        // Limit to 4 items as required for Aspose.Pdf evaluation mode.
        int maxCount = Math.Min(codeTexts.Count, 4);

        // Prepare a list to hold the memory streams until the PDF is saved.
        var barcodeStreams = new List<MemoryStream>();

        // Create a new PDF document.
        using (var pdfDoc = new Document())
        {
            for (int i = 0; i < maxCount; i++)
            {
                string codeText = codeTexts[i];

                // Create a barcode generator for Swiss Post Parcel.
                using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, codeText))
                {
                    // Optional: set barcode colors if desired.
                    generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                    generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                    // Save the barcode image to a memory stream in PNG format.
                    var ms = new MemoryStream();
                    generator.Save(ms, BarCodeImageFormat.Png);
                    ms.Position = 0; // Reset stream position for reading.

                    // Keep the stream for later disposal.
                    barcodeStreams.Add(ms);

                    // Add a new page to the PDF.
                    var page = pdfDoc.Pages.Add();

                    // Create an Aspose.Pdf.Image from the barcode stream.
                    var pdfImage = new Aspose.Pdf.Image
                    {
                        ImageStream = ms,
                        // Adjust size as needed.
                        FixWidth = 200.0,
                        FixHeight = 200.0,
                        HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center,
                        Margin = new Aspose.Pdf.MarginInfo { Top = 20 }
                    };

                    // Add the image to the page.
                    page.Paragraphs.Add(pdfImage);
                }
            }

            // Save the multi‑page PDF to disk.
            string outputPath = "SwissPostParcelBarcodes.pdf";
            pdfDoc.Save(outputPath);
            Console.WriteLine($"PDF saved to {Path.GetFullPath(outputPath)}");
        }

        // Dispose all barcode streams.
        foreach (var stream in barcodeStreams)
        {
            stream.Dispose();
        }
    }
}