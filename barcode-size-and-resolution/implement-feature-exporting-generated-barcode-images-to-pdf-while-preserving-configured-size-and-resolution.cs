// Title: Export Barcode Image to PDF with Preserved Size and Resolution
// Description: Demonstrates generating a PDF417 barcode, setting its resolution, and embedding the resulting image into a PDF while keeping the original dimensions.
// Category-Description: This example belongs to the Aspose.BarCode image export category, illustrating how to combine Aspose.BarCode and Aspose.Pdf APIs. It shows creating a barcode, configuring resolution, converting it to an image stream, and placing it on a PDF page with accurate size calculations. Developers working on document generation, reporting, or printing often need to embed high‑resolution barcodes into PDFs while preserving physical dimensions.
// Prompt: Implement feature exporting generated barcode images to PDF while preserving configured size and resolution.
// Tags: pdf417, barcode generation, image export, pdf export, resolution, aspose.barcode, aspose.pdf

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Example program that creates a PDF417 barcode, sets its resolution,
/// and embeds the barcode image into a PDF document while preserving size and DPI.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, writes it to a PDF, and reports the output path.
    /// </summary>
    static void Main()
    {
        // Define the output PDF file path in the current working directory.
        string outputPdf = Path.Combine(Directory.GetCurrentDirectory(), "BarcodeOutput.pdf");

        // Desired image resolution in dots per inch (DPI). Higher DPI yields sharper images.
        int resolution = 300;

        // Initialize a barcode generator for the PDF417 symbology with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Aspose.Barcode Example"))
        {
            // Apply the configured resolution to the generated image.
            generator.Parameters.Resolution = resolution;

            // Generate a bitmap to obtain the exact pixel dimensions of the barcode.
            Aspose.Drawing.Bitmap bitmap = generator.GenerateBarCodeImage();

            // Save the barcode image to a memory stream in PNG format (lossless).
            using (var imageStream = new MemoryStream())
            {
                generator.Save(imageStream, BarCodeImageFormat.Png);
                imageStream.Position = 0; // Reset stream position for reading.

                // Create a new PDF document and add a blank page.
                using (var pdfDoc = new Document())
                {
                    var pdfPage = pdfDoc.Pages.Add();

                    // Convert bitmap dimensions from pixels to PDF points (1 point = 1/72 inch).
                    double widthPoints = (bitmap.Width * 72.0) / resolution;
                    double heightPoints = (bitmap.Height * 72.0) / resolution;

                    // Define placement coordinates (in points) relative to the page's lower‑left corner.
                    double leftPos = 10; // Distance from the left edge.
                    double topPos = 20;  // Distance from the top edge.

                    // Calculate rectangle bounds for the image within the PDF page.
                    double left = leftPos;
                    double top = topPos;
                    double right = left + widthPoints;
                    double bottom = top + heightPoints;

                    // Aspose.Pdf uses a coordinate system where Y=0 is at the bottom,
                    // so we invert the Y values to position the image correctly.
                    var pdfRect = new Aspose.Pdf.Rectangle(
                        left,
                        pdfPage.Rect.Height - bottom,
                        right,
                        pdfPage.Rect.Height - top);

                    // Insert the barcode image into the PDF page using the calculated rectangle.
                    pdfPage.AddImage(imageStream, pdfRect);

                    // Persist the PDF document to the specified file.
                    pdfDoc.Save(outputPdf);
                }
            }

            // Release the bitmap resources.
            bitmap.Dispose();
        }

        // Inform the user where the PDF was saved.
        Console.WriteLine($"PDF saved to: {outputPdf}");
    }
}