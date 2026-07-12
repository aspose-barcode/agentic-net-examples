// Title: Generate and embed a Postnet postal barcode into a PDF
// Description: Demonstrates creating a Postnet barcode and placing it at a specific location within an existing PDF document.
// Category-Description: This example belongs to the Aspose.BarCode for .NET PDF manipulation category, showcasing how to generate barcode images using BarcodeGenerator (EncodeTypes.Postnet) and embed them into PDF pages via Aspose.Pdf Document and Page classes. Typical use cases include adding shipping barcodes to invoices or labels directly in PDFs, a common requirement for logistics and e‑commerce applications.
// Prompt: Generate a postal barcode and embed it directly into an existing PDF page at a specified coordinate.
// Tags: postnet, barcode generation, pdf embedding, aspnet, aspose.barcode, aspose.pdf

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Example program that generates a Postnet barcode and embeds it into an existing PDF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode, inserts it into the PDF, and saves the result.
    /// </summary>
    static void Main()
    {
        // Define file paths for the source PDF and the resulting PDF
        string inputPdfPath = "input.pdf";
        string outputPdfPath = "output.pdf";

        // Ensure the source PDF exists before proceeding
        if (!File.Exists(inputPdfPath))
        {
            Console.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Barcode configuration
        string postalCode = "12345"; // Postnet requires 5, 6 or 9 digits
        float llx = 100f; // Lower‑left X coordinate (points)
        float lly = 200f; // Lower‑left Y coordinate (points)
        float width = 150f; // Desired barcode image width (points)
        float height = 50f; // Desired barcode image height (points)

        // Create a BarcodeGenerator for the Postnet symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, postalCode))
        {
            // Set visual appearance of the barcode
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Render the barcode to a memory stream in PNG format
            using (var barcodeStream = new MemoryStream())
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0; // Reset stream position for reading

                // Load the existing PDF document
                using (var pdfDoc = new Document(inputPdfPath))
                {
                    // Access the first page (adjust index if a different page is required)
                    var page = pdfDoc.Pages[1];

                    // Define the rectangle where the barcode image will be placed
                    var rect = new Aspose.Pdf.Rectangle(llx, lly, llx + width, lly + height);

                    // Insert the barcode image into the PDF page at the specified coordinates
                    page.AddImage(barcodeStream, rect);

                    // Save the modified PDF to the output path
                    pdfDoc.Save(outputPdfPath);
                }
            }
        }

        Console.WriteLine($"Barcode embedded successfully. Output saved to: {outputPdfPath}");
    }
}