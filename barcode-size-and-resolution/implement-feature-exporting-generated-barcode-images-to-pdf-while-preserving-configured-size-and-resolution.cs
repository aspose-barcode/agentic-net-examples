// Title: Export Barcode Image to PDF with Preserved Size and Resolution
// Description: Generates a Code128 barcode, configures its resolution, converts it to a PNG image, and embeds the image into a PDF while maintaining the original physical dimensions.
// Category-Description: This example demonstrates how to use Aspose.BarCode to create barcodes and Aspose.Pdf to embed generated images into PDF documents. It covers setting barcode resolution, exporting the image to a stream, calculating size in points, and adding the image to a PDF page. Developers working with barcode generation and PDF reporting often need to preserve exact sizing for print‑ready outputs.
// Prompt: Implement feature exporting generated barcode images to PDF while preserving configured size and resolution.
// Tags: barcode symbology, generation, export, pdf, image, resolution, aspose.barcode, aspose.pdf

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Demonstrates exporting a generated barcode image to a PDF while preserving its configured size and resolution.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, saves it as an image, and embeds it into a PDF.
    /// </summary>
    static void Main()
    {
        // Define barcode generation parameters.
        const int resolution = 300;               // Desired DPI for the barcode image.
        const string codeText = "1234567890";     // Text to encode in the barcode.
        const string outputPdf = "BarcodeOutput.pdf"; // Name of the resulting PDF file.

        // Create a barcode generator for Code128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Apply the resolution setting to the generator.
            generator.Parameters.Resolution = resolution;

            // Generate the barcode image as a bitmap.
            using (Aspose.Drawing.Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Prepare a memory stream to hold the PNG representation.
                using (var imageStream = new MemoryStream())
                {
                    // Save the bitmap to the stream in PNG format.
                    generator.Save(imageStream, BarCodeImageFormat.Png);
                    imageStream.Position = 0; // Reset stream position for reading.

                    // Convert bitmap dimensions from pixels to PDF points (1 point = 1/72 inch).
                    double widthPoints = (bitmap.Width * 72.0) / resolution;
                    double heightPoints = (bitmap.Height * 72.0) / resolution;

                    // Create a new PDF document.
                    using (var pdfDoc = new Document())
                    {
                        // Add a page to the document.
                        var page = pdfDoc.Pages.Add();

                        // Define the rectangle where the image will be placed, preserving size.
                        var pdfRect = new Rectangle(0, 0, widthPoints, heightPoints);

                        // Embed the PNG image into the PDF page.
                        page.AddImage(imageStream, pdfRect);

                        // Determine the full output path.
                        string outPath = Path.Combine(Environment.CurrentDirectory, outputPdf);

                        // Save the PDF document to disk.
                        pdfDoc.Save(outPath);

                        // Inform the user of the successful operation.
                        Console.WriteLine($"PDF saved to {outPath}");
                    }
                }
            }
        }
    }
}