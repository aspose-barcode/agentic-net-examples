// Title: Measure memory usage during barcode recognition in a multi-page PDF
// Description: Demonstrates how to load a PDF, convert each page to an image, recognize multiple barcode symbologies, and measure the memory consumed for each page's recognition process.
// Category-Description: This example belongs to the Aspose.BarCode for .NET barcode recognition category. It shows how to use Aspose.Pdf to render PDF pages, Aspose.BarCode.BarCodeRecognition's BarCodeReader to detect barcodes (PDF417, QR, DataMatrix, Aztec), and .NET's GC.GetTotalMemory to monitor memory consumption. Developers working with large documents can use this pattern to benchmark and optimize memory usage when processing many pages.
// Prompt: Measure memory consumption while recognizing barcodes in a large multi‑page PDF document.
// Tags: barcode recognition memory pdf aspose.pdf aspose.barcode pdf417 qr datamatrix aztec

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

/// <summary>
/// Demonstrates measuring memory consumption while recognizing barcodes in a multi‑page PDF document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts an optional PDF file path, renders each page to an image, reads barcodes,
    /// and reports memory used for the recognition of each page.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument can be the PDF file path.</param>
    static void Main(string[] args)
    {
        // Determine PDF file path: use first argument if provided, otherwise default to "sample.pdf".
        string pdfPath = args.Length > 0 ? args[0] : "sample.pdf";

        // Verify that the PDF file exists before proceeding.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document.
        using (var pdfDoc = new Document(pdfPath))
        {
            // Initialize a PdfConverter to render PDF pages as images.
            using (var pdfConverter = new PdfConverter(pdfDoc))
            {
                // Enable barcode optimization for faster rendering of barcode regions.
                pdfConverter.RenderingOptions.BarcodeOptimization = true;
                // Set the rendering resolution (DPI).
                pdfConverter.Resolution = new Resolution(300);

                // Iterate through each page in the PDF.
                for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
                {
                    // Configure the converter to process a single page.
                    pdfConverter.StartPage = pageNumber;
                    pdfConverter.EndPage = pageNumber;
                    pdfConverter.DoConvert();

                    // Capture the rendered page image into a memory stream.
                    using (var ms = new MemoryStream())
                    {
                        pdfConverter.GetNextImage(ms);
                        ms.Position = 0; // Reset stream position for reading.

                        // Record memory usage before barcode recognition.
                        long before = GC.GetTotalMemory(true);

                        // Create a BarCodeReader to detect specified barcode types.
                        using (var reader = new BarCodeReader(ms,
                            DecodeType.Pdf417,
                            DecodeType.QR,
                            DecodeType.DataMatrix,
                            DecodeType.Aztec))
                        {
                            // Iterate through all detected barcodes on the page.
                            foreach (var result in reader.ReadBarCodes())
                            {
                                Console.WriteLine($"Page {pageNumber}: Type={result.CodeTypeName}, Text={result.CodeText}");
                            }
                        }

                        // Record memory usage after barcode recognition.
                        long after = GC.GetTotalMemory(true);
                        long used = after - before;
                        Console.WriteLine($"Page {pageNumber}: Memory used for recognition: {used} bytes");
                    }
                }
            }
        }
    }
}