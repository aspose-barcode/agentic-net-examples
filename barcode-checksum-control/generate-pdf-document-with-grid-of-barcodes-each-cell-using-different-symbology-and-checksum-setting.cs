using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Define barcode specifications: symbology, text, checksum setting
        var specs = new List<(BaseEncodeType type, string text, EnableChecksum checksum)>
        {
            (EncodeTypes.Code128, "ABC123456", EnableChecksum.Yes),          // Code128 with checksum
            (EncodeTypes.Code39FullASCII, "CODE39*%$", EnableChecksum.No),   // Code39 Full ASCII without checksum
            (EncodeTypes.Codabar, "A123456A", EnableChecksum.No),           // Codabar (checksum not applicable)
            (EncodeTypes.EAN13, "123456789012", EnableChecksum.Yes)        // EAN13 (checksum always used)
        };

        // Create a PDF document
        using (var pdfDoc = new Document())
        {
            // Add a single page
            var page = pdfDoc.Pages.Add();

            // Page dimensions (points)
            double pageWidth = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;

            // Grid configuration (2x2)
            int rows = 2;
            int cols = 2;
            double cellWidth = pageWidth / cols;
            double cellHeight = pageHeight / rows;

            int index = 0;
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    if (index >= specs.Count) break;

                    var spec = specs[index];

                    // Generate barcode image in memory
                    using (var generator = new BarcodeGenerator(spec.type, spec.text))
                    {
                        // Apply checksum setting where applicable
                        generator.Parameters.Barcode.IsChecksumEnabled = spec.checksum;

                        // For demonstration, show checksum text for Code128
                        if (spec.type == EncodeTypes.Code128)
                        {
                            generator.Parameters.Barcode.ChecksumAlwaysShow = true;
                        }

                        // Save barcode to a memory stream as PNG
                        using (var ms = new MemoryStream())
                        {
                            generator.Save(ms, BarCodeImageFormat.Png);
                            ms.Position = 0;

                            // Calculate rectangle for the current cell
                            double llx = col * cellWidth;
                            double lly = pageHeight - (row + 1) * cellHeight;
                            double urx = llx + cellWidth;
                            double ury = lly + cellHeight;
                            var rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                            // Insert the image into the PDF page
                            page.AddImage(ms, rect, (int)cellWidth, (int)cellHeight, true);
                        }
                    }

                    index++;
                }
            }

            // Save the PDF document
            pdfDoc.Save("BarcodesGrid.pdf");
        }

        Console.WriteLine("PDF with barcode grid generated: BarcodesGrid.pdf");
    }
}