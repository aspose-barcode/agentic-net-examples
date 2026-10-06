// Title: Export Code128 barcode to EMF and embed in PDF
// Description: Demonstrates generating a Code128 barcode, saving it as an EMF file, and then embedding the barcode into a PDF using Aspose.Pdf.
// Category-Description: This example belongs to the Aspose.BarCode generation and conversion category. It shows how to use BarcodeGenerator (Aspose.BarCode.Generation) to create barcodes, export them to vector formats like EMF, and combine them with Aspose.Pdf to produce PDF documents. Developers often need to generate barcodes for printing or embedding in reports, and this pattern illustrates the typical workflow of creating a barcode, handling licensing messages, and converting the image for PDF output.
// Prompt: Export a barcode as an EMF file, then convert it to PDF using a third‑party library.
// Tags: barcode, code128, emf, pdf, aspose.barcode, aspose.pdf, image conversion, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Demonstrates exporting a barcode to EMF and converting it to PDF.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a Code128 barcode, saves it as EMF, and embeds it in a PDF.
    /// </summary>
    static void Main()
    {
        // Define output directory in the temporary folder and ensure it exists.
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeEmfPdfDemo");
        Directory.CreateDirectory(outputDir);

        // Paths for the intermediate EMF file and final PDF file.
        string emfPath = Path.Combine(outputDir, "barcode.emf");
        string pdfPath = Path.Combine(outputDir, "barcode.pdf");
        string codeText = "12345678";

        // Initialize the barcode generator for Code128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set barcode and background colors.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            try
            {
                // Save the barcode as an EMF vector image.
                generator.Save(emfPath, BarCodeImageFormat.Emf);
                Console.WriteLine($"EMF saved to {emfPath}");
            }
            catch (Exception ex)
            {
                // Handle licensing evaluation errors specifically.
                if (ex.Message.Contains("evaluation"))
                {
                    Console.WriteLine("EMF export requires a valid Aspose.BarCode license.");
                }
                else
                {
                    Console.WriteLine($"Error saving EMF: {ex.Message}");
                    return;
                }
            }

            // Convert the barcode to PNG in memory for PDF embedding.
            using (var pngStream = new MemoryStream())
            {
                generator.Save(pngStream, BarCodeImageFormat.Png);
                pngStream.Position = 0; // Reset stream position before reading.

                // Create a new PDF document and add a page.
                var pdfDoc = new Document();
                var page = pdfDoc.Pages.Add();

                // Create an image object from the PNG stream and set its size and alignment.
                var pdfImage = new Image
                {
                    ImageStream = pngStream,
                    FixWidth = 200.0,
                    FixHeight = 100.0,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                page.Paragraphs.Add(pdfImage);

                // Save the PDF document to the specified path.
                pdfDoc.Save(pdfPath);
                Console.WriteLine($"PDF saved to {pdfPath}");
            }
        }
    }
}