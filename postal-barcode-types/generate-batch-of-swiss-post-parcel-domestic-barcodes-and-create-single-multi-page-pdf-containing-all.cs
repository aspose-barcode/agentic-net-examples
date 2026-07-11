// Title: Generate Swiss Post Parcel barcodes and compile into multi‑page PDF
// Description: This example creates several Swiss Post Parcel domestic barcodes and assembles them into a single PDF document, one barcode per page.
// Category-Description: Demonstrates batch barcode generation using Aspose.BarCode and PDF composition with Aspose.Pdf. It showcases the BarcodeGenerator class for SwissPostParcel symbology, configuring colors, rendering to PNG, and embedding images into a multi‑page PDF via Aspose.Pdf Document. Useful for developers needing to produce printable barcode sheets or shipping labels in bulk.
// Prompt: Generate a batch of Swiss Post Parcel domestic barcodes and create a single multi‑page PDF containing all.
// Tags: swisspostparcel, barcode, pdf, batch, generation, aspose.barcode, aspose.pdf

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Generates a batch of Swiss Post Parcel domestic barcodes and compiles them into a multi‑page PDF.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates barcode images, adds each to a PDF page, and saves the document.
    /// </summary>
    static void Main()
    {
        // Path for the resulting PDF file
        string outputPdfPath = "SwissPostParcelBarcodes.pdf";

        // Sample Swiss Post Parcel domestic barcode texts (limited to 4 for evaluation mode)
        var codeTexts = new List<string>
        {
            "123456789012", // Sample domestic parcel
            "234567890123",
            "345678901234",
            "456789012345"
        };

        // Keep barcode image streams alive until the PDF is saved
        var barcodeStreams = new List<MemoryStream>();

        // Create a new PDF document
        using (var pdfDoc = new Document())
        {
            // Generate a barcode for each code text and add it to a new PDF page
            foreach (var text in codeTexts)
            {
                // Generate barcode image into a memory stream
                using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, text))
                {
                    // Optional: set colors (fully qualified to avoid ambiguity)
                    generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                    generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                    var ms = new MemoryStream();
                    // Save barcode as PNG into the stream
                    generator.Save(ms, BarCodeImageFormat.Png);
                    ms.Position = 0;

                    // Keep the stream for later disposal
                    barcodeStreams.Add(ms);

                    // Add a new page to the PDF
                    var page = pdfDoc.Pages.Add();

                    // Create an image object that uses the barcode stream
                    var pdfImage = new Image
                    {
                        ImageStream = ms,
                        // Adjust size as needed
                        FixWidth = 200,
                        FixHeight = 100,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };

                    // Add the image to the page
                    page.Paragraphs.Add(pdfImage);
                }
            }

            // Save the multi‑page PDF
            pdfDoc.Save(outputPdfPath);
        }

        // Dispose all barcode streams after the PDF has been saved
        foreach (var stream in barcodeStreams)
        {
            stream.Dispose();
        }

        Console.WriteLine($"PDF with Swiss Post Parcel barcodes created: {outputPdfPath}");
    }
}