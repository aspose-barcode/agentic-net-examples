// Title: Measure memory usage while recognizing barcodes in a multi‑page PDF
// Description: Demonstrates how to load a PDF, render its pages to images, recognize barcodes, and track memory consumption before and after processing.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, showcasing the use of Document, PngDevice, BarCodeReader, and QualitySettings classes. It illustrates typical scenarios where developers need to process large PDFs, extract barcodes, and monitor resource usage for performance tuning.
// Prompt: Measure memory consumption while recognizing barcodes in a large multi‑page PDF document.
// Tags: barcode, recognition, pdf, memory, consumption, aspose.barcode, aspose.pdf

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

/// <summary>
/// Demonstrates measuring memory consumption while recognizing barcodes in a large multi‑page PDF document.
/// </summary>
class Program
{
    /// <summary>
    /// Main entry point. Loads a PDF, renders up to four pages to PNG images, reads barcodes, and reports memory usage.
    /// </summary>
    static void Main()
    {
        // Path to the PDF document to be processed
        const string pdfPath = "sample.pdf";

        // Verify that the file exists before proceeding
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Capture memory usage before any processing
        Process proc = Process.GetCurrentProcess();
        long memoryBefore = proc.PrivateMemorySize64;

        // Load the PDF document
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Limit processing to a maximum of 4 pages as per the rules
            int pagesToProcess = Math.Min(pdfDocument.Pages.Count, 4);

            // Iterate through each page to be processed
            for (int pageIndex = 1; pageIndex <= pagesToProcess; pageIndex++)
            {
                // Render the current page to a PNG image in memory
                using (MemoryStream imageStream = new MemoryStream())
                {
                    Resolution resolution = new Resolution(300);
                    PngDevice pngDevice = new PngDevice(resolution);
                    pngDevice.Process(pdfDocument.Pages[pageIndex], imageStream);
                    imageStream.Position = 0;

                    // Load the rendered image into an Aspose.Drawing.Bitmap
                    using (Aspose.Drawing.Bitmap bitmap = new Aspose.Drawing.Bitmap(imageStream))
                    {
                        // Initialize the barcode reader for all supported symbologies
                        using (BarCodeReader reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
                        {
                            // Use the highest quality preset to ensure maximum detection
                            reader.QualitySettings = QualitySettings.MaxQuality;

                            // Iterate over detected barcodes and output their details
                            foreach (var result in reader.ReadBarCodes())
                            {
                                Console.WriteLine($"Page {pageIndex}: Type={result.CodeTypeName}, Text={result.CodeText}");
                            }
                        }
                    }
                }
            }
        }

        // Capture memory usage after processing
        long memoryAfter = proc.PrivateMemorySize64;

        // Report memory consumption statistics
        Console.WriteLine($"Memory before processing: {memoryBefore / 1024} KB");
        Console.WriteLine($"Memory after processing : {memoryAfter / 1024} KB");
        Console.WriteLine($"Memory increase          : {(memoryAfter - memoryBefore) / 1024} KB");
    }
}