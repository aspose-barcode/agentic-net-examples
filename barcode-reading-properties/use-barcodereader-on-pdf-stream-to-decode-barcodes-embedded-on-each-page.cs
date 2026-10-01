// Title: Decode Barcodes from Each Page of a PDF Using BarCodeReader
// Description: Demonstrates how to read barcodes embedded in a PDF document by converting each page to an image stream and using Aspose.BarCode's BarCodeReader.
// Category-Description: This example belongs to the Aspose.BarCode PDF processing collection, illustrating how to combine Aspose.Pdf and Aspose.BarCode APIs to extract barcodes from PDF pages. It showcases the use of Document, PdfConverter, and BarCodeReader classes for typical scenarios such as inventory scanning, document verification, and automated data capture. Developers often need to decode multiple barcode symbologies across multi‑page PDFs, and this pattern provides a reliable approach.
// Prompt: Use BarCodeReader on a PDF stream to decode barcodes embedded on each page.
// Tags: barcode, pdf, barcodereader, decode, aspose.barcode, aspose.pdf

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

/// <summary>
/// Example program that extracts and decodes barcodes from each page of a PDF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Loads a PDF, converts each page to an image stream, and reads all supported barcodes.
    /// </summary>
    /// <param name="args">Optional command‑line argument specifying the PDF file path.</param>
    static void Main(string[] args)
    {
        // Determine PDF file path (argument or default)
        string pdfPath = args.Length > 0 ? args[0] : "sample.pdf";

        // Validate file existence
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load PDF document and set up converter
        using (var pdfDoc = new Document(pdfPath))
        {
            using (var pdfConverter = new PdfConverter(pdfDoc))
            {
                // Enable barcode optimization for better extraction
                pdfConverter.RenderingOptions.BarcodeOptimization = true;

                // Limit to maximum 4 pages due to evaluation mode restrictions
                int maxPages = Math.Min(pdfDoc.Pages.Count, 4);

                // Process each page individually
                for (int pageNumber = 1; pageNumber <= maxPages; pageNumber++)
                {
                    // Convert the current page to an image stream
                    pdfConverter.StartPage = pageNumber;
                    pdfConverter.EndPage = pageNumber;
                    pdfConverter.DoConvert();

                    using (var imageStream = new MemoryStream())
                    {
                        pdfConverter.GetNextImage(imageStream);
                        imageStream.Position = 0; // Reset stream position for reading

                        // Read barcodes from the rendered page image
                        using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
                        {
                            var results = reader.ReadBarCodes();

                            // Output each detected barcode
                            foreach (var result in results)
                            {
                                Console.WriteLine($"Page {pageNumber}: CodeText = {result.CodeText}, Symbology = {result.CodeTypeName}, Quality = {result.ReadingQuality}");
                                var rect = result.Region.Rectangle;
                                Console.WriteLine($"  Region: X={rect.X}, Y={rect.Y}, Width={rect.Width}, Height={rect.Height}, Angle={result.Region.Angle}");
                            }

                            // Inform if no barcodes were found on the page
                            if (results.Length == 0)
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