// Title: Generate and embed a postal barcode into a PDF
// Description: Demonstrates creating a Postnet postal barcode image and placing it at a specific location on a new PDF page.
// Category-Description: This example belongs to the Aspose.BarCode for .NET PDF integration category. It shows how to use BarcodeGenerator (Aspose.BarCode.Generation) to produce a barcode image, retrieve its dimensions, and embed the image into an Aspose.Pdf Document at a defined coordinate. Typical use cases include adding shipping barcodes to invoices, labels, or reports where precise placement on a PDF is required.
// Prompt: Generate a postal barcode and embed it directly into an existing PDF page at a specified coordinate.
// Tags: postnet, postal barcode, pdf embedding, image placement, aspose.barcode, aspose.pdf, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Pdf;

/// <summary>
/// Demonstrates generating a Postnet postal barcode and embedding it into a PDF at a specific position.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates the barcode, calculates its size, and adds it to a PDF document.
    /// </summary>
    static void Main()
    {
        // Define the output PDF file path in the current working directory.
        string outputPdf = Path.Combine(Directory.GetCurrentDirectory(), "PostalBarcodeInPdf.pdf");

        // Postal barcode data (example using Postnet format).
        string postalCode = "1159628792";

        // Initialize the barcode generator for the Postnet symbology with the provided data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, postalCode))
        {
            // Set a high resolution to ensure the generated image is sharp.
            generator.Parameters.Resolution = 300;

            // Optionally adjust the module (bar) size in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 3;

            // Generate the barcode image as a bitmap to obtain its pixel dimensions.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the barcode image to a memory stream in PNG format.
                using (var imageStream = new MemoryStream())
                {
                    generator.Save(imageStream, BarCodeImageFormat.Png);
                    imageStream.Position = 0; // Reset stream position for reading.

                    // Create a new PDF document.
                    using (var pdfDoc = new Document())
                    {
                        // Add a blank page to the document.
                        var page = pdfDoc.Pages.Add();

                        // Convert bitmap dimensions from pixels to PDF points (1 point = 1/72 inch).
                        double widthPoints = (bitmap.Width * 72.0) / generator.Parameters.Resolution;
                        double heightPoints = (bitmap.Height * 72.0) / generator.Parameters.Resolution;

                        // Define the lower‑left corner where the image will be placed (in points).
                        double left = 100;   // X coordinate.
                        double bottom = 200; // Y coordinate.

                        // Create a rectangle that represents the image bounds on the PDF page.
                        var rect = new Aspose.Pdf.Rectangle(left, bottom, left + widthPoints, bottom + heightPoints);

                        // Insert the barcode image into the page at the specified rectangle.
                        page.AddImage(imageStream, rect);

                        // Save the populated PDF to the file system.
                        pdfDoc.Save(outputPdf);
                    }
                }
            }
        }

        // Inform the user where the PDF has been saved.
        Console.WriteLine($"PDF with postal barcode saved to: {outputPdf}");
    }
}