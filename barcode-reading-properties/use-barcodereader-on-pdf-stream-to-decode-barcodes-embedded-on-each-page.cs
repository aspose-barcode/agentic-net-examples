// Title: Decode barcodes from each page of a PDF using BarCodeReader
// Description: Demonstrates how to read barcodes embedded in a PDF file by converting each page to an image stream and using Aspose.BarCode's BarCodeReader to decode them.
// Category-Description: This example belongs to the Aspose.BarCode PDF barcode extraction category. It shows how to combine Aspose.Pdf (Document, PdfConverter) with Aspose.BarCode (BarCodeReader, DecodeType) to process multi‑page PDFs, a common scenario for developers who need to batch‑read barcodes from scanned documents, invoices, or shipping manifests. The code illustrates typical use of Document, PdfConverter, and BarCodeReader for barcode recognition tasks.
// Prompt: Use BarCodeReader on a PDF stream to decode barcodes embedded on each page.
// Tags: pdf, barcode, decoding, barcodereader, aspnet, aspnet-core, aspose.pdf, aspose.barcode, decode, all-supported-types

using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that reads barcodes from each page of a PDF file using Aspose.Pdf and Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts an optional PDF file path argument, converts each page to an image stream,
    /// and decodes any barcodes found on that page.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument can be a PDF file path.</param>
    static void Main(string[] args)
    {
        // Determine PDF file path: use argument if provided, otherwise default to "sample.pdf".
        string pdfPath = args.Length > 0 ? args[0] : "sample.pdf";

        // Verify that the PDF file exists before proceeding.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document.
        using (var pdfDoc = new Document(pdfPath))
        {
            // Initialize a PdfConverter to render pages as images.
            using (var pdfConverter = new PdfConverter(pdfDoc))
            {
                // Enable barcode optimization for better barcode rendering.
                pdfConverter.RenderingOptions.BarcodeOptimization = true;
                // Set conversion resolution (DPI).
                pdfConverter.Resolution = new Resolution(300);

                int pageCount = pdfDoc.Pages.Count;

                // Process each page individually.
                for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
                {
                    // Configure converter to work on a single page.
                    pdfConverter.StartPage = pageNumber;
                    pdfConverter.EndPage = pageNumber;
                    pdfConverter.DoConvert();

                    // Capture the rendered page image into a memory stream.
                    using (var ms = new MemoryStream())
                    {
                        pdfConverter.GetNextImage(ms);
                        ms.Position = 0; // Reset stream position for reading.

                        // Use BarCodeReader to decode all supported barcode types from the image stream.
                        using (var reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
                        {
                            foreach (BarCodeResult result in reader.ReadBarCodes())
                            {
                                Console.WriteLine($"Page {pageNumber}: Type {result.CodeTypeName}, Text {result.CodeText}");
                            }
                        }
                    }
                }
            }
        }
    }
}