// Title: Export Barcode Image to PDF with Preserved Size and Resolution
// Description: Demonstrates generating a PDF417 barcode, configuring its resolution, and exporting the resulting image to a PDF while keeping the original dimensions.
// Category-Description: This example belongs to the Aspose.BarCode image generation and Aspose.Pdf document creation category. It shows how to use BarcodeGenerator, set resolution, render the barcode to a bitmap, and embed the image into a PDF using Aspose.Pdf's Document and Page classes. Developers often need to embed barcodes in PDFs with exact sizing for printing or scanning purposes.
// Prompt: Implement feature exporting generated barcode images to PDF while preserving configured size and resolution.
// Tags: pdf417, barcode generation, image export, pdf creation, resolution, aspose.barcode, aspose.pdf

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Generates a PDF417 barcode, converts it to an image, and embeds the image into a PDF document
/// while preserving the configured size and resolution.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, saves it as PNG in memory,
    /// calculates size in PDF points, and writes the final PDF file.
    /// </summary>
    static void Main()
    {
        // Define barcode generation parameters
        int resolution = 300;               // DPI for the generated image
        int leftPosition = 10;              // Left margin in PDF points
        int topPosition = 20;               // Top margin in PDF points
        string barcodeText = "Aspose.Barcode Example";
        string outputPdfPath = Path.Combine(Directory.GetCurrentDirectory(), "BarcodeOutput.pdf");

        // Initialize the barcode generator for PDF417 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, barcodeText))
        {
            // Apply the desired resolution to the generator
            generator.Parameters.Resolution = resolution;

            // Generate the barcode image as a bitmap
            using (Aspose.Drawing.Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the bitmap to a memory stream in PNG format
                using (var imageStream = new MemoryStream())
                {
                    generator.Save(imageStream, BarCodeImageFormat.Png);
                    imageStream.Position = 0; // Reset stream position for reading

                    // Create a new PDF document
                    using (var pdfDoc = new Document())
                    {
                        // Add a blank page to the document
                        var pdfPage = pdfDoc.Pages.Add();

                        // Convert bitmap dimensions from pixels to PDF points (1 point = 1/72 inch)
                        double imgWidthPoints = (bitmap.Width * 72.0) / resolution;
                        double imgHeightPoints = (bitmap.Height * 72.0) / resolution;

                        // Calculate rectangle coordinates for image placement
                        double left = leftPosition;
                        double bottom = topPosition;
                        double right = leftPosition + imgWidthPoints;
                        double top = topPosition + imgHeightPoints;

                        // Create a rectangle that respects PDF coordinate system (origin at bottom‑left)
                        var pdfRect = new Aspose.Pdf.Rectangle(
                            left,
                            pdfPage.Rect.Height - top,
                            right,
                            pdfPage.Rect.Height - bottom);

                        // Embed the image stream into the PDF page using the calculated rectangle
                        pdfPage.AddImage(imageStream, pdfRect);

                        // Save the final PDF file to disk
                        pdfDoc.Save(outputPdfPath);
                    }
                }
            }
        }

        // Inform the user where the PDF was saved
        Console.WriteLine($"PDF saved to {outputPdfPath}");
    }
}