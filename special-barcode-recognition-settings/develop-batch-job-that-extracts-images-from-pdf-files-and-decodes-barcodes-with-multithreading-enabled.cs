// Title: Batch PDF Barcode Extraction with Multithreading
// Description: Demonstrates extracting images from a PDF, decoding embedded barcodes, and processing pages in parallel using Aspose.BarCode and Aspose.Pdf.
// Category-Description: This example belongs to the Aspose.BarCode PDF processing collection, showing how to generate a barcode, embed it in a PDF, and then read it back using the BarCodeReader with multithreaded execution. It highlights key API classes such as BarcodeGenerator, Document, PdfConverter, and BarCodeReader, which developers commonly use for batch barcode extraction from documents.
// Prompt: Develop a batch job that extracts images from PDF files and decodes barcodes with multithreading enabled.
// Tags: code128, barcode generation, barcode recognition, pdf, multithreading, aspose.barcode, aspose.pdf

using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Sample program that creates a PDF with a barcode, then extracts each page as an image and decodes the barcode using multithreading.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a sample PDF, enables parallel barcode reading, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample PDF and intermediate files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BatchBarCode_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the path for the sample PDF
        string pdfPath = Path.Combine(tempFolder, "sample.pdf");

        // ------------------------------------------------------------
        // Generate a sample barcode image and embed it into a new PDF
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            using (var barcodeStream = new MemoryStream())
            {
                // Save the barcode as PNG into the memory stream
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0;

                // Create a new PDF document and add the barcode image to the first page
                using (var pdfDoc = new Document())
                {
                    var page = pdfDoc.Pages.Add();
                    var img = new Aspose.Pdf.Image
                    {
                        ImageStream = barcodeStream
                    };
                    page.Paragraphs.Add(img);
                    pdfDoc.Save(pdfPath);
                }
            }
        }

        // Verify that the PDF was created successfully
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine("Failed to create sample PDF.");
            return;
        }

        // ------------------------------------------------------------
        // Configure barcode reader to use all CPU cores for parallel processing
        // ------------------------------------------------------------
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 4;

        // Build a list of page numbers to process
        List<int> pageNumbers = new List<int>();
        using (var pdfDoc = new Document(pdfPath))
        {
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
                pageNumbers.Add(i);
        }

        // ------------------------------------------------------------
        // Process each PDF page in parallel: convert to image and decode barcode
        // ------------------------------------------------------------
        Parallel.ForEach(pageNumbers, pageNumber =>
        {
            using (var pdfDoc = new Document(pdfPath))
            {
                using (var pdfConverter = new PdfConverter(pdfDoc))
                {
                    // Optimize conversion for barcode detection and set resolution
                    pdfConverter.RenderingOptions.BarcodeOptimization = true;
                    pdfConverter.Resolution = new Resolution(300);
                    pdfConverter.StartPage = pageNumber;
                    pdfConverter.EndPage = pageNumber;
                    pdfConverter.DoConvert();

                    using (var imageStream = new MemoryStream())
                    {
                        // Retrieve the rendered image for the current page
                        pdfConverter.GetNextImage(imageStream);
                        imageStream.Position = 0;

                        // Decode all supported barcode types from the image
                        using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
                        {
                            var results = reader.ReadBarCodes();
                            foreach (var result in results)
                            {
                                Console.WriteLine($"Page {pageNumber}: Type={result.CodeTypeName}, Text={result.CodeText}");
                            }
                        }
                    }
                }
            }
        });

        // ------------------------------------------------------------
        // Clean up the temporary folder and its contents
        // ------------------------------------------------------------
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}