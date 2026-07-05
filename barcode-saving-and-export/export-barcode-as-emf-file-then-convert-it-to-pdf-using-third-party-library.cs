// Title: Export Code128 barcode to EMF and convert to PDF
// Description: Demonstrates generating a Code128 barcode, saving it as an EMF vector image, then embedding that image into a PDF using Aspose libraries.
// Prompt: Export a barcode as an EMF file, then convert it to PDF using a third‑party library.
// Tags: barcode, code128, export, emf, pdf, aspose.barcode, aspose.pdf

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Pdf;

/// <summary>
/// Demonstrates exporting a barcode to EMF and converting it to PDF.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode, saves as EMF, then embeds into a PDF.
    /// </summary>
    static void Main()
    {
        // Define file paths for the intermediate EMF and final PDF files
        string emfPath = "barcode.emf";
        string pdfPath = "barcode.pdf";

        // ------------------------------------------------------------
        // Generate a Code128 barcode and save it as an EMF file
        // ------------------------------------------------------------
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Optional: set image dimensions (in points) for better quality
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 150f;

                // Save the generated barcode as an EMF vector image
                generator.Save(emfPath, BarCodeImageFormat.Emf);
            }
        }
        catch (Exception ex)
        {
            // Handle licensing issues specific to EMF export for this barcode type
            if (ex.Message.Contains("evaluation"))
            {
                Console.WriteLine("A valid Aspose.BarCode license is required for EMF export of this barcode type.");
                return;
            }
            throw;
        }

        // Verify that the EMF file was successfully created
        if (!File.Exists(emfPath))
        {
            Console.WriteLine($"Failed to create EMF file at '{emfPath}'.");
            return;
        }

        // ------------------------------------------------------------
        // Convert the EMF image to a PDF document using Aspose.Pdf
        // ------------------------------------------------------------
        using (var pdfDocument = new Document())
        {
            // Add a new page to the PDF document
            var page = pdfDocument.Pages.Add();

            // Load the EMF image from the file system and add it to the PDF page
            using (var emfStream = new FileStream(emfPath, FileMode.Open, FileAccess.Read))
            {
                var pdfImage = new Aspose.Pdf.Image
                {
                    ImageStream = emfStream
                };
                page.Paragraphs.Add(pdfImage);
            }

            // Save the PDF document to the specified path
            pdfDocument.Save(pdfPath);
        }

        // Verify that the PDF file was successfully created and report the result
        if (File.Exists(pdfPath))
        {
            Console.WriteLine($"Barcode successfully exported to EMF ('{emfPath}') and converted to PDF ('{pdfPath}').");
        }
        else
        {
            Console.WriteLine("PDF conversion failed.");
        }
    }
}