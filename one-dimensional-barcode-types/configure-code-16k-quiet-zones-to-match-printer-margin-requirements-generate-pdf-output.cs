// Title: Generate Code 16K barcode with custom quiet zones and embed in PDF
// Description: Demonstrates configuring Code 16K barcode quiet zones to meet printer margin requirements and saving the result as a PDF document.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to customize barcode parameters such as quiet zone coefficients, X‑dimension, and colors, then embed the generated image into a PDF using Aspose.Pdf. Developers often need to adjust quiet zones for printing constraints and combine barcodes with other document content, making use of BarcodeGenerator, BarcodeParameters, and Document classes.
// Prompt: Configure Code 16K quiet zones to match printer margin requirements, generate PDF output.
// Tags: barcode, code16k, quiet zone, pdf, aspose.barcode, aspose.pdf, generation, image embedding

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;
using Aspose.Pdf.Text;

/// <summary>
/// Demonstrates generating a Code 16K barcode with custom quiet zones and embedding it into a PDF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, creates a PDF, and saves it to a temporary location.
    /// </summary>
    static void Main()
    {
        // Define the output PDF path in the system temporary folder.
        string outputPdf = Path.Combine(Path.GetTempPath(), "Code16K.pdf");
        string outputDir = Path.GetDirectoryName(outputPdf);

        // Ensure the output directory exists.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Initialize the barcode generator for Code 16K symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, "Aspose.Barcode"))
        {
            // Set barcode visual parameters.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;               // Width of a single module.
            generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = 10;      // Left quiet zone coefficient.
            generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = 10;     // Right quiet zone coefficient.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black; // Barcode color.

            // Render the barcode to a memory stream as PNG.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.

                // Create a new PDF document.
                using (var pdfDoc = new Document())
                {
                    // Add a page to the PDF.
                    var page = pdfDoc.Pages.Add();

                    // Create an image object from the barcode stream.
                    var pdfImage = new Aspose.Pdf.Image
                    {
                        ImageStream = ms,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Margin = new MarginInfo { Top = 10, Bottom = 10 }
                    };

                    // Add the image to the page's paragraphs collection.
                    page.Paragraphs.Add(pdfImage);

                    // Save the PDF to the specified file.
                    pdfDoc.Save(outputPdf);
                }
            }
        }

        // Inform the user where the PDF was saved.
        Console.WriteLine($"PDF saved to: {outputPdf}");
    }
}