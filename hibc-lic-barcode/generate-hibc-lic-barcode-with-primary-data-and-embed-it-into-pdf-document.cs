// Title: Generate HIBC LIC Primary Data Barcode and Embed in PDF
// Description: Demonstrates creating a HIBC QR LIC barcode with primary data, saving it as a PNG image, and embedding that image into a PDF document.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of HIBCLICPrimaryDataCodetext and ComplexBarcodeGenerator to produce HIBC LIC barcodes, and Aspose.Pdf to embed generated images into PDF files. Developers working with healthcare or logistics labeling often need to generate HIBC barcodes with detailed product information and combine them with PDF reports or documents.
/// Prompt: Generate a HIBC LIC barcode with primary data and embed it into a PDF document.
/// Tags: hibc, lic, barcode, generation, pdf, aspose.barcode, aspose.pdf, complexbarcode, primarydata

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Example program that creates a HIBC LIC barcode with primary data,
/// saves the barcode as a PNG image, and embeds the image into a PDF document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates the barcode, writes the image file, creates a PDF, and embeds the barcode image.
    /// </summary>
    static void Main()
    {
        // Define output file paths for the PDF and optional PNG image
        string outputPdfPath = Path.Combine(Directory.GetCurrentDirectory(), "HIBCLICPrimary.pdf");
        string barcodeImagePath = Path.Combine(Directory.GetCurrentDirectory(), "HIBCLICPrimary.png");

        // Create and configure the primary data codetext for a HIBC QR LIC barcode
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

        // Generate the barcode image using ComplexBarcodeGenerator
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(complexCodetext))
        {
            // Set visual appearance of the barcode
            generator.Parameters.Barcode.XDimension.Pixels = 10f;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Store the generated barcode in a memory stream
            using (MemoryStream barcodeStream = new MemoryStream())
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0; // Reset stream for subsequent reads

                // Optionally save the barcode image as a separate PNG file
                using (FileStream file = new FileStream(barcodeImagePath, FileMode.Create, FileAccess.Write))
                {
                    barcodeStream.CopyTo(file);
                }

                // Reset stream position again before embedding into PDF
                barcodeStream.Position = 0;

                // Create a new PDF document and embed the barcode image
                using (Document pdfDoc = new Document())
                {
                    var page = pdfDoc.Pages.Add();

                    var pdfImage = new Aspose.Pdf.Image
                    {
                        ImageStream = barcodeStream,
                        FixWidth = 200.0,
                        FixHeight = 200.0,
                        HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center,
                        Margin = new Aspose.Pdf.MarginInfo { Top = 20 }
                    };

                    page.Paragraphs.Add(pdfImage);
                    pdfDoc.Save(outputPdfPath);
                }
            }
        }

        // Inform the user where the PDF was saved
        Console.WriteLine("PDF with HIBC LIC barcode generated at:");
        Console.WriteLine(outputPdfPath);
    }
}