using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Demonstrates generating a Codabar barcode, saving it as PNG,
/// embedding the image into a PDF, and saving the PDF report.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, creates a PDF with the barcode image,
    /// and writes the output files to disk.
    /// </summary>
    static void Main()
    {
        // Define file paths for the generated PNG and PDF files
        string pngPath = "codabar.png";
        string pdfPath = "report.pdf";

        // Generate a Codabar barcode with start symbol 'A' and stop symbol 'D'
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "123456"))
        {
            // Configure start and stop symbols for the Codabar barcode
            generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.A;
            generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.D;

            // Save the generated barcode as a PNG image
            generator.Save(pngPath, BarCodeImageFormat.Png);
        }

        // Create a new PDF document and add a page to it
        var pdfDoc = new Document();
        var page = pdfDoc.Pages.Add();

        // Load the PNG image bytes into a memory stream (kept open until PDF is saved)
        byte[] pngBytes = File.ReadAllBytes(pngPath);
        var imageStream = new MemoryStream(pngBytes);

        // Create an Aspose.Pdf.Image object using the memory stream
        var pdfImage = new Aspose.Pdf.Image
        {
            ImageStream = imageStream,
            // Set desired dimensions for the image within the PDF
            FixWidth = 200.0,
            FixHeight = 100.0
        };

        // Add the image to the PDF page's paragraph collection
        page.Paragraphs.Add(pdfImage);

        // Save the PDF document to the specified path
        pdfDoc.Save(pdfPath);

        // Dispose of the memory stream to release resources
        imageStream.Dispose();

        // Output the full paths of the generated files to the console
        Console.WriteLine($"Barcode image saved to: {Path.GetFullPath(pngPath)}");
        Console.WriteLine($"PDF report saved to: {Path.GetFullPath(pdfPath)}");
    }
}