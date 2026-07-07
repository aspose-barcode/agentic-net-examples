// Title: Generate ITF14 barcode with custom quiet zone and embed into PDF
// Description: Demonstrates creating an ITF14 barcode with a quiet zone coefficient, then inserting it into an existing PDF report.
// Category-Description: This example belongs to the Aspose.BarCode PDF integration category, illustrating how to generate barcodes (using BarcodeGenerator, EncodeTypes) and embed them into PDF documents (using Aspose.Pdf Document). Typical use cases include adding product identifiers to reports, invoices, or shipping documents. Developers often need to customize barcode appearance such as quiet zones before placing them in PDFs.
// Prompt: Generate ITF barcodes with quiet zone coefficient 0.2, embed into existing PDF report.
// Tags: itf, barcode, quietzone, pdf, aspose.barcode, aspose.pdf, generation, embedding

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Example program that generates an ITF14 barcode with a specified quiet zone coefficient
/// and embeds the resulting image into an existing PDF document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Paths for the input PDF report and the output PDF with the embedded barcode
        string inputPdfPath = "input.pdf";
        string outputPdfPath = "output.pdf";

        // Verify that the input PDF exists before proceeding
        if (!File.Exists(inputPdfPath))
        {
            Console.WriteLine($"Input PDF not found at path: {Path.GetFullPath(inputPdfPath)}");
            return;
        }

        // Sample ITF14 code text (14 digits required)
        string itfCodeText = "12345678901231";

        // Desired quiet zone coefficient (the API requires an integer >= 10)
        double requestedQuietZoneCoef = 0.2;

        // Create the ITF barcode generator with the specified symbology and data
        using (var generator = new BarcodeGenerator(EncodeTypes.ITF14, itfCodeText))
        {
            // Set the quiet zone coefficient only if it meets the API's minimum requirement
            if (requestedQuietZoneCoef >= 10)
            {
                generator.Parameters.Barcode.ITF.QuietZoneCoef = (int)requestedQuietZoneCoef;
            }
            else
            {
                Console.WriteLine("Quiet zone coefficient is less than the minimum allowed (10). Skipping setting this property.");
            }

            // Generate the barcode image into a memory stream (PNG format)
            using (var barcodeStream = new MemoryStream())
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0; // Reset stream position for reading

                // Load the existing PDF document
                using (var pdfDoc = new Document(inputPdfPath))
                {
                    // Add a new page to place the barcode (or use an existing page as needed)
                    var page = pdfDoc.Pages.Add();

                    // Create an Aspose.Pdf.Image from the barcode stream
                    var pdfImage = new Aspose.Pdf.Image
                    {
                        ImageStream = barcodeStream,
                        FixWidth = 200.0,   // Adjust width as required
                        FixHeight = 100.0   // Adjust height as required
                    };

                    // Add the image to the page's paragraph collection
                    page.Paragraphs.Add(pdfImage);

                    // Save the modified PDF to the output path
                    pdfDoc.Save(outputPdfPath);
                }
            }
        }

        Console.WriteLine($"Barcode embedded successfully. Output saved to: {Path.GetFullPath(outputPdfPath)}");
    }
}