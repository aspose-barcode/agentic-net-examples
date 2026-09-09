// Title: Generate PDF with a 2x2 barcode grid
// Description: Demonstrates creating a PDF document containing a grid of barcodes, each cell using a distinct symbology and checksum configuration.
// Category-Description: This example belongs to the Aspose.BarCode PDF generation category, showcasing how to use BarcodeGenerator, BarCodeImageFormat, and Aspose.Pdf.Document to embed various barcode types (Code39, Code128, QR, DataMatrix) into a PDF. Typical use cases include batch printing, reports, and inventory labels where multiple barcode formats are required on a single page. Developers often need to configure symbology settings, colors, and error correction levels before rendering barcodes to images and placing them in PDF pages.
// Prompt: Generate a PDF document with a grid of barcodes, each cell using a different symbology and checksum setting.
// Tags: barcode, symbology, pdf, grid, aspose.barcode, aspose.pdf, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Program that creates a PDF file containing a grid of different barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode grid PDF and saves it to the current directory.
    /// </summary>
    static void Main()
    {
        // Define the output PDF file path
        string outputPdf = Path.Combine(Directory.GetCurrentDirectory(), "BarcodesGrid.pdf");

        // Create a new PDF document
        using (var pdfDoc = new Document())
        {
            // Add a single page to the document
            var page = pdfDoc.Pages.Add();

            // Retrieve page dimensions for layout calculations
            double pageWidth = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;

            // Configure a 2x2 grid (rows x columns)
            int rows = 2;
            int cols = 2;
            double cellWidth = pageWidth / cols;
            double cellHeight = pageHeight / rows;

            // Define barcode specifications: type, text, and optional configuration
            var specs = new (BaseEncodeType type, string text, Action<BarcodeGenerator> configure)[]
            {
                // Code39 with checksum disabled
                (EncodeTypes.Code39FullASCII,
                 "CODE39",
                 gen =>
                 {
                     gen.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;
                     gen.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                     gen.Parameters.BackColor = Aspose.Drawing.Color.White;
                 }),

                // Code128 (checksum obligatory)
                (EncodeTypes.Code128,
                 "CODE128",
                 gen =>
                 {
                     gen.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                     gen.Parameters.BackColor = Aspose.Drawing.Color.White;
                 }),

                // QR with high error correction level
                (EncodeTypes.QR,
                 "https://example.com",
                 gen =>
                 {
                     gen.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
                     gen.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                     gen.Parameters.BackColor = Aspose.Drawing.Color.White;
                 }),

                // DataMatrix with a specific version
                (EncodeTypes.DataMatrix,
                 "DataMatrix",
                 gen =>
                 {
                     gen.Parameters.Barcode.DataMatrix.Version = DataMatrixVersion.ECC200_32x32;
                     gen.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                     gen.Parameters.BackColor = Aspose.Drawing.Color.White;
                 })
            };

            // Iterate over each barcode specification and place it in the grid
            for (int i = 0; i < specs.Length; i++)
            {
                int row = i / cols; // Determine current row
                int col = i % cols; // Determine current column

                // Create a barcode generator for the current spec
                using (var generator = new BarcodeGenerator(specs[i].type, specs[i].text))
                {
                    // Apply any custom configuration (e.g., colors, checksum, error level)
                    specs[i].configure?.Invoke(generator);

                    // Render the barcode to a memory stream as PNG
                    using (var ms = new MemoryStream())
                    {
                        generator.Save(ms, BarCodeImageFormat.Png);
                        ms.Position = 0; // Reset stream position for reading

                        // Calculate the rectangle where the image will be placed
                        double llx = col * cellWidth;
                        double lly = pageHeight - (row + 1) * cellHeight;
                        double urx = (col + 1) * cellWidth;
                        double ury = pageHeight - row * cellHeight;
                        var rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                        // Add the barcode image to the PDF page within the calculated rectangle
                        page.AddImage(ms, rect, (int)cellWidth, (int)cellHeight, true);
                    }
                }
            }

            // Save the populated PDF document to disk
            pdfDoc.Save(outputPdf);
        }

        // Inform the user where the PDF was saved
        Console.WriteLine($"PDF with barcode grid saved to: {outputPdf}");
    }
}