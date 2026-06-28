using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

/// <summary>
/// Demonstrates how to read barcodes from PDF files using Aspose.BarCode and Aspose.Pdf.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Processes a list of PDF files (provided via command‑line arguments or a default list),
    /// renders each page to an image, and extracts any barcodes found on the pages.
    /// </summary>
    /// <param name="args">Optional PDF file paths passed as command‑line arguments.</param>
    static void Main(string[] args)
    {
        // ----------------------------------------------------------------------
        // Prepare the list of PDF files to process.
        // ----------------------------------------------------------------------
        // Default sample files – replace with your own paths or pass as command‑line arguments.
        List<string> pdfFiles = new List<string>
        {
            "sample1.pdf",
            "sample2.pdf",
            "sample3.pdf"
        };

        // If arguments are provided, treat them as PDF paths (fallback to the sample list if none).
        if (args.Length > 0)
        {
            pdfFiles = new List<string>(args);
        }

        // Limit processing to a safe number of files for the runner (max 3).
        int maxFiles = Math.Min(3, pdfFiles.Count);
        pdfFiles = pdfFiles.GetRange(0, maxFiles);

        // ----------------------------------------------------------------------
        // Configure barcode reader to utilize all available processor cores.
        // ----------------------------------------------------------------------
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;

        // ----------------------------------------------------------------------
        // Process each PDF file in parallel.
        // ----------------------------------------------------------------------
        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            // Verify that the file exists before attempting to process it.
            if (!File.Exists(pdfPath))
            {
                Console.WriteLine($"File not found: {pdfPath}");
                return;
            }

            try
            {
                // Load the PDF document.
                using (var pdfDoc = new Document(pdfPath))
                {
                    // Create a converter for rendering PDF pages to images.
                    using (var pdfConverter = new PdfConverter(pdfDoc))
                    {
                        // Enable barcode optimization during rendering.
                        pdfConverter.RenderingOptions.BarcodeOptimization = true;

                        // Process each page sequentially (page rendering is not thread‑safe per document).
                        for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
                        {
                            // Set the page range to a single page for conversion.
                            pdfConverter.StartPage = pageNumber;
                            pdfConverter.EndPage = pageNumber;

                            // Perform the conversion for the current page.
                            pdfConverter.DoConvert();

                            // Retrieve the rendered image into a memory stream.
                            using (var imageStream = new MemoryStream())
                            {
                                pdfConverter.GetNextImage(imageStream);
                                imageStream.Position = 0; // Reset stream position for reading.

                                // Recognize barcodes from the rendered page image.
                                using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
                                {
                                    // Optional: use a higher quality preset for better detection.
                                    reader.QualitySettings = QualitySettings.HighQuality;

                                    // Iterate over all detected barcodes.
                                    foreach (var result in reader.ReadBarCodes())
                                    {
                                        Console.WriteLine($"PDF: {Path.GetFileName(pdfPath)} | Page: {pageNumber}");
                                        Console.WriteLine($"  Type: {result.CodeTypeName}");
                                        Console.WriteLine($"  Text: {result.CodeText}");
                                        Console.WriteLine($"  Confidence: {result.Confidence}");
                                        Console.WriteLine($"  ReadingQuality: {result.ReadingQuality}");
                                        Console.WriteLine();
                                    }
                                }
                            }
                        }
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