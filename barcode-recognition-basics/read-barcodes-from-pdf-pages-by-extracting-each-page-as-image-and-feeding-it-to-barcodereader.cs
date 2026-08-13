// Title: Read barcodes from PDF pages by converting each page to an image
// Description: Demonstrates extracting each page of a PDF as an image and using Aspose.BarCode's BarCodeReader to detect all supported barcode types.
// Category-Description: This example belongs to the Aspose.BarCode PDF processing category, illustrating how to combine Aspose.Pdf and Aspose.BarCode APIs. It shows how to render PDF pages to images, enable barcode optimization, and read barcodes using BarCodeReader. Developers often need to scan documents for embedded barcodes, automate data capture, or validate printed codes in PDFs.
// Prompt: Read barcodes from PDF pages by extracting each page as an image and feeding it to BarCodeReader.
// Tags: pdf, barcode, extraction, image, aspose.pdf, aspose.barcode, decode, allsupportedtypes

using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates reading barcodes from each page of a PDF by converting pages to images and using BarCodeReader.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Processes the PDF, extracts pages as images, and prints detected barcode information.
    /// </summary>
    static void Main()
    {
        // Path to the PDF file to be processed.
        const string pdfPath = "sample.pdf";

        // Verify that the PDF file exists before attempting to read it.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"File not found: {pdfPath}");
            Console.WriteLine("Please provide a PDF containing barcodes and place it in the executable directory.");
            return;
        }

        // Open the PDF document.
        using (var pdfDocument = new Document(pdfPath))
        {
            // Initialize the PDF converter.
            using (var pdfConverter = new PdfConverter(pdfDocument))
            {
                // Enable barcode optimization for better extraction.
                pdfConverter.RenderingOptions.BarcodeOptimization = true;

                // Process each page individually.
                for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
                {
                    // Configure the converter to render only the current page.
                    pdfConverter.StartPage = pageNumber;
                    pdfConverter.EndPage = pageNumber;

                    // Perform the conversion.
                    pdfConverter.DoConvert();

                    // Retrieve the rendered page as an image stream.
                    using (var imageStream = new MemoryStream())
                    {
                        pdfConverter.GetNextImage(imageStream);
                        imageStream.Position = 0; // Reset stream for reading.

                        // Create a barcode reader for the image stream.
                        using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
                        {
                            // Iterate through all detected barcodes on this page.
                            foreach (var result in reader.ReadBarCodes())
                            {
                                Console.WriteLine($"Page {pageNumber}: Type = {result.CodeTypeName}, Text = {result.CodeText}");
                            }
                        }
                    }
                }
            }
        }
    }
}