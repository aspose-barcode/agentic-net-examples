// Title: Generate GS1 Code 128 Barcode and Embed in PDF
// Description: Demonstrates how to create a GS1 Code 128 barcode, place it into a PDF document, and save the result.
// Category-Description: This example belongs to the Aspose.BarCode PDF integration category, showcasing the use of BarcodeGenerator (Aspose.BarCode.Generation) to produce a barcode image and Aspose.Pdf (Document, Image) to embed that image into a PDF. Typical scenarios include generating product labels, shipping documents, or any printable material that requires a GS1-compliant barcode.
// Prompt: Generate a GS1 Code 128 barcode, embed it in a PDF document, and save the PDF.
// Tags: gs1 code 128, barcode generation, pdf output, aspose.barcode, aspose.pdf

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Example program that creates a GS1 Code 128 barcode, inserts it into a PDF, and saves the file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output PDF file name
        const string pdfPath = "GS1Code128.pdf";

        // Initialize a barcode generator for GS1 Code 128 with sample AI code text
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, "(01)12345678901231"))
        {
            // Render the barcode to a memory stream in PNG format
            using (var barcodeStream = new MemoryStream())
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                // Reset stream position to the beginning for reading
                barcodeStream.Position = 0;

                // Create a new PDF document
                using (var pdfDoc = new Document())
                {
                    // Add a new page to the PDF
                    var page = pdfDoc.Pages.Add();

                    // Create an image object from the barcode stream
                    var image = new Image
                    {
                        ImageStream = barcodeStream
                    };

                    // Insert the barcode image into the page's paragraph collection
                    page.Paragraphs.Add(image);

                    // Save the PDF document to the specified path
                    pdfDoc.Save(pdfPath);
                }
            }
        }

        // Inform the user that the PDF has been saved
        Console.WriteLine($"PDF with GS1 Code 128 barcode saved to '{pdfPath}'.");
    }
}