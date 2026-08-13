// Title: Export barcode to EMF and convert to PDF
// Description: Demonstrates generating a Code128 barcode, saving it as an EMF vector image, and then embedding that image into a PDF using Aspose.Pdf.
// Category-Description: This example belongs to the Aspose.BarCode image export and document conversion category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeImageFormat for vector output, and Aspose.Pdf Document for embedding images into PDF files—common tasks for developers needing high‑quality printable barcodes in reports or invoices.
// Prompt: Export a barcode as an EMF file, then convert it to PDF using a third‑party library.
// Tags: barcode, code128, emf, pdf, aspose.barcode, aspose.pdf, image-export, document-conversion

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Generates a Code128 barcode, saves it as an EMF file, and converts the EMF to a PDF document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes barcode generation, EMF export, and PDF conversion.
    /// </summary>
    static void Main()
    {
        // Define output file paths
        string emfPath = "barcode.emf";
        string pdfPath = "barcode.pdf";

        // ------------------------------------------------------------
        // Generate a barcode and export it as EMF
        // ------------------------------------------------------------
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Set barcode and background colors using Aspose.Drawing types
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Save the barcode image in EMF format
                generator.Save(emfPath, BarCodeImageFormat.Emf);
                Console.WriteLine($"Barcode saved as EMF: {emfPath}");
            }
        }
        catch (Exception ex)
        {
            // Handle licensing errors specific to EMF export
            if (ex.Message.Contains("evaluation", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("A valid Aspose.BarCode license is required for EMF export of this barcode type.");
                return;
            }
            throw;
        }

        // Verify that the EMF file was created before proceeding
        if (!File.Exists(emfPath))
        {
            Console.WriteLine($"EMF file not found: {emfPath}");
            return;
        }

        // ------------------------------------------------------------
        // Convert the EMF image to a PDF document using Aspose.Pdf
        // ------------------------------------------------------------
        using (var pdfDoc = new Document())
        {
            // Add a new page to the PDF
            var page = pdfDoc.Pages.Add();

            // Open the EMF file as a stream and embed it as an image
            using (var emfStream = new FileStream(emfPath, FileMode.Open, FileAccess.Read))
            {
                var image = new Aspose.Pdf.Image
                {
                    ImageStream = emfStream
                };
                page.Paragraphs.Add(image);
            }

            // Save the resulting PDF file
            pdfDoc.Save(pdfPath);
            Console.WriteLine($"PDF created from EMF: {pdfPath}");
        }
    }
}