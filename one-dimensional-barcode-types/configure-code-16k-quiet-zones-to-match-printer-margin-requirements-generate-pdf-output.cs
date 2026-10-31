// Title: Generate Code 16K barcode with custom quiet zones and embed in PDF
// Description: Demonstrates configuring quiet zone coefficients for a Code 16K barcode, rendering it as PNG, and embedding the image into a PDF document.
// Category-Description: This example belongs to the Aspose.BarCode PDF integration category, illustrating how to use BarcodeGenerator (Aspose.BarCode.Generation) to customize barcode parameters and combine the output with Aspose.Pdf to create PDF files. Typical use cases include generating barcodes that meet printer margin requirements and embedding them in reports or invoices. Developers often need to adjust quiet zones, image dimensions, and combine barcode images with other document content.
// Prompt: Configure Code 16K quiet zones to match printer margin requirements, generate PDF output.
// Tags: barcode, code16k, quietzone, pdf, aspose.barcode, aspose.pdf, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Demonstrates generating a Code 16K barcode with custom quiet zones and embedding it into a PDF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the output directory, generates the barcode, embeds it in a PDF, and saves the result.
    /// </summary>
    static void Main()
    {
        // Define output folder and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting PDF file
        string pdfPath = Path.Combine(outputDir, "Code16K_QuietZone.pdf");

        // Initialize barcode generator for Code 16K symbology with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, "Aspose.Barcode"))
        {
            // Set X-dimension (module width) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Configure quiet zone coefficients to control left and right margins
            generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = 10;
            generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = 10;

            // Render barcode to an in‑memory PNG image
            using (var imageStream = new MemoryStream())
            {
                generator.Save(imageStream, BarCodeImageFormat.Png);
                imageStream.Position = 0; // Reset stream position for reading

                // Create a new PDF document and add a page
                using (var pdfDoc = new Document())
                {
                    var page = pdfDoc.Pages.Add();

                    // Create an Aspose.Pdf Image object from the barcode stream
                    var pdfImage = new Image
                    {
                        ImageStream = imageStream
                        // Optional: set width/height if needed, e.g., FixWidth = 200
                    };

                    // Add the image to the page's paragraph collection
                    page.Paragraphs.Add(pdfImage);

                    // Save the PDF to the specified path
                    pdfDoc.Save(pdfPath);
                }
            }
        }

        // Inform the user where the PDF was saved
        Console.WriteLine($"PDF generated at: {pdfPath}");
    }
}