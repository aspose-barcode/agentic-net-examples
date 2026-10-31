// Title: Export Barcode to EMF and Convert to PDF
// Description: Demonstrates generating a Code 39 barcode, saving it as an EMF vector image, and embedding the barcode into a PDF using Aspose.Pdf.
// Category-Description: This example belongs to the Aspose.BarCode image export and Aspose.Pdf document creation category. It showcases the BarcodeGenerator class for creating barcodes, the BarCodeImageFormat enum for exporting to various image formats (EMF, PNG), and the Aspose.Pdf Document API for embedding images into PDF pages. Developers often need to generate high‑quality vector barcodes for print media and then combine them with other content in PDF reports or invoices.
// Prompt: Export a barcode as an EMF file, then convert it to PDF using a third‑party library.
// Tags: barcode, symbology, export, emf, pdf, aspose.barcode, aspose.pdf, code39, image conversion

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Generates a Code 39 barcode, saves it as an EMF file, and embeds the barcode into a PDF document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary output folders, generates the barcode,
    /// exports it to EMF and PNG, and builds a PDF containing the PNG image.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary output directory.
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define file paths for the EMF and PDF outputs.
        string emfPath = Path.Combine(outputDir, "barcode.emf");
        string pdfPath = Path.Combine(outputDir, "barcode.pdf");

        // Initialize the barcode generator for Code 39 with the value "12345".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "12345"))
        {
            // -----------------------------------------------------------------
            // Export the barcode as an EMF vector image.
            // -----------------------------------------------------------------
            try
            {
                generator.Save(emfPath, BarCodeImageFormat.Emf);
                Console.WriteLine($"EMF file saved to: {emfPath}");
            }
            catch (Exception ex)
            {
                // EMF export requires a licensed version of Aspose.BarCode.
                if (ex.Message.Contains("evaluation"))
                {
                    Console.WriteLine("EMF export requires a valid Aspose.BarCode license.");
                }
                else
                {
                    Console.WriteLine($"Error saving EMF: {ex.Message}");
                }
                return;
            }

            // -----------------------------------------------------------------
            // Export the same barcode as a PNG image into a memory stream.
            // This stream will be used to embed the image into a PDF.
            // -----------------------------------------------------------------
            var pngStream = new MemoryStream();
            try
            {
                generator.Save(pngStream, BarCodeImageFormat.Png);
                pngStream.Position = 0; // Reset stream position for reading.
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving PNG stream: {ex.Message}");
                return;
            }

            // -----------------------------------------------------------------
            // Create a PDF document and add a page containing the PNG barcode.
            // -----------------------------------------------------------------
            using (var pdfDoc = new Document())
            {
                var page = pdfDoc.Pages.Add();

                var pdfImage = new Aspose.Pdf.Image
                {
                    ImageStream = pngStream,
                    FixWidth = 200,
                    FixHeight = 100,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                page.Paragraphs.Add(pdfImage);
                pdfDoc.Save(pdfPath);
                Console.WriteLine($"PDF file saved to: {pdfPath}");
            }

            // Release the memory stream after the PDF has been saved.
            pngStream.Dispose();
        }
    }
}