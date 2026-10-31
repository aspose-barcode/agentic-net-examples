// Title: Batch barcode extraction from PDF with multithreading
// Description: Demonstrates extracting images from each page of a PDF, decoding any barcodes found, and processing pages in parallel using Aspose.BarCode and Aspose.Pdf.
// Category-Description: This example belongs to the Aspose.BarCode PDF processing category, showing how to combine Aspose.Pdf for rendering pages to images and Aspose.BarCode for barcode recognition. Typical use cases include batch scanning of documents, automated data capture, and high‑throughput barcode decoding. Developers often need to enable multithreaded processing to utilize all CPU cores for faster throughput.
// Prompt: Develop a batch job that extracts images from PDF files and decodes barcodes with multithreading enabled.
// Tags: pdf, barcode, multithreading, extraction, aspose.barcode, aspose.pdf, decode, batch

using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Facades;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates a batch job that extracts images from a PDF and decodes barcodes using multithreading.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a sample PDF with an embedded QR code, then reads barcodes from each page in parallel.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for the sample PDF and intermediate files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BatchBarcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Path for the generated sample PDF
        string pdfPath = Path.Combine(tempFolder, "sample.pdf");

        // Generate a PDF that contains a QR code image
        GenerateSamplePdfWithBarcode(pdfPath);

        // Enable multithreading for barcode reading (use all available CPU cores)
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;

        // Open the PDF document for processing
        using (var pdfDoc = new Document(pdfPath))
        {
            int pageCount = pdfDoc.Pages.Count;

            // Build a list of page numbers to feed into Parallel.ForEach
            var pageNumbers = new List<int>();
            for (int i = 1; i <= pageCount; i++) pageNumbers.Add(i);

            // Process each page concurrently
            Parallel.ForEach(pageNumbers, pageNumber =>
            {
                try
                {
                    // Render the current PDF page to a PNG image stored in a memory stream
                    using (MemoryStream pageStream = pdfDoc.Pages[pageNumber].ConvertToPNGMemoryStream())
                    {
                        pageStream.Position = 0;

                        // Initialize the barcode reader for all supported symbologies
                        using (var reader = new BarCodeReader(pageStream, DecodeType.AllSupportedTypes))
                        {
                            // Iterate through all detected barcodes on the page
                            foreach (BarCodeResult result in reader.ReadBarCodes())
                            {
                                // Synchronize console output to avoid interleaved lines
                                lock (Console.Out)
                                {
                                    Console.WriteLine($"Page {pageNumber}: Type={result.CodeTypeName}, Text={result.CodeText}");
                                }
                            }
                        }
                    }
                }
                catch (ArgumentException ex)
                {
                    // Handle cases where the page could not be rendered as an image
                    lock (Console.Out)
                    {
                        Console.WriteLine($"Page {pageNumber}: Skipped - {ex.Message}");
                    }
                }
                catch (Exception ex)
                {
                    // Log any unexpected errors during processing
                    lock (Console.Out)
                    {
                        Console.WriteLine($"Page {pageNumber}: Error - {ex.Message}");
                    }
                }
            });
        }

        // Optional cleanup of the temporary folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Suppress any errors that occur during cleanup
        }
    }

    /// <summary>
    /// Generates a PDF file containing a single QR code image.
    /// </summary>
    /// <param name="pdfPath">Full path where the PDF will be saved.</param>
    static void GenerateSamplePdfWithBarcode(string pdfPath)
    {
        // Create a QR code generator with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Save the generated barcode to a memory stream in PNG format
            using (var barcodeStream = new MemoryStream())
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0;

                // Create a new PDF document and add a page containing the barcode image
                using (var pdfDoc = new Document())
                {
                    var page = pdfDoc.Pages.Add();

                    var image = new Aspose.Pdf.Image
                    {
                        ImageStream = barcodeStream,
                        // Set a fixed width for the image; height scales proportionally
                        FixWidth = 200
                    };
                    page.Paragraphs.Add(image);

                    // Save the PDF to the specified path
                    pdfDoc.Save(pdfPath);
                }
            }
        }
    }
}