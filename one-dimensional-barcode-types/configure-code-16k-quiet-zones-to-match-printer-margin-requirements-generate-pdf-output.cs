// Title: Generate Code 16K Barcode with Custom Quiet Zones and Export to PDF
// Description: Demonstrates how to configure quiet zone coefficients for a Code 16K barcode, render it as PNG, and embed the image into a PDF document using Aspose.BarCode and Aspose.Pdf.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize barcode parameters (quiet zones, colors, dimensions) and combine the output with Aspose.Pdf for document creation. Key API classes include BarcodeGenerator, EncodeTypes, BarCodeImageFormat, and Aspose.Pdf.Document. Typical use cases involve preparing barcodes that meet specific printer margin requirements and packaging them into PDF reports or labels.
// Prompt: Configure Code 16K quiet zones to match printer margin requirements, generate PDF output.
// Tags: code16k, quiet zones, barcode generation, pdf output, aspose.barcode, aspose.pdf, c#

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Pdf;

/// <summary>
/// Example program that creates a Code 16K barcode with custom quiet zones,
/// saves it as a PNG image, and embeds the image into a PDF document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output PDF file path.
        string pdfPath = "Code16K.pdf";

        // Create a Code16K barcode generator with a sample numeric value.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, "1234567890123456"))
        {
            // Set foreground (barcode) and background colors.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Configure quiet zones to satisfy printer margin requirements.
            // Minimum allowed values are 10 (left) and 1 (right); increase as needed.
            generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = 12;
            generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = 2;

            // Optional: adjust the module size (X dimension) for better visibility.
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Render the barcode to a memory stream in PNG format.
            using (var barcodeStream = new MemoryStream())
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0; // Reset stream position for reading.

                // Create a new PDF document and add a page.
                using (var pdfDoc = new Document())
                {
                    var page = pdfDoc.Pages.Add();

                    // Create an image object that references the barcode stream.
                    var pdfImage = new Aspose.Pdf.Image
                    {
                        ImageStream = barcodeStream,
                        // Set the displayed size (values are in points).
                        FixWidth = 200.0,
                        FixHeight = 100.0,
                        // Center the image on the page.
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };

                    // Add the image to the page's paragraph collection.
                    page.Paragraphs.Add(pdfImage);

                    // Save the PDF document to the specified file.
                    pdfDoc.Save(pdfPath);
                }
            }
        }

        Console.WriteLine($"PDF with Code16K barcode generated: {pdfPath}");
    }
}