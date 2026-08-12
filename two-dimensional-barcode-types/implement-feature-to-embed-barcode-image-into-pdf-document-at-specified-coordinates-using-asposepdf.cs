// Title: Embed Code128 Barcode into PDF at Specified Coordinates
// Description: Demonstrates generating a Code128 barcode with Aspose.BarCode, converting it to an image, and placing it at defined coordinates in a PDF using Aspose.Pdf.
// Category-Description: This example belongs to the Aspose.BarCode and Aspose.Pdf integration category, showing how to combine barcode generation with PDF document creation. It highlights key API classes such as BarcodeGenerator, BarCodeImageFormat, Document, Page, and Rectangle. Developers often need to embed barcodes into reports, invoices, or shipping labels, and this pattern illustrates the typical workflow for generating a barcode image in memory and positioning it precisely within a PDF page.
// Prompt: Implement feature to embed barcode image into PDF document at specified coordinates using Aspose.PDF
// Tags: barcode, code128, embed, pdf, aspose.barcode, aspose.pdf, image, coordinates

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Demonstrates embedding a generated Code128 barcode image into a PDF document at specific coordinates.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a barcode, inserts it into a PDF, and saves the result.
    /// </summary>
    static void Main()
    {
        // Define the output PDF file path.
        string outputPdfPath = "BarcodeDocument.pdf";

        // Initialize a barcode generator for the Code128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the text to be encoded in the barcode.
            generator.CodeText = "1234567890";

            // Render the barcode to a memory stream in PNG format.
            using (var barcodeStream = new MemoryStream())
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0; // Reset stream position for subsequent reading.

                // Create a new PDF document.
                using (var pdfDocument = new Document())
                {
                    // Add a single page to the PDF.
                    var page = pdfDocument.Pages.Add();

                    // Define the rectangle (lower-left x, lower-left y, upper-right x, upper-right y)
                    // where the barcode image will be placed on the page.
                    var barcodeRect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

                    // Insert the barcode image into the page at the specified rectangle.
                    page.AddImage(barcodeStream, barcodeRect);

                    // Save the populated PDF document to disk.
                    pdfDocument.Save(outputPdfPath);
                }
            }
        }

        // Output the full path of the generated PDF for verification.
        Console.WriteLine($"PDF with embedded barcode saved to: {Path.GetFullPath(outputPdfPath)}");
    }
}