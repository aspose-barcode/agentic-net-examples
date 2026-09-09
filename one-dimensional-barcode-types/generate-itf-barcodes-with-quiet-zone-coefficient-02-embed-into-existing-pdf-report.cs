// Title: Generate ITF14 barcode and embed into PDF with quiet zone coefficient
// Description: Demonstrates creating an ITF14 barcode, optionally setting a quiet zone coefficient, and embedding the barcode image into a PDF document.
// Category-Description: This example belongs to the Aspose.BarCode generation and Aspose.Pdf embedding category. It shows how to use BarcodeGenerator (EncodeTypes.ITF14) to produce a barcode, configure barcode parameters such as X‑Dimension and quiet zone, and then insert the resulting image into a PDF using Aspose.Pdf Document, Page, and Image classes. Developers creating reports, invoices, or shipping labels often need to generate barcodes and place them directly into PDF files.
// Prompt: Generate ITF barcodes with quiet zone coefficient 0.2, embed into existing PDF report.
// Tags: itf, barcode, pdf, embed, quietzone, aspose.barcode, aspose.pdf, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;
using Aspose.Pdf.Text;

/// <summary>
/// Example program that generates an ITF14 barcode and embeds it into a PDF report.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, applies quiet zone settings, and creates a PDF file containing the barcode image.
    /// </summary>
    static void Main()
    {
        // Sample data
        string barcodeText = "12345678901231"; // 14 digits for ITF14
        float quietZoneCoefficient = 0.2f; // Desired coefficient (invalid per API)

        // Validate quiet zone coefficient
        if (quietZoneCoefficient < 10f)
        {
            Console.WriteLine("Quiet zone coefficient must be at least 10. Using default value.");
        }

        // Prepare output PDF path
        string outputPdfPath = Path.Combine(Path.GetTempPath(), "ITFReport.pdf");

        // Generate ITF barcode
        using (var generator = new BarcodeGenerator(EncodeTypes.ITF14, barcodeText))
        {
            // Set basic barcode parameters
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Resolution = 300;

            // Apply quiet zone coefficient only if valid
            if (quietZoneCoefficient >= 10f)
            {
                generator.Parameters.Barcode.ITF.QuietZoneCoef = (int)quietZoneCoefficient;
            }

            // Save barcode to memory stream
            using (var barcodeStream = new MemoryStream())
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0;

                // Create PDF and embed barcode image
                using (var pdfDoc = new Document())
                {
                    var page = pdfDoc.Pages.Add();

                    var pdfImage = new Aspose.Pdf.Image
                    {
                        ImageStream = barcodeStream,
                        FixWidth = 200.0,
                        FixHeight = 100.0,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Margin = new MarginInfo { Top = 20 }
                    };

                    page.Paragraphs.Add(pdfImage);
                    pdfDoc.Save(outputPdfPath);
                }
            }
        }

        Console.WriteLine($"PDF with embedded ITF barcode saved to: {outputPdfPath}");
    }
}