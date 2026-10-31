// Title: Generate GS1 Code 128 barcode and embed in PDF
// Description: Demonstrates creating a GS1 Code 128 barcode, converting it to an image, and placing it into a PDF document saved to disk.
// Category-Description: This example belongs to the Aspose.BarCode for .NET barcode generation category, illustrating how to use BarcodeGenerator with EncodeTypes.GS1Code128, customize barcode appearance, and integrate the generated image into an Aspose.Pdf Document. Typical use cases include adding product identifiers to invoices, shipping labels, or reports where a PDF output is required. Developers often need to combine barcode creation with PDF manipulation using Aspose.BarCode and Aspose.Pdf APIs.
// Prompt: Generate a GS1 Code 128 barcode, embed it in a PDF document, and save the PDF.
// Tags: barcode, gs1code128, pdf, aspose.barcode, aspose.pdf, generation, image, embed

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
    /// Entry point of the example. Creates the barcode, adds it to a PDF page, and writes the PDF file.
    /// </summary>
    static void Main()
    {
        // Define the output PDF file path in the current directory.
        string outputPdfPath = Path.Combine(Directory.GetCurrentDirectory(), "GS1Code128.pdf");

        // Initialize the barcode generator for GS1 Code 128 with the required data.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, "(01)12345678901231"))
        {
            // Set barcode visual parameters.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

            // Render the barcode to a memory stream in PNG format.
            using (var barcodeStream = new MemoryStream())
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0; // Reset stream position for reading.

                // Create a new PDF document.
                using (var pdfDoc = new Document())
                {
                    // Add a page to the PDF.
                    var page = pdfDoc.Pages.Add();

                    // Create an image object from the barcode stream.
                    var pdfImage = new Aspose.Pdf.Image
                    {
                        ImageStream = barcodeStream,
                        FixWidth = 200,
                        FixHeight = 200,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };

                    // Add the image to the page's paragraph collection.
                    page.Paragraphs.Add(pdfImage);

                    // Save the PDF document to the specified path.
                    pdfDoc.Save(outputPdfPath);
                }
            }
        }

        // Inform the user where the PDF was saved.
        Console.WriteLine($"PDF saved to: {outputPdfPath}");
    }
}