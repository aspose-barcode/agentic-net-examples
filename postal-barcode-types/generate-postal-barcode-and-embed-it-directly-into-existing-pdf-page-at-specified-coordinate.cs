// Title: Generate and Embed a Postnet Barcode into a PDF
// Description: Demonstrates creating a Postnet postal barcode image and placing it at a specific location on a PDF page using Aspose.BarCode and Aspose.Pdf.
// Category-Description: This example belongs to the Aspose.BarCode PDF integration category, showing how to generate barcode images (e.g., Postnet, QR, Code128) and embed them into existing PDF documents. It highlights key classes such as BarcodeGenerator, BarCodeImageFormat, Document, Page, and Rectangle, which developers commonly use for automated document processing, labeling, and shipping workflows.
// Prompt: Generate a postal barcode and embed it directly into an existing PDF page at a specified coordinate.
// Tags: postnet, barcode, pdf, image, aspose.barcode, aspose.pdf

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Example program that creates a Postnet barcode and embeds it into a PDF document at a defined position.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode, calculates its size, and inserts it into a PDF page.
    /// </summary>
    static void Main()
    {
        const string outputPdf = "PostalBarcode.pdf";
        const int resolution = 300;               // DPI used for barcode image generation
        const float leftPosition = 100f;          // Horizontal offset from the left edge (points)
        const float topPosition = 200f;           // Vertical offset from the bottom edge (points)

        // Create a new PDF document with a single blank page
        using (var pdfDoc = new Document())
        {
            var page = pdfDoc.Pages.Add();

            // Generate a Postnet barcode image and store it in a memory stream
            using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, "1159628792"))
            {
                generator.Parameters.Resolution = resolution;
                generator.Parameters.Barcode.XDimension.Pixels = 3f;

                using (var bitmap = generator.GenerateBarCodeImage())
                {
                    var imageStream = new MemoryStream();
                    generator.Save(imageStream, BarCodeImageFormat.Png);
                    imageStream.Position = 0; // Reset stream position for reading

                    // Convert bitmap dimensions from pixels to PDF points (1 point = 1/72 inch)
                    float widthPoints = (float)(bitmap.Width * 72) / resolution;
                    float heightPoints = (float)(bitmap.Height * 72) / resolution;

                    // Define the rectangle where the image will be placed (lower‑left to upper‑right)
                    var pdfRect = new Rectangle(
                        leftPosition,
                        page.Rect.Height - (topPosition + heightPoints), // lower‑left Y coordinate
                        leftPosition + widthPoints,
                        page.Rect.Height - topPosition);                // upper‑right Y coordinate

                    // Insert the barcode image into the PDF page at the calculated rectangle
                    page.AddImage(imageStream, pdfRect);

                    // Save the final PDF document to disk
                    pdfDoc.Save(outputPdf);

                    imageStream.Dispose();
                }
            }
        }

        Console.WriteLine("PDF created: " + Path.GetFullPath(outputPdf));
    }
}