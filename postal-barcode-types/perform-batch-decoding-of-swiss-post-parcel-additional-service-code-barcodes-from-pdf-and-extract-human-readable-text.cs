using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

/// <summary>
/// Demonstrates how to extract Swiss Post Parcel barcodes from a PDF file using Aspose libraries.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Accepts an optional PDF file path as a command‑line argument, renders each page to an image,
    /// and reads Swiss Post Parcel barcodes from the rendered images.
    /// </summary>
    /// <param name="args">Command‑line arguments; the first argument may be a PDF file path.</param>
    static void Main(string[] args)
    {
        // Determine PDF file path: use first argument if provided, otherwise fallback to "sample.pdf".
        string pdfPath = args.Length > 0 ? args[0] : "sample.pdf";

        // Verify that the specified PDF file exists before proceeding.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document into Aspose.Pdf's Document object.
        var pdfDocument = new Document(pdfPath);

        // Initialize a PdfConverter to render PDF pages as images.
        using (var pdfConverter = new PdfConverter(pdfDocument))
        {
            // Enable barcode optimization to improve barcode detection performance.
            pdfConverter.RenderingOptions.BarcodeOptimization = true;

            // Determine the total number of pages in the PDF.
            int pageCount = pdfDocument.Pages.Count;

            // Limit processing to the first four pages when running in evaluation mode.
            int maxPages = Math.Min(pageCount, 4);

            // Iterate over each page up to the defined limit.
            for (int page = 1; page <= maxPages; page++)
            {
                // Configure the converter to process a single page.
                pdfConverter.StartPage = page;
                pdfConverter.EndPage = page;

                // Perform the conversion for the current page.
                pdfConverter.DoConvert();

                // Capture the rendered page image into a memory stream.
                using (var imageStream = new MemoryStream())
                {
                    pdfConverter.GetNextImage(imageStream);
                    imageStream.Position = 0; // Reset stream position for reading.

                    // Initialize a BarCodeReader to detect Swiss Post Parcel barcodes in the image.
                    using (var reader = new BarCodeReader(imageStream, DecodeType.SwissPostParcel))
                    {
                        // Read all barcodes found on the page.
                        var results = reader.ReadBarCodes();

                        // Output results based on whether any barcodes were detected.
                        if (results.Length == 0)
                        {
                            Console.WriteLine($"Page {page}: No Swiss Post Parcel barcode found.");
                        }
                        else
                        {
                            foreach (var result in results)
                            {
                                Console.WriteLine($"Page {page}: Detected barcode type '{result.CodeTypeName}' with text '{result.CodeText}'.");
                            }
                        }
                    }
                }
            }
        }
    }
}