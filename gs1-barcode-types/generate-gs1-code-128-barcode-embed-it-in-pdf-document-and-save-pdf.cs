// Title: Generate GS1 Code 128 Barcode and Embed in PDF
// Description: Demonstrates creating a GS1 Code 128 barcode, embedding it into a PDF document, and saving the file.
// Category-Description: This example belongs to the Aspose.BarCode and Aspose.Pdf integration category. It shows how to use the BarcodeGenerator class to produce a GS1 Code 128 symbology image and the Aspose.Pdf Document API to insert the image into a PDF page. Typical use cases include generating product labels, shipping documents, or any scenario where GS1 barcodes must be combined with PDF output. Developers often need to customize barcode appearance, size, and placement within PDF files.
// Prompt: Generate a GS1 Code 128 barcode, embed it in a PDF document, and save the PDF.
// Tags: gs1,code128,barcode,pdf,generation,aspose.barcode,aspose.pdf

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;
using Aspose.Pdf.Text;

/// <summary>
/// Example program that creates a GS1 Code 128 barcode, embeds it into a PDF, and saves the result.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output PDF file path
        const string pdfPath = "barcode.pdf";

        // GS1 Code 128 requires a GTIN in AI (01) with exactly 14 digits.
        // Example GTIN-14 (including check digit)
        const string gs1CodeText = "(01)01234567890123";

        // Generate the barcode image and store it in a memory stream
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, gs1CodeText))
        {
            // Optional: set barcode and background colors
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            using (var barcodeStream = new MemoryStream())
            {
                // Save the barcode as a PNG image into the stream
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0; // Reset stream position for reading

                // Create a new PDF document and add a page
                using (var pdfDoc = new Document())
                {
                    var page = pdfDoc.Pages.Add();

                    // Create an Image object that reads from the barcode stream
                    var pdfImage = new Image
                    {
                        ImageStream = barcodeStream,
                        // Set desired size (points). Adjust as needed.
                        FixWidth = 200,
                        FixHeight = 100,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Margin = new MarginInfo { Top = 20 }
                    };

                    // Add the image to the page's paragraph collection
                    page.Paragraphs.Add(pdfImage);

                    // Save the PDF document to the specified path
                    pdfDoc.Save(pdfPath);
                }
            }
        }

        // Inform the user where the PDF was saved
        Console.WriteLine($"PDF with GS1 Code 128 barcode saved to: {Path.GetFullPath(pdfPath)}");
    }
}