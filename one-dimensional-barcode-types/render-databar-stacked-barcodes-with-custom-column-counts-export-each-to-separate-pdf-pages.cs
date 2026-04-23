using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Define sample data for DataBar stacked barcodes with different column counts
        var columnCounts = new int[] { 2, 4, 6 };
        var codeText = "01234567890123"; // Sample numeric code text suitable for DataBar

        // List to hold image streams until the PDF is saved
        var imageStreams = new List<MemoryStream>();

        // Generate a barcode image for each column count
        foreach (int columns in columnCounts)
        {
            // Create a barcode generator for DataBar stacked symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.DatabarStacked, codeText))
            {
                // Set the custom number of columns
                generator.Parameters.Barcode.DataBar.Columns = columns;

                // Optional: adjust other parameters if needed
                // generator.Parameters.Barcode.DataBar.Rows = 1;
                // generator.Parameters.Barcode.DataBar.AspectRatio = 1.0f;

                // Save the barcode image to a memory stream in PNG format
                var ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading
                imageStreams.Add(ms);
            }
        }

        // Create a PDF document and add each barcode image to a separate page
        using (var pdfDoc = new Document())
        {
            foreach (var imgStream in imageStreams)
            {
                // Add a new page
                var page = pdfDoc.Pages.Add();

                // Define the rectangle where the image will be placed (points)
                // Adjust the size as needed; here we use 500x300 points
                var rect = new Aspose.Pdf.Rectangle(0, 0, 500, 300);

                // Add the image to the page using the stream
                page.AddImage(imgStream, rect);
            }

            // Save the PDF to a file
            pdfDoc.Save("DataBarStacked.pdf");
        }

        // Dispose all image streams after the PDF has been saved
        foreach (var ms in imageStreams)
        {
            ms.Dispose();
        }

        Console.WriteLine("PDF with DataBar stacked barcodes created successfully.");
    }
}