// Title: Generate ITF14 barcode with quiet zone and embed into PDF
// Description: Demonstrates creating an ITF14 barcode, configuring its quiet zone coefficient, and inserting the barcode image into an existing PDF report.
// Category-Description: This example belongs to the Aspose.BarCode generation and Aspose.Pdf manipulation category. It shows how to use BarcodeGenerator (Aspose.BarCode.Generation) to produce a barcode image, adjust barcode parameters such as X‑dimension and quiet‑zone coefficient, and then embed the resulting image into a PDF document using Aspose.Pdf.Document. Typical use cases include adding machine‑readable identifiers to reports, invoices, or shipping documents where a PDF output is required.
// Prompt: Generate ITF barcodes with quiet zone coefficient 0.2, embed into existing PDF report.
// Tags: itf, barcode, quietzone, pdf, aspose.barcode, aspose.pdf, generation, embedding

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Example program that creates an ITF14 barcode, configures its quiet zone,
/// and embeds the barcode image into a PDF document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the path to the PDF report.
        string pdfPath = "Report.pdf";

        // Load the existing PDF if it exists; otherwise create a new document with a single page.
        Aspose.Pdf.Document pdfDoc;
        if (File.Exists(pdfPath))
        {
            pdfDoc = new Aspose.Pdf.Document(pdfPath);
        }
        else
        {
            pdfDoc = new Aspose.Pdf.Document();
            pdfDoc.Pages.Add();
        }

        // Create an ITF14 barcode generator with the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.ITF14, "12345678901231"))
        {
            // Set the module (X‑dimension) size in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // The QuietZoneCoef property expects an integer >= 10.
            // The requested coefficient 0.2 is not valid, so we use the minimum allowed value.
            int quietZoneCoef = 10;
            generator.Parameters.Barcode.ITF.QuietZoneCoef = quietZoneCoef;
            Console.WriteLine($"QuietZoneCoef set to minimum valid value: {quietZoneCoef}");

            // Render the barcode to a memory stream in PNG format.
            using (var barcodeStream = new MemoryStream())
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0; // Reset stream position for reading.

                // Embed the barcode image into the PDF document.
                using (pdfDoc)
                {
                    // Ensure the PDF has at least one page.
                    if (pdfDoc.Pages.Count == 0)
                    {
                        pdfDoc.Pages.Add();
                    }

                    // Get the first page of the PDF.
                    var page = pdfDoc.Pages[1];

                    // Create an Aspose.Pdf.Image object from the barcode stream.
                    var pdfImage = new Aspose.Pdf.Image
                    {
                        ImageStream = barcodeStream,
                        FixWidth = 200.0,
                        FixHeight = 100.0,
                        HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center,
                        VerticalAlignment = Aspose.Pdf.VerticalAlignment.Center
                    };

                    // Add the image to the page's paragraph collection.
                    page.Paragraphs.Add(pdfImage);

                    // Save the updated PDF back to the original file path.
                    pdfDoc.Save(pdfPath);
                    Console.WriteLine($"Barcode embedded into PDF: {pdfPath}");
                }
            }
        }
    }
}