// Title: Export Barcode Image to PDF with Preserved Size and Resolution
// Description: Demonstrates generating a Code128 barcode, configuring its dimensions and DPI, and exporting it as a PNG embedded in a PDF while keeping the specified size.
// Category-Description: This example belongs to the Aspose.BarCode image generation and PDF integration category. It shows how to use BarcodeGenerator to set image size and resolution, save the barcode to a stream, and embed it into an Aspose.Pdf Document. Developers often need to create barcodes for reports, invoices, or shipping labels and export them to PDF with exact dimensions.
// Prompt: Implement feature exporting generated barcode images to PDF while preserving configured size and resolution.
// Tags: barcode, code128, pdf, image export, size, resolution, aspose.barcode, aspose.pdf

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Demonstrates generating a barcode, configuring its size and resolution,
/// and exporting it to a PDF document while preserving those settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, embeds it in a PDF,
    /// and saves the result to the output folder.
    /// </summary>
    static void Main()
    {
        // Prepare the output directory where the PDF will be saved.
        string outputDir = "output";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Define the full path for the resulting PDF file.
        string pdfPath = Path.Combine(outputDir, "barcode.pdf");

        // Create a barcode generator for Code128 symbology with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the barcode image dimensions in points (1 point = 1/72 inch).
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Set the image resolution in DPI to ensure high-quality rendering.
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.

                // Create a new PDF document and add a page to host the barcode image.
                using (var pdfDoc = new Document())
                {
                    var page = pdfDoc.Pages.Add();

                    // Create an Aspose.Pdf.Image object linked to the barcode stream.
                    var pdfImage = new Aspose.Pdf.Image
                    {
                        ImageStream = ms,
                        // Preserve the configured width and height in the PDF.
                        FixWidth = generator.Parameters.ImageWidth.Point,
                        FixHeight = generator.Parameters.ImageHeight.Point
                    };

                    // Add the image to the page's paragraph collection.
                    page.Paragraphs.Add(pdfImage);

                    // Save the PDF document to the specified path.
                    pdfDoc.Save(pdfPath);
                }
            }
        }

        // Inform the user where the PDF has been saved.
        Console.WriteLine($"Barcode PDF saved to: {pdfPath}");
    }
}