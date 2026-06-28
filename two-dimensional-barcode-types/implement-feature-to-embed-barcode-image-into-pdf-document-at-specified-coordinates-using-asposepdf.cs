using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode, embedding it into a PDF,
/// and saving the resulting document to disk.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, embeds it in a PDF, and writes the PDF file.
    /// </summary>
    static void Main()
    {
        // Create a barcode generator for Code128 with the specified data.
        using (var barcodeGenerator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Optional: configure barcode appearance (size, colors, etc.) here.

            // Store the generated barcode image in a memory stream.
            using (var barcodeStream = new MemoryStream())
            {
                // Save the barcode as a PNG image into the memory stream.
                barcodeGenerator.Save(barcodeStream, BarCodeImageFormat.Png);
                // Reset stream position to the beginning for reading.
                barcodeStream.Position = 0;

                // Create a new PDF document to hold the barcode image.
                using (var pdfDocument = new Document())
                {
                    // Add a blank page to the PDF.
                    var page = pdfDocument.Pages.Add();

                    // Create an Aspose.Pdf.Image object and configure its source and layout.
                    var pdfImage = new Aspose.Pdf.Image
                    {
                        // Use the barcode image stream as the image source.
                        ImageStream = barcodeStream,
                        // Set the displayed width and height of the image (in points).
                        FixWidth = 200.0,
                        FixHeight = 100.0,
                        // Position the image on the page: 100 points from the left, 500 points from the top.
                        Margin = new MarginInfo { Left = 100.0, Top = 500.0 }
                    };

                    // Add the configured image to the page's paragraph collection.
                    page.Paragraphs.Add(pdfImage);

                    // Save the PDF document to a file on disk.
                    pdfDocument.Save("BarcodeEmbedded.pdf");
                }
            }
        }

        // Inform the user that the PDF was created successfully.
        Console.WriteLine("PDF with embedded barcode created successfully.");
    }
}