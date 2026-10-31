// Title: Generate HIBC LIC Barcode and Embed in PDF
// Description: Demonstrates creating a HIBC LIC barcode with primary data and inserting it into a PDF document using Aspose.BarCode and Aspose.Pdf.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on complex barcode creation (HIBC LIC) and embedding the resulting image into a PDF via Aspose.Pdf. It showcases the use of ComplexBarcodeGenerator, HIBCLICPrimaryDataCodetext, and PDF document manipulation—common tasks for developers needing to produce printable barcodes in documents.
// Prompt: Generate a HIBC LIC barcode with primary data and embed it into a PDF document.
// Tags: hibc, lic, barcode generation, pdf, aspose.barcode, aspose.pdf, complex barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;

/// <summary>
/// Example program that creates a HIBC LIC barcode with primary data and embeds it into a PDF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode, embeds it in a PDF, and saves the document.
    /// </summary>
    static void Main()
    {
        // Define the output PDF file path
        string outputPdfPath = "HIBCLICPrimary.pdf";

        // Create primary data codetext for HIBC LIC barcode
        var primaryCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCQRLIC,
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",
                LabelerIdentificationCode = "A999",
                UnitOfMeasureID = 1
            }
        };

        // Generate the barcode image and store it in a memory stream
        using (var generator = new ComplexBarcodeGenerator(primaryCodetext))
        {
            // Set barcode image resolution (X dimension) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 10f;

            using (var ms = new MemoryStream())
            {
                // Save the generated barcode as PNG into the memory stream
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // Create a new PDF document and add a page
                using (var pdfDoc = new Document())
                {
                    var page = pdfDoc.Pages.Add();

                    // Create an Aspose.Pdf.Image object from the barcode stream
                    var pdfImage = new Aspose.Pdf.Image
                    {
                        ImageStream = ms,
                        FixWidth = 200.0,
                        FixHeight = 200.0,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Margin = new MarginInfo { Top = 20 }
                    };

                    // Add the image to the PDF page
                    page.Paragraphs.Add(pdfImage);

                    // Save the PDF document to the specified file
                    pdfDoc.Save(outputPdfPath);
                }
            }
        }

        // Output the full path of the saved PDF file
        Console.WriteLine($"PDF saved to {Path.GetFullPath(outputPdfPath)}");
    }
}