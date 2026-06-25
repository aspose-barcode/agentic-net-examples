using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Demonstrates generating Swiss Post Parcel barcodes and embedding them into a multi‑page PDF.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes for sample code texts, creates a PDF with each barcode on its own page, and saves the document.
    /// </summary>
    static void Main()
    {
        // Sample Swiss Post Parcel domestic codetexts (up to 4 for evaluation mode)
        var codeTexts = new List<string>
        {
            "1234567890",
            "2345678901",
            "3456789012",
            "4567890123"
        };

        // Store generated barcode images in memory streams for later PDF insertion
        var imageStreams = new List<MemoryStream>();

        // Generate a barcode image for each codetext
        foreach (var text in codeTexts)
        {
            // Create a new memory stream to hold the PNG image
            var ms = new MemoryStream();

            // Use Aspose.BarCode to generate the barcode and write it to the stream
            using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, text))
            {
                // Save the barcode as PNG into the memory stream
                generator.Save(ms, BarCodeImageFormat.Png);
                // Reset stream position so it can be read from the beginning later
                ms.Position = 0;
            }

            // Keep the stream for later use when building the PDF
            imageStreams.Add(ms);
        }

        // Create a new PDF document
        var pdfDoc = new Document();

        // Add each barcode image to a separate page in the PDF
        foreach (var stream in imageStreams)
        {
            // Add a new page to the document
            var page = pdfDoc.Pages.Add();

            // Create an Aspose.Pdf.Image object that reads from the memory stream
            var pdfImage = new Aspose.Pdf.Image
            {
                ImageStream = stream,
                // Set fixed dimensions for the barcode image (adjust as needed)
                FixWidth = 200.0,
                FixHeight = 100.0
            };

            // Add the image to the page's paragraph collection
            page.Paragraphs.Add(pdfImage);
        }

        // Define the output PDF file path
        const string pdfPath = "SwissPostParcelBarcodes.pdf";

        // Save the multi‑page PDF to disk
        pdfDoc.Save(pdfPath);

        // Dispose of all memory streams now that the PDF has been saved
        foreach (var stream in imageStreams)
        {
            stream.Dispose();
        }

        // Inform the user that the operation completed successfully
        Console.WriteLine($"PDF with {codeTexts.Count} Swiss Post Parcel barcodes saved to '{pdfPath}'.");
    }
}