using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a GS1 Code 128 barcode and embedding it into a PDF using Aspose libraries.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Generates a barcode, inserts it into a PDF, and saves the file.
    /// </summary>
    static void Main()
    {
        // Define the output PDF file name.
        const string outputPdfPath = "GS1Code128.pdf";

        // Barcode content: GS1 Code 128 with GTIN example.
        const string barcodeText = "(01)12345678901231";

        // Initialize the barcode generator for GS1 Code 128.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, barcodeText))
        {
            // Render the barcode to a memory stream in PNG format.
            using (var barcodeStream = new MemoryStream())
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                // Reset stream position to the beginning for reading.
                barcodeStream.Position = 0;

                // Create a new PDF document.
                using (var pdfDoc = new Document())
                {
                    // Add a blank page to the document.
                    var page = pdfDoc.Pages.Add();

                    // Create an image object that uses the barcode stream.
                    var pdfImage = new Aspose.Pdf.Image
                    {
                        ImageStream = barcodeStream
                        // Optional: set fixed dimensions (points) if needed.
                        // FixWidth = 200.0,
                        // FixHeight = 100.0
                    };

                    // Insert the image into the page's paragraph collection.
                    page.Paragraphs.Add(pdfImage);

                    // Save the PDF to the specified file path.
                    pdfDoc.Save(outputPdfPath);
                }
            }
        }

        // Inform the user that the PDF has been created.
        Console.WriteLine($"PDF with GS1 Code 128 barcode saved to: {outputPdfPath}");
    }
}