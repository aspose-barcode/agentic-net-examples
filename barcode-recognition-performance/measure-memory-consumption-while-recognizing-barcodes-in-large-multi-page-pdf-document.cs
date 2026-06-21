using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

/// <summary>
/// Demonstrates barcode recognition in PDF pages using Aspose libraries.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Accepts an optional PDF file path argument, processes up to four pages,
    /// converts each page to an image, and reads any barcodes present.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument may be a PDF file path.</param>
    static void Main(string[] args)
    {
        // Determine PDF file path: use first argument if supplied, otherwise default to "sample.pdf".
        string pdfPath = args.Length > 0 ? args[0] : "sample.pdf";

        // Verify that the file exists before proceeding.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Open the PDF document within a using block to ensure proper disposal.
        using (var pdfDoc = new Document(pdfPath))
        {
            // Process a maximum of four pages to stay within evaluation limits.
            int pagesToProcess = Math.Min(pdfDoc.Pages.Count, 4);

            // Iterate over each page to be processed.
            for (int page = 1; page <= pagesToProcess; page++)
            {
                // Create a PdfConverter for the current page.
                using (var pdfConverter = new PdfConverter(pdfDoc))
                {
                    // Enable barcode optimization for better recognition performance.
                    pdfConverter.RenderingOptions.BarcodeOptimization = true;

                    // Restrict conversion to the current page only.
                    pdfConverter.StartPage = page;
                    pdfConverter.EndPage = page;

                    // Perform the conversion.
                    pdfConverter.DoConvert();

                    // Store the resulting image in a memory stream.
                    using (var imageStream = new MemoryStream())
                    {
                        pdfConverter.GetNextImage(imageStream);
                        imageStream.Position = 0; // Reset stream position for reading.

                        // Capture memory usage before barcode recognition.
                        long memoryBefore = Process.GetCurrentProcess().PrivateMemorySize64;

                        // Initialize the barcode reader for all supported types.
                        using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
                        {
                            // Enumerate and output each detected barcode.
                            foreach (var result in reader.ReadBarCodes())
                            {
                                Console.WriteLine($"Page {page}: Type={result.CodeTypeName}, Text={result.CodeText}");
                            }
                        }

                        // Capture memory usage after barcode recognition.
                        long memoryAfter = Process.GetCurrentProcess().PrivateMemorySize64;
                        long diffKB = (memoryAfter - memoryBefore) / 1024;

                        // Output memory consumption details.
                        Console.WriteLine($"Page {page}: Memory before={memoryBefore / 1024} KB, after={memoryAfter / 1024} KB, diff={diffKB} KB");
                    }
                }
            }
        }
    }
}