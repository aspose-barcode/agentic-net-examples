// Title: Batch PDF Barcode Extraction with Multithreaded Processing
// Description: Demonstrates how to generate a barcode, embed it in a PDF, extract each page as an image, and decode barcodes using Aspose.BarCode with multithreading enabled.
// Category-Description: This example belongs to the Aspose.BarCode for .NET PDF barcode extraction category. It shows how to use Aspose.Pdf to render PDF pages to images and Aspose.BarCode.BarCodeRecognition to read barcodes, leveraging BarCodeReader.ProcessorSettings for parallel processing. Developers often need to process large PDF batches, extract embedded barcodes, and improve performance with multi‑core utilization.
// Prompt: Develop a batch job that extracts images from PDF files and decodes barcodes with multithreading enabled.
// Tags: code128, barcode generation, barcode recognition, pdf, multithreading, aspose.barcode, aspose.pdf

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

/// <summary>
/// Demonstrates batch processing of a PDF to extract images and decode barcodes using multithreading.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a sample barcode, embeds it in a PDF, then reads barcodes from each page.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for intermediate files
        string tempFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define paths for the generated barcode image and PDF document
        string barcodeImagePath = Path.Combine(tempFolder, "barcode.png");
        string pdfPath = Path.Combine(tempFolder, "sample.pdf");

        // Generate a sample Code128 barcode image
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            generator.Save(barcodeImagePath, BarCodeImageFormat.Png);
        }

        // Create a new PDF document and embed the generated barcode image
        using (var pdfDoc = new Document())
        {
            var page = pdfDoc.Pages.Add();
            var image = new Aspose.Pdf.Image { File = barcodeImagePath };
            page.Paragraphs.Add(image);
            pdfDoc.Save(pdfPath);
        }

        // Verify that the PDF file was created successfully
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine("PDF file not found. Exiting.");
            return;
        }

        // Enable multithreaded barcode processing (uses all available CPU cores)
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;

        // Open the PDF document for reading
        using (var pdfDoc = new Document(pdfPath))
        {
            int pageCount = pdfDoc.Pages.Count;
            var pageNumbers = Enumerable.Range(1, pageCount).ToList();

            // Iterate through each page; barcode reading itself runs on multiple threads
            foreach (int pageNumber in pageNumbers)
            {
                // Convert the current PDF page to an image using PdfConverter
                using (var pdfConverter = new PdfConverter(pdfDoc))
                {
                    pdfConverter.RenderingOptions.BarcodeOptimization = true;
                    pdfConverter.Resolution = new Resolution(300);
                    pdfConverter.StartPage = pageNumber;
                    pdfConverter.EndPage = pageNumber;
                    pdfConverter.DoConvert();

                    // Retrieve the rendered image into a memory stream
                    using (var ms = new MemoryStream())
                    {
                        pdfConverter.GetNextImage(ms);
                        ms.Position = 0;

                        try
                        {
                            // Decode all supported barcode types from the image
                            using (var reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
                            {
                                foreach (var result in reader.ReadBarCodes())
                                {
                                    Console.WriteLine($"Page {pageNumber}: Type={result.CodeTypeName}, Text={result.CodeText}, Quality={result.ReadingQuality}");
                                }
                            }
                        }
                        catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
                        {
                            Console.WriteLine($"Page {pageNumber}: Unable to load image for barcode reading.");
                        }
                    }
                }
            }
        }

        // Cleanup temporary files (optional)
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}