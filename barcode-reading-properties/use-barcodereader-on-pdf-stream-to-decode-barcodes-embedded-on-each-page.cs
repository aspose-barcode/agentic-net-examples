// Title: Decode Barcodes from Each Page of a PDF Using BarCodeReader
// Description: Demonstrates how to convert each PDF page to an image stream and use BarCodeReader to detect all supported barcode types.
// Prompt: Use BarCodeReader on a PDF stream to decode barcodes embedded on each page.
// Tags: barcode, pdf, decoding, aspnet, aspose, barcodereader, image

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

/// <summary>
/// Sample console application that reads a PDF file, renders each page to an image,
/// and decodes any barcodes found on the page using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Path to the PDF file containing barcodes.
        const string pdfPath = "sample.pdf";

        // Verify that the PDF file exists before proceeding.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Open the PDF document for processing.
        using (var pdfDocument = new Document(pdfPath))
        {
            // Initialize a PDF converter to render pages as images.
            using (var pdfConverter = new PdfConverter(pdfDocument))
            {
                // Enable barcode optimization to improve rendering quality of barcodes.
                pdfConverter.RenderingOptions.BarcodeOptimization = true;

                // Iterate through each page in the PDF.
                for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
                {
                    // Configure the converter to process a single page.
                    pdfConverter.StartPage = pageNumber;
                    pdfConverter.EndPage = pageNumber;
                    pdfConverter.DoConvert();

                    // Render the current page to an in‑memory image stream.
                    using (var pageImageStream = new MemoryStream())
                    {
                        pdfConverter.GetNextImage(pageImageStream);
                        pageImageStream.Position = 0; // Reset stream position for reading.

                        // Create a BarCodeReader to scan the image for any supported barcode types.
                        using (var reader = new BarCodeReader(pageImageStream, DecodeType.AllSupportedTypes))
                        {
                            // Optional: improve detection of low‑quality barcodes.
                            reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;

                            bool anyFound = false;

                            // Enumerate all detected barcodes on the page.
                            foreach (var result in reader.ReadBarCodes())
                            {
                                anyFound = true;
                                Console.WriteLine($"Page {pageNumber}: Type = {result.CodeTypeName}, Text = {result.CodeText}");
                                var bounds = result.Region.Rectangle;
                                Console.WriteLine($"    Region: X={bounds.X}, Y={bounds.Y}, Width={bounds.Width}, Height={bounds.Height}");
                            }

                            // Inform the user if no barcodes were detected on the current page.
                            if (!anyFound)
                            {
                                Console.WriteLine($"Page {pageNumber}: No barcodes detected.");
                            }
                        }
                    }
                }
            }
        }
    }
}