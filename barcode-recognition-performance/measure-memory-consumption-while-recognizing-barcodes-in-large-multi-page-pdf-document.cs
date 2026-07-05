// Title: Memory consumption measurement while recognizing barcodes in a PDF
// Description: Demonstrates rendering PDF pages to images, recognizing all supported barcodes, and measuring memory usage per page.
// Prompt: Measure memory consumption while recognizing barcodes in a large multi‑page PDF document.
// Tags: barcode, recognition, pdf, memory, aspose.barcode, aspose.pdf, aspose.drawing

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Drawing;

/// <summary>
/// Sample console application that renders PDF pages to images, reads all supported barcodes,
/// and reports the memory consumption for each processed page.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Path to the PDF file to be processed.
        const string pdfPath = "sample.pdf";

        // Validate that the PDF file exists before attempting to load it.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document into Aspose.Pdf.Document.
        using (var pdfDocument = new Document(pdfPath))
        {
            // Initialize a PdfConverter to render PDF pages as images.
            using (var pdfConverter = new PdfConverter(pdfDocument))
            {
                // Enable barcode optimization to improve rendering performance for barcode‑rich pages.
                pdfConverter.RenderingOptions.BarcodeOptimization = true;

                // Process a limited number of pages (up to 4) to keep the example lightweight.
                int maxPages = Math.Min(pdfDocument.Pages.Count, 4);
                long totalMemoryUsed = 0;

                // Iterate through each page to render, recognize barcodes, and measure memory.
                for (int pageNumber = 1; pageNumber <= maxPages; pageNumber++)
                {
                    // Configure the converter to process the current page only.
                    pdfConverter.StartPage = pageNumber;
                    pdfConverter.EndPage = pageNumber;
                    pdfConverter.DoConvert();

                    // Render the page into a memory stream (image format).
                    using (var imageStream = new MemoryStream())
                    {
                        pdfConverter.GetNextImage(imageStream);
                        imageStream.Position = 0; // Reset stream position for reading.

                        // Load the rendered image into an Aspose.Drawing.Bitmap for barcode scanning.
                        using (var bitmap = new Bitmap(imageStream))
                        {
                            // Capture memory usage before barcode recognition.
                            long memoryBefore = Process.GetCurrentProcess().PrivateMemorySize64;
                            long gcBefore = GC.GetTotalMemory(true);

                            // Perform barcode recognition on the bitmap using all supported symbologies.
                            using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
                            {
                                foreach (var result in reader.ReadBarCodes())
                                {
                                    Console.WriteLine($"Page {pageNumber}: Type = {result.CodeTypeName}, Text = {result.CodeText}");
                                }
                            }

                            // Capture memory usage after barcode recognition.
                            long memoryAfter = Process.GetCurrentProcess().PrivateMemorySize64;
                            long gcAfter = GC.GetTotalMemory(true);

                            // Calculate deltas to determine memory impact of the recognition step.
                            long memoryDelta = memoryAfter - memoryBefore;
                            long gcDelta = gcAfter - gcBefore;

                            Console.WriteLine($"Memory used for page {pageNumber}: Process = {memoryDelta} bytes, GC = {gcDelta} bytes");
                            totalMemoryUsed += memoryDelta;
                        }
                    }
                }

                // Output the aggregate memory consumption for all processed pages.
                Console.WriteLine($"Total memory consumed for processing {maxPages} page(s): {totalMemoryUsed} bytes");
            }
        }
    }
}