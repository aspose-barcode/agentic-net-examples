// Title: Generate Postal Barcode and Embed in PDF
// Description: Demonstrates creating a POSTNET barcode and placing it at a specific location inside a new PDF document.
// Category-Description: This example belongs to the Aspose.BarCode for .NET PDF integration category. It shows how to use BarcodeGenerator (Aspose.BarCode.Generation) to produce a barcode image, convert it to a stream, and then embed the image into an Aspose.Pdf Document at precise coordinates. Developers working with shipping labels, postal services, or any scenario that requires barcode graphics inside PDF files can follow this pattern to generate and position barcodes programmatically.
// Prompt: Generate a postal barcode and embed it directly into an existing PDF page at a specified coordinate.
// Tags: postnet, barcode generation, pdf embedding, aspnet, aspose.barcode, aspose.pdf, image conversion

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Example program that creates a POSTNET barcode and embeds it into a PDF at a given coordinate.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, converts it to a PNG stream,
    /// calculates its size in PDF points, and places it on a new PDF page.
    /// </summary>
    static void Main()
    {
        // Define barcode generation settings
        int resolution = 300;                 // DPI for the barcode image
        string barcodeText = "1159628792";    // Data to encode in the POSTNET barcode
        int leftPos = 100;                    // X coordinate (points) from the left edge of the page
        int bottomPos = 200;                  // Y coordinate (points) from the bottom edge of the page

        // Create a barcode generator for POSTNET symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, barcodeText))
        {
            // Set image resolution and X-dimension (module width) in pixels
            generator.Parameters.Resolution = resolution;
            generator.Parameters.Barcode.XDimension.Pixels = 3f;

            // Generate the barcode as a bitmap
            using (Aspose.Drawing.Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the bitmap to a memory stream in PNG format
                using (var imageStream = new MemoryStream())
                {
                    generator.Save(imageStream, BarCodeImageFormat.Png);
                    imageStream.Position = 0; // Reset stream position for reading

                    // Convert bitmap dimensions from pixels to PDF points (1 point = 1/72 inch)
                    double widthPoints = (bitmap.Width * 72.0) / resolution;
                    double heightPoints = (bitmap.Height * 72.0) / resolution;

                    // Create a new PDF document and add a page
                    using (var pdfDoc = new Document())
                    {
                        var page = pdfDoc.Pages.Add();

                        // Define the rectangle where the barcode image will be placed
                        var pdfRect = new Aspose.Pdf.Rectangle(
                            leftPos,
                            page.Rect.Height - (bottomPos + heightPoints), // Top coordinate
                            leftPos + widthPoints,
                            page.Rect.Height - bottomPos);                // Bottom coordinate

                        // Embed the barcode image into the PDF page at the specified rectangle
                        page.AddImage(imageStream, pdfRect);

                        // Save the resulting PDF to the current directory
                        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "PostalBarcode.pdf");
                        pdfDoc.Save(outputPath);
                        Console.WriteLine($"PDF saved to {outputPath}");
                    }
                }
            }
        }
    }
}