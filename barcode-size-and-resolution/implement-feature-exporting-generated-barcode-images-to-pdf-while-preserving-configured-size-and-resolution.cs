// Title: Export Barcode Image to PDF with Preserved Size and Resolution
// Description: Generates a Code128 barcode, configures its dimensions and DPI, and embeds the image into a PDF while maintaining the exact size.
// Category-Description: This example belongs to the Aspose.BarCode generation and Aspose.Pdf export category. It demonstrates how to use BarcodeGenerator (Aspose.BarCode.Generation) to create a barcode, adjust its AutoSizeMode, image dimensions, and resolution, then embed the resulting image into a PDF document (Aspose.Pdf) using Image objects. Developers often need to produce printable barcodes with precise sizing for labels, invoices, or reports, and this pattern shows the typical workflow for such scenarios.
// Prompt: Implement feature exporting generated barcode images to PDF while preserving configured size and resolution.
// Tags: code128, barcode generation, pdf export, image size, resolution, aspose.barcode, aspose.pdf

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Demonstrates exporting a generated barcode image to a PDF file while preserving the configured size and resolution.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, configures its dimensions and DPI, and saves it inside a PDF.
    /// </summary>
    static void Main()
    {
        // Define the output PDF file name.
        const string pdfPath = "barcode.pdf";

        // Create a barcode generator for Code128 with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Preserve size and resolution.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Point = 300f;   // Width in points (1 point = 1/72 inch).
            generator.Parameters.ImageHeight.Point = 150f; // Height in points.
            generator.Parameters.Resolution = 300;         // DPI (dots per inch).

            // Save the barcode image to a memory stream in PNG format.
            using (var imageStream = new MemoryStream())
            {
                generator.Save(imageStream, BarCodeImageFormat.Png);
                imageStream.Position = 0; // Reset stream position for reading.

                // Create a PDF document and add the barcode image.
                using (var pdfDocument = new Document())
                {
                    // Add a new page to the PDF.
                    var page = pdfDocument.Pages.Add();

                    // Create an Aspose.Pdf.Image object that reads from the memory stream.
                    var pdfImage = new Aspose.Pdf.Image
                    {
                        ImageStream = imageStream,
                        // Set image size to match the barcode dimensions.
                        FixWidth = generator.Parameters.ImageWidth.Point,
                        FixHeight = generator.Parameters.ImageHeight.Point
                    };

                    // Add the image to the page's paragraph collection.
                    page.Paragraphs.Add(pdfImage);

                    // Save the PDF document to the specified path.
                    pdfDocument.Save(pdfPath);
                }
            }
        }

        // Output the full path of the generated PDF for verification.
        Console.WriteLine($"Barcode exported to PDF: {Path.GetFullPath(pdfPath)}");
    }
}