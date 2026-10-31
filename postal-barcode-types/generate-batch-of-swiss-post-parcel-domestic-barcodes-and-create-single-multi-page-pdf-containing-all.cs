// Title: Generate Swiss Post Parcel Barcodes and Combine into Multi‑Page PDF
// Description: Demonstrates how to generate Swiss Post Parcel domestic barcodes using Aspose.BarCode and embed them into a multi‑page PDF with Aspose.Pdf.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and Aspose.Pdf document creation category. It shows the use of BarcodeGenerator (EncodeTypes.SwissPostParcel) to create PNG images in memory, then adds each image as a separate page in a PDF document. Typical use cases include batch creation of shipping labels, parcel tracking documents, or any scenario where multiple barcodes must be compiled into a single printable file. Developers often need to combine barcode images into PDFs for distribution or archival, making this pattern a common building block in logistics and e‑commerce solutions.
// Prompt: Generate a batch of Swiss Post Parcel domestic barcodes and create a single multi‑page PDF containing all.
// Tags: swisspostparcel, barcode generation, pdf creation, aspose.barcode, aspose.pdf, barcode image, multi-page pdf

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;

/// <summary>
/// Generates a set of Swiss Post Parcel domestic barcodes and compiles them into a multi‑page PDF document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates barcode images in memory, adds each to a PDF page, and saves the result.
    /// </summary>
    static void Main()
    {
        // Sample Swiss Post Parcel domestic barcode values
        string[] codes = new string[]
        {
            "98.34.123456.12345678",
            "99.12.654321.87654321",
            "983412345612345678"
        };

        // Limit the number of barcodes to a maximum of 4 (evaluation mode restriction)
        int count = Math.Min(codes.Length, 4);
        var barcodeStreams = new List<MemoryStream>();

        // Generate barcode images and store them in memory streams
        for (int i = 0; i < count; i++)
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, codes[i]))
            {
                // Configure visual appearance
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Parameters.Barcode.BarHeight.Pixels = 40f;

                // Save barcode as PNG into a memory stream
                var ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for later reading
                barcodeStreams.Add(ms);
            }
        }

        // Ensure the output directory exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        string pdfPath = Path.Combine(outputDir, "SwissPostParcelBarcodes.pdf");

        // Create a new PDF document and add each barcode image as a separate page
        using (var pdfDoc = new Document())
        {
            for (int i = 0; i < barcodeStreams.Count; i++)
            {
                var page = pdfDoc.Pages.Add(); // Add a new blank page
                double pageWidth = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;

                // Define the rectangle that covers the whole page
                var rect = new Aspose.Pdf.Rectangle(0, 0, pageWidth, pageHeight);
                int imgWidth = (int)pageWidth;
                int imgHeight = (int)pageHeight;

                // Insert the barcode image, scaling it to fill the page
                page.AddImage(barcodeStreams[i], rect, imgWidth, imgHeight, true);
            }

            // Save the assembled PDF to disk
            pdfDoc.Save(pdfPath);
        }

        // Release all memory streams now that the PDF has been saved
        foreach (var ms in barcodeStreams)
        {
            ms.Dispose();
        }

        // Inform the user where the PDF was created
        Console.WriteLine($"PDF with {count} Swiss Post Parcel barcodes created at:");
        Console.WriteLine(pdfPath);
    }
}