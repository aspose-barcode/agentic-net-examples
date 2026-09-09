// Title: Embed Code128 barcode into PDF at specific coordinates
// Description: This example generates a Code128 barcode image and embeds it into a PDF document at defined X/Y positions. It demonstrates converting the barcode to an image stream and placing it precisely on a PDF page.
// Category-Description: Shows how to work with Aspose.BarCode and Aspose.Pdf to create barcodes, convert them to images, and insert them into PDF files. Typical use cases include adding product codes, shipping labels, or QR codes to generated PDFs. Developers often need to control barcode resolution, colors, and placement using BarcodeGenerator, image handling, and PDF page graphics APIs.
// Prompt: Implement feature to embed barcode image into PDF document at specified coordinates using Aspose.PDF
// Tags: code128, barcode, embed, pdf, aspose.barcode, aspose.pdf, image, coordinates, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Demonstrates embedding a Code128 barcode image into a PDF document at specified coordinates using Aspose.BarCode and Aspose.Pdf.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a barcode, converts it to a PNG stream, and places it on a new PDF page at the given position.
    /// </summary>
    static void Main()
    {
        // Output PDF file name
        string outputPdf = "BarcodeEmbedded.pdf";

        // Barcode configuration
        string codeText = "1234567890";
        int resolution = 300; // DPI
        float leftPosition = 50f;   // points from left edge
        float topPosition = 700f;   // points from bottom edge

        // Create a barcode generator for Code128
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set barcode image resolution and colors
            generator.Parameters.Resolution = resolution;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save barcode as PNG into a memory stream
            using (var imageStream = new MemoryStream())
            {
                generator.Save(imageStream, BarCodeImageFormat.Png);
                imageStream.Position = 0;

                // Determine image dimensions in pixels
                int imgWidth;
                int imgHeight;
                using (var bitmap = new Aspose.Drawing.Bitmap(imageStream))
                {
                    imgWidth = bitmap.Width;
                    imgHeight = bitmap.Height;
                }
                imageStream.Position = 0; // Reset stream for PDF insertion

                // Create a new PDF document and add a page
                using (var pdfDoc = new Document())
                {
                    var page = pdfDoc.Pages.Add();

                    // Convert pixel dimensions to PDF points (1 point = 1/72 inch)
                    double widthPoints = (imgWidth * 72.0) / resolution;
                    double heightPoints = (imgHeight * 72.0) / resolution;

                    // Calculate rectangle coordinates for image placement
                    double llx = leftPosition;                     // lower-left X
                    double lly = topPosition - heightPoints;       // lower-left Y
                    double urx = leftPosition + widthPoints;       // upper-right X
                    double ury = topPosition;                      // upper-right Y

                    var rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                    // Add the barcode image to the PDF page within the defined rectangle
                    page.AddImage(imageStream, rect);

                    // Save the resulting PDF file
                    pdfDoc.Save(outputPdf);
                }
            }
        }

        Console.WriteLine($"PDF saved to {Path.GetFullPath(outputPdf)}");
    }
}