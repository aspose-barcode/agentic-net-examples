using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Demonstrates generating a Code16K barcode and embedding it into a PDF document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Code16K barcode, inserts it into a PDF,
    /// and saves the PDF to disk.
    /// </summary>
    static void Main()
    {
        // Define the output PDF file name.
        const string pdfPath = "code16k.pdf";

        // Initialize a barcode generator for Code16K with sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, "12345678901234567890"))
        {
            // Set quiet zone coefficients to align barcode margins with printer margins.
            generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = 20;   // left quiet zone (in x-dimension units)
            generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = 20;  // right quiet zone (in x-dimension units)

            // Increase resolution for higher print quality.
            generator.Parameters.Resolution = 300f;

            // Render the barcode to a memory stream in PNG format.
            var barcodeStream = new MemoryStream();
            generator.Save(barcodeStream, BarCodeImageFormat.Png);
            barcodeStream.Position = 0; // Reset stream position for reading.

            // Create a new PDF document and add a page.
            var pdfDoc = new Document();
            var page = pdfDoc.Pages.Add();

            // Create an image object that uses the barcode stream.
            var pdfImage = new Aspose.Pdf.Image
            {
                ImageStream = barcodeStream,
                // Set the displayed dimensions of the barcode image.
                FixWidth = 300.0,
                FixHeight = 150.0
            };

            // Add the image to the page's paragraph collection.
            page.Paragraphs.Add(pdfImage);

            // Save the PDF document to the specified path.
            pdfDoc.Save(pdfPath);

            // Release the memory stream resources after saving the PDF.
            barcodeStream.Dispose();
        }

        // Output the full path of the generated PDF file.
        Console.WriteLine($"PDF with Code16K barcode saved to: {Path.GetFullPath(pdfPath)}");
    }
}