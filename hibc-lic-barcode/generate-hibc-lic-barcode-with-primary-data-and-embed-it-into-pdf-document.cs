// Title: Generate HIBC LIC barcode and embed in PDF
// Description: Demonstrates creating a HIBC LIC barcode with primary data and placing it into a PDF file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as HIBC QR‑LIC. It shows how to configure the HIBCLICPrimaryDataCodetext, generate a PNG image with ComplexBarcodeGenerator, and embed the image into a PDF using Aspose.Pdf. Developers working with healthcare or logistics labeling often need to produce HIBC barcodes and combine them with document outputs.
// Prompt: Generate a HIBC LIC barcode with primary data and embed it into a PDF document.
// Tags: hibc, lic, barcode, generation, pdf, aspnet, aspose.barcode, aspose.pdf, complexbarcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Pdf;
using Aspose.Pdf.Text;

/// <summary>
/// Example program that creates a HIBC LIC barcode with primary data
/// and embeds the generated image into a PDF document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates the barcode, inserts it into a PDF, and saves the file.
    /// </summary>
    static void Main()
    {
        // Define the output PDF file path in the current working directory.
        string outputPdf = Path.Combine(Directory.GetCurrentDirectory(), "HIBCLICPrimary.pdf");

        // Build the primary data codetext required for a HIBC QR‑LIC barcode.
        HIBCLICPrimaryDataCodetext complexCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCQRLIC,
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",
                LabelerIdentificationCode = "A999",
                UnitOfMeasureID = 1
            }
        };

        // Generate the barcode image using ComplexBarcodeGenerator.
        using (ComplexBarcodeGenerator gen = new ComplexBarcodeGenerator(complexCodetext))
        {
            // Set the X‑dimension (module size) of the barcode in pixels.
            gen.Parameters.Barcode.XDimension.Pixels = 10;

            // Save the generated barcode to a memory stream in PNG format.
            using (MemoryStream barcodeStream = new MemoryStream())
            {
                gen.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0; // Reset stream position for reading.

                // Create a new PDF document and add a page.
                using (Document pdfDoc = new Document())
                {
                    Page page = pdfDoc.Pages.Add();

                    // Create an image object that references the barcode stream.
                    Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image
                    {
                        ImageStream = barcodeStream,
                        FixWidth = 200,
                        FixHeight = 200,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };

                    // Add the image to the page's paragraph collection.
                    page.Paragraphs.Add(pdfImage);

                    // Save the PDF document to the specified file path.
                    pdfDoc.Save(outputPdf);
                }
            }
        }

        // Inform the user where the PDF was saved.
        Console.WriteLine($"PDF with HIBC LIC barcode saved to: {outputPdf}");
    }
}