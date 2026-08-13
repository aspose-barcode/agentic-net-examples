// Title: Decode RM4SCC barcode from a PDF document
// Description: This example creates a PDF file with an embedded RM4SCC barcode, then reads the PDF, renders each page to an image, and decodes the barcode to extract its original data.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition within PDF files using Aspose.Pdf. It covers creating a barcode image with BarcodeGenerator, inserting it into a PDF via Aspose.Pdf.Document, and extracting barcode data using BarCodeReader on rendered page images. Ideal for developers needing to embed and later read RM4SCC (or other) barcodes in PDF workflows.
// Prompt: Decode an RM4SCC barcode embedded in a PDF page and extract the original data.
// Tags: rm4scc, barcode, decode, pdf, aspose.barcode, aspose.pdf, generation, recognition

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

/// <summary>
/// Demonstrates creating a PDF with an RM4SCC barcode and then decoding that barcode from the PDF.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a PDF with a barcode if needed and then decodes it.
    /// </summary>
    static void Main()
    {
        // Define the full path to the sample PDF file.
        string pdfPath = Path.Combine(Directory.GetCurrentDirectory(), "sample.pdf");

        // Create a PDF containing an RM4SCC barcode if the file does not already exist.
        if (!File.Exists(pdfPath))
        {
            CreatePdfWithRm4sccBarcode(pdfPath);
        }

        // Decode the RM4SCC barcode from the existing PDF.
        DecodeRm4sccFromPdf(pdfPath);
    }

    /// <summary>
    /// Generates a PDF document that contains a single RM4SCC barcode image.
    /// </summary>
    /// <param name="pdfPath">The file path where the PDF will be saved.</param>
    static void CreatePdfWithRm4sccBarcode(string pdfPath)
    {
        // Sample data to encode in the RM4SCC barcode.
        const string barcodeText = "1234567890";

        // Generate the barcode image into a memory stream (PNG format).
        using (var barcodeStream = new MemoryStream())
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.RM4SCC, barcodeText))
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
            }

            // Reset stream position before reading.
            barcodeStream.Position = 0;

            // Create a new PDF document and add a page.
            var pdfDoc = new Document();
            var page = pdfDoc.Pages.Add();

            // Create an image object from the barcode stream and set its width.
            var image = new Aspose.Pdf.Image
            {
                ImageStream = barcodeStream,
                FixWidth = 200
            };

            // Add the image to the page's paragraph collection.
            page.Paragraphs.Add(image);

            // Save the PDF to the specified path.
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF created at: {pdfPath}");
    }

    /// <summary>
    /// Loads a PDF, renders each page to an image, and attempts to read an RM4SCC barcode from each page.
    /// </summary>
    /// <param name="pdfPath">The path to the PDF file to be processed.</param>
    static void DecodeRm4sccFromPdf(string pdfPath)
    {
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document.
        using (var pdfDocument = new Document(pdfPath))
        {
            // Initialize the PDF converter which will render pages to images.
            using (var pdfConverter = new PdfConverter(pdfDocument))
            {
                // Enable barcode optimization to improve detection speed.
                pdfConverter.RenderingOptions.BarcodeOptimization = true;

                // Limit processing to the first four pages (or fewer if the document is shorter).
                int maxPages = Math.Min(pdfDocument.Pages.Count, 4);
                for (int pageNumber = 1; pageNumber <= maxPages; pageNumber++)
                {
                    // Configure the converter to process a single page.
                    pdfConverter.StartPage = pageNumber;
                    pdfConverter.EndPage = pageNumber;
                    pdfConverter.DoConvert();

                    // Retrieve the rendered page image into a memory stream.
                    using (var pageImageStream = new MemoryStream())
                    {
                        pdfConverter.GetNextImage(pageImageStream);
                        pageImageStream.Position = 0;

                        // Use BarCodeReader to detect RM4SCC barcodes in the page image.
                        using (var reader = new BarCodeReader(pageImageStream, DecodeType.RM4SCC))
                        {
                            var results = reader.ReadBarCodes();

                            if (results.Length == 0)
                            {
                                Console.WriteLine($"No RM4SCC barcode found on page {pageNumber}.");
                            }
                            else
                            {
                                foreach (var result in results)
                                {
                                    Console.WriteLine($"Page {pageNumber} - Detected RM4SCC barcode:");
                                    Console.WriteLine($"  Code Text: {result.CodeText}");
                                    Console.WriteLine($"  Code Type: {result.CodeTypeName}");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}