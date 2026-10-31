// Title: Measure memory usage while recognizing barcodes in a PDF
// Description: Demonstrates how to load a multi‑page PDF, render each page to an image, recognize barcodes, and report memory consumption before and after processing each page.
// Category-Description: This example belongs to the Aspose.BarCode for .NET barcode recognition category. It shows how to combine Aspose.Pdf for PDF rendering with Aspose.BarCode.BarCodeRecognition to extract barcodes from PDF pages. Typical use cases include batch processing of scanned documents, auditing memory footprint of large PDF workloads, and optimizing server‑side barcode scanning pipelines. Developers often need to render PDF pages to bitmap streams, feed them to BarCodeReader, and monitor resource usage.
// Prompt: Measure memory consumption while recognizing barcodes in a large multi‑page PDF document.
// Tags: barcode recognition, memory measurement, pdf, aspose.barcode, aspose.pdf, c#

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

/// <summary>
/// Demonstrates measuring memory consumption while recognizing barcodes in a PDF document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Processes a PDF, renders pages to PNG, reads barcodes, and logs memory usage.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument is optional PDF path.</param>
    static void Main(string[] args)
    {
        // Determine PDF path: use first argument if provided, otherwise default to "sample.pdf"
        string pdfPath = args.Length > 0 ? args[0] : "sample.pdf";

        // Verify that the PDF file exists before proceeding
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Capture process memory usage before any processing
        long processMemoryBefore = Process.GetCurrentProcess().PrivateMemorySize64;
        Console.WriteLine($"Process memory before processing: {processMemoryBefore} bytes");

        // Load the PDF document using Aspose.Pdf
        using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(pdfPath))
        {
            // Set up a PNG device to render pages at 300 DPI
            Aspose.Pdf.Devices.PngDevice pngDevice = new Aspose.Pdf.Devices.PngDevice(new Aspose.Pdf.Devices.Resolution(300));

            // Limit processing to the first 4 pages in evaluation mode
            int pageCount = Math.Min(pdfDoc.Pages.Count, 4);

            // Iterate through each page to render and recognize barcodes
            for (int i = 1; i <= pageCount; i++)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    // Render the current PDF page to a PNG image stored in memory
                    pngDevice.Process(pdfDoc.Pages[i], ms);
                    ms.Position = 0; // Reset stream position for reading

                    // Record managed heap memory before barcode recognition
                    long memBefore = GC.GetTotalMemory(true);

                    // Perform barcode recognition on the rendered image
                    using (BarCodeReader reader = new BarCodeReader(ms))
                    {
                        foreach (var result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"Page {i}: Type={result.CodeTypeName}, Data={result.CodeText}");
                        }
                    }

                    // Record managed heap memory after barcode recognition
                    long memAfter = GC.GetTotalMemory(true);
                    Console.WriteLine($"Page {i} memory before: {memBefore} bytes, after: {memAfter} bytes, diff: {memAfter - memBefore} bytes");
                }
            }
        }

        // Capture process memory usage after processing
        long processMemoryAfter = Process.GetCurrentProcess().PrivateMemorySize64;
        Console.WriteLine($"Process memory after processing: {processMemoryAfter} bytes");
        Console.WriteLine($"Total process memory change: {processMemoryAfter - processMemoryBefore} bytes");
    }
}