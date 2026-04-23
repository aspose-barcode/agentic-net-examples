using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Define output PDF path
        string pdfPath = "DataBarExpandedStacked.pdf";

        // Prepare a list to hold barcode image streams
        var barcodeStreams = new List<MemoryStream>();

        // Column counts to vary (limited to 4 items for evaluation mode)
        int[] columnCounts = new int[] { 2, 4, 6, 8 };

        // Common codetext for Databar Expanded Stacked (GTIN format)
        string codeText = "(01)12345678901231";

        // Generate barcodes with different column counts
        foreach (int columns in columnCounts)
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.DatabarExpandedStacked, codeText))
            {
                // Set DataBar specific columns
                generator.Parameters.Barcode.DataBar.Columns = columns;

                // Set image size (using Interpolation mode)
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 150f;

                // Set colors
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Save barcode to memory stream as PNG
                var ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset position for reading
                barcodeStreams.Add(ms);
            }
        }

        // Create PDF document and add each barcode image
        using (var pdfDoc = new Document())
        {
            foreach (var stream in barcodeStreams)
            {
                // Add a new page for each barcode
                var page = pdfDoc.Pages.Add();

                // Define image rectangle (left, bottom, right, top)
                var rect = new Aspose.Pdf.Rectangle(0, 0, 500, 200);

                // Add image to the page
                page.AddImage(stream, rect);
            }

            // Save the compiled PDF
            pdfDoc.Save(pdfPath);
        }

        // Dispose all barcode streams
        foreach (var stream in barcodeStreams)
        {
            stream.Dispose();
        }

        Console.WriteLine($"PDF with DataBar Expanded Stacked barcodes saved to: {pdfPath}");
    }
}