using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

/// <summary>
/// Demonstrates how to generate a barcode image and embed it into a PDF document using Aspose libraries.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a POSTNET barcode, inserts it into the first page of an existing PDF, and saves the result.
    /// </summary>
    static void Main()
    {
        // Define input and output PDF file paths.
        string inputPdfPath = "input.pdf";
        string outputPdfPath = "output.pdf";

        // Verify that the input PDF file exists before proceeding.
        if (!File.Exists(inputPdfPath))
        {
            Console.WriteLine($"Input PDF file not found: {inputPdfPath}");
            return;
        }

        // Create a memory stream to hold the generated barcode image.
        using (MemoryStream barcodeStream = new MemoryStream())
        {
            // Generate a POSTNET barcode with the value "12345".
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Postnet, "12345"))
            {
                // Optional: set barcode color if needed.
                // generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

                // Save the barcode image to the memory stream in PNG format.
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                // Reset stream position to the beginning for later reading.
                barcodeStream.Position = 0;
            }

            // Load the existing PDF document.
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Access the first page of the PDF.
                Page page = pdfDocument.Pages[1];

                // Create an image object that references the barcode stream.
                Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image
                {
                    ImageStream = barcodeStream,
                    FixWidth = 150f,   // Set desired width of the barcode image.
                    FixHeight = 50f,   // Set desired height of the barcode image.
                    Margin = new MarginInfo { Left = 100f, Top = 200f } // Position the image on the page.
                };

                // Add the image to the page's paragraph collection.
                page.Paragraphs.Add(pdfImage);

                // Save the modified PDF to the specified output path.
                pdfDocument.Save(outputPdfPath);
            }
        }
    }
}