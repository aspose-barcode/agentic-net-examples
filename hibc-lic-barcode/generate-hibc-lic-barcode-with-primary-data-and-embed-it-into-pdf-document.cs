using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using Aspose.Pdf;

/// <summary>
/// Demonstrates generating a HIBC LIC barcode, embedding it into a PDF, and saving the result.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a HIBC LIC barcode image, inserts it into a PDF document, and saves the PDF.
    /// </summary>
    static void Main()
    {
        // Define the output PDF file name.
        const string pdfPath = "hibc_barcode.pdf";

        // Prepare primary HIBC LIC data (product information) for barcode generation.
        var primaryCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",   // Product or catalog identifier.
                LabelerIdentificationCode = "A999", // Labeler ID.
                UnitOfMeasureID = 1                 // Unit of measure identifier.
            }
        };

        // Generate the barcode image and embed it into a PDF document.
        using (var generator = new ComplexBarcodeGenerator(primaryCodetext))
        using (var barcodeImage = generator.GenerateBarCodeImage())
        using (var imageStream = new MemoryStream())
        {
            // Save the generated barcode image to a memory stream in PNG format.
            barcodeImage.Save(imageStream, Aspose.Drawing.Imaging.ImageFormat.Png);
            imageStream.Position = 0; // Reset stream position for reading.

            // Create a new PDF document and add a page.
            var pdfDoc = new Document();
            var page = pdfDoc.Pages.Add();

            // Create an Aspose.Pdf.Image object that references the barcode image stream.
            var pdfImage = new Aspose.Pdf.Image
            {
                ImageStream = imageStream,
                // Optional: set explicit dimensions for the image.
                // FixWidth = 200.0,
                // FixHeight = 100.0
            };

            // Add the image to the page's paragraph collection.
            page.Paragraphs.Add(pdfImage);

            // Save the PDF document to the specified file path.
            pdfDoc.Save(pdfPath);
        }

        // Output the full path of the created PDF file to the console.
        Console.WriteLine("PDF with HIBC LIC barcode created: " + Path.GetFullPath(pdfPath));
    }
}