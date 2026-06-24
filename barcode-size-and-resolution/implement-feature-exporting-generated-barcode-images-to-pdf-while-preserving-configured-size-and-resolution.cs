using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using Aspose.Pdf;

/// <summary>
/// Demonstrates generating a Code128 barcode, converting it to an image,
/// and embedding that image into a PDF document using Aspose libraries.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, embeds it in a PDF, and saves the PDF to disk.
    /// </summary>
    static void Main()
    {
        // Define the output PDF file path.
        string pdfPath = "barcode_output.pdf";

        // Create a barcode generator for Code128 with the specified data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set image dimensions (in points) and resolution (dpi).
            generator.Parameters.ImageWidth.Point = 200f;   // Width in points.
            generator.Parameters.ImageHeight.Point = 100f;  // Height in points.
            generator.Parameters.Resolution = 300f;         // Resolution in DPI.
            generator.Parameters.AutoSizeMode = AutoSizeMode.None; // Disable auto-sizing.

            // Save the generated barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.

                // Determine the pixel dimensions of the generated PNG image.
                int pixelWidth;
                int pixelHeight;
                using (var bmp = new Bitmap(ms))
                {
                    pixelWidth = bmp.Width;
                    pixelHeight = bmp.Height;
                }

                // Reset stream position again before embedding into PDF.
                ms.Position = 0;

                // Create a new PDF document and add a single page.
                var pdfDoc = new Document();
                var page = pdfDoc.Pages.Add();

                // Convert pixel dimensions to PDF points (1 point = 1/72 inch).
                // points = pixels * 72 / dpi
                double widthInPoints = pixelWidth * 72.0 / generator.Parameters.Resolution;
                double heightInPoints = pixelHeight * 72.0 / generator.Parameters.Resolution;

                // Create an Aspose.Pdf.Image object using the memory stream.
                var pdfImage = new Aspose.Pdf.Image
                {
                    ImageStream = ms,
                    FixWidth = widthInPoints,
                    FixHeight = heightInPoints
                };

                // Add the image to the page's paragraph collection.
                page.Paragraphs.Add(pdfImage);

                // Save the PDF document to the specified file path.
                pdfDoc.Save(pdfPath);
            }
        }

        // Output the full path of the saved PDF file.
        Console.WriteLine($"PDF with barcode saved to: {Path.GetFullPath(pdfPath)}");
    }
}