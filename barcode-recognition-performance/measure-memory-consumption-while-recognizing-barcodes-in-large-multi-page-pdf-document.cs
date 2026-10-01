// Title: Measure memory usage while recognizing barcodes in a multi‑page PDF
// Description: Demonstrates how to generate a PDF with barcodes, read each page as an image, recognize barcodes, and measure the process memory before and after recognition.
// Category-Description: This example belongs to the Aspose.BarCode for .NET PDF barcode processing category. It shows how to use Aspose.Pdf to render PDF pages to images, Aspose.BarCode.BarCodeRecognition.BarCodeReader to detect all supported barcode types, and System.Diagnostics.Process to monitor memory consumption. Developers working with large PDF documents can use this pattern to evaluate performance and resource usage when extracting barcodes.
// Prompt: Measure memory consumption while recognizing barcodes in a large multi‑page PDF document.
// Tags: barcode, memory, pdf, recognition, aspnet, aspose.barcode, aspose.pdf, decode, performance

using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates measuring memory consumption while recognizing barcodes in a multi‑page PDF document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a sample PDF, measures memory before and after barcode recognition, and outputs results.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary directory for the sample PDF
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodePdfDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string pdfPath = Path.Combine(tempDir, "sample.pdf");

        // Step 1: Create a PDF containing up to 4 pages, each with a barcode
        CreateSamplePdf(pdfPath);

        // Verify that the PDF was successfully created
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine("Failed to create the PDF file.");
            return;
        }

        // Step 2: Record memory usage before barcode recognition
        long memoryBefore = Process.GetCurrentProcess().PrivateMemorySize64;

        // Step 3: Open the PDF and process each page individually
        using (var pdfDocument = new Aspose.Pdf.Document(pdfPath))
        {
            var pdfConverter = new Aspose.Pdf.Facades.PdfConverter(pdfDocument);
            pdfConverter.RenderingOptions.BarcodeOptimization = true; // enable barcode‑specific optimizations

            // Limit processing to a maximum of 4 pages (evaluation limit)
            int totalPages = Math.Min(pdfDocument.Pages.Count, 4);

            for (int pageNumber = 1; pageNumber <= totalPages; pageNumber++)
            {
                // Configure the converter to render a single page
                pdfConverter.StartPage = pageNumber;
                pdfConverter.EndPage = pageNumber;
                pdfConverter.DoConvert();

                // Capture the rendered page as an image stream
                using (var imageStream = new MemoryStream())
                {
                    pdfConverter.GetNextImage(imageStream);
                    imageStream.Position = 0;

                    // Recognize all supported barcodes in the page image
                    using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
                    {
                        foreach (BarCodeResult result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"Page {pageNumber}: Type={result.CodeTypeName}, Text={result.CodeText}");
                        }
                    }
                }
            }

            pdfConverter.Close();
        }

        // Step 4: Record memory usage after barcode recognition
        long memoryAfter = Process.GetCurrentProcess().PrivateMemorySize64;
        long memoryDiff = memoryAfter - memoryBefore;

        // Output memory consumption details
        Console.WriteLine($"Memory before: {memoryBefore / 1024} KB");
        Console.WriteLine($"Memory after : {memoryAfter / 1024} KB");
        Console.WriteLine($"Memory increase: {memoryDiff / 1024} KB");

        // Cleanup temporary files and directories
        try
        {
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored – cleanup failures should not affect program exit
        }
    }

    // Generates a PDF with up to 4 pages, each containing a simple Code128 barcode
    private static void CreateSamplePdf(string outputPath)
    {
        // Keep image streams alive until the PDF is saved
        var imageStreams = new List<MemoryStream>();

        // Create a new PDF document
        var pdfDoc = new Aspose.Pdf.Document();

        // Sample barcode texts (one per page)
        string[] codes = { "ABC123", "9876543210", "HELLO2023", "BARCODE4" };
        int pageCount = Math.Min(codes.Length, 4); // respect evaluation limit

        for (int i = 0; i < pageCount; i++)
        {
            // Generate a barcode image for the current text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codes[i]))
            {
                var ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;
                imageStreams.Add(ms);

                // Add a new page to the PDF and place the barcode image on it
                var page = pdfDoc.Pages.Add();
                var pdfImage = new Aspose.Pdf.Image { ImageStream = ms };
                page.Paragraphs.Add(pdfImage);
            }
        }

        // Save the assembled PDF to the specified path
        pdfDoc.Save(outputPath);

        // Dispose all image streams after the PDF has been saved
        foreach (var ms in imageStreams)
        {
            ms.Dispose();
        }
    }
}