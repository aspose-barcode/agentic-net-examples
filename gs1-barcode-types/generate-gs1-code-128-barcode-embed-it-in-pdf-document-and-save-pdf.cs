// Title: Generate GS1 Code 128 Barcode and Embed in PDF
// Description: This example creates a GS1 Code 128 barcode, renders it as a PNG image, inserts the image into a PDF document, and saves the PDF file.
// Category-Description: Demonstrates Aspose.BarCode barcode generation (using BarcodeGenerator, EncodeTypes) together with Aspose.Pdf PDF creation (using Document, Image). Typical for developers who need to embed machine‑readable barcodes into printable documents such as invoices, shipping labels, or product catalogs. The example shows how to configure barcode dimensions, stream the image, and place it on a PDF page.
// Prompt: Generate a GS1 Code 128 barcode, embed it in a PDF document, and save the PDF.
// Tags: gs1, code128, barcode, pdf generation, aspose.barcode, aspose.pdf, image embedding, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Demonstrates generating a GS1 Code 128 barcode, embedding it into a PDF, and saving the result.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, adds it to a PDF, and writes the PDF to disk.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the GS1 data string (Application Identifier 01 for GTIN).
        string codeText = "(01)12345678901231";

        // Initialize the barcode generator with GS1 Code 128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
        {
            // Set the X‑dimension (module width) to 2 pixels for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Render the barcode to a memory stream in PNG format.
            var barcodeStream = new MemoryStream();
            generator.Save(barcodeStream, BarCodeImageFormat.Png);
            barcodeStream.Position = 0; // Reset stream position for reading.

            // Create a new PDF document.
            using (var pdfDoc = new Document())
            {
                // Add a page to the PDF.
                var page = pdfDoc.Pages.Add();

                // Create an Image object that uses the barcode stream.
                var pdfImage = new Image
                {
                    ImageStream = barcodeStream,
                    FixWidth = 200,   // Desired width of the barcode image in the PDF.
                    FixHeight = 100,  // Desired height of the barcode image in the PDF.
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                // Add the image to the page's paragraph collection.
                page.Paragraphs.Add(pdfImage);

                // Determine the output file path in the current working directory.
                string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "GS1Code128.pdf");

                // Save the PDF document to disk.
                pdfDoc.Save(outputPath);
            }

            // Clean up the memory stream.
            barcodeStream.Dispose();
        }

        // Inform the user that the PDF was generated successfully.
        Console.WriteLine("PDF with GS1 Code 128 barcode generated successfully.");
    }
}