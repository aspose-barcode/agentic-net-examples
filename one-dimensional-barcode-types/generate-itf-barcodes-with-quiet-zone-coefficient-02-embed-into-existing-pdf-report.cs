// Title: Generate ITF-14 barcode and embed into PDF with quiet zone coefficient
// Description: Demonstrates creating an ITF-14 barcode, configuring its quiet zone coefficient, and inserting the barcode image into an existing PDF report.
// Category-Description: This example belongs to the Aspose.BarCode and Aspose.Pdf integration category. It shows how to use BarcodeGenerator (Aspose.BarCode.Generation) to produce a barcode image, adjust barcode parameters such as X‑Dimension and quiet‑zone, and then embed the image into a PDF document using Aspose.Pdf's Document and Page classes. Developers working on automated report generation, invoice creation, or any workflow that requires barcodes inside PDF files will find this pattern useful.
// Prompt: Generate ITF barcodes with quiet zone coefficient 0.2, embed into existing PDF report.
// Tags: itf, barcode, pdf, embed, aspose.barcode, aspose.pdf, quietzone, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;
using Aspose.Pdf.Text;

/// <summary>
/// Example program that creates an ITF‑14 barcode, sets a quiet‑zone coefficient,
/// and embeds the barcode image into a PDF report.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Paths for the source PDF and the output PDF with the embedded barcode.
        string inputPdfPath = "report.pdf";
        string outputPdfPath = "report_with_barcode.pdf";

        // Ensure the source PDF exists; create a simple placeholder if it is missing.
        if (!File.Exists(inputPdfPath))
        {
            using (var doc = new Document())
            {
                var page = doc.Pages.Add();
                var placeholder = new TextFragment("Sample Report");
                page.Paragraphs.Add(placeholder);
                doc.Save(inputPdfPath);
            }
            Console.WriteLine($"Created placeholder PDF: {inputPdfPath}");
        }

        // Generate an ITF‑14 barcode with the specified data.
        using (var generator = new BarcodeGenerator(EncodeTypes.ITF14, "12345678901231"))
        {
            // Set the X‑Dimension (module width) in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // QuietZoneCoef requires an integer (minimum 10). Using 10 as the closest valid value.
            generator.Parameters.Barcode.ITF.QuietZoneCoef = 10;

            // Optional: remove the border around the barcode.
            generator.Parameters.Barcode.ITF.BorderType = ITF14BorderType.None;

            // Save the generated barcode to a memory stream in PNG format.
            using (var barcodeStream = new MemoryStream())
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0; // Reset stream position for reading.

                // Load the existing PDF and embed the barcode image on a new page.
                using (var pdfDoc = new Document(inputPdfPath))
                {
                    var page = pdfDoc.Pages.Add();

                    // Define the rectangle where the barcode image will be placed.
                    var rect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);
                    page.AddImage(barcodeStream, rect);

                    // Save the updated PDF to the output path.
                    pdfDoc.Save(outputPdfPath);
                }
            }
        }

        Console.WriteLine($"Barcode embedded into PDF: {outputPdfPath}");
    }
}