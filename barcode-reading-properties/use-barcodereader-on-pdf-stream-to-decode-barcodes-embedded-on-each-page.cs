// Title: Decode barcodes from PDF pages using BarCodeReader
// Description: Demonstrates how to read and decode all supported barcode types embedded in each page of a PDF document by converting pages to images and scanning them.
// Category-Description: This example belongs to the Aspose.BarCode reading category, showing how to combine Aspose.Pdf (PdfConverter) with Aspose.BarCode (BarCodeReader) to extract barcodes from PDF files. Typical use cases include processing invoices, tickets, or shipping documents where barcodes are printed on PDF pages. Developers often need to render PDF pages to images, then use BarCodeReader to recognize various symbologies.
/// Prompt: Use BarCodeReader on a PDF stream to decode barcodes embedded on each page.
/// Tags: barcode, reading, pdf, aspose.barcode, aspose.pdf, decode, image conversion, all-supported-types

using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates decoding barcodes from each page of a PDF document using Aspose.Pdf and Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts an optional PDF file path, converts each page to an image stream, and reads any barcodes present.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument may specify the PDF file path.</param>
    static void Main(string[] args)
    {
        // Determine PDF file path: use first argument if provided, otherwise default to "sample.pdf".
        string pdfPath = args.Length > 0 ? args[0] : "sample.pdf";

        // Verify that the PDF file exists before proceeding.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document.
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Initialize a PdfConverter to render PDF pages as images.
            using (PdfConverter pdfConverter = new PdfConverter(pdfDoc))
            {
                // Enable barcode optimization to improve detection accuracy.
                pdfConverter.RenderingOptions.BarcodeOptimization = true;
                // Set rendering resolution (DPI).
                pdfConverter.Resolution = new Resolution(300);
                // Process all pages in the document.
                pdfConverter.StartPage = 1;
                pdfConverter.EndPage = pdfDoc.Pages.Count;
                // Perform the conversion.
                pdfConverter.DoConvert();

                int pageNumber = 1;

                // Iterate through each rendered page image.
                while (pdfConverter.HasNextImage())
                {
                    // Retrieve the next page image into a memory stream.
                    using (MemoryStream ms = new MemoryStream())
                    {
                        pdfConverter.GetNextImage(ms);
                        ms.Position = 0; // Reset stream position for reading.

                        // Use BarCodeReader to scan the image stream for all supported barcode types.
                        using (BarCodeReader reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
                        {
                            BarCodeResult[] results = reader.ReadBarCodes();

                            // Output results for the current page.
                            if (results.Length == 0)
                            {
                                Console.WriteLine($"Page {pageNumber}: No barcode detected.");
                            }
                            else
                            {
                                foreach (BarCodeResult result in results)
                                {
                                    Console.WriteLine($"Page {pageNumber}: Type={result.CodeTypeName}, Text={result.CodeText}");
                                }
                            }
                        }
                    }

                    pageNumber++;
                }
            }
        }
    }
}