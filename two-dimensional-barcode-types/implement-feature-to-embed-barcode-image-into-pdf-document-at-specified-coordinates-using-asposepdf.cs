// Title: Embed PDF417 Barcode Image into PDF at Specific Coordinates
// Description: Demonstrates generating a PDF417 barcode with Aspose.BarCode, converting it to an image, and embedding it into a PDF document at defined X/Y positions.
// Category-Description: This example belongs to the Aspose.BarCode and Aspose.Pdf integration category, showcasing how to create barcodes (using BarcodeGenerator, EncodeTypes) and place them within PDF files (using Document, Page, Rectangle). Typical use cases include adding scannable codes to reports, invoices, or tickets where precise positioning is required. Developers often need to combine barcode generation with PDF layout manipulation, making this pattern a common reference for such tasks.
// Prompt: Implement feature to embed barcode image into PDF document at specified coordinates using Aspose.PDF
// Tags: pdf417, barcode, embed, pdf, aspose.barcode, aspose.pdf, image, coordinates

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Example program that generates a PDF417 barcode and embeds it into a PDF document at specified coordinates.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, calculates placement, and saves the resulting PDF.
    /// </summary>
    static void Main()
    {
        // Define sample parameters
        int resolution = 300;               // DPI for the barcode image
        int leftPosition = 50;              // Horizontal offset from the left edge (points)
        int topPosition = 100;              // Vertical offset from the top edge (points)
        string outputPdfPath = Path.Combine(Directory.GetCurrentDirectory(), "BarcodeEmbedded.pdf");

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(outputPdfPath);
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Generate the barcode and embed it into a new PDF document
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Aspose.Barcode Example"))
        {
            // Set the image resolution for accurate size calculations
            generator.Parameters.Resolution = resolution;

            // Create a bitmap representation of the barcode
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
                        var page = pdfDoc.Pages.Add();

                        // Convert bitmap dimensions from pixels to PDF points (1 point = 1/72 inch)
                        double widthPoints = (bitmap.Width * 72.0) / resolution;
                        double heightPoints = (bitmap.Height * 72.0) / resolution;

                        // Calculate rectangle coordinates for image placement
                        double llx = leftPosition;                                 // lower-left X
                        double urx = leftPosition + widthPoints;                  // upper-right X
                        double ury = page.PageInfo.Height - topPosition;          // upper-right Y (from top)
                        double lly = ury - heightPoints;                          // lower-left Y

                        var pdfRect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                        // Add the barcode image to the page at the calculated rectangle
                        page.AddImage(imageStream, pdfRect);

                        // Save the PDF to the specified path
                        pdfDoc.Save(outputPdfPath);
                    }
                }
            }
        }

        Console.WriteLine($"PDF with embedded barcode saved to: {outputPdfPath}");
    }
}