// Title: Retrieve MaxiCode mode and postal code from PDF pages
// Description: Demonstrates how to extract MaxiCode barcode data, specifically the mode and postal code, from each page of a PDF document.
// Category-Description: This example belongs to the Aspose.BarCode PDF barcode extraction category. It shows how to use Aspose.Pdf.Facades.PdfConverter to render PDF pages to images, then Aspose.BarCode.BarCodeRecognition.BarCodeReader with DecodeType.MaxiCode to read MaxiCode symbols. Developers often need to process shipping labels or logistics documents containing MaxiCode, extracting mode-specific information such as postal codes for routing and tracking.
// Prompt: Retrieve MaxiCode mode and postal code data from a PDF containing MaxiCode symbols.
// Tags: maxicode, barcode, pdf, extraction, aspose.barcode, aspose.pdf, decoding, postalcode

using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode;

/// <summary>
/// Example program that extracts MaxiCode mode and postal code information from a PDF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Scans each page of the specified PDF for MaxiCode barcodes and prints their mode and postal code.
    /// </summary>
    static void Main()
    {
        // Path to the source PDF containing MaxiCode symbols.
        string pdfPath = "sample.pdf";

        // Verify that the PDF file exists before proceeding.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Initialize PdfConverter to render PDF pages as images.
        using (var pdfConverter = new PdfConverter())
        {
            pdfConverter.BindPdf(pdfPath);
            // Enable barcode optimization to improve detection accuracy.
            pdfConverter.RenderingOptions.BarcodeOptimization = true;

            // Get total number of pages in the PDF.
            int pageCount = pdfConverter.Document.Pages.Count;

            // Process each page individually.
            for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
            {
                // Configure converter to render only the current page.
                pdfConverter.StartPage = pageNumber;
                pdfConverter.EndPage = pageNumber;
                pdfConverter.DoConvert();

                // Store the rendered page image in a memory stream.
                using (var ms = new MemoryStream())
                {
                    pdfConverter.GetNextImage(ms);
                    ms.Position = 0; // Reset stream position for reading.

                    // Create a BarCodeReader for MaxiCode detection.
                    using (var reader = new BarCodeReader(ms, DecodeType.MaxiCode))
                    {
                        // Iterate through all detected MaxiCode barcodes on the page.
                        foreach (var result in reader.ReadBarCodes())
                        {
                            // Retrieve the MaxiCode mode from the extended result.
                            var mode = result.Extended.MaxiCode.Mode;
                            // Decode the complex codetext based on the mode.
                            var complex = ComplexCodetextReader.TryDecodeMaxiCode(mode, result.CodeText);

                            // Output postal code based on the specific MaxiCode mode.
                            if (complex is MaxiCodeCodetextMode2 mode2)
                            {
                                Console.WriteLine($"Page {pageNumber}: Mode = 2, PostalCode = {mode2.PostalCode}");
                            }
                            else if (complex is MaxiCodeCodetextMode3 mode3)
                            {
                                Console.WriteLine($"Page {pageNumber}: Mode = 3, PostalCode = {mode3.PostalCode}");
                            }
                            else
                            {
                                Console.WriteLine($"Page {pageNumber}: Unrecognized MaxiCode mode.");
                            }
                        }
                    }
                }
            }
        }
    }
}