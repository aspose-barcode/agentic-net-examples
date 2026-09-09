// Title: Export Barcode to EMF and Convert to PDF
// Description: Demonstrates generating a Code128 barcode, saving it as an EMF vector image, and embedding that image into a PDF using Aspose.Pdf.
// Category-Description: This example belongs to the Aspose.BarCode image export and Aspose.Pdf document creation category. It showcases the BarcodeGenerator class for creating barcodes, the BarCodeImageFormat enumeration for vector image export, and the Aspose.Pdf Document API for embedding images into PDF pages. Developers often need to generate high‑resolution barcodes for print media and then combine them with other content in PDF reports or invoices.
// Prompt: Export a barcode as an EMF file, then convert it to PDF using a third‑party library.
// Tags: barcode, code128, export, emf, pdf, aspose.barcode, aspose.pdf, image conversion

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Demonstrates how to generate a barcode, export it as an EMF file,
/// and then embed that EMF image into a PDF document using Aspose libraries.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, saves it as EMF,
    /// and creates a PDF that contains the barcode image.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Define temporary output directory and file paths
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        string emfPath = Path.Combine(outputDir, "barcode.emf");
        string pdfPath = Path.Combine(outputDir, "barcode.pdf");

        // --------------------------------------------------------------------
        // Generate barcode and save it as an EMF vector image
        // --------------------------------------------------------------------
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
            {
                // Set high resolution for better quality EMF output
                generator.Parameters.Resolution = 300f;
                generator.Save(emfPath, BarCodeImageFormat.Emf);
            }

            Console.WriteLine($"EMF file saved to: {emfPath}");
        }
        catch (Exception ex) when (ex.Message.Contains("evaluation"))
        {
            // EMF export requires a licensed version of Aspose.BarCode
            Console.WriteLine("EMF export requires a valid Aspose.BarCode license.");
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving EMF: {ex.Message}");
            return;
        }

        // Verify that the EMF file was created successfully
        if (!File.Exists(emfPath))
        {
            Console.WriteLine("EMF file was not created.");
            return;
        }

        // --------------------------------------------------------------------
        // Create a PDF document and embed the EMF image
        // --------------------------------------------------------------------
        try
        {
            // Determine the pixel dimensions of the barcode image
            int imageWidthPixels;
            int imageHeightPixels;
            using (var genForSize = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
            {
                genForSize.Parameters.Resolution = 300f;
                using (Aspose.Drawing.Bitmap bmp = genForSize.GenerateBarCodeImage())
                {
                    imageWidthPixels = bmp.Width;
                    imageHeightPixels = bmp.Height;
                }
            }

            // Initialize a new PDF document and add a page
            var pdfDoc = new Document();
            var page = pdfDoc.Pages.Add();

            // Open the EMF file as a stream for embedding
            using (FileStream emfStream = new FileStream(emfPath, FileMode.Open, FileAccess.Read))
            {
                // Convert pixel dimensions to PDF points (1 point = 1/72 inch)
                float widthPoints = (imageWidthPixels * 72f) / 300f;
                float heightPoints = (imageHeightPixels * 72f) / 300f;

                // Define the rectangle where the image will be placed (10 points from lower‑left)
                var rect = new Aspose.Pdf.Rectangle(10, 10, 10 + widthPoints, 10 + heightPoints);
                page.AddImage(emfStream, rect);
            }

            // Save the resulting PDF file
            pdfDoc.Save(pdfPath);
            Console.WriteLine($"PDF file saved to: {pdfPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating PDF: {ex.Message}");
        }
    }
}