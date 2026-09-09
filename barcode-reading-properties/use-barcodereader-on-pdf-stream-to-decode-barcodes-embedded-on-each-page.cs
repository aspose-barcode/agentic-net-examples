// Title: Decode barcodes from each page of a PDF using BarCodeReader
// Description: Demonstrates how to convert each PDF page to an image stream and use Aspose.BarCode's BarCodeReader to decode any barcodes present. Useful for extracting barcode data from multi‑page PDF documents.
// Category-Description: This example belongs to the Aspose.BarCode PDF processing category, showing how to combine Aspose.Pdf conversion with BarCodeReader to recognize barcodes in PDF files. It highlights key classes such as Document, PdfConverter, BarCodeReader, and DecodeType, which developers commonly use to extract barcode information from scanned documents, invoices, or shipping manifests. Ideal for scenarios where barcodes are embedded in PDF pages and need to be read programmatically.
// Prompt: Use BarCodeReader on a PDF stream to decode barcodes embedded on each page.
// Tags: pdf, barcode, decoding, barcodereader, aspnet, aspnetcore, aspose.barcode, aspose.pdf, image conversion

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

/// <summary>
/// Demonstrates decoding barcodes from each page of a PDF file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Reads a PDF file (path from args or default), converts each page to an image,
    /// and decodes any barcodes found using BarCodeReader.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument may specify the PDF file path.</param>
    static void Main(string[] args)
    {
        // Determine PDF file path: use first argument if provided, otherwise fallback to "sample.pdf"
        string pdfPath = args.Length > 0 ? args[0] : "sample.pdf";

        // Verify that the PDF file exists before proceeding
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document
        using (var pdfDoc = new Document(pdfPath))
        {
            // Initialize a PdfConverter to render PDF pages as images
            using (var pdfConverter = new PdfConverter(pdfDoc))
            {
                // Enable barcode optimization for better recognition performance
                pdfConverter.RenderingOptions.BarcodeOptimization = true;
                // Set the resolution of the rendered images (300 DPI)
                pdfConverter.Resolution = new Resolution(300);

                int pageCount = pdfDoc.Pages.Count;

                // Process each page individually
                for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
                {
                    // Configure the converter to render only the current page
                    pdfConverter.StartPage = pageNumber;
                    pdfConverter.EndPage = pageNumber;
                    pdfConverter.DoConvert();

                    // Capture the rendered page into a memory stream
                    using (var imageStream = new MemoryStream())
                    {
                        pdfConverter.GetNextImage(imageStream);
                        imageStream.Position = 0; // Reset stream position for reading

                        // Create a BarCodeReader to decode all supported barcode types from the image stream
                        using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
                        {
                            BarCodeResult[] results;
                            try
                            {
                                // Attempt to read barcodes from the current page image
                                results = reader.ReadBarCodes();
                            }
                            catch (Exception ex)
                            {
                                // Log any errors encountered during barcode reading and continue with next page
                                Console.WriteLine($"Error reading page {pageNumber}: {ex.Message}");
                                continue;
                            }

                            // Output results based on whether any barcodes were detected
                            if (results.Length == 0)
                            {
                                Console.WriteLine($"Page {pageNumber}: No barcodes detected.");
                            }
                            else
                            {
                                foreach (var result in results)
                                {
                                    Console.WriteLine($"Page {pageNumber}: Type={result.CodeTypeName}, Text={result.CodeText}, Quality={result.ReadingQuality}");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}