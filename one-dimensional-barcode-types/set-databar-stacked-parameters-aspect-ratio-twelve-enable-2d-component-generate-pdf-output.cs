// Title: Generate DataBar Stacked barcode with 2D component and export to PDF
// Description: Demonstrates how to configure a DataBar Stacked barcode, set its aspect ratio, enable the 2D composite component, and embed the resulting image into a PDF file using Aspose.BarCode and Aspose.Pdf.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on DataBar symbologies. It showcases the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to create a barcode, and Aspose.Pdf Document and Image classes to embed the barcode into a PDF. Typical scenarios include generating product labels, receipts, or any documents that require high‑density barcodes with optional 2D components.
// Prompt: Set DataBar stacked parameters aspect ratio twelve, enable 2D component, generate PDF output.
// Tags: databar, stacked, aspectratio, 2dcomponent, pdf, aspose.barcode, aspose.pdf, barcode-generation

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Example program that creates a DataBar Stacked barcode with a 2D composite component,
/// embeds it into a PDF document, and saves the result to disk.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output PDF file name.
        const string outputPdfPath = "DataBarStacked.pdf";

        // Initialize a barcode generator for the DataBar Stacked symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.DatabarStacked))
        {
            // Set the barcode text to a sample GTIN code (required format for DataBar Stacked).
            generator.CodeText = "(01)12345678901231";

            // Configure DataBar‑specific parameters.
            generator.Parameters.Barcode.DataBar.AspectRatio = 12f;               // Aspect ratio = 12
            generator.Parameters.Barcode.DataBar.Is2DCompositeComponent = true; // Enable 2D component

            // Render the barcode to a memory stream in PNG format.
            using (var barcodeStream = new MemoryStream())
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0; // Reset stream position for subsequent reading.

                // Create a new PDF document and add a page.
                using (var pdfDoc = new Document())
                {
                    var page = pdfDoc.Pages.Add();

                    // Create an Aspose.Pdf.Image object that reads the barcode from the stream.
                    var pdfImage = new Aspose.Pdf.Image
                    {
                        ImageStream = barcodeStream,
                        FixWidth = 200f,
                        FixHeight = 100f,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };

                    // Add the image to the page's paragraph collection.
                    page.Paragraphs.Add(pdfImage);

                    // Save the PDF document to the specified file.
                    pdfDoc.Save(outputPdfPath);
                }
            }
        }

        // Inform the user where the PDF was saved.
        Console.WriteLine($"PDF with DataBar Stacked barcode saved to: {outputPdfPath}");
    }
}