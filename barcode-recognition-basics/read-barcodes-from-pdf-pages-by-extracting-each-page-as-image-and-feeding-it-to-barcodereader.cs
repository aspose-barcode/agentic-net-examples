using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

/// <summary>
/// Demonstrates barcode detection in PDF pages using Aspose libraries.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Accepts an optional PDF file path argument,
    /// renders each page to an image, and reads any barcodes present.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument may be a PDF file path.</param>
    static void Main(string[] args)
    {
        // Determine PDF file path: use first argument if provided, otherwise default to "sample.pdf".
        string pdfPath = args.Length > 0 ? args[0] : "sample.pdf";

        // Verify that the specified PDF file exists before proceeding.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Open the PDF document for reading.
        using (var pdfDoc = new Document(pdfPath))
        {
            // Create a converter to render PDF pages as images.
            using (var pdfConverter = new PdfConverter(pdfDoc))
            {
                // Enable barcode optimization to improve barcode rendering quality.
                pdfConverter.RenderingOptions.BarcodeOptimization = true;

                // Iterate through each page in the PDF.
                int pageCount = pdfDoc.Pages.Count;
                for (int page = 1; page <= pageCount; page++)
                {
                    // Configure the converter to process a single page.
                    pdfConverter.StartPage = page;
                    pdfConverter.EndPage = page;

                    // Perform the conversion of the specified page to an image.
                    pdfConverter.DoConvert();

                    // Retrieve the rendered image into a memory stream.
                    using (var ms = new MemoryStream())
                    {
                        pdfConverter.GetNextImage(ms);
                        ms.Position = 0; // Reset stream position for reading.

                        // Initialize the barcode reader on the image stream.
                        using (var reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
                        {
                            // Read all barcodes detected in the image.
                            var results = reader.ReadBarCodes();

                            // Output detection results to the console.
                            if (results.Length == 0)
                            {
                                Console.WriteLine($"Page {page}: No barcodes detected.");
                            }
                            else
                            {
                                Console.WriteLine($"Page {page}: Detected {results.Length} barcode(s).");
                                foreach (var result in results)
                                {
                                    Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}