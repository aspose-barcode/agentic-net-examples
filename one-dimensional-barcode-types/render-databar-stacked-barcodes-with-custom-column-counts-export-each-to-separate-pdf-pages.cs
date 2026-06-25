using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

/// <summary>
/// Demonstrates generating DataBar stacked barcodes, embedding them in a PDF,
/// and saving the resulting document to disk.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcode images, adds them to a PDF,
    /// saves the PDF, and disposes of resources.
    /// </summary>
    static void Main()
    {
        // Define the output PDF file path in the current working directory.
        string pdfPath = Path.Combine(Directory.GetCurrentDirectory(), "DataBarStacked.pdf");

        // Create a new PDF document. Aspose.Pdf.Document does NOT implement IDisposable.
        var pdfDoc = new Document();

        // Collect memory streams for each generated barcode image.
        var barcodeStreams = new List<MemoryStream>();

        // Configuration tuples: barcode type, column count, and a descriptive label.
        var barcodeConfigs = new (BaseEncodeType Type, int Columns, string Description)[]
        {
            (EncodeTypes.DatabarStacked, 2, "Databar Stacked - 2 columns"),
            (EncodeTypes.DatabarStackedOmniDirectional, 3, "Databar Stacked Omni - 3 columns"),
            (EncodeTypes.DatabarExpandedStacked, 4, "Databar Expanded Stacked - 4 columns")
        };

        // Sample codetext that is valid for DataBar symbologies.
        const string codeText = "(01)12345678901231";

        // Iterate over each barcode configuration to generate images and add them to the PDF.
        foreach (var config in barcodeConfigs)
        {
            // Generate a barcode image in memory using the specified type and codetext.
            using (var generator = new BarcodeGenerator(config.Type, codeText))
            {
                // Apply the custom column count for the DataBar barcode.
                generator.Parameters.Barcode.DataBar.Columns = config.Columns;

                // Save the barcode image to a memory stream in PNG format.
                var ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for subsequent reading.

                // Store the stream for later disposal after the PDF is saved.
                barcodeStreams.Add(ms);

                // Add a new page to the PDF document for this barcode.
                var page = pdfDoc.Pages.Add();

                // Create an Aspose.Pdf.Image object that references the barcode stream.
                var pdfImage = new Aspose.Pdf.Image
                {
                    ImageStream = ms
                    // Optional: set fixed dimensions (in points) if desired.
                    // FixWidth = 200.0,
                    // FixHeight = 100.0
                };

                // Insert the image into the page's paragraph collection.
                page.Paragraphs.Add(pdfImage);
            }
        }

        // Persist the PDF document containing all barcode pages to disk.
        pdfDoc.Save(pdfPath);
        Console.WriteLine($"PDF saved to: {pdfPath}");

        // Release all memory streams now that the PDF has been saved.
        foreach (var stream in barcodeStreams)
        {
            stream.Dispose();
        }
    }
}