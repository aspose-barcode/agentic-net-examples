// Title: Batch PDF Image Extraction and Barcode Decoding with Multithreading
// Description: Demonstrates how to extract page images from PDF files and decode any barcodes found using Aspose.Pdf and Aspose.BarCode in a parallel batch job.
// Category-Description: This example belongs to the Aspose.BarCode and Aspose.Pdf integration category, showing how to combine PDF rendering with barcode recognition. It covers key API classes such as Document, PdfConverter, and BarCodeReader, typical for scenarios like invoice processing, shipping label verification, or bulk document scanning where developers need to efficiently extract images and read barcodes from multiple PDFs concurrently.
// Prompt: Develop a batch job that extracts images from PDF files and decodes barcodes with multithreading enabled.
// Tags: barcode symbology, decoding, image extraction, multithreading, aspose.pdf, aspose.barcode

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that processes PDF files in a folder, extracts each page as an image,
/// and decodes any barcodes found using Aspose.Pdf and Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Accepts an optional folder path argument,
    /// processes up to three PDF files in parallel, and writes barcode results to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument can specify the input folder.</param>
    static void Main(string[] args)
    {
        // Determine input folder: use first argument or default to "./pdfs" relative to the current directory.
        string inputFolder = args.Length > 0
            ? args[0]
            : Path.Combine(Directory.GetCurrentDirectory(), "pdfs");

        // Verify that the input folder exists.
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Retrieve all PDF files in the folder (limit to three files for safe execution in evaluation mode).
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found.");
            return;
        }

        int maxFiles = Math.Min(pdfFiles.Length, 3);
        var filesToProcess = pdfFiles[..maxFiles];

        // Process each selected PDF file in parallel, using a degree of parallelism equal to the processor count.
        Parallel.ForEach(
            filesToProcess,
            new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount },
            pdfPath =>
            {
                try
                {
                    // Load the PDF document.
                    using var pdfDocument = new Document(pdfPath);

                    // Initialize a converter to render PDF pages to images.
                    var converter = new PdfConverter(pdfDocument)
                    {
                        // Enable barcode optimization to improve recognition speed.
                        RenderingOptions = { BarcodeOptimization = true }
                    };

                    // Limit processing to the first four pages (evaluation mode restriction).
                    int pageCount = Math.Min(pdfDocument.Pages.Count, 4);

                    for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
                    {
                        // Configure the converter to process a single page.
                        converter.StartPage = pageNumber;
                        converter.EndPage = pageNumber;
                        converter.DoConvert();

                        // Retrieve the rendered image into a memory stream.
                        using var imageStream = new MemoryStream();
                        converter.GetNextImage(imageStream);
                        imageStream.Position = 0;

                        // Create a barcode reader that attempts to decode all supported symbologies.
                        using var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes);

                        // Iterate through all detected barcodes and output their details.
                        foreach (var result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"{Path.GetFileName(pdfPath)} - Page {pageNumber}: Type={result.CodeTypeName}, Text={result.CodeText}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log any errors that occur while processing the current PDF.
                    Console.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
                }
            });
    }
}