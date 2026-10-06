// Title: Extract barcodes from an encrypted PDF using Aspose.BarCode
// Description: Demonstrates opening a password‑protected PDF, rendering each page to an image, and reading all supported barcode types.
// Category-Description: This example belongs to the Aspose.BarCode PDF processing collection, illustrating how to combine Aspose.Pdf (Document, PdfConverter) with Aspose.BarCode (BarCodeReader, DecodeType) to extract barcode data from secured documents. Typical use cases include automated invoice processing, shipping label verification, and archival document scanning where barcodes are embedded in encrypted PDFs. Developers often need to supply the PDF password, render pages to images, and decode barcodes in a single workflow.
// Prompt: Process encrypted PDF files by providing password to BarCodeReader and extracting barcode data.
// Tags: barcode, pdf, encrypted, password, barcodereader, decode, aspose.pdf, aspose.barcode

using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that reads barcodes from a password‑protected PDF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts optional command‑line arguments for the PDF path and password,
    /// opens the encrypted document, converts each page to an image, and prints detected barcodes.
    /// </summary>
    /// <param name="args">args[0] – PDF file path (default: sample_encrypted.pdf); args[1] – password (default: password).</param>
    static void Main(string[] args)
    {
        // Determine PDF file path and password from command‑line arguments or use defaults.
        string pdfPath = args.Length > 0 ? args[0] : "sample_encrypted.pdf";
        string password = args.Length > 1 ? args[1] : "password";

        // Verify that the specified PDF file exists.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Open the encrypted PDF document using the provided password.
        using (var pdfDoc = new Document(pdfPath, password))
        {
            // Initialize a PDF converter to render pages as images.
            using (var pdfConverter = new PdfConverter(pdfDoc))
            {
                // Enable barcode optimization for better recognition performance.
                pdfConverter.RenderingOptions.BarcodeOptimization = true;
                // Set the rendering resolution (DPI).
                pdfConverter.Resolution = new Resolution(300);

                int pageCount = pdfDoc.Pages.Count;
                // Process each page individually.
                for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
                {
                    // Configure the converter to work on a single page.
                    pdfConverter.StartPage = pageNumber;
                    pdfConverter.EndPage = pageNumber;
                    pdfConverter.DoConvert();

                    // Capture the rendered page image into a memory stream.
                    using (var imageStream = new MemoryStream())
                    {
                        pdfConverter.GetNextImage(imageStream);
                        imageStream.Position = 0; // Reset stream position for reading.

                        // Create a barcode reader that scans the image for all supported types.
                        using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
                        {
                            // Iterate through all detected barcodes and output their details.
                            foreach (var result in reader.ReadBarCodes())
                            {
                                Console.WriteLine($"Page {pageNumber}: Type = {result.CodeTypeName}, Data = {result.CodeText}");
                            }
                        }
                    }
                }
            }
        }
    }
}