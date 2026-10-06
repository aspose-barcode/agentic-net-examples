// Title: Measure memory usage while recognizing barcodes in a multi‑page PDF
// Description: Demonstrates rendering PDF pages to PNG images, recognizing barcodes on each page, and measuring the memory consumed during the recognition process.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating how to work with PDF documents using Aspose.Pdf, render pages with Aspose.Pdf.Devices, and decode barcodes with Aspose.BarCode.BarCodeRecognition. Typical scenarios include scanning large PDFs for embedded barcodes, profiling memory usage, and optimizing batch processing pipelines. Developers often need to combine Document, PngDevice, and BarCodeReader classes to extract barcode data efficiently from multi‑page documents.
// Prompt: Measure memory consumption while recognizing barcodes in a large multi‑page PDF document.
// Tags: barcode, recognition, pdf, memory, aspose.barcode, aspose.pdf, pngdevice, gc

using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that renders PDF pages to PNG, reads barcodes, and reports memory usage per page.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts an optional PDF file path, processes up to the first four pages,
    /// and outputs barcode information together with memory consumption for each page.
    /// </summary>
    /// <param name="args">Command‑line arguments; the first argument may specify the PDF file path.</param>
    static void Main(string[] args)
    {
        // Determine PDF file path: use argument if supplied, otherwise default to "sample.pdf".
        string pdfPath = args.Length > 0 ? args[0] : "sample.pdf";

        // Verify that the PDF file exists before proceeding.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document.
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Limit processing to a maximum of four pages to keep the demo concise.
            int totalPages = Math.Min(pdfDoc.Pages.Count, 4);
            if (totalPages == 0)
            {
                Console.WriteLine("PDF contains no pages.");
                return;
            }

            // Create a PNG rendering device with a resolution of 300 DPI.
            PngDevice pngDevice = new PngDevice(new Resolution(300));

            // Iterate through each selected page.
            for (int pageNumber = 1; pageNumber <= totalPages; pageNumber++)
            {
                // Render the current page into a memory stream.
                using (MemoryStream pageStream = new MemoryStream())
                {
                    pngDevice.Process(pdfDoc.Pages[pageNumber], pageStream);
                    pageStream.Position = 0; // Reset stream position for reading.

                    // Capture memory usage before barcode recognition.
                    long memoryBefore = GC.GetTotalMemory(true);

                    // Initialize the barcode reader for PDF417, QR, and DataMatrix symbologies.
                    using (BarCodeReader reader = new BarCodeReader(pageStream, DecodeType.Pdf417, DecodeType.QR, DecodeType.DataMatrix))
                    {
                        // Enumerate and display each detected barcode.
                        foreach (var result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"Page {pageNumber}: Type={result.CodeTypeName}, Text={result.CodeText}");
                        }
                    }

                    // Capture memory usage after barcode recognition.
                    long memoryAfter = GC.GetTotalMemory(true);
                    Console.WriteLine($"Memory used for page {pageNumber}: {memoryAfter - memoryBefore} bytes");
                }
            }
        }
    }
}