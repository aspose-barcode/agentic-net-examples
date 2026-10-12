// Title: Decode RM4SCC barcode from PDF pages
// Description: Demonstrates how to extract and decode an RM4SCC barcode embedded in a PDF document using Aspose.BarCode and Aspose.Pdf.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showing how to convert PDF pages to images and read barcodes with BarCodeReader. It highlights key classes such as Document, PdfConverter, BarCodeReader, and DecodeType, useful for developers who need to process scanned documents, invoices, or shipping labels containing barcodes.
// Prompt: Decode an RM4SCC barcode embedded in a PDF page and extract the original data.
// Tags: rm4scc, barcode, decoding, pdf, aspose.barcode, aspose.pdf, image conversion, barcoderecognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

/// <summary>
/// Demonstrates decoding RM4SCC barcodes from a PDF document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Loads a PDF, converts each page to an image, and reads any barcodes found.
    /// </summary>
    static void Main()
    {
        // Path to the source PDF file
        string pdfPath = "sample.pdf";

        // Verify that the PDF file exists before proceeding
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document using Aspose.Pdf
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Initialize a PdfConverter to render PDF pages as images
            using (PdfConverter pdfConverter = new PdfConverter(pdfDoc))
            {
                // Enable barcode optimization for better recognition results
                pdfConverter.RenderingOptions.BarcodeOptimization = true;
                // Set the resolution of the rendered images (300 DPI)
                pdfConverter.Resolution = new Resolution(300);

                // Iterate through each page in the PDF
                int pageCount = pdfDoc.Pages.Count;
                for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
                {
                    // Configure the converter to process a single page
                    pdfConverter.StartPage = pageNumber;
                    pdfConverter.EndPage = pageNumber;
                    // Perform the conversion for the current page
                    pdfConverter.DoConvert();

                    // Store the rendered image in a memory stream
                    using (MemoryStream imageStream = new MemoryStream())
                    {
                        pdfConverter.GetNextImage(imageStream);
                        imageStream.Position = 0; // Reset stream position for reading

                        // Initialize BarCodeReader to detect all supported barcode types
                        using (BarCodeReader reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
                        {
                            // Read all barcodes found in the image
                            BarCodeResult[] results = reader.ReadBarCodes();
                            if (results.Length == 0)
                            {
                                Console.WriteLine($"Page {pageNumber}: No barcode detected.");
                            }
                            else
                            {
                                // Output each detected barcode's type and data
                                foreach (BarCodeResult result in results)
                                {
                                    Console.WriteLine($"Page {pageNumber}: Type={result.CodeTypeName}, Data={result.CodeText}");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}