// Title: Export Barcode to EMF and Convert to PDF
// Description: Demonstrates generating a Code128 barcode, saving it as an EMF vector image, and then embedding that image into a PDF using Aspose.Pdf.
// Category-Description: This example belongs to the Aspose.BarCode image export and Aspose.Pdf conversion category. It showcases the use of BarcodeGenerator (Aspose.BarCode.Generation) to create barcodes, BarCodeImageFormat for vector image output, and Aspose.Pdf Document for PDF creation. Developers often need to generate high‑resolution barcode graphics (EMF, SVG, EPS) and incorporate them into documents such as invoices, shipping labels, or reports.
// Prompt: Export a barcode as an EMF file, then convert it to PDF using a third‑party library.
// Tags: barcode, code128, emf, pdf, aspose.barcode, aspose.pdf, image-export, conversion

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Provides a simple console demo that creates a barcode, saves it as an EMF file,
/// and then converts the EMF image into a PDF document using Aspose.Pdf.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates a Code128 barcode, exports it to EMF,
    /// and embeds the EMF into a PDF file.
    /// </summary>
    static void Main()
    {
        // Prepare output directory and file paths
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeEmfPdfDemo");
        Directory.CreateDirectory(outputDir);
        string emfPath = Path.Combine(outputDir, "barcode.emf");
        string pdfPath = Path.Combine(outputDir, "barcode.pdf");

        // Generate a barcode and save it as EMF
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Save directly to EMF file
                generator.Save(emfPath, BarCodeImageFormat.Emf);
            }
            Console.WriteLine($"Barcode saved as EMF to: {emfPath}");
        }
        catch (Exception ex)
        {
            // Evaluation version may restrict EMF export to certain symbologies
            if (ex.Message.Contains("evaluation"))
            {
                Console.WriteLine("EMF export requires a valid Aspose.BarCode license. Operation aborted.");
                return;
            }
            Console.WriteLine($"Error generating EMF: {ex.Message}");
            return;
        }

        // Verify EMF file exists before conversion
        if (!File.Exists(emfPath))
        {
            Console.WriteLine("EMF file was not created. Cannot convert to PDF.");
            return;
        }

        // Convert the EMF barcode image to a PDF using Aspose.Pdf
        try
        {
            using (var pdfDoc = new Document())
            {
                var page = pdfDoc.Pages.Add();

                // Open the EMF file as a stream and keep it open until after PDF is saved
                using (var emfStream = new FileStream(emfPath, FileMode.Open, FileAccess.Read))
                {
                    var pdfImage = new Aspose.Pdf.Image
                    {
                        ImageStream = emfStream,
                        FixWidth = 300,
                        FixHeight = 100,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Margin = new MarginInfo { Top = 20 }
                    };
                    page.Paragraphs.Add(pdfImage);

                    // Save the PDF while the image stream is still open
                    pdfDoc.Save(pdfPath);
                }
            }
            Console.WriteLine($"PDF created at: {pdfPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error converting EMF to PDF: {ex.Message}");
        }
    }
}