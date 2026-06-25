using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Demonstrates generating a Codabar barcode, embedding it into a PDF,
/// and saving the resulting document to disk.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Codabar barcode with specific start/stop symbols,
    /// inserts it into a PDF, and saves the PDF file.
    /// </summary>
    static void Main()
    {
        // Create a barcode generator for Codabar type
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar))
        {
            // Set the data to encode (excluding start/stop symbols)
            generator.CodeText = "123456";

            // Define start and stop symbols for the Codabar barcode
            generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.A;
            generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.B;

            // Render the barcode to a memory stream in PNG format
            using (var barcodeStream = new MemoryStream())
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                // Reset stream position to the beginning for reading
                barcodeStream.Position = 0;

                // Initialize a new PDF document
                var pdfDocument = new Document();
                // Add a blank page to the document
                var page = pdfDocument.Pages.Add();

                // Create an image object from the barcode stream
                var pdfImage = new Aspose.Pdf.Image
                {
                    ImageStream = barcodeStream,
                    // Set desired dimensions for the barcode image
                    FixWidth = 200.0,
                    FixHeight = 100.0
                };

                // Insert the barcode image into the PDF page
                page.Paragraphs.Add(pdfImage);

                // Define output PDF file path
                const string pdfPath = "CodabarReport.pdf";
                // Save the PDF document to disk
                pdfDocument.Save(pdfPath);
                // Inform the user that the PDF has been generated
                Console.WriteLine($"PDF report generated: {pdfPath}");
            }
        }
    }
}