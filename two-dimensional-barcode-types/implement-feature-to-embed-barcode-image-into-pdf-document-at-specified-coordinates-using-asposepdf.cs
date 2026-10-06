// Title: Embed Barcode Image into PDF at Specific Coordinates
// Description: Demonstrates generating a Code128 barcode with Aspose.BarCode, converting it to PNG, and placing it at defined X/Y positions inside a PDF using Aspose.Pdf.
// Category-Description: This example belongs to the Aspose.BarCode and Aspose.Pdf integration category, showcasing how to generate barcodes (using BarcodeGenerator, EncodeTypes) and embed them into PDF documents (using Document, Page, Rectangle). Typical use cases include adding product identifiers, shipping labels, or QR codes to generated PDFs. Developers often need to control image resolution and exact placement coordinates when combining barcode generation with PDF creation.
// Prompt: Implement feature to embed barcode image into PDF document at specified coordinates using Aspose.PDF
// Tags: barcode, code128, embed, pdf, aspose.barcode, aspose.pdf, image, coordinates, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Example program that creates a Code128 barcode and embeds it into a PDF document at specified coordinates.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, converts it to an image, and inserts it into a new PDF file.
    /// </summary>
    static void Main()
    {
        // Sample parameters
        string barcodeText = "1234567890";
        string outputPdfPath = "BarcodeOutput.pdf";
        int resolution = 300; // DPI
        double leftPosition = 10.0; // points from left edge
        double topPosition = 20.0;  // points from top edge

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPdfPath));
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Generate the barcode and embed it into a PDF
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, barcodeText))
        {
            // Set image resolution (dots per inch)
            generator.Parameters.Resolution = resolution;

            // Generate barcode image as a bitmap
            using (var bitmap = generator.GenerateBarCodeImage())
            {
                // Save bitmap to a memory stream in PNG format
                using (var imageStream = new MemoryStream())
                {
                    generator.Save(imageStream, BarCodeImageFormat.Png);
                    imageStream.Position = 0; // Reset stream position for reading

                    // Create a new PDF document
                    using (var pdfDoc = new Document())
                    {
                        // Add a blank page to the document
                        var page = pdfDoc.Pages.Add();

                        // Convert image size from pixels to PDF points (1 inch = 72 points)
                        double imgWidthPt = (bitmap.Width * 72.0) / resolution;
                        double imgHeightPt = (bitmap.Height * 72.0) / resolution;

                        // PDF coordinate system origin is at the lower-left corner
                        double llx = leftPosition; // lower-left X
                        double lly = page.PageInfo.Height - (topPosition + imgHeightPt); // lower-left Y
                        double urx = leftPosition + imgWidthPt; // upper-right X
                        double ury = page.PageInfo.Height - topPosition; // upper-right Y

                        // Define rectangle where the image will be placed
                        var pdfRect = new Rectangle(llx, lly, urx, ury);

                        // Add the barcode image to the page within the defined rectangle
                        page.AddImage(imageStream, pdfRect);

                        // Save the PDF to the specified path
                        pdfDoc.Save(outputPdfPath);
                    }
                }
            }
        }

        Console.WriteLine($"Barcode embedded into PDF: {outputPdfPath}");
    }
}