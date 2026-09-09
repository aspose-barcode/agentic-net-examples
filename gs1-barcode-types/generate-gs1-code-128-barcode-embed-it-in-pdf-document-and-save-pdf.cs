// Title: Generate GS1 Code 128 barcode and embed in PDF
// Description: Demonstrates creating a GS1 Code 128 barcode, converting it to an image, and placing it into a PDF document saved to disk.
// Category-Description: This example belongs to the Aspose.BarCode for .NET barcode generation category, illustrating how to use BarcodeGenerator with EncodeTypes.GS1Code128, customize dimensions, and integrate the generated image into an Aspose.Pdf Document. Developers often need to embed barcodes into PDFs for labeling, shipping, or inventory applications, and this snippet shows the typical workflow using Aspose.BarCode and Aspose.Pdf APIs.
// Prompt: Generate a GS1 Code 128 barcode, embed it in a PDF document, and save the PDF.
// Tags: barcode, gs1code128, pdf, aspose.barcode, aspose.pdf, generation, embedding

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Example program that creates a GS1 Code 128 barcode, embeds it into a PDF, and saves the file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, adds it to a PDF page, and writes the PDF to the current directory.
    /// </summary>
    static void Main()
    {
        // Define the output PDF file path in the current working directory.
        string outputPdfPath = Path.Combine(Directory.GetCurrentDirectory(), "GS1Code128.pdf");

        // Initialize the barcode generator with GS1 Code 128 symbology and the required data string.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, "(02)04006664241007(37)1"))
        {
            // Set the X-dimension (module width) of the barcode in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Create a memory stream to hold the generated barcode image.
            using (var ms = new MemoryStream())
            {
                // Save the barcode as a PNG image into the memory stream.
                generator.Save(ms, BarCodeImageFormat.Png);
                // Reset the stream position to the beginning for subsequent reading.
                ms.Position = 0;

                // Create a new PDF document.
                using (var pdfDoc = new Document())
                {
                    // Add a new page to the PDF.
                    var page = pdfDoc.Pages.Add();

                    // Create an Image object from the barcode stream and configure its size and alignment.
                    var pdfImage = new Image
                    {
                        ImageStream = ms,
                        FixWidth = 200.0,
                        FixHeight = 200.0,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };

                    // Add the image to the page's paragraph collection.
                    page.Paragraphs.Add(pdfImage);
                    // Save the PDF document to the specified file path.
                    pdfDoc.Save(outputPdfPath);
                }
            }
        }

        // Output the location of the saved PDF file.
        Console.WriteLine($"PDF saved to {outputPdfPath}");
    }
}