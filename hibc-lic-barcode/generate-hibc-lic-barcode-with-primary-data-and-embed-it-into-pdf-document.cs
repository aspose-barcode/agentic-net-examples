// Title: Generate HIBC LIC Barcode and Embed into PDF
// Description: Demonstrates creating a HIBC LIC barcode using Aspose.BarCode, converting it to PNG, and embedding the image into a PDF document with Aspose.Pdf.
// Category-Description: This example belongs to the Aspose.BarCode generation and Aspose.Pdf integration category. It shows how to use ComplexBarcodeGenerator, HIBCLICPrimaryDataCodetext, and PrimaryData to produce a HIBC LIC barcode, then embed the resulting PNG image into a PDF using Document, Page, and Image classes. Developers working on product labeling, healthcare packaging, or any scenario requiring HIBC LIC barcodes in PDF reports will find this pattern useful.
// Prompt: Generate a HIBC LIC barcode with primary data and embed it into a PDF document.
// Tags: hibc, lic, barcode, generation, pdf, embedding, aspose.barcode, aspose.pdf, complexbarcode, png, image

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Pdf;

/// <summary>
/// Example program that creates a HIBC LIC barcode and inserts it into a PDF document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, embeds it into a PDF, and saves the file.
    /// </summary>
    static void Main()
    {
        // Prepare primary data for the HIBC LIC barcode
        var primaryData = new PrimaryData
        {
            ProductOrCatalogNumber = "12345",
            LabelerIdentificationCode = "A999",
            UnitOfMeasureID = 1
        };

        // Wrap the primary data in a HIBCLICPrimaryDataCodetext object and specify the barcode type
        var hibcCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            Data = primaryData
        };

        // Generate the barcode image and store it in a memory stream
        using (var generator = new ComplexBarcodeGenerator(hibcCodetext))
        {
            var barcodeStream = new MemoryStream();
            generator.Save(barcodeStream, BarCodeImageFormat.Png);
            barcodeStream.Position = 0; // Reset stream position for subsequent reading

            // Create a new PDF document and add a page
            var pdfDoc = new Document();
            var page = pdfDoc.Pages.Add();

            // Create an Aspose.Pdf.Image object that reads the barcode from the memory stream
            var pdfImage = new Aspose.Pdf.Image
            {
                ImageStream = barcodeStream,
                FixWidth = 200.0,
                FixHeight = 100.0,
                HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center,
                VerticalAlignment = Aspose.Pdf.VerticalAlignment.Center,
                Margin = new Aspose.Pdf.MarginInfo { Top = 20 }
            };

            // Add the image to the page's paragraph collection
            page.Paragraphs.Add(pdfImage);

            // Save the PDF document to disk
            const string outputPath = "HIBC_LIC.pdf";
            pdfDoc.Save(outputPath);

            // Dispose the memory stream after the PDF has been saved
            barcodeStream.Dispose();
        }

        Console.WriteLine("PDF with HIBC LIC barcode created successfully.");
    }
}