// Title: Multithreaded PDF Image Extraction and Barcode Decoding Example
// Description: Demonstrates a batch job that extracts images from PDF files and decodes any embedded barcodes using Aspose.BarCode and Aspose.Pdf.
// Category-Description: This example belongs to the Aspose.BarCode PDF processing category, showcasing how to combine Aspose.Pdf for image extraction with Aspose.BarCode for barcode recognition. It covers key API classes such as Document, PdfConverter, BarCodeReader, and BarcodeGenerator, typical for developers who need to automate barcode scanning from PDF documents in high‑throughput scenarios.
// Prompt: Develop a batch job that extracts images from PDF files and decodes barcodes with multithreading enabled.
// Tags: barcode, pdf, multithreading, aspose.barcode, aspose.pdf, code128, image-extraction, barcode-recognition

using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates extracting images from PDF files and decoding barcodes using Aspose libraries with parallel processing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Sets up input/output folders, creates a sample PDF if needed,
    /// and processes PDF files in parallel to extract images and read barcodes.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define input and output directories relative to the current working directory.
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "InputPdfs");
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "OutputResults");

        // Ensure the required folders exist.
        if (!Directory.Exists(inputFolder))
            Directory.CreateDirectory(inputFolder);
        if (!Directory.Exists(outputFolder))
            Directory.CreateDirectory(outputFolder);

        // Retrieve all PDF files from the input folder.
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");

        // If no PDFs are present, create a sample PDF containing a barcode.
        if (pdfFiles.Length == 0)
        {
            CreateSamplePdf(Path.Combine(inputFolder, "Sample.pdf"));
            pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        }

        // Limit processing to a maximum of five files for safety.
        var filesToProcess = new List<string>(pdfFiles);
        if (filesToProcess.Count > 5)
            filesToProcess = filesToProcess.GetRange(0, 5);

        // Configure parallel execution to use all available processor cores.
        ParallelOptions parallelOptions = new ParallelOptions
        {
            MaxDegreeOfParallelism = Environment.ProcessorCount
        };

        // Process each PDF file concurrently.
        Parallel.ForEach(filesToProcess, parallelOptions, pdfPath =>
        {
            try
            {
                ProcessPdf(pdfPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing '{Path.GetFileName(pdfPath)}': {ex.Message}");
            }
        });

        Console.WriteLine("Processing completed.");
    }

    // Generates a simple PDF containing a Code128 barcode image.
    private static void CreateSamplePdf(string pdfPath)
    {
        // Create a barcode generator for Code128 with sample text.
        using (var generator = new Aspose.BarCode.Generation.BarcodeGenerator(Aspose.BarCode.Generation.EncodeTypes.Code128, "Sample123"))
        {
            // Render the barcode to a bitmap.
            using (Aspose.Drawing.Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the bitmap to a memory stream as PNG.
                using (var imageStream = new MemoryStream())
                {
                    bitmap.Save(imageStream, ImageFormat.Png);
                    imageStream.Position = 0;

                    // Create a new PDF document and embed the barcode image.
                    var doc = new Document();
                    var page = doc.Pages.Add();
                    var pdfImage = new Aspose.Pdf.Image
                    {
                        ImageStream = imageStream
                    };
                    page.Paragraphs.Add(pdfImage);
                    doc.Save(pdfPath);
                }
            }
        }
    }

    // Extracts images from a PDF and decodes any barcodes found on each page.
    private static void ProcessPdf(string pdfPath)
    {
        // Load the PDF document.
        using (var pdfDocument = new Document(pdfPath))
        {
            int totalPages = pdfDocument.Pages.Count;
            // Process up to the first four pages to limit workload.
            int pagesToProcess = Math.Min(totalPages, 4);

            // Initialize a PdfConverter for image rendering.
            using (var pdfConverter = new PdfConverter(pdfDocument))
            {
                pdfConverter.RenderingOptions.BarcodeOptimization = true;

                // Iterate through the selected pages.
                for (int pageNumber = 1; pageNumber <= pagesToProcess; pageNumber++)
                {
                    pdfConverter.StartPage = pageNumber;
                    pdfConverter.EndPage = pageNumber;
                    pdfConverter.DoConvert();

                    // Retrieve the rendered page image.
                    using (var imageStream = new MemoryStream())
                    {
                        pdfConverter.GetNextImage(imageStream);
                        imageStream.Position = 0;

                        // Use BarCodeReader to detect and decode any barcodes in the image.
                        using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
                        {
                            foreach (var result in reader.ReadBarCodes())
                            {
                                Console.WriteLine($"File: {Path.GetFileName(pdfPath)}, Page: {pageNumber}, Type: {result.CodeTypeName}, Text: {result.CodeText}");
                            }
                        }
                    }
                }
            }
        }
    }
}