// Title: Generate HIBC LIC barcode and embed in PDF
// Description: Demonstrates creating a HIBC LIC barcode with primary data and inserting it into a PDF document.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode creation using the ComplexBarcodeGenerator and HIBCLICPrimaryDataCodetext classes. It shows how to encode product information into a HIBC LIC symbology and embed the resulting image into a PDF via Aspose.Pdf. Developers often need to generate regulatory or healthcare barcodes and combine them with document workflows, making this pattern useful for automated report or label generation.
// Prompt: Generate a HIBC LIC barcode with primary data and embed it into a PDF document.
// Tags: hibc, lic, barcode, generation, pdf, aspose.barcode, aspose.pdf

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using Aspose.Pdf;
using PdfImage = Aspose.Pdf.Image;

/// <summary>
/// Demonstrates generating a HIBC LIC barcode with primary data and embedding it into a PDF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, converts it to PNG, and adds it to a PDF.
    /// </summary>
    static void Main()
    {
        // Define the primary data for the HIBC LIC barcode.
        var primaryCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",
                LabelerIdentificationCode = "A999",
                UnitOfMeasureID = 1
            }
        };

        // Generate the barcode image using ComplexBarcodeGenerator.
        using (var generator = new ComplexBarcodeGenerator(primaryCodetext))
        // Render the barcode to a bitmap.
        using (var bitmap = generator.GenerateBarCodeImage())
        // Store the bitmap in a memory stream as PNG.
        using (var imageStream = new MemoryStream())
        {
            bitmap.Save(imageStream, ImageFormat.Png);
            imageStream.Position = 0; // Reset stream position for reading.

            // Create a new PDF document.
            using (var pdfDoc = new Document())
            {
                // Add a page to the PDF.
                var page = pdfDoc.Pages.Add();

                // Create an Aspose.Pdf.Image from the barcode stream.
                var pdfImage = new PdfImage { ImageStream = imageStream };

                // Insert the image into the page's paragraph collection.
                page.Paragraphs.Add(pdfImage);

                // Save the PDF to disk.
                pdfDoc.Save("HIBC_LIC.pdf");
            }
        }

        Console.WriteLine("PDF with HIBC LIC barcode created successfully.");
    }
}